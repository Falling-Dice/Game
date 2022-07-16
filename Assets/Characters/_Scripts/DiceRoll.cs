using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DiceRoll : MonoBehaviour
{
	#region variables
	[SerializeField] private Transform _objectToRotate;
	[SerializeField] private float _rotateSpeed = 1.5f;
	#endregion

	#region data
	private Vector3 rotationDestination;
	#endregion


	#region unity events
	private void Update()
	{
		// var desiredRotation = Quaternion.Euler(rotationDestination);
		// _objectToRotate.rotation = Quaternion.Lerp(_objectToRotate.rotation, desiredRotation, Time.deltaTime * _rotateSpeed);
	}
	#endregion

	#region privates
	public void Roll(RollFace face)
	{
		rotationDestination = face.Rotation;
	}
	#endregion
}

public class RollFace
{
	public static RollFace One = new(new Vector3(0, 0, -90));
	public static RollFace Two = new(new Vector3(0, 0, -180));
	public static RollFace Three = new(new Vector3(0, 0, -270));
	public static RollFace Four = new(new Vector3(0, 0, -360));
	public static RollFace Five = new(new Vector3(-90, 0, 0));
	public static RollFace Six = new(new Vector3(90, 0, 0));

	public Vector3 Rotation { get; private set; }
	public RollFace(Vector3 rotation)
	{
		Rotation = rotation;
	}
}

[CustomEditor(typeof(DiceRoll))]
public class RollsInspector : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		var rolls = (DiceRoll)target;
		if (GUILayout.Button("Roll"))
		{
			switch (Random.Range(1, 7))
			{
				case 1:
					rolls.Roll(RollFace.One);
					break;
				case 2:
					rolls.Roll(RollFace.Two);
					break;
				case 3:
					rolls.Roll(RollFace.Three);
					break;
				case 4:
					rolls.Roll(RollFace.Four);
					break;
				case 5:
					rolls.Roll(RollFace.Five);
					break;
				case 6:
					rolls.Roll(RollFace.Six);
					break;
			}
		}
	}
}