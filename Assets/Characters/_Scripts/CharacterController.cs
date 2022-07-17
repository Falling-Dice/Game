using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterDash))]
[RequireComponent(typeof(AudioSource))]
public class CharacterController : MonoBehaviour
{
	#region variables
	[SerializeField] private float _moveSpeed = 5f;
	[SerializeField] private float _lookSpeed = 5f;
	[SerializeField] private DiceRoll _diceRoll;
	[SerializeField] private float _changeSizeSpeed = 10f;
	[SerializeField] private Collider _rangeCollider;
	[SerializeField] private Transform _zone;

	[Header("Audios")]
	[SerializeField] private AudioClip _dashClip;
	[SerializeField] private float _dashClipVolume = 1.0f;
	[SerializeField] private AudioClip _sideChangeClip;
	[SerializeField] private float _sideChangeClipVolume = 1.0f;
	[SerializeField] private AudioClip _sideChangeSizeClip;
	[SerializeField] private float _sideChangeClipSizeVolume = 1.0f;
	[SerializeField] private AudioClip _collisionClip;
	[SerializeField] private float _collisionClipVolume = 1.0f;
	#endregion

	#region data
	public CharacterSide Side { get; private set; }
	public Vector3 Move { get; set; }
	public Quaternion Aiming { get; private set; }
	public Rigidbody Rigibody { get; private set; }
	public CharacterDash CharacterDash { get; private set; }
	public DiceRoll DiceRoll { get; private set; }
	public AudioSource AudioSource { get; private set; }
	public bool IsDashing { get; private set; }

	private Vector3 desiredSize;
	#endregion


	#region unity events
	void Awake()
	{
		// components
		Rigibody = GetComponent<Rigidbody>();
		CharacterDash = GetComponent<CharacterDash>();
		AudioSource = GetComponent<AudioSource>();

		DiceRoll = _diceRoll;
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

		if (Aiming != Quaternion.identity)
			HandleAiming(Aiming);
	}
	void OnCollisionEnter(Collision collision)
	{
		if (!IsDashing || !collision.gameObject.TryGetComponent<CharacterController>(out var controller)) return;

		var direction = collision.contacts[0].point - controller.transform.position;
		var force = -direction.normalized.ResetY();
		controller.Rigibody.AddForce(force * Side.Range * 3, ForceMode.Impulse);

		controller.DiceRoll.Roll(CharacterSide.All.PickRandom());

		// audio
		AudioSource.PlayOneShot(_collisionClip, _collisionClipVolume);
	}
	#endregion

	#region methods
	public void SetAiming(Quaternion aiming, float? lookSpeed = null)
	{
		Aiming = aiming;
		_lookSpeed = lookSpeed ?? _lookSpeed;
	}

	public void ChangeSide(CharacterSide newSide)
	{
		Side = newSide;
		DiceRoll.Roll(Side);
		desiredSize = new Vector3(Side.Size, Side.Size, Side.Size);
		// Rigibody.mass = Side.Size;

		if (_rangeCollider != null)
			_rangeCollider.transform.localScale = new Vector3(1, 1, Side.Size);

		_zone.localScale = new Vector3(Side.Range, Side.Range, _zone.localScale.z);
		ColorUtility.TryParseHtmlString(Side.Color, out var newColor);
		_zone.GetComponent<Renderer>().material.color = newColor;

		// audio
		AudioSource.PlayOneShot(_sideChangeClip, _sideChangeClipVolume);
	}

	public void Dash()
	{
		if (IsDashing)
			return;

		AudioSource.PlayOneShot(_dashClip, _dashClipVolume);
		IsDashing = true;
		CharacterDash.Dash(Side);
		var time = .4f * Side.Size + .5f;
		StartCoroutine(WhenDashing(time));
	}
	#endregion

	#region privates	
	private void HandleAiming(Quaternion aimingRotation)
	{
		transform.rotation = Quaternion.Slerp(transform.rotation, aimingRotation, _lookSpeed * Time.deltaTime);
	}

	private void HandleMove(Vector3 movePosition)
	{
		Rigibody.MovePosition(transform.position + Helpers.GetVectorFromCameraPivot(movePosition) * Time.deltaTime * _moveSpeed);
	}

	private IEnumerator WhenDashing(float timeWait)
	{
		yield return new WaitForSeconds(timeWait);
		ChangeSide(CharacterSide.All.PickRandom());

		IsDashing = false;
	}
	#endregion
}