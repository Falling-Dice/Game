using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
	#region properties
	public CharacterDash CharacterDash { get; private set; }
	#endregion


	#region unity events
	void Start()
	{
		// components
		CharacterDash = GetComponent<CharacterDash>();
	}
	#endregion

	#region methods
	public void Attack(InputAction.CallbackContext context)
	{
		if (!context.performed) return;
		CharacterDash.Dash();
	}
	#endregion
}
