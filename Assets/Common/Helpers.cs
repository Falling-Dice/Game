using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Helpers
{
	#region data
	private static Camera _camera;
	#endregion


	#region properties
	public static Camera Camera
		=> _camera ??= Camera.main;
	#endregion

	#region methods
	public static T PickRandom<T>(this List<T> list)
	{
		var randomIndex = Random.Range(0, (list.Count()));
		return list[randomIndex];
	}

	public static Vector3 ResetY(this Vector3 vector, float y = 0.0f, bool immutable = false)
	{
		if (immutable)
			return new Vector3(vector.x, y, vector.z);

		vector.y = y;
		return vector;
	}
	#endregion
}