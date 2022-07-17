using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	#region data
	[SerializeField] private AudioSource _mainAudioSource;
	[SerializeField] private Transform _playerSpawn;
	[SerializeField] private CameraFollow _cameraFollow;
	[SerializeField] private PlayerMovement _player;
	[SerializeField] private int numberEnemies;
	[SerializeField] private EnemyAgent _enemyPrefab;
	[SerializeField] private Transform[] _enemiesSpawn;
	[SerializeField] private GameOverController _gameOverController;
	[SerializeField] private GameObject _scorePanel;
	[SerializeField] private TextMeshProUGUI _scoreText;

	[Header("Audios")]
	[SerializeField] private AudioClip _dieClip;
	[SerializeField] private float _dieClipVolume = 1f;


	#endregion
	private Transform player;

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
		Helpers.ResetCamera();

		player = Instantiate(_player, _playerSpawn.position, _playerSpawn.rotation).transform;
		_cameraFollow.SetTarget(player);

		for (var i = 0; i < numberEnemies; i++)
		{
			SpawnEnemy();
		}
	}
	#endregion

	#region properties
	public Transform Player
		=> player;
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
			_scoreText.text = $"Score: {Score}";
		}
		else
		{
			Debug.Log("tomb�");
			_scorePanel.SetActive(false);
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