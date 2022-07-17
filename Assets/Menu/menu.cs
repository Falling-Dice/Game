using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{

    public Button playButton;
    public Button exitButton;


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
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    public void loadLevel()
    {
        Debug.Log("loading");
        SceneManager.LoadScene("Level1");
        

    }
}
