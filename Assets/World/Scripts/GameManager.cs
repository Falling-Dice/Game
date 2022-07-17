using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region data
	[SerializeField] private Transform _player;
	[SerializeField] private Transform[] _enemiesSpawn;
	#endregion

	#region singleton
	private static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
			{
				var go = new GameObject(nameof(GameManager));
				go.AddComponent<GameManager>();
			}
			return _instance;
		}
	}
	#endregion

	#region properties
	public Transform Player
		=> _player;
	#endregion


	#region events
	void Awake()
	{
		_instance = this;
	}
	#endregion
}