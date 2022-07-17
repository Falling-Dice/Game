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


    public void showGameOver()
    {
        this.gameOverScreen.gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(this.tryAgainButton);
        tryAgainButton.GetComponent<Button>().onClick.AddListener(loadLevel);
        goBackToMenuButton.GetComponent<Button>().onClick.AddListener(loadMenu);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadLevel()
    {

        Debug.Log("loadlevel");

        SceneManager.LoadScene("Level1");
    }

    public void loadMenu()
    {
        Debug.Log("loadMenu");
        SceneManager.LoadScene("MenuScene");


    }

    private void OnDisable()
    {
        tryAgainButton.GetComponent<Button>().onClick.RemoveAllListeners();
        goBackToMenuButton.GetComponent<Button>().onClick.RemoveAllListeners();

    }
}
