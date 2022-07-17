using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAgent : MonoBehaviour
{
	#region variables
	#endregion

	#region data
	public NavMeshAgent NavMeshAgent { get; private set; }
	public CharacterController Controller { get; private set; }

	public EnemyStateMachine StateMachine { get; private set; }
	public bool IsPlayerInRange { get; private set; }

	public float NextTimeCheckVelocity { get; set; }
	public bool IsMoving { get; set; }

	private float minMagnitudeBeforeNextCorner = .45f;
	#endregion


	#region unity events
	void Awake()
	{
		// components
		Controller = GetComponent<CharacterController>();
		NavMeshAgent = GetComponent<NavMeshAgent>();

		StateMachine = new EnemyStateMachine(this, new FollowPlayerState());

		// config
		NavMeshAgent.updatePosition = false;
		NavMeshAgent.Stop(true);
	}

	void Update()
	{
		StateMachine.Update();
	}

	void LateUpdate()
	{
		if (Time.time > NextTimeCheckVelocity)
		{
			ToggleAgent(true);
		}
	}

	void FixedUpdate()
	{
		// var path = NavMeshAgent.path;

		var path = new NavMeshPath();
		NavMesh.CalculatePath(transform.position, GameManager.Instance.Player.position, NavMesh.AllAreas, path);

		if (!path.corners.Any()) return;

		var towards = GetNextTowardsPosition(path.corners);

		if (towards == Vector3.zero)
			return;

		var destination = towards * NavMeshAgent.speed * Time.deltaTime;
		Controller.Rigibody.MovePosition(transform.position + destination);

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

	#region methods
	public void ToggleAgent(bool toggle)
	{
		// Controller.Rigibody.isKinematic = toggle;
		// NavMeshAgent.enabled = toggle;

		// if (!toggle)
		// {
		// 	NextTimeCheckVelocity = Time.time + .5f;
		// }
		// else
		// {
		// 	NavMeshAgent.nextPosition = Controller.Rigibody.position;
		// }
	}
	#endregion

	#region privates
	private Vector3 GetNextTowardsPosition(Vector3[] corners)
	{
		for (var i = 0; i < corners.Length; i++)
		{
			var target = corners[i];
			var towards = (target - transform.position).normalized;
			var magnitude = towards.ResetY(immutable: true).magnitude;

			if (magnitude > minMagnitudeBeforeNextCorner)
				return towards.ResetY(0);
		}

		return Vector3.zero;
	}
	#endregion
}
