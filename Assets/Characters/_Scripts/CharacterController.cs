using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class CharacterController : MonoBehaviour
{
	#region variables
	[SerializeField] private float _moveSpeed = 5f;
	[SerializeField] private float _lookSpeed = 5f;
	[SerializeField] private DiceRoll _diceRoll;
	[SerializeField] private float _changeSizeSpeed = 10f;
	#endregion

	#region data
	public CharacterSide Side { get; private set; }
	public Vector3 Move { get; set; }
	public Vector3 Aiming { get; set; }
	public Rigidbody Rigibody { get; private set; }

	private Vector3 desiredSize;
	#endregion


	#region unity events
	void Awake()
	{
		// components
		Rigibody = GetComponent<Rigidbody>();
	}

	void Start()
	{
		ChangeSide(CharacterSide.All.PickRandom());
	}

	void Update()
	{
		transform.localScale = Vector3.Lerp(transform.localScale, desiredSize, Time.deltaTime * _changeSizeSpeed);
	}

	void FixedUpdate()
	{
		if (Move != Vector3.zero)
			HandleMove(Move);

		if (Aiming != Vector3.zero)
			HandleAiming(Aiming);
	}
	void OnCollisionEnter(Collision collision)
	{
		foreach (var contact in collision.contacts)
		{
			if (!contact.otherCollider.TryGetComponent<CharacterController>(out var controller)) return;

			if (contact.otherCollider.TryGetComponent<EnemyAgent>(out var agent))
			{
				agent.ToggleAgent(false);
			}

			var rb = contact.otherCollider.GetComponent<Rigidbody>();
			rb.AddForce(transform.forward * 500);
		}

	}

	#endregion

	#region methods
	public void ChangeSide(CharacterSide newSide)
	{
		Side = newSide;
		_diceRoll.Roll(Side);
		desiredSize = new Vector3(Side.Size, Side.Size, Side.Size);
		Rigibody.mass = Side.Size;
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
		Rigibody.MovePosition(transform.position + movePosition * Time.deltaTime * _moveSpeed);
	}
	#endregion
}