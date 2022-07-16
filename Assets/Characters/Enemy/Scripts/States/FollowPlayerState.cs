using UnityEngine;

public class FollowPlayerState : EnemyStateBase
{
	public override EnemyStateId Id { get; } = EnemyStateId.FollowPlayer;

	#region data
	private float timerUpdatePosition = .4f;
	private Vector3 position;
	#endregion

	public FollowPlayerState()
	{
		position = GameManager.Instance.Player.transform.position;
	}

	#region implements 
	public override void Enter()
	{
	}

	public override void Update()
	{
		UpdatePosition();
	}

	public override void Exit()
	{
		Agent.NavMeshAgent.isStopped = true;
		Agent.NavMeshAgent.ResetPath();
	}
	#endregion

	#region privates
	private void UpdatePosition()
	{
		timerUpdatePosition -= Time.deltaTime;
		if (timerUpdatePosition >= 0)
			return;

		if (Agent.NavMeshAgent.enabled)
			Agent.NavMeshAgent.SetDestination(GameManager.Instance.Player.transform.position);
	}
	#endregion
}

