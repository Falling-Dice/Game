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

		if (Agent.Controller.Side.Range <= Vector3.Distance(Agent.transform.position, GameManager.Instance.Player.position)) return;
		Agent.StateMachine.ChangeState(new FollowPlayerState());
	}
	#endregion
}

