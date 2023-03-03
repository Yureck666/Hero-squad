using System.Collections;
using UnityEngine;

namespace HeroSquad.Characters.Player.Shooting
{
	public class ShotgunShooter: Shooter
	{
		[SerializeField] private Transform[] shootDirections;
		[SerializeField] private int bulletsByShoot;

		protected override IEnumerator Shooting()
		{
			var instr = new WaitForSeconds(shootsDelay);
			while (true)
			{
				yield return instr;

				var shots = 0;
				while (shots < bulletsByShoot)
				{
					var bullet = SpawnBullet();
					var velocity = shootDirections[Random.Range(0, shootDirections.Length)].forward * bulletSpeed;
					bullet.SetBulletVelocity(velocity);
					shots++;
				}
			}
		}
	}
}