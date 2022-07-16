using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterDash))]
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
	public CharacterDash CharacterDash { get; private set; }

	private Vector3 desiredSize;
	private bool canDash = true;
	#endregion


	#region unity events
	void Awake()
	{
		// components
		Rigibody = GetComponent<Rigidbody>();
		CharacterDash = GetComponent<CharacterDash>();
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
		if (!collision.gameObject.TryGetComponent<CharacterController>(out var controller)) return;

		// if (collision.gameObject.TryGetComponent<EnemyAgent>(out var agent))
		// {
		// 	agent.ToggleAgent(false);
		// 	Debug.Log(collision.relativeVelocity);
		// 	var rb = collision.gameObject.GetComponent<Rigidbody>();
		// 	rb.AddForce(-collision.relativeVelocity * 150);
		// }

		Rigibody.AddForce(collision.relativeVelocity * 50);
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

	public void Dash()
	{
		if (!canDash)
			return;

		canDash = false;
		CharacterDash.Dash(Side);
		var time = .4f * Side.Size + .5f;
		StartCoroutine(WhenDashEnd(time));
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

	private IEnumerator WhenDashEnd(float timeWait)
	{
		yield return new WaitForSeconds(timeWait);
		ChangeSide(CharacterSide.All.PickRandom());
		canDash = true;
	}
	#endregion
}