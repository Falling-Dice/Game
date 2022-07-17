using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
public class EnemyAgent : MonoBehaviour
{
	#region variables
	[SerializeField] private float _speed = 3.5f;
	[SerializeField] private float _minMagnitudeBeforeNextCorner = .5f;
	#endregion

	#region data
	public CharacterController Controller { get; private set; }

	public EnemyStateMachine StateMachine { get; private set; }
	public bool IsPlayerInRange { get; private set; }

	public bool IsMoving { get; set; }
	public NavMeshPath NavPath { get; private set; }
	#endregion


	#region unity events
	void Awake()
	{
		// components
		Controller = GetComponent<CharacterController>();

		StateMachine = new EnemyStateMachine(this, new FollowPlayerState());

		// config 
		NavPath = new NavMeshPath();
	}

	void Update()
	{
		StateMachine.Update();
	}

	void LateUpdate()
	{
	}

	void FixedUpdate()
	{
		if (!IsMoving)
			return;

		if (!NavPath.corners.Any()) return;

		var towards = GetNextTowardsPosition(NavPath.corners);

		if (towards == Vector3.zero)
			return;

		var destination = towards * _speed * Time.deltaTime;
		Controller.Rigibody.MovePosition(transform.position + destination);

		var lookAt = Quaternion.LookRotation(towards);
		transform.rotation = Quaternion.Slerp(transform.rotation, lookAt, Time.deltaTime * Controller.Side.RotationSpeed);

		// var destination = NavMeshAgent.nextPosition.ResetY(transform.position.y);
		// Controller.Rigibody.MovePosition(destination);

		// NavMeshAgent.nextPosition = Controller.Rigibody.position;
		// Controller.Rigibody.velocity = direction;
	}

	void OnTriggerEnter(Collider other)
	{
		if (!other.TryGetComponent<PlayerMovement>(out var player)) return;
		IsPlayerInRange = true;
	}

	void OnTriggerExit(Collider other)
	{
		if (!other.TryGetComponent<PlayerMovement>(out var player)) return;
		IsPlayerInRange = false;
	}
	#endregion

	#region privates
	private Vector3 GetNextTowardsPosition(Vector3[] corners)
	{
		for (var i = 0; i < corners.Length; i++)
		{
			var target = corners[i];
			var towards = (target - transform.position).normalized.ResetY();
			// var magnitude = towards.ResetY(immutable: true).magnitude;

			if (towards.magnitude > _minMagnitudeBeforeNextCorner)
				return towards;
		}

		return Vector3.zero;
	}
	#endregion
}
