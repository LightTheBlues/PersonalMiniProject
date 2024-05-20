using UnityEngine;

public class BoxDestroy : MonoBehaviour
{
    [SerializeField] private GameObject box001;

    private Animator animator;
    private Rigidbody rb;
    [SerializeField] public GameObject coinPrefab;
    [SerializeField] private GameObject potionPrefab;
    [SerializeField] private GameObject expPrefab;
    private bool SpawnItem = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (box001 != null && !box001.activeSelf)
        {
            animator.SetTrigger("Destroy");

            // Disable the Rigidbody

            if (rb != null)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                rb.isKinematic = true;
            }
            if(SpawnItem == false)
            {
                // Randomly decide which item to drop
                float randomValue = Random.value;
                if (randomValue < 0.3f)
                {
                   
                }
                else if (randomValue < 0.75f)
                {
                    Instantiate(coinPrefab, transform.position, Quaternion.identity);
                }
                else if (randomValue < 0.95f)
                {
                    Instantiate(potionPrefab, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(expPrefab, transform.position, Quaternion.identity);
                }

                SpawnItem = true;
            }
            
            Destroy(gameObject, 2);
        }
    }
    void OnDestroy()
    {
       
    }
}
