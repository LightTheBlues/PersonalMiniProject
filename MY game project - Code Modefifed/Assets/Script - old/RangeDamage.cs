using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDamage : MonoBehaviour
{
    public float speed = 5.0f;
    public int damage = 15;
    public GameObject destroyEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,0, speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if(player != null)
        {
            player.TakeDamage(damage);
        }
        GameObject effectInstance = Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(effectInstance, 2.0f);
        Destroy(this.gameObject);
    }

}
