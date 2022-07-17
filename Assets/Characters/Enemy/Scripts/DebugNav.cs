using UnityEngine;
using UnityEngine.AI;

public class DebugNav : MonoBehaviour
{
	private NavMeshAgent agent;

	void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	void OnDrawGizmos()
	{
		if (!agent)
			return;

		// var path = agent.path;
		// var previousCorner = transform.position;

		// Gizmos.color = Color.red;
		// foreach (var corner in path.corners)
		// {
		// 	Gizmos.DrawLine(previousCorner, corner);
		// 	Gizmos.DrawSphere(corner, 0.1f);

		// 	previousCorner = corner;
		// }



		var path = new NavMeshPath();
		NavMesh.CalculatePath(transform.position, agent.destination, NavMesh.AllAreas, path);
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