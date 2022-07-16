using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	#region data
	[SerializeField] private Transform _target;
	[SerializeField] private float _smoothSpeed = 2f;
	[SerializeField] private Vector3 _offset;
	#endregion


	#region events
	void FixedUpdate()
	{
		if (_target == null)
			return;

		var desiredPosition = _target.position + _offset;
		var smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
		transform.position = smoothedPosition;
	}
	#endregion

	#region methods
	public void SetTarget(Transform target)
		=> _target = target;
	#endregion
}
