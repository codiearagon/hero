using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private float health;

    private int eggCollisionCount = 0;
    private Vector2 origPos;

    void Start()
    {
        origPos = transform.position;
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
            transform.position = new Vector2(Random.Range(origPos.x - 15, origPos.x + 15), Random.Range(origPos.y - 15, origPos.y + 15));
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1.0f);
            health = maxHealth;
            eggCollisionCount = 0;
        }
    }
}
