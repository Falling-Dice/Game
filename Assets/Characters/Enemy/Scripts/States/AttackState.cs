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
			Debug.Log("Dash");
		// Agent.Controller.Dash();
		else
			Agent.Controller.SetAiming(GameManager.Instance.Player.position, Agent.Controller.Side.RotationSpeed);

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

	public static bool LineLineIntersection(out Vector3 intersection, Vector3 linePoint1,
		Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2)
	{

		Vector3 lineVec3 = linePoint2 - linePoint1;
		Vector3 crossVec1and2 = Vector3.Cross(lineVec1, lineVec2);
		Vector3 crossVec3and2 = Vector3.Cross(lineVec3, lineVec2);

		float planarFactor = Vector3.Dot(lineVec3, crossVec1and2);

		//is coplanar, and not parallel
		if (Mathf.Abs(planarFactor) < 0.0001f
				&& crossVec1and2.sqrMagnitude > 0.0001f)
		{
			float s = Vector3.Dot(crossVec3and2, crossVec1and2)
					/ crossVec1and2.sqrMagnitude;
			intersection = linePoint1 + (lineVec1 * s);
			return true;
		}
		else
		{
			intersection = Vector3.zero;
			return false;
		}
	}
	#endregion
}

