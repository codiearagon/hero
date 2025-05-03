using UnityEngine;
using UnityEngine.XR;

public class Enemy : MonoBehaviour
{
    enum EState { Normal, Stunned, Egg, PlayerCollided, Chase };

    public static int enemyCount = 0;
    public GameObject currentWaypointDir;

    [SerializeField] private Sprite eggSprite;
    [SerializeField] private UIManager uiManager;
    [SerializeField] private float maxHealth = 100.0f;
    [SerializeField] private float health;
    [SerializeField] private float speed;

    private GameObject chaseObject;
    private static int destroyedEnemies = 0;
    private int eggCollisionCount = 0;
    private EState eState = EState.Normal;

    private float playerFoundAnimDuration;

    void Start()
    {
        EnemySpawnManager.enemies.Add(this);

        health = maxHealth;
        enemyCount++;
        uiManager.UpdateEnemyCountText(enemyCount);

        UpdatePath(WaypointManager.GetNextDestination(currentWaypointDir));
    }

    private void Update()
    {
        if (eState == EState.Normal)
            transform.Translate(Vector2.up * speed * Time.deltaTime);

        else if (eState == EState.Stunned)
            transform.RotateAround(transform.position, Vector3.forward, -180.0f * Time.deltaTime);

        else if (eState == EState.PlayerCollided)
        {
            playerFoundAnimDuration -= Time.deltaTime;

            if (playerFoundAnimDuration > 0.5f)
                transform.RotateAround(transform.position, Vector3.forward, 180.0f * Time.deltaTime);
            else if (playerFoundAnimDuration > 0)
                transform.RotateAround(transform.position, Vector3.forward, -180.0f * Time.deltaTime);
            else
                eState = EState.Chase;
        }

        else if (eState == EState.Chase)
        {
            Vector2 posDiff = chaseObject.transform.position - transform.position;

            transform.Translate(Vector2.up * speed * Time.deltaTime);
            transform.up = posDiff;

            // Change back to waypoint direction if too far
            if (posDiff.sqrMagnitude > Mathf.Pow(40, 2))
            {
                eState = EState.Normal;
                transform.up = currentWaypointDir.transform.position - transform.position;
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (eState == EState.Chase)
            {
                Destroy(gameObject);
                return;
            } 
            else if (eState == EState.Normal)
            {
                eState = EState.PlayerCollided;
                chaseObject = collision.gameObject;
                playerFoundAnimDuration = 1.0f;
                GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 1);
            }
        }
        else if (collision.name == "PlaneDetector" && collision.transform.parent.name == currentWaypointDir.name && eState != EState.Chase)
        {
            UpdatePath(WaypointManager.GetNextDestination(currentWaypointDir));
        }
    }

    public void UpdatePath(GameObject dir)
    {
        currentWaypointDir = dir;
        RotateTo(currentWaypointDir);
    }

    public void SetUIManager(UIManager uiM)
    {
        uiManager = uiM;
    }

    public void TakeDamageByMaxPercent(float percent)
    {
        eggCollisionCount++;
        health -= maxHealth * (percent / 100);

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a * 0.80f);

        if (health <= 0 || eggCollisionCount >= 3)
        {
            Destroy(gameObject);
        }

        ChangeState();
    }

    private void RotateTo(GameObject direction)
    {
        Vector3 target = direction.transform.position;
        transform.up = target - transform.position;
    }

    private void ChangeState()
    {
        if (eggCollisionCount == 1)
        {
            eState = EState.Stunned;
        }
        else if (eggCollisionCount == 2)
        {
            eState = EState.Egg;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = eggSprite;
        }
    }

    private void OnDestroy()
    {
        enemyCount--;
        destroyedEnemies++;
        EnemySpawnManager.enemies.Remove(this);
        uiManager.UpdateEnemyCountText(enemyCount);
        uiManager.UpdateDestroyedEnemiesText(destroyedEnemies);
    }
}
