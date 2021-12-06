using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject ContinueButton;
    public GameObject ExitButton;

    private bool canContinue = false;
    private bool canExit = false;

    void Start()
    {
        AnalyseProgress();
    }

    void Update()
    {
        ListenKeys();
    }

    void ListenKeys()
    {
        // New Game
        if(Input.GetKeyDown(KeyCode.N))
        {
            NewGame();
        }
        
        // Continue
        if(canContinue && Input.GetKeyDown(KeyCode.E))
        {
            ContinueGame();
        }

        // Exit
        if(canExit && Input.GetKeyDown(KeyCode.Escape))
        {
            Exit();
        }
    }

    void AnalyseProgress()
    {
        canContinue = PlayerPrefs.HasKey("LEVEL") &&
            PlayerPrefs.HasKey("LIVES") &&
            (
                PlayerPrefs.GetInt("LEVEL") != 1 ||
                PlayerPrefs.GetInt("CHECKPOINT") != 0
            );

        canExit = Application.platform != RuntimePlatform.WebGLPlayer;

        ContinueButton.SetActive(canContinue);
        ExitButton.SetActive(canExit);
    }

    public void ContinueGame()
    {
        FindObjectOfType<AudioManager>().Play("Button");

        SceneManager.LoadScene(PlayerPrefs.GetInt("LEVEL"));
    }

    public void NewGame()
    {
        FindObjectOfType<AudioManager>().Play("Button");

        PlayerPrefs.SetInt("LEVEL", 1);
        PlayerPrefs.SetInt("CHECKPOINT", 0);
        PlayerPrefs.SetInt("LIVES", 9);

        SceneManager.LoadScene("Earth");
    }

    public void Exit()
    {
        Application.Quit();
    }
    
}
