using UnityEngine;

namespace HeroSquad.ScriptableObjects
{
	[CreateAssetMenu(fileName = "MovementSettings", menuName = "Settings/Movement")]
	public class MovementSettings: ScriptableObject
	{
		[SerializeField] private float playerMoveSpeed;
		[SerializeField] private float followersMoveSpeed;
		[SerializeField] private float followersRotationSpeed;
		[SerializeField] private float enemiesSpeed;
		
		public float PlayerMoveSpeed => playerMoveSpeed;
		public float FollowersMoveSpeed => followersMoveSpeed;
		public float FollowersRotationSpeed => followersRotationSpeed;
		public float EnemiesSpeed => enemiesSpeed;
	}
}