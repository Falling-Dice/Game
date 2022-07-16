using UnityEngine;

public class AttackState : EnemyStateBase
{
	public override EnemyStateId Id { get; } = EnemyStateId.Attacking;

	#region data
	private float durationBeforeCheckDistance = .25f;
	private float timeBeforeCheckDistance;
	#endregion

	public AttackState()
	{
	}

	#region implements 
	public override void Enter()
	{
		Agent.ToggleAgent(false);
	}

	public override void Update()
	{
		Agent.Controller.Dash();

		CheckDistance();
	}

	public override void Exit()
	{
		Agent.ToggleAgent(true);
	}
	#endregion

	#region privates
	private void CheckDistance()
	{
		if (Time.time < timeBeforeCheckDistance) return;
		timeBeforeCheckDistance = Time.time + durationBeforeCheckDistance;

		if (Vector3.Distance(Agent.transform.position, GameManager.Instance.Player.position) <= Agent.Controller.Side.Range) return;
		Agent.StateMachine.ChangeState(new FollowPlayerState());
	}
	#endregion
}

