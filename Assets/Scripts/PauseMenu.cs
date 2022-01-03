using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public Button resumeButton;
    public Button mainMenuButton;
    public Button exitButton;
    private PlayerFPS playerFPSScript;
    // Start is called before the first frame update
    void Start()
    {
        Button btn1 = resumeButton.GetComponent<Button>();
        Button btn2 = mainMenuButton.GetComponent<Button>();
        Button btn3 = exitButton.GetComponent<Button>();

        playerFPSScript = GameObject.Find("TestPlayer").GetComponent<PlayerFPS>();

        btn1.onClick.AddListener(Resume);
        btn2.onClick.AddListener(MainMenu);
        btn3.onClick.AddListener(Exit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Resume()
    {
        playerFPSScript.Resume();
        playerFPSScript.onPause = false;
    }

    void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void Exit()
    {
        Application.Quit();
    }
}
