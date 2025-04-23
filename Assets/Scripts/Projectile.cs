using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float damagePercent = 20.0f;
    [SerializeField] private float bounds = 15.0f;
    [SerializeField] private float projectileSpeed = 40.0f; // Move at 40 units/sec

    private static int eggCount = 0;

    void Start()
    {
        eggCount++;
        UIManager.UpdateEggsCountText(eggCount);
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
            collision.GetComponent<Enemy>().TakeDamageByMaxPercent(damagePercent);
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
        eggCount--;
        UIManager.UpdateEggsCountText(eggCount);
    }
}
