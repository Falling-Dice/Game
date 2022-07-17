using System.Collections;
using UnityEngine;

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
	[SerializeField] private Collider _rangeCollider;
	#endregion

	#region data
	public CharacterSide Side { get; private set; }
	public Vector3 Move { get; set; }
	public Vector3 Aiming { get; private set; }
	public Rigidbody Rigibody { get; private set; }
	public CharacterDash CharacterDash { get; private set; }
	public bool IsDashing { get; private set; }

	private Vector3 desiredSize;
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
		if (!IsDashing || !collision.gameObject.TryGetComponent<CharacterController>(out var controller)) return;

		var agent = collision.gameObject.GetComponent<EnemyAgent>();
		if (agent != null)
			agent.ToggleAgent(false);

		var direction = collision.contacts[0].point - controller.transform.position;
		var force = -direction.normalized;
		force.y = 0;
		controller.Rigibody.AddForce(force * Rigibody.velocity.magnitude * 2, ForceMode.Impulse);
		// var time = .4f * Side.Size + .5f;
	}
	#endregion

	#region methods
	public void SetAiming(Vector3 aiming, float? lookSpeed = null)
	{
		Aiming = aiming;
		_lookSpeed = lookSpeed ?? _lookSpeed;
	}

	public void ChangeSide(CharacterSide newSide)
	{
		Side = newSide;
		_diceRoll.Roll(Side);
		desiredSize = new Vector3(Side.Size, Side.Size, Side.Size);
		Rigibody.mass = Side.Size;

		if (_rangeCollider != null)
			_rangeCollider.transform.localScale = new Vector3(1, 1, Side.Size);
	}

	public void Dash()
	{
		if (IsDashing)
			return;

		IsDashing = true;
		CharacterDash.Dash(Side);
		var time = .4f * Side.Size + .5f;
		StartCoroutine(WhenDashing(time));
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

	private IEnumerator WhenDashing(float timeWait)
	{
		yield return new WaitForSeconds(timeWait);
		ChangeSide(CharacterSide.All.PickRandom());

		IsDashing = false;
	}
	#endregion
}