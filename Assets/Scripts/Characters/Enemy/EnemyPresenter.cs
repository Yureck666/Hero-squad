using HeroSquad.Characters.Player;
using HeroSquad.Characters.Player.Movement;
using HeroSquad.ScriptableObjects;
using HeroSquad.StateMachine;
using QFSW.MOP2;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

namespace HeroSquad.Characters.Enemy
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class EnemyPresenter: CharacterPresenter
	{
		[SerializeField] private ObjectPool pool;
		[SerializeField] private float stoppingDistance;
		[SerializeField] private EnemyShootingArea shootingArea;

		public ObjectPool Pool => pool;
		
		private FollowTarget _followTarget;


		protected override void Awake()
		{
			speed = settings.EnemiesSpeed;
			base.Awake();
			navMeshAgent.stoppingDistance = stoppingDistance;
			if (shootingArea) 
				shootingArea.Init(SetShootingStateActive);
		}
		
		public void SetFollowTarget(FollowTarget followTarget)
		{
			_followTarget = followTarget;
			SetNewTarget();
		}
		
		public override void SetNewTarget()
		{
			var target = _followTarget.GetRandomFollower();
			if (target != default)
			{
				shootingTarget = target;
				shootingTarget.OnDieEvent.AddListener(SetNewTarget);
			}
			else
			{
				stateMachine.ChangeState(new CharacterRunState(this));
			}
		}
		protected override void RefreshAgentTarget()
		{
			navMeshAgent.SetDestination(shootingTarget.transform.position);
		}

		protected override void FixedUpdate()
		{
			base.FixedUpdate();
			if (shootingTarget)
				RefreshAgentTarget();
		}
	}
}