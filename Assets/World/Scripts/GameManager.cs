using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region data
	[SerializeField] private AudioSource _mainAudioSource;
	[SerializeField] private Transform _player;
	[SerializeField] private int numberEnemies;
	[SerializeField] private EnemyAgent _enemyPrefab;
	[SerializeField] private Transform[] _enemiesSpawn;
	[SerializeField] private GameOverController _gameOverController;

    [Header("Audios")]
	[SerializeField] private AudioClip _dieClip;
	[SerializeField] private float _dieClipVolume = 1f;
   

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

	void Awake()
	{
		_instance = this;
	}
	#endregion


	#region unity events
	void Start()
	{
		for (var i = 0; i < numberEnemies; i++)
		{
			SpawnEnemy();
		}
	}
	#endregion

	#region properties
	public Transform Player
		=> _player;
	public AudioSource MainAudioSource
		=> _mainAudioSource;
	public int Score { get; set; }
	#endregion

	#region methods
	public void OnCharacterFall(CharacterController controller)
	{
		if (controller.TryGetComponent<EnemyAgent>(out var agent))
		{
			Score++;
			MainAudioSource.PlayOneShot(_dieClip, _dieClipVolume);
			Destroy(controller.gameObject);
			SpawnEnemy();
		}
		else
		{
			Debug.Log("tombé");
			this._gameOverController.showGameOver();
		}
	}
	#endregion

	#region privates
	private void SpawnEnemy()
	{

		var spawn = _enemiesSpawn.ToList().PickRandom();
		Instantiate(_enemyPrefab, spawn.position, spawn.rotation);
	}
	#endregion
}