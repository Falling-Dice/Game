using UnityEngine;

public class FollowPlayerState : EnemyStateBase
{
	public override EnemyStateId Id { get; } = EnemyStateId.FollowPlayer;

	#region data
	private float timerUpdatePosition = .4f;
	private Vector3 position;

	private float durationBeforeCheckDistance = .25f;
	private float timeBeforeCheckDistance;
	#endregion


	#region implements 
	public override void Enter()
	{
		position = GameManager.Instance.Player.transform.position;
	}

	public override void Update()
	{
		UpdatePosition();
		CheckDistance();
	}

	public override void Exit()
	{
		if (Agent.NavMeshAgent.enabled)
		{
			Agent.NavMeshAgent.isStopped = true;
			Agent.NavMeshAgent.ResetPath();
		}
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

	private void CheckDistance()
	{
		if (Time.time < timeBeforeCheckDistance) return;
		timeBeforeCheckDistance = Time.time + durationBeforeCheckDistance;

		if (Vector3.Distance(Agent.transform.position, GameManager.Instance.Player.position) > Agent.Controller.Side.Range) return;

		Agent.StateMachine.ChangeState(new AttackState());
	}
	#endregion
}

