using HeroSquad.Characters.Enemy;
using HeroSquad.Characters.Player.Movement;
using HeroSquad.Characters.Player.Shooting;
using HeroSquad.ScriptableObjects;
using HeroSquad.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace HeroSquad.Characters.Player
{
	[RequireComponent(typeof(NavMeshAgent))]
	public class FollowerPresenter : CharacterPresenter
	{
		[SerializeField] private FollowTarget target;


		private Transform _targetTransform;

		protected override void Awake()
		{
			speed = settings.FollowersMoveSpeed;
			base.Awake();
		}

		private void Start()
		{
			_targetTransform = target.SetFollower(this);
			RefreshAgentTarget();
		}

		public override void SetNewTarget()
		{
			shootingTarget = target.GetNearestShootingTarget();
			if (shootingTarget != default)
			{
				shootingTarget.OnDieEvent.AddListener(SetNewTarget);
			}
		}

		protected override void RefreshAgentTarget()
		{
			navMeshAgent.SetDestination(_targetTransform.position);
		}

		protected override void FixedUpdate()
		{
			base.FixedUpdate();
			if (_targetTransform && target.IsInMove)
				RefreshAgentTarget();
		}
	}
}