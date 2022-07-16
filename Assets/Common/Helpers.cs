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
}