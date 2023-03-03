using HeroSquad.Characters.Enemy;
using HeroSquad.Characters.Player.Movement;
using QFSW.MOP2;
using UnityEngine;

namespace HeroSquad
{
	public class EnemySpawner: MonoBehaviour
	{
		[SerializeField] private float spawnDistance;
		[SerializeField] private EnemyPresenter[] enemiesPrefab;

		private FollowTarget _followTarget;

		public void Init(FollowTarget followTarget)
		{
			_followTarget = followTarget;
		}

		[ContextMenu("Spawn enemy")]
		private void SpawnOneEnemy()
		{
			SpawnEnemies(1);
		}
		
		public EnemyPresenter[] SpawnEnemies(int count)
		{
			var enemies = new EnemyPresenter[count];
			for (int i = 0; i < count; i++)
			{
				var position = RandomPointOnCircleEdge();
				position += _followTarget.transform.position;
				var enemy = enemiesPrefab[Random.Range(0, enemiesPrefab.Length)].Pool.GetObjectComponent<EnemyPresenter>();
				enemy.transform.position = position;
				enemy.SetFollowTarget(_followTarget);
				enemies[i] = enemy;
			}

			return enemies;
		}
		
		private Vector3 RandomPointOnCircleEdge()
		{
			var vector2 = Random.insideUnitCircle.normalized * spawnDistance;
			return vector2.ToVector3Y();
		}
	}
}