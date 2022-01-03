using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //private PlayerFPS playerFPSScript;
    public Button playButton;
    public Button settingButton;
    public Button aboutButton;
    public Button exitButton;

    public GameObject settingMenu;
    public GameObject aboutMenu;
    //public Button mainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        Button play = playButton.GetComponent<Button>();
        Button setting = settingButton.GetComponent<Button>();
        Button about = aboutButton.GetComponent<Button>();
        Button exit = exitButton.GetComponent<Button>();

        play.onClick.AddListener(Play);
        setting.onClick.AddListener(Setting);
        about.onClick.AddListener(About);
        exit.onClick.AddListener(Exit);

        settingMenu.SetActive(false);
        aboutMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = Mathf.Approximately(Time.timeScale, 1.0f) ? 1.0f : 1.0f;
        //SceneManager.LoadScene("MainScene 2", LoadSceneMode.Additive);
    }

    public void Setting()
    {

        settingMenu.SetActive(true);
        aboutMenu.SetActive(false);
    }

    public void About()
    {

        settingMenu.SetActive(false);
        aboutMenu.SetActive(true);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
