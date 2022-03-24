using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject hairball;
    public Transform rounds;

    private Rigidbody rb;
    private List<Collider> groundColliders = new List<Collider>();
    
    public bool enableShooting = false;

    public float movementSpeed = 7.5f;
    public int bulletSpeed = 25;

    public float jumpForce = 3.0f;
    public float lowJumpMultiplier = 2.5f;
    public float fallMultiplier = 2.0f;

    private bool isForward = true;
    private bool canShoot = true;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        EventListener();
    }


    public void EventListener()
    {
        // Jumping
        if(Input.GetKey(KeyCode.W))
        {
            JumpPlayer(true);
        } else {
            JumpPlayer(false);
        }
        
        // Moving
        if(Input.GetKey(KeyCode.A))
        {
            MovePlayer(-1);
        } else if(Input.GetKey(KeyCode.D))
        {
            MovePlayer(1);
        }

        // Shooting
        if(Input.GetKey(KeyCode.Space))
        {
            ShootPlayer();
        }
    }

    public void MovePlayer(int direction)
    {
        // Moving
        if(direction == -1) {
            // Handling rotation            
            if(isForward) {
                transform.rotation = Quaternion.Euler(0, 180, 0);

                isForward = false;
            }
        } else if(direction == 1) {
            // Handling rotation
            if(!isForward) {
                transform.rotation = Quaternion.Euler(0, 0, 0);

                isForward = true;
            }
        }

        Vector3 movement = new Vector3(0.0f, 0.0f, direction);

        transform.position += movement * movementSpeed * Time.deltaTime;
    }

    public void JumpPlayer(bool isJumping)
    {
        // Jumping
        if(isJumping && groundColliders.Count > 0) {
            rb.velocity = Vector3.up * jumpForce;
        }
        
        // Improving
        if(rb.velocity.y < 0) {
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime * 2;
        } else if(rb.velocity.y > 0 && !isJumping) {
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime * 2;
        }
    }

    public void ShootPlayer()
    {
        // Shooting
        if(enableShooting && canShoot) {
            FindObjectOfType<AudioManager>().Play("Shoot");

            GameObject bullet = Instantiate(hairball, transform.position + new Vector3(0, 0.35f, isForward ? 1.5f : -1.5f), Quaternion.Euler(0, 0, 10), rounds) as GameObject;
            
            bullet.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, isForward ? bulletSpeed : -bulletSpeed);
            
            Object.Destroy(bullet, 2.0f);

            StartCoroutine(ShootDelay());
        }
    }
    

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.CompareTag("Ground")) {
            groundColliders.Add(col.collider);
        }
    }
    
    void OnCollisionExit(Collision col)
    {
        if(col.gameObject.CompareTag("Ground")) {
            groundColliders.Remove(col.collider);

            if(groundColliders.Count == 0)
            {
                FindObjectOfType<AudioManager>().Play("Jump");
            }
        }
    }


    IEnumerator ShootDelay()
    {
        canShoot = false;

        yield return new WaitForSeconds(0.1f);

        canShoot = true;
    }

}
