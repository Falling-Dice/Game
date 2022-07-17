using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerInput))]
public class PlayerMovement : MonoBehaviour
{
	#region variables
	[SerializeField] private LayerMask _aimLayerMask;
	#endregion

	#region data
	private PlayerInput Input { get; set; }
	private CharacterController controller;
	#endregion


	#region unity events
	void Awake()
	{
		// components
		controller = GetComponent<CharacterController>();
		Input = GetComponent<PlayerInput>();
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

		if (Input.currentControlScheme == "Gamepad")
		{
			var direction = Vector3.right * input.x + Vector3.forward * input.y;
			if (direction.sqrMagnitude > 0.0f)
			{
				var rotation = Quaternion.LookRotation(Helpers.GetVectorFromCameraPivot(direction), Vector3.up);
				controller.SetAiming(rotation);
			}
		}
		else
		{
			if (Helpers.Camera == null) return;
			var ray = Helpers.Camera.ScreenPointToRay(input);
			if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, _aimLayerMask))
				return;

			var aiming = hit.point;
			aiming.y = transform.position.y;
			var rotation = Quaternion.LookRotation(aiming - transform.position);
			controller.SetAiming(rotation);
		}
	}
	#endregion
}
