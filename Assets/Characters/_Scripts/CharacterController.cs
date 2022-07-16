using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{
	#region variables
	[SerializeField] private float _moveSpeed = 5f;
	[SerializeField] private float _lookSpeed = 5f;
	[SerializeField] private DiceRoll _diceRoll;
	#endregion

	#region data
	public CharacterSide Side { get; private set; }
	public Vector3 Move { get; set; }
	public Vector3 Aiming { get; set; }

	private Rigidbody rigibody;
	#endregion


	#region unity events
	void Awake()
	{
		// components
		rigibody = GetComponent<Rigidbody>();
	}

	void FixedUpdate()
	{
		if (Move != Vector3.zero)
			HandleMove(Move);

		if (Aiming != Vector3.zero)
			HandleAiming(Aiming);
	}
	#endregion

	#region methods
	public void ChangeSide(CharacterSide newSide)
	{
		Side = newSide;
		_diceRoll.Roll(Side);
	}
	#endregion

	#region privates	
	private void HandleAiming(Vector3 aimingPosition)
	{
		aimingPosition.y = transform.position.y;
		var rotation = Quaternion.LookRotation(aimingPosition - transform.position);
		LookAt(rotation);

		// methods
		void LookAt(Quaternion rotation)
			=> transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _lookSpeed * Time.deltaTime);
	}

	private void HandleMove(Vector3 movePosition)
	{
		rigibody.MovePosition(transform.position + movePosition * Time.deltaTime * _moveSpeed);
	}
	#endregion
}
