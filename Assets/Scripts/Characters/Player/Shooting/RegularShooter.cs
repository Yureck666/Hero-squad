using System.Collections;
using UnityEngine;

namespace HeroSquad.Characters.Player.Shooting
{
	public class RegularShooter: Shooter
	{
		[SerializeField] private Transform bulletDirection;

		protected override IEnumerator Shooting()
		{
			var instr = new WaitForSeconds(shootsDelay);
			while (true)
			{
				yield return instr;
				var bullet = SpawnBullet();
				var velocity = bulletDirection.forward * bulletSpeed;
				bullet.SetBulletVelocity(velocity);
			}
		}
	}
}