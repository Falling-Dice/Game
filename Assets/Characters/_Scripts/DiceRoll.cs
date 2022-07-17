using UnityEngine;
using UnityEditor;

public class DiceRoll : MonoBehaviour
{
	#region variables
	[SerializeField] private float _rotateSpeed = 1.5f;
	#endregion

	#region data
	private Vector3 rotationDestination;
	#endregion


	#region unity events
	private void Update()
	{
		var desiredRotation = Quaternion.Euler(rotationDestination);
		transform.localRotation = Quaternion.Lerp(transform.localRotation, desiredRotation, Time.deltaTime * _rotateSpeed);
	}
	#endregion

	#region privates
	public void Roll(CharacterSide side)
	{
		rotationDestination = side.Rotation;
	}
	#endregion
}