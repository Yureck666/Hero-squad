using UnityEngine;

namespace HeroSquad.ScriptableObjects
{
	[CreateAssetMenu(fileName = "RoundSettings", menuName = "Settings/Round")]
	public class RoundsSettings: ScriptableObject
	{
		[SerializeField] private int startEnemiesCount;
		[SerializeField] private int addByRoundEnemiesCount;
		[SerializeField] private int maximumEnemiesCount;
		
		public int StartEnemiesCount =>  startEnemiesCount;
		public int AddByRoundEnemiesCount => addByRoundEnemiesCount;
		public int MaximumEnemiesCount => maximumEnemiesCount;
	}
}