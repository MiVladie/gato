using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text[] lives;
    public GameObject controls;
    public GameObject shootingButton;

    private PlayerController pController;

    private bool jumping = false;
    private bool shooting = false;
    private bool movingLeft = false;
    private bool movingRight = false;

    void Start()
    {
        Initialize();

        UpdateLives();
    }

    void FixedUpdate()
    {
        if(pController.isActiveAndEnabled)
        {
            pController.JumpPlayer(jumping);
            pController.ShootPlayer(shooting);
            pController.MovePlayer(movingLeft ? -1 : movingRight ? 1 : 0);
        }
    }

    void Initialize()
    {
        pController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            controls.SetActive(false);
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

    public void EnableShooting()
    {
        shootingButton.SetActive(true);
    }

    public void OpenMenu()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        
        SceneManager.LoadScene("Menu");
    }


    public void Jump(bool pressing)
    {
        jumping = pressing;
    }
    
    public void Shoot(bool pressing)
    {
        shooting = pressing;
    }

    public void MoveLeft(bool pressing)
    {
        movingLeft = pressing;
    }

    public void MoveRight(bool pressing)
    {
        movingRight = pressing;
    }

}
