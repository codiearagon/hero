using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float bounds = 15.0f;

    private float projectileSpeed = 4.0f; // Move at 40 units/sec

    // Update is called once per frame
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
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
}
