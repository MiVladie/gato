using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject ink;
    public Transform rounds;

    [SerializeField]
    private int health = 100;

    public float shootingRate = 0.5f;

    public bool isDefending = false;
    private bool canShoot = true;

    public bool isAlive = true;


    void FixedUpdate()
    {
        ShootInk();

        OnDeathEvent();
    }

    void ShootInk()
    {
        if(!isDefending || !canShoot || !isAlive)
        {
            return;
        }
        
        GameObject bullet = Instantiate(ink, transform.position + new Vector3(0, 0.35f, -3f), Quaternion.Euler(0, 0, 0), rounds) as GameObject;
        
        int angle = Random.Range(0, 20);
        int speed =  Random.Range(15, 40);

        bullet.GetComponent<Rigidbody>().velocity = new Vector3(0, angle, -speed);
        
        Object.Destroy(bullet, 2.0f);

        StartCoroutine(ShootDelay());
    }

    void OnDeathEvent()
    {
        if(health <= 0)
        {
            FindObjectOfType<AudioManager>().Play("OctopusDead");

            isAlive = false;

            GetComponent<MeshCollider>().enabled = false;
            GetComponent<Animator>().enabled = false;

            StartCoroutine(DestroyDead());
        }
    }

    
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Bullet")) {
            Destroy(col.gameObject);

            FindObjectOfType<AudioManager>().Play("OctopusHit");

            health--;
        }
    }


    IEnumerator ShootDelay()
    {
        canShoot = false;

        yield return new WaitForSeconds(shootingRate);

        canShoot = true;
    }

    IEnumerator DestroyDead()
    {
        yield return new WaitForSeconds(1.5f);

        Destroy(gameObject);
    }

}
