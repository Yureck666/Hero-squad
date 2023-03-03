using System;
using System.Collections.Generic;
using System.Linq;
using HeroSquad.Characters.Enemy;
using HeroSquad.Characters.Player.Shooting;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HeroSquad.Characters.Player.Movement
{
	public class FollowTarget : MonoBehaviour
	{
		[SerializeField] private PlayerShootingArea shootingArea;
		[SerializeField] private Transform[] points;

		public bool IsInMove { get; private set; }

		private Dictionary<Transform, FollowerPresenter> _followers;

		private void Awake()
		{
			_followers = new Dictionary<Transform, FollowerPresenter>();
			shootingArea.Init(SetFollowersShooting);
			var asd = _followers.Values;
		}

		public void AddActionToAllFollowersDieEvent(Action action)
		{
			foreach (var follower in _followers.Values)
			{
				follower.OnDieEvent.AddListener(action.Invoke);
			}
		}

		public int GetFollowersCount()
		{
			return _followers.Count;
		}


		public void SetFollowersShooting(bool active)
		{
			foreach (var follower in _followers.Values)
			{
				follower.SetShootingStateActive(active);
			}
		}

		public CharacterPresenter GetRandomFollower()
		{
			return GetRandomPresenter(_followers.Values);
		}

		public CharacterPresenter GetNearestShootingTarget()
		{
			return GetRandomPresenter(shootingArea.enemies);
		}

		public CharacterPresenter GetRandomPresenter(List<CharacterPresenter> presenters)
		{
			presenters.Where(presenter => presenter.IsAlive);
			return presenters.Any() ? presenters.ElementAt(Random.Range(0, presenters.Count())) : default;
		}

		public CharacterPresenter GetRandomPresenter(IEnumerable<CharacterPresenter> presenters)
		{
			presenters.Where(presenter => presenter.IsAlive);
			return presenters.Any() ? presenters.ElementAt(Random.Range(0, presenters.Count())) : default;
		}

		public void SetInMove(bool active)
		{
			IsInMove = active;
		}

		[CanBeNull]
		public Transform SetFollower(FollowerPresenter follower)
		{
			var point = points.First(transform1 => !_followers.ContainsKey(transform1));
			if (point != null)
				_followers.Add(point, follower);
			return point;
		}
	}
}