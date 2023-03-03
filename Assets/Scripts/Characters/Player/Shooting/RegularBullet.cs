using HeroSquad.Characters.Enemy;
using UnityEngine;

namespace HeroSquad.Characters.Player.Shooting
{
	public class RegularBullet : Bullet
	{
		protected override void OnTrigger(Collider other)
		{
			if (other.TryGetComponent(out IDamageable enemy) && enemy.IsEnemy != isEnemy)
			{
				enemy.GetDamage(damage);
				Release();
			}
		}
	}
}