using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text[] lives;

    void Start()
    {
        UpdateLives();
    }

    void Update()
    {
        ListenKeys();
    }

    void ListenKeys()
    {
        // Pause
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OpenMenu();
        }
    }

    void UpdateLives()
    {
        int CURRENT_LIVES = PlayerPrefs.GetInt("LIVES");

        for(int i = 0; i < lives.Length; i++)
        {
            lives[i].text = CURRENT_LIVES.ToString();
        }
    }

    public void OpenMenu()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        
        SceneManager.LoadScene("Menu");
    }

}
