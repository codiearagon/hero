using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private float health;

    private int eggCollisionCount = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    public void TakeDamageByMaxPercent(float percent)
    {
        eggCollisionCount++;
        health -= maxHealth * (percent / 100);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a * 0.25f);

        if (health <= 0 || eggCollisionCount >= 4)
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        //Instantiate(gameObject, new Vector2(transform.position.x + 1, transform.position.y + 1), transform.rotation, transform.parent);
    }
}
