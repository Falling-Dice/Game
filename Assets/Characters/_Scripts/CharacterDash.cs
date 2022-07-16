using UnityEditor;
using UnityEngine;

public class CharacterDash : MonoBehaviour
{
	#region data
	public CharacterSide Side { get; private set; }
	public Rigidbody Rigidbody { get; private set; }
	#endregion


	#region unity events
	void Start()
	{
		// components
		Rigidbody = GetComponent<Rigidbody>();

		Side = CharacterSide.Four;
	}
	#endregion

	#region methods
	public void Dash()
	{
		Rigidbody.AddForce(transform.forward * Side.Range * 100);
	}
	#endregion
}

[CustomEditor(typeof(CharacterDash))]
public class CharacterDashInspector : Editor
{
	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		var characterDash = (CharacterDash)target;
		if (GUILayout.Button("Dash"))
		{
			characterDash.Dash();
		}
	}
}
