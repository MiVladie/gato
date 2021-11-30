using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject ContinueButton;

    void Start()
    {
        AnalyseProgress();
    }

    void AnalyseProgress()
    {
        ContinueButton.SetActive(
            PlayerPrefs.HasKey("LEVEL") &&
            PlayerPrefs.HasKey("LIVES") &&
            PlayerPrefs.GetInt("LIVES") >= 1 && (
                PlayerPrefs.GetInt("LEVEL") != 1 ||
                PlayerPrefs.GetInt("CHECKPOINT") != 0
            )
        );
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
