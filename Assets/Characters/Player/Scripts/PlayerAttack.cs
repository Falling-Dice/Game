using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
	#region properties
	public CharacterController Controller { get; private set; }
	#endregion


	#region unity events
	void Start()
	{
		// components
		Controller = GetComponent<CharacterController>();
	}
	#endregion

	#region methods
	public void Attack(InputAction.CallbackContext context)
	{
		if (!context.performed) return;
		Controller.Dash();
	}
	#endregion
}
