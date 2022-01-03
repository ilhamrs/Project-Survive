using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private PlayerFPS playerFPSScript;
    // Start is called before the first frame update
    void Start()
    {
        playerFPSScript = GameObject.Find("TestPlayer").GetComponent<PlayerFPS>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RestartGame()
    {
        //load ulang scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        //Time.timeScale = Mathf.Approximately(Time.timeScale, 1.0f) ? 1.0f : 1.0f;
        //playerFPSScript.Resume();
    }
}
