using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Waypoint : MonoBehaviour
{

    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private float health;

    private int eggCollisionCount = 0;
    private Vector2 origPos;

    private bool isShaking;
    private Vector2 initialPos;
    private float shakeTime;

    void Start()
    {
        origPos = transform.position;
        health = maxHealth;
    }

    private void Update()
    {
        if (isShaking)
        {
            transform.position = initialPos + Random.insideUnitCircle * eggCollisionCount;
            shakeTime -= Time.deltaTime;

            if (shakeTime<= 0)
            {
                isShaking = false;
                WaypointManager.DisableCam();
            }
        }
    }

    public void TakeDamageByMaxPercent(float percent)
    {
        eggCollisionCount++;
        health -= maxHealth * (percent / 100);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a * 0.75f);

        WaypointManager.SetCamera(this);
        Shake();

        if (health <= 0 || eggCollisionCount >= 4)
        {
            transform.position = new Vector2(Random.Range(origPos.x - 15, origPos.x + 15), Random.Range(origPos.y - 15, origPos.y + 15));
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1.0f);
            health = maxHealth;
            eggCollisionCount = 0;
            shakeTime = 0;

            WaypointManager.ReconfigurePath(gameObject);
        }
    }

    public int GetEggCount()
    {
        return eggCollisionCount;
    }

    public void Shake()
    {
        if (eggCollisionCount > 3)
            return;

        isShaking = true;
        initialPos = transform.position;
        shakeTime = eggCollisionCount;
    }
}
