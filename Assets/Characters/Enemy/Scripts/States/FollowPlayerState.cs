using UnityEngine;
using UnityEngine.AI;

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
		Agent.IsMoving = true;
	}

	public override void Update()
	{
		UpdatePosition();
		CheckDistance();
	}

	public override void Exit()
	{
		Agent.NavPath.ClearCorners();
		Agent.IsMoving = false;
	}
	#endregion

	#region privates
	private void UpdatePosition()
	{
		timerUpdatePosition -= Time.deltaTime;
		if (timerUpdatePosition >= 0)
			return;

		NavMesh.CalculatePath(Agent.transform.position, GameManager.Instance.Player.position, NavMesh.AllAreas, Agent.NavPath);
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

