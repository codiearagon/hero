using System.Collections.Generic;
using UnityEditor.Purchasing;
using UnityEngine;

public class ChaseCamera : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private UIManager uiManager;

    private List<Enemy> chasingEnemies = new List<Enemy>();
    private Camera cam;

    private Enemy enemy;

    private bool chasing = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (chasing)
        {
            float distance = Vector2.SqrMagnitude(enemy.transform.position - player.transform.position);
            cam.orthographicSize = Mathf.Sqrt(distance);

            Vector2 midpoint = (enemy.transform.position + player.transform.position) / 2;
            transform.position = new Vector3(midpoint.x, midpoint.y, -10);
        }
    }

    public void SetEnemy(Enemy e)
    {
        uiManager.UpdateChaseCamVisibility(true);
        uiManager.UpdateChaseCamText("Active");

        chasing = true;
        chasingEnemies.Add(e);
        enemy = e;
    }

    public void DisableChase(Enemy e)
    {
        chasingEnemies.Remove(e);

        if(chasingEnemies.Count == 0)
        {
            chasing = false;
            uiManager.UpdateChaseCamVisibility(false);
            uiManager.UpdateChaseCamText("Inactive");
        } 

        else
        {
            enemy = chasingEnemies[chasingEnemies.Count - 1];
        }
    } 
}
