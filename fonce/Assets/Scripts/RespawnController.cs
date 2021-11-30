using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RespawnController : MonoBehaviour
{
    private const float DURATION = 2f;

    public Text textLives;

    void Start()
    {
        textLives.text = PlayerPrefs.GetInt("LIVES").ToString();

        StartCoroutine(RespawnPlayer());
    }

    IEnumerator RespawnPlayer() 
    {
        yield return new WaitForSeconds(DURATION); 

        SceneManager.LoadScene(PlayerPrefs.GetInt("LEVEL"));
    }
}
