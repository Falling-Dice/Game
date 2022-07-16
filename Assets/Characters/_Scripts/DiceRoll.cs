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
					rolls.Roll(CharacterSide.One);
					break;
				case 2:
					rolls.Roll(CharacterSide.Two);
					break;
				case 3:
					rolls.Roll(CharacterSide.Three);
					break;
				case 4:
					rolls.Roll(CharacterSide.Four);
					break;
				case 5:
					rolls.Roll(CharacterSide.Five);
					break;
				case 6:
					rolls.Roll(CharacterSide.Six);
					break;
			}
		}
	}
}