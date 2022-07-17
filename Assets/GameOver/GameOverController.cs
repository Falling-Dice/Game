using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverController : MonoBehaviour
{
	public GameObject gameOverScreen;

	public Button tryAgainButton;
	public Button goBackToMenuButton;
	public TextMeshProUGUI scoreText;


	public void showGameOver(int score)
	{
		this.gameOverScreen.SetActive(true);
		scoreText.text = $"{score} Score";
	}
	// Start is called before the first frame update
	void Start()
	{
		tryAgainButton.onClick.AddListener(loadLevel);
		goBackToMenuButton.onClick.AddListener(loadMenu);
	}

	// Update is called once per frame

	public void loadLevel()
	{
		Debug.Log("loading");
		SceneManager.LoadScene("Level1");
	}

	public void loadMenu()
	{
		SceneManager.LoadScene("MenuScene");
	}
}
