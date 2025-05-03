using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class WaypointManager : MonoBehaviour
{
    enum Mode {Random, Sequential};

    [SerializeField] private static UIManager uiManager;
    [SerializeField] private static Camera cam;
    private static List<GameObject> waypoints;
    private static Mode mode;

    private bool visible;
    private static bool camInUse;

    void Start()
    {
        cam = GameObject.Find("WPCamera").GetComponent<Camera>();
        uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();

        mode = Mode.Sequential;
        visible = true;
        camInUse = false;
        uiManager.UpdateWaypointVisibilityText("Visible");

        waypoints = new List<GameObject>();

        foreach (Transform child in transform)
        {
            if (child.name == "WPCamera")
                continue;

            waypoints.Add(child.gameObject); // Will add sequentially as long as hierarchy is sequential top-to-bottom
        }

        uiManager.UpdateWaypointModeText("Sequential");
    }

    void Update()
    {
        // Switch between sequential and random
        if(Input.GetKeyDown(KeyCode.J))
        {
            SwitchModes();
        }

        // Hide waypoints
        if (Input.GetKeyDown(KeyCode.H))
        {
            ChangeVisibility();
            foreach (GameObject wp in waypoints)
            {
                wp.GetComponent<SpriteRenderer>().enabled = !wp.GetComponent<SpriteRenderer>().enabled;
                wp.GetComponent<BoxCollider2D>().enabled = !wp.GetComponent<BoxCollider2D>().enabled;
            }
        }
    }

    void SwitchModes()
    {
        if (mode == Mode.Sequential)
        {
            mode = Mode.Random;
            uiManager.UpdateWaypointModeText("Random");
        }
        else
        {
            mode = Mode.Sequential;
            uiManager.UpdateWaypointModeText("Sequential");
        }
    }

    public static GameObject GetNextDestination(GameObject currentDestination)
    {
        if(currentDestination == null) 
            return waypoints[0];

        int currentDestIndex = waypoints.IndexOf(currentDestination);

        if (mode == Mode.Sequential)
        {
            if (currentDestIndex == (waypoints.Count - 1))
                return waypoints[0];
            else
                return waypoints[currentDestIndex + 1];
        }
        else
        {
            int randomIndex = Random.Range(0, waypoints.Count);

            while(randomIndex == currentDestIndex)
            {
                randomIndex = Random.Range(0, waypoints.Count);
            }

            return waypoints[randomIndex];
        }
    }

    public static void ReconfigurePath(GameObject wp)
    {
        foreach(Enemy e in EnemySpawnManager.enemies)
        {
            if(e.currentWaypointDir == wp)
            {
                e.UpdatePath(wp);
            }
        }
    }

    public static void SetCamera(Waypoint wp)
    {
        if (camInUse)
            return;

        camInUse = true;
        uiManager.UpdateWPCamText("Active");
        uiManager.UpdateWPCamVisibility(true);
        cam.transform.position = new Vector3(wp.transform.position.x, wp.transform.position.y, -10);
    }

    public static void DisableCam()
    {
        camInUse = false;
        uiManager.UpdateWPCamText("Inactive");
        uiManager.UpdateWPCamVisibility(false);
    }

    private void ChangeVisibility()
    {
        if (visible)
        {
            visible = false;
            uiManager.UpdateWaypointVisibilityText("Hidden");
        }
        else
        {
            visible = true;
            uiManager.UpdateWaypointVisibilityText("Visible");
        }
    }
}
