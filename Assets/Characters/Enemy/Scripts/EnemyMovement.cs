using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemyMovement : MonoBehaviour
{
	#region variables
	#endregion

	#region data
	private CharacterController controller;
	#endregion


	#region unity events
	void Awake()
	{
		// components
		controller = GetComponent<CharacterController>();
	}
	#endregion

}
