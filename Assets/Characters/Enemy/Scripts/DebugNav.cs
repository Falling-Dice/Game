using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyAgent))]
public class DebugNav : MonoBehaviour
{
	private EnemyAgent agent;

	void Awake()
	{
		agent = GetComponent<EnemyAgent>();
	}

	void OnDrawGizmos()
	{
		if (!agent)
			return;


		var path = agent.NavPath;
		var previousCorner = transform.position;

		Gizmos.color = Color.red;
		foreach (var corner in path.corners)
		{
			Gizmos.DrawLine(previousCorner, corner);
			Gizmos.DrawSphere(corner, 0.1f);

			previousCorner = corner;
		}
	}
}