using System;
using HeroSquad.Characters.Player.Shooting;
using UnityEngine;
using UnityEngine.AI;

namespace HeroSquad.Characters.Player.Movement
{
	[RequireComponent(typeof(Animator))]
	public class CharacterAnimator: MonoBehaviour
	{
		[SerializeField] private NavMeshAgent navMeshAgent;
		[SerializeField] private Shooter shooter;
		[SerializeField] private float speedToRunAnimation;

		private Animator _animator;
		private static readonly int RunHash = Animator.StringToHash("Run");
		private static readonly int ShootHash = Animator.StringToHash("Shoot");
		private static readonly int SpeedHash = Animator.StringToHash("Speed");

		private void Awake()
		{
			_animator = GetComponent<Animator>();
		}

		private void Start()
		{
			if (shooter) 
				shooter.OnShootingEvent.AddListener(active => _animator.SetBool(ShootHash, active));
		}
		private void FixedUpdate()
		{
			var speed = navMeshAgent.velocity.magnitude;
			_animator.SetFloat(SpeedHash, speed);
			_animator.SetBool(RunHash, speed >= speedToRunAnimation);
		}
	}
}