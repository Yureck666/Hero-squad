namespace HeroSquad.Characters
{
	public interface IDamageable
	{
		public bool IsEnemy { get; }
		public void GetDamage(int damage);
	}
}