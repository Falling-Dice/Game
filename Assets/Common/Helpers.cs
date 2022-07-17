using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Helpers
{
	#region data
	private static Camera _camera;
	private static Transform _cameraPivot;
	private static float _cameraAngle;
	private static Matrix4x4 _cameraIsoMatrix = RotateMatrixFromAngle(0);
	#endregion


	#region properties
	public static Camera Camera
		=> _camera ??= Camera.main;
	public static Transform CameraPivot
		=> _cameraPivot ??= Camera.transform.parent;
	public static float CameraPivotAngle
		=> CameraPivot.eulerAngles.y;
	#endregion

	#region methods
	public static Matrix4x4 GetMatrixFromCameraPivotAngle()
	{
		if (_cameraAngle != CameraPivot.eulerAngles.y)
		{
			_cameraAngle = CameraPivot.eulerAngles.y;
			_cameraIsoMatrix = RotateMatrixFromAngle(_cameraAngle);
		}

		return _cameraIsoMatrix;
	}
	public static Vector3 ToIsoFromCameraAngle(this Vector3 input)
		=> GetMatrixFromCameraPivotAngle().MultiplyPoint3x4(input);

	public static Vector3 GetVectorFromCameraPivot(Vector3 vector)
	{
		if (Camera == null) return Vector3.zero;
		return Quaternion.Euler(0, CameraPivotAngle, 0) * vector;
	}

	public static float Modulo(float value, float max)
		=> (value % max + max) % max;

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

	public static void ResetCamera()
	{
		_cameraPivot = null;
		_camera = null;
	}
	#endregion

	#region privates
	private static Matrix4x4 RotateMatrixFromAngle(float angle)
		=> Matrix4x4.Rotate(Quaternion.Euler(0, angle, 0));
	#endregion
}