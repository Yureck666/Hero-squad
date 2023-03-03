using HeroSquad.Characters.Player;
using UnityEngine;

namespace HeroSquad.Characters.Enemy
{
	public class EnemyShootingArea: ShootingArea
	{
		protected override void OnTriggerEnter(Collider other)
		{
			CheckAndSaveEnemy<FollowerPresenter>(other);
		}
		protected override void OnTriggerExit(Collider other)
		{
			CheckAndRemove<FollowerPresenter>(other);
		}
	}
}