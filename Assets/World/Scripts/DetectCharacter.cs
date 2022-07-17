using UnityEngine;

public class DetectCharacter : MonoBehaviour
{
	#region unity events
	void OnTriggerEnter(Collider other)
	{
		if (!other.TryGetComponent<CharacterController>(out var controller)) return;

		GameManager.Instance.OnCharacterFall(controller);
	}
	#endregion
}
