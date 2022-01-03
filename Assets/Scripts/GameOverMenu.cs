using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public Button restartButton;
    public Button mainMenuButton;

    // Start is called before the first frame update
    void Start()
    {
        Button btn1 = restartButton.GetComponent<Button>();
        Button btn2 = mainMenuButton.GetComponent<Button>();

        //btn1.onClick.AddListener();
        btn2.onClick.AddListener(MainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void Restart()
    {

    }
}
