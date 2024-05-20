using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingFireBall : MonoBehaviour
{
    public float obstacleRange = 5.0f;

    [SerializeField] private GameObject fireballPrefab;
    public float projectileSpeed = 10.0f;
    public float fireRate = 2.0f; // Fire rate in seconds
    private float nextFireTime;
    public bool range = false;

    Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
            // Check if it's time to fire a fireball
            if (Time.time > nextFireTime)
            {
                Ray ray = new Ray(transform.position, transform.forward);
                RaycastHit hit;
                
                if (Physics.SphereCast(ray, 1.75f, out hit))
                {
                    GameObject hitObject = hit.transform.gameObject;
                    if(hitObject.GetComponent<PlayerCharacter>() && range == true)
                    {
                        Debug.Log("fire");
                        animator.SetTrigger("fireball");
                        FireAtPlayer();
                    }
                }

                // Update the next fire time
                nextFireTime = Time.time + 1 / fireRate;
            }
    }

    void FireAtPlayer()
    {
        GameObject closestDragonHead = GameObject.FindGameObjectWithTag("DragonHead");

        Vector3 dragonHeadPosition = closestDragonHead.transform.position + transform.forward * 1.2f;

        // Instantiate the fireball at the head position
        GameObject fireball = Instantiate(fireballPrefab, dragonHeadPosition, Quaternion.identity);
        Rigidbody rb = fireball.GetComponent<Rigidbody>(); // Get the Rigidbody component from the fireball

        // Ignore collisions between the fireball and the object that shot it
        //Physics.IgnoreCollision(fireball.GetComponent<Collider>(), GetComponent<Collider>());

        // Find the position of the player
        Vector3 playerPosition = PlayerCharacter.Instance.transform.position;

        // Calculate the direction from the fireball to the player
        Vector3 directionToPlayer = (playerPosition - fireball.transform.position).normalized;

        // Set the fireball's velocity using the Rigidbody component
        rb.velocity = directionToPlayer * projectileSpeed;

        // Set a lifetime for the fireball (adjust as needed)
        Destroy(fireball, 5.0f);

        // Visualize the fireball's path with a debug ray
        Debug.DrawRay(fireball.transform.position, rb.velocity.normalized, Color.red, 2.0f);
        
        // Output a debug log with the fireball's position
        Debug.Log("Fireball was shot at position: " + fireball.transform.position);
    }

}
