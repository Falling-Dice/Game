using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    public GameObject gameOverScreen;

    public Button tryAgainButton;
    public Button goBackToMenuButton;

    public int score { get; set; } = 0;


    public void deactivate()
    {
        this.gameOverScreen.GetComponent<GameObject>().SetActive(false);
    }

    public void activate()
    {

        this.gameOverScreen.GetComponent<GameObject>().SetActive(true);
    }


    public void showGameOver()
    {
        this.gameOverScreen.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {

        tryAgainButton.GetComponent<Button>().onClick.AddListener(loadLevel);
        goBackToMenuButton.GetComponent<Button>().onClick.AddListener(loadMenu);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadLevel()
    {
        Debug.Log("loading");
        SceneManager.LoadScene("Level1");
    }

    public void loadMenu()
    {
        Application.Quit();
        Debug.Log("MenuScene");

    }
}
