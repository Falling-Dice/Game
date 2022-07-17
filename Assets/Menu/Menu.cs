using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
	public Button playButton;
	public Button exitButton;
	public AudioSource buttonClickSound;
	public AudioSource buttonSelectSound;
	// Start is called before the first frame update
	void Start()
	{

		playButton.GetComponent<Button>().onClick.AddListener(loadLevel);
		exitButton.GetComponent<Button>().onClick.AddListener(ExitGame);

	}


	// Update is called once per frame
	void Update()
	{

	}

	public void ExitGame()
	{
		clickSound();
		Application.Quit();
		Debug.Log("Game is exiting");

	}

	public void loadLevel()
	{
		clickSound();
		Debug.Log("loading");
		SceneManager.LoadScene("Level1");
	}

	public void clickSound()
	{
		buttonClickSound.Play();

	}

	public void selectSound()
	{
		buttonSelectSound.Play();

	}
}

