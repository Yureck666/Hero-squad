using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace HeroSquad.Characters.Player.Shooting
{
	public abstract class Shooter: MonoBehaviour
	{
		[SerializeField] protected Transform bulletSpawnPosition;
		[SerializeField] protected float shootsDelay;
		[SerializeField] protected float bulletSpeed;
		[SerializeField] protected int damage;
		[SerializeField] protected Bullet bulletPrefab;
		public CoroutineObject ShootingCoroutine { get; private set; }
		public UnityEvent<bool> OnShootingEvent => ShootingCoroutine.OnActiveEvent;

		private bool _isEnemy;
		
		public virtual void Init(bool isEnemy)
		{
			ShootingCoroutine = new CoroutineObject(this, Shooting);
			_isEnemy = isEnemy;
		}

		protected Bullet SpawnBullet()
		{
			var bullet = bulletPrefab.Pool.GetObjectComponent<Bullet>();
			bullet.transform.position = bulletSpawnPosition.position;
			bullet.SetDamage(damage);
			bullet.SetIsEnemy(_isEnemy);
			return bullet;
		}

		protected abstract IEnumerator Shooting();
	}
}