using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TriggerCutscene : MonoBehaviour
{
    public PlayableDirector Cutscene;
    public bool disablePlayer = true;

    private bool hasTriggered = false;
    public bool listenEnding = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if(hasTriggered) {
            return;
        }

        if(other.gameObject.CompareTag("Player"))
        {
            Cutscene.Play();
            hasTriggered = true;

            if(disablePlayer) {
                StartCoroutine(DeactivatePlayer((float)Cutscene.duration, other.gameObject));
            }

            if(listenEnding) {
                StartCoroutine(NotifyGameplay((float)Cutscene.duration, other.gameObject));
            }
        }
    }

    IEnumerator DeactivatePlayer(float duration, GameObject player) 
    {
        player.GetComponent<PlayerController>().enabled = false;

        yield return new WaitForSeconds(duration);

        player.GetComponent<PlayerController>().enabled = true;
    }

    IEnumerator NotifyGameplay(float duration, GameObject player) 
    {
        yield return new WaitForSeconds(duration); 

        player.GetComponent<Gameplay>().onCutsceneEnd(Cutscene.transform.parent.name);

    }

}
