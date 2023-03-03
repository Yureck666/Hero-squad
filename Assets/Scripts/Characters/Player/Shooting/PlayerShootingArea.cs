using System;
using System.Collections.Generic;
using System.Linq;
using HeroSquad.Characters.Enemy;
using UnityEngine;

namespace HeroSquad.Characters.Player.Shooting
{
	public class PlayerShootingArea: ShootingArea
	{
		protected override void OnTriggerEnter(Collider other)
		{
			CheckAndSaveEnemy<EnemyPresenter>(other);
		}
		protected override void OnTriggerExit(Collider other)
		{
			CheckAndRemove<EnemyPresenter>(other);
		}
	}
}