using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    enum Mode {Random, Sequential};

    private static List<GameObject> waypoints;
    private static Mode mode;

    void Start()
    {
        mode = Mode.Sequential;
        waypoints = new List<GameObject>();

        foreach (Transform child in transform)
        {
            waypoints.Add(child.gameObject); // Will add sequentially as long as hierarchy is sequential top-to-bottom
        }

        UIManager.UpdateWaypointModeText("Sequential");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            SwitchModes();
        }
    }

    void SwitchModes()
    {
        if (mode == Mode.Sequential)
        {
            mode = Mode.Random;
            UIManager.UpdateWaypointModeText("Random");
        }
        else
        {
            mode = Mode.Sequential;
            UIManager.UpdateWaypointModeText("Sequential");
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
}
