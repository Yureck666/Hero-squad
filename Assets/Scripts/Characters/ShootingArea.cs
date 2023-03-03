using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HeroSquad.Characters
{
	[RequireComponent(typeof(SphereCollider))]
	public abstract class ShootingArea: MonoBehaviour
	{
		protected Action<bool> shootingAction;
		private List<CharacterPresenter> _enemies;
		private SphereCollider _collider;

		protected abstract void OnTriggerEnter(Collider other);
		protected abstract void OnTriggerExit(Collider other);
		
		public void Init(Action<bool> startShootingAction)
		{
			shootingAction = startShootingAction;
			_collider = GetComponent<SphereCollider>();
			_collider.isTrigger = true;
			_enemies = new List<CharacterPresenter>();
		}

		public CharacterPresenter GetFirstEnemy()
		{
			return _enemies.FirstOrDefault();
		}

		protected void CheckAndSaveEnemy<T>(Collider other) where T: CharacterPresenter
		{
			if (other.TryGetComponent(out T enemy))
			{
				_enemies.Add(enemy);
				enemy.OnDieEvent.AddListener(() => RemoveCharacterItem(enemy));
				
				if (_enemies.Count == 1)
				{
					shootingAction.Invoke(true);
				}
			}
		}

		protected void CheckAndRemove<T>(Collider other) where T : CharacterPresenter
		{
			if (other.TryGetComponent(out T enemy))
			{
				RemoveCharacterItem(enemy);
			}
		}

		private void RemoveCharacterItem(CharacterPresenter character)
		{
			if (_enemies.Contains(character))
			{
				_enemies.Remove(character);
				
				if (_enemies.Count == 0)
				{
					shootingAction.Invoke(false);
				}
			}
		}
	}
}