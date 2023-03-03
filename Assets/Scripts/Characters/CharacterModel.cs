using UnityEngine.Events;

namespace HeroSquad.Characters
{
	public class CharacterModel
	{
		public int LifeCount { get; private set; }
		public float MoveSpeed { get; private set; }
		public UnityEvent OnDieEvent { get; }
		public UnityEvent<float> OnSpeedChange { get; }
		public bool IsAlive => LifeCount > 0;

		public CharacterModel(int lifeCount, float moveSpeed)
		{
			OnDieEvent = new UnityEvent();
			OnSpeedChange = new UnityEvent<float>();
			LifeCount = lifeCount;
			MoveSpeed = moveSpeed;
		}

		public void SetSpeed(float speed)
		{
			MoveSpeed = speed;
			OnSpeedChange.Invoke(speed);
		}

		public void GetDamage(int damage)
		{
			LifeCount -= damage;
			if (LifeCount <= 0)
			{
				OnDieEvent.Invoke();
			}
		}
	}
}