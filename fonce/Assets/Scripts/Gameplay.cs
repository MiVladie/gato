using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{
    public GameObject checkpoints;
    public Text objective;

    private bool isPlaying = false;

    void Awake()
    {
        Spawn();
    }


    public void onCutsceneEnd(string name)
    {
        switch (name)
        {
            case "Kidnapping":
                objective.text = GetObjectiveByCutscene("Kidnapping");
                break;

            case "Flying":
                NextLevel();
                break;
                
            case "Noticing":
                objective.text = GetObjectiveByCutscene("Noticing");
                break;

            case "Chasing":
                NextLevel();
                break;
                
            case "Escaping":
                objective.text = GetObjectiveByCutscene("Escaping");
                break;
                
            case "Preparation":
                objective.text = GetObjectiveByCutscene("Preparation");
                break;
                
            case "Ending":
                RestartGame();
                break;
                
            default:
                break;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        switch(col.gameObject.tag) {
            case "Obstacle":
                ObstacleHit(col);
                break;

            default:
                break;
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        switch(col.gameObject.tag) {
            case "Checkpoint":
                CheckpointHit(col);
                break;
            
            case "Hairballs":
                GetComponent<PlayerController>().enableShooting = true;
                FindObjectOfType<UIManager>().EnableShooting();
                Destroy(col.gameObject);
                break;

            default:
                break;
        }
    }

    private void ObstacleHit(Collision col)
    {
        if(!isPlaying)
            return;

        isPlaying = false;

        int currentLives = PlayerPrefs.GetInt("LIVES") - 1;

        FindObjectOfType<AudioManager>().Play("Die");

        PlayerPrefs.SetInt("LIVES", currentLives);

        if(currentLives >= 1) {
            SceneManager.LoadScene("Respawn");
        } else {
            SceneManager.LoadScene("Menu");
        }
    }

    private string GetObjectiveByCutscene(string name)
    {
        switch (name)
        {
            case "Kidnapping":
                return "Find a Way to Reach the Moon!";
            
            case "Noticing":
                return "Reach the UFO!";
            
            case "Escaping":
                return "Chase After the UFO!";
            
            case "Preparation":
                return "Destroy the Octopus!";

            default:
                return "";
        } 
    }


    private void CheckpointHit(Collider col)
    {
        int checkpointIndex = col.gameObject.transform.GetSiblingIndex();

        col.gameObject.SetActive(false);

        PlayerPrefs.SetInt("CHECKPOINT", checkpointIndex);
    }

    private void RestartGame()
    {
        PlayerPrefs.SetInt("LIVES", 9);

        PlayerPrefs.SetInt("LEVEL", 1);
        PlayerPrefs.SetInt("CHECKPOINT", 0);

        SceneManager.LoadScene("Menu");
    }

    private void NextLevel()
    {
        int nextLevel = PlayerPrefs.GetInt("LEVEL") + 1;
        
        PlayerPrefs.SetInt("LEVEL", nextLevel);
        PlayerPrefs.SetInt("CHECKPOINT", 0);

        SceneManager.LoadScene(nextLevel);
    }

    private void Spawn()
    {
        // Get current checkpoint
        int currentCheckpoint = PlayerPrefs.GetInt("CHECKPOINT");

        // Disabling all previous checkpoints
        for(int i = 0; i < currentCheckpoint; i++) 
        {
            checkpoints.transform.GetChild(i).gameObject.SetActive(false);
        }

        // Get cutscenes
        PlayableDirector[] cutscenes = GameObject.FindObjectsOfType<PlayableDirector>();

        // Forwarding all previous cutscenes
        for(int i = 0; i < cutscenes.Length; i++) {
            if(cutscenes[i].transform.position.z <= checkpoints.transform.GetChild(currentCheckpoint).position.z) {
                cutscenes[i].GetComponent<PlayableDirector>().time = cutscenes[i].GetComponent<PlayableDirector>().duration;
                cutscenes[i].GetComponent<PlayableDirector>().Play();

                cutscenes[i].transform.parent.Find("Trigger").gameObject.SetActive(false);
                
                // Updating objective
                objective.text = GetObjectiveByCutscene(cutscenes[cutscenes.Length - 1].name);
            }
        }

        // Move player into the last checkpoint
        transform.position = checkpoints.transform.GetChild(currentCheckpoint).gameObject.transform.position;

        isPlaying = true;
    }

}
