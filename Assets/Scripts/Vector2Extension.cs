using UnityEngine;

namespace HeroSquad
{
	public static class Vector2Extension
	{
		public static Vector3 ToVector3Y(this Vector2 vector2, float y = 0)
		{
			return new Vector3(vector2.x, y, vector2.y);
		}
	}
}