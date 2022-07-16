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
	#endregion


	#region unity events
	void Awake()
	{
		// components
		Controller = GetComponent<CharacterController>();
		NavMeshAgent = GetComponent<NavMeshAgent>();

		StateMachine = new EnemyStateMachine(this, new FollowPlayerState());
	}

	void Update()
	{
		StateMachine.Update();
	}
	#endregion

	#region methods
	public void ToggleAgent(bool toggle)
	{
		Controller.Rigibody.isKinematic = toggle;
		NavMeshAgent.enabled = toggle;
	}
	#endregion
}
