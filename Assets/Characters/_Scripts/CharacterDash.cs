using UnityEditor;
using UnityEngine;

public class CharacterDash : MonoBehaviour
{
	#region data
	public Rigidbody Rigidbody { get; private set; }
	#endregion


	#region unity events
	void Start()
	{
		// components
		Rigidbody = GetComponent<Rigidbody>();
	}
	#endregion

	#region methods
	public void Dash(CharacterSide side)
	{
		Rigidbody.AddForce(transform.forward * side.Range * 6, ForceMode.Impulse);
	}
	#endregion
}
