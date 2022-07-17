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
	}

	public override void Update()
	{
		if (Agent.IsPlayerInRange)
		{
			Agent.Controller.Dash();
		}
		else
		{
			var towards = (GameManager.Instance.Player.position - Agent.transform.position).normalized;
			var rotation = Quaternion.LookRotation(towards);
			Agent.Controller.SetAiming(rotation, Agent.Controller.Side.RotationSpeed);
		}

		CheckDistance();
	}

	public override void Exit()
	{
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

