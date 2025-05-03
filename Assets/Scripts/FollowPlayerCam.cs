using UnityEngine;

public class FollowPlayerCam : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private float speed = 50.0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);
    }
}
