using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI eggsCountText;
    [SerializeField] private TextMeshProUGUI enemyCountText;
    [SerializeField] private TextMeshProUGUI enemiesDestroyedText;
    [SerializeField] private TextMeshProUGUI heroModeText;
    [SerializeField] private TextMeshProUGUI waypointModeText;
    [SerializeField] private TextMeshProUGUI waypointVisibilityText;
    [SerializeField] private TextMeshProUGUI wpCamText;

    [SerializeField] private GameObject wpCam;

    public void UpdateDestroyedEnemiesText(int count)
    {
        enemiesDestroyedText.text = "Enemies Destroyed: " + count;
    }

    public void UpdateEnemyCountText(int count)
    {
        enemyCountText.text = "Enemy Count: " + count;
    }

    public void UpdateEggsCountText(int count)
    {
        eggsCountText.text = "Eggs Count: " + count;
    }

    public void UpdateHeroModeText(string mode)
    {
        heroModeText.text = "Hero Mode: " + mode;
    }
    
    public void UpdateWaypointModeText(string mode)
    {
        waypointModeText.text = "Waypoint Mode: " + mode;
    }

    public void UpdateWaypointVisibilityText(string visibility)
    {
        waypointVisibilityText.text = "WaypointVisibility: " + visibility;
    }

    public void UpdateWPCamVisibility(bool active)
    {
        wpCam.SetActive(active);
    }

    public void UpdateWPCamText(string active)
    {
        wpCamText.text = "Waypoint Cam: " + active;
    }
}
