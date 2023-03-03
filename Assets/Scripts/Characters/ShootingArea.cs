using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HeroSquad.Characters
{
	[RequireComponent(typeof(SphereCollider))]
	public abstract class ShootingArea: MonoBehaviour
	{
		public List<CharacterPresenter> enemies { get; private set; }
		
		protected Action<bool> shootingAction;
		
		private SphereCollider _collider;

		protected abstract void OnTriggerEnter(Collider other);
		protected abstract void OnTriggerExit(Collider other);
		
		public void Init(Action<bool> startShootingAction)
		{
			shootingAction = startShootingAction;
			_collider = GetComponent<SphereCollider>();
			_collider.isTrigger = true;
			enemies = new List<CharacterPresenter>();
		}

		protected void CheckAndSaveEnemy<T>(Collider other) where T: CharacterPresenter
		{
			if (other.TryGetComponent(out T enemy))
			{
				enemies.Add(enemy);
				enemy.OnDieEvent.AddListener(() => RemoveCharacterItem(enemy));
				
				if (enemies.Count == 1)
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
			if (enemies.Contains(character))
			{
				enemies.Remove(character);
				
				if (enemies.Count == 0)
				{
					shootingAction.Invoke(false);
				}
			}
		}
	}
}