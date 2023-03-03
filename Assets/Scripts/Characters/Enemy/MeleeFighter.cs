using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace HeroSquad.Characters.Enemy
{
	public class MeleeFighter: MonoBehaviour
	{
		[SerializeField] private CharacterPresenter presenter;
		[SerializeField] private int damage;
		[SerializeField] private float attackSpeed;

		private CoroutineObject _damageCoroutine;
		private List<IDamageable> _enemies;

		private void Awake()
		{
			_damageCoroutine = new CoroutineObject(this, DamageCoroutine);
			_enemies = new List<IDamageable>();
			_damageCoroutine.Start();
		}

		private IEnumerator DamageCoroutine()
		{
			var inst = new WaitForSeconds(attackSpeed);
			while (true)
			{
				_enemies.ForEach(damageable => damageable.GetDamage(damage));
				yield return inst;
			}
		}

		private void OnTriggerEnter(Collider other)
		{
			if (other.TryGetComponent(out IDamageable damageable) && damageable.IsEnemy != presenter.IsEnemy)
			{
				_enemies.Add(damageable);
			}
		}

		private void OnTriggerExit(Collider other)
		{
			if (other.TryGetComponent(out IDamageable damageable) && _enemies.Contains(damageable))
			{
				_enemies.Remove(damageable);
			}
		}
	}
}