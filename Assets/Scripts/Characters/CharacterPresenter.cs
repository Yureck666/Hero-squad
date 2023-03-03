using HeroSquad.Characters.Player.Shooting;
using HeroSquad.ScriptableObjects;
using HeroSquad.StateMachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace HeroSquad.Characters
{
	public abstract class CharacterPresenter : MonoBehaviour, IDamageable
	{
		[SerializeField] private int startLifeCount;
		[SerializeField] private Shooter shooter;
		[SerializeField] private Vector3 shootDeltaRotation;
		[SerializeField] private bool isEnemy;
		[SerializeField] protected MovementSettings settings;

		public UnityEvent OnDieEvent => model.OnDieEvent;
		public bool IsAlive => model.IsAlive;
		public bool IsEnemy => isEnemy;
		
		protected NavMeshAgent navMeshAgent;
		protected CharacterStateMachine stateMachine;
		protected CharacterPresenter shootingTarget;
		protected CharacterModel model;

		protected float speed;

		public abstract void SetNewTarget();
		protected abstract void RefreshAgentTarget();
		
		protected virtual void Awake()
		{
			navMeshAgent = GetComponent<NavMeshAgent>();
			stateMachine = new CharacterStateMachine();
			stateMachine.Init(new CharacterRunState(this));
			model = new CharacterModel(startLifeCount, speed);
			navMeshAgent.speed = model.MoveSpeed;
			model.OnSpeedChange.AddListener(value => navMeshAgent.speed = value);
			OnDieEvent.AddListener(() => stateMachine.ChangeState(new CharacterDeadState(this)));
			if (shooter) 
				shooter.Init(isEnemy);
		}

		protected virtual void FixedUpdate()
		{
			stateMachine.CurrentState.FixedUpdate();
		}

		public void Die()
		{
			gameObject.SetActive(false);
		}

		public void GetDamage(int damage)
		{
			model.GetDamage(damage);
		}

		private NavMeshAgent ValidateNavMeshAgent
		{
			get
			{
				if (navMeshAgent == null)
					navMeshAgent = GetComponent<NavMeshAgent>();
				return navMeshAgent;
			}
		}
		
		private void OnValidate()
		{
			ValidateNavMeshAgent.speed = settings.FollowersMoveSpeed;
		}
		
		public void SetNavMeshRotationActive(bool active)
		{
			navMeshAgent.angularSpeed = active ? settings.FollowersRotationSpeed : 0;
		}
		
		public void SetRotationToShootingTarget()
		{
			var rotation = Quaternion.LookRotation(transform.position - shootingTarget.transform.position);
			rotation.eulerAngles += shootDeltaRotation;
			rotation.x = 0;
			
			var delta = Quaternion.Angle(transform.rotation, rotation);
			var rotateValue = settings.FollowersRotationSpeed/delta * Time.fixedDeltaTime;
			rotation = Quaternion.Lerp(transform.rotation, rotation, rotateValue);
			
			transform.rotation = rotation;
		}
		
		public void SetShootingActive(bool active)
		{
			if (model.IsAlive) 
				shooter.ShootingCoroutine.SetActive(active);
		}
		
		public void SetShootingStateActive(bool active)
		{
			stateMachine.ChangeState(active? new CharacterShootState(this): new CharacterRunState(this));
		}
	}
}