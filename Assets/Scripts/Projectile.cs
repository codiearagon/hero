using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectileSpeed = 40.0f; // Move at 40 units/sec

    private float bounds = 100.0f;
    private static int eggCount = 0;

    private UIManager uiManager;

    void Start()
    {
        eggCount++;
        uiManager.UpdateEggsCountText(eggCount);
    }

    void Update()
    {
        CheckOutOfBounds();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.up * projectileSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            collision.GetComponent<Enemy>().TakeDamageByMaxPercent(20.0f); // 20% of health
        }
        else if (collision.CompareTag("Waypoint"))
        {
            Destroy(gameObject);
            collision.GetComponent<Waypoint>().TakeDamageByMaxPercent(25.0f); // 25% of health
        }
    }

    void CheckOutOfBounds()
    {
        if(transform.position.y > bounds ||  transform.position.y < -bounds ||
            transform.position.x > bounds || transform.position.x < -bounds) 
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        //Debug.Log("Projectile destroyed");
        eggCount--;
        uiManager.UpdateEggsCountText(eggCount);
    }

    public void SetUIManager(UIManager uiM)
    {
        uiManager = uiM;
    }
}
