using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
	#region properties
	public CharacterDash CharacterDash { get; private set; }
	public CharacterController Controller { get; private set; }
	#endregion


	#region unity events
	void Start()
	{
		// components
		CharacterDash = GetComponent<CharacterDash>();
		Controller = GetComponent<CharacterController>();
	}
	#endregion

	#region methods
	public void Attack(InputAction.CallbackContext context)
	{
		if (!context.performed) return;
		CharacterDash.Dash(Controller.Side);

		Controller.ChangeSide(CharacterSide.All.PickRandom());
	}
	#endregion
}
