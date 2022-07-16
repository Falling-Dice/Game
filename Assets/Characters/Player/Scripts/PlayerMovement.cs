using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
	#region variables
	[SerializeField] private LayerMask _aimLayerMask;
	[SerializeField] private float _moveSpeed = 5f;
	[SerializeField] private float _lookSpeed = 5f;
	#endregion

	#region data
	private CharacterController controller;
	#endregion


	#region unity events
	void Awake()
	{
		// components
		controller = GetComponent<CharacterController>();
	}
	#endregion

	#region inputs
	public void Move(InputAction.CallbackContext context)
	{
		var position = context.ReadValue<Vector2>();
		controller.Move = new Vector3(position.x, 0, position.y);
	}
	public void Aiming(InputAction.CallbackContext context)
	{
		var input = context.ReadValue<Vector2>();
		var ray = Helpers.Camera.ScreenPointToRay(input);
		if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, _aimLayerMask) || hit.collider.isTrigger)
			return;

		controller.Aiming = hit.point;
	}
	#endregion
}
