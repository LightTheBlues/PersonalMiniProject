using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceBall: MonoBehaviour
{
    public float obstacleRange = 5.0f;

    [SerializeField] private GameObject IceSphere;
    public float projectileSpeed = 10.0f;
    public float fireRate = 2.0f; // Fire rate in seconds
    private float nextFireTime;
    public bool range = true;

    Animator animator;


    void Start()
    {
        animator = GetComponent<Animator>();
        range = true;
    }
    void Update()
    {
        // Check if it's time to fire a fireball
        if (Time.time > nextFireTime && range == true)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            if (Physics.SphereCast(ray, 1.75f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    Debug.Log("fire");
                    animator.SetTrigger("Iceball");

                    // Start the WaitForDelayAndFire coroutine
                    StartCoroutine(WaitForDelayAndFire());
                }
            }

            // Update the next fire time
            nextFireTime = Time.time + fireRate;
        }
    }

    IEnumerator WaitForDelayAndFire()
    {
        // Wait for 1 seconds
        yield return new WaitForSeconds(1);

        FireAtPlayer();
    }

    void FireAtPlayer()
    {
        GameObject magicWand = GameObject.FindGameObjectWithTag("magicWand");

        Vector3 magicWandPostion = magicWand.transform.position + transform.forward * 1.2f;

        // Instantiate the fireball at the head position
        GameObject iceSphere = Instantiate(IceSphere, magicWandPostion, Quaternion.identity);
        Rigidbody rb = iceSphere.GetComponent<Rigidbody>(); // Get the Rigidbody component from the fireball

        // Ignore collisions between the fireball and the object that shot it
        //Physics.IgnoreCollision(fireball.GetComponent<Collider>(), GetComponent<Collider>());

        // Find the position of the player
        Vector3 playerPosition = PlayerCharacter.Instance.transform.position;
        playerPosition.y += 1;

        // Calculate the direction from the fireball to the player
        Vector3 directionToPlayer = (playerPosition - iceSphere.transform.position).normalized;

        // Set the fireball's velocity using the Rigidbody component
        rb.velocity = directionToPlayer * projectileSpeed;

        // Set a lifetime for the fireball (adjust as needed)
        Destroy(iceSphere, 5.0f);

        // Visualize the fireball's path with a debug ray
        //Debug.DrawRay(fireball.transform.position, rb.velocity.normalized, Color.red, 2.0f);

        // Output a debug log with the fireball's position
        //Debug.Log("Fireball was shot at position: " + fireball.transform.position);
    }

}
