using System;
using System.Collections;
using QFSW.MOP2;
using UnityEngine;

namespace HeroSquad.Characters.Player.Shooting
{
	[RequireComponent(typeof(Rigidbody))]
	public abstract class Bullet: MonoBehaviour
	{
		[SerializeField] private ObjectPool pool;
		[SerializeField] private float lifeTime;

		public ObjectPool Pool => pool;

		protected int damage;
		protected bool isEnemy;
		
		private Rigidbody _rigidbody;
		private CoroutineObject _releaseCoroutineObject;
		
		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
			_releaseCoroutineObject = new CoroutineObject(this, ReleaseCoroutine);
		}

		public IEnumerator ReleaseCoroutine()
		{
			yield return new WaitForSeconds(lifeTime);
			Release();
		}

		public void SetDamage(int value)
		{
			damage = value;
		}

		public void SetIsEnemy(bool value)
		{
			isEnemy = value;
		}

		public void SetBulletVelocity(Vector3 velocity)
		{
			_rigidbody.velocity = velocity;
		}

		public void Release()
		{
			pool.Release(gameObject);
		}

		private void OnEnable()
		{
			_releaseCoroutineObject.Restart();
		}

		private void OnDisable()
		{
			_releaseCoroutineObject.Stop();
		}

		private void OnTriggerEnter(Collider other)
		{
			OnTrigger(other);
		}

		protected abstract void OnTrigger(Collider other);
	}
}