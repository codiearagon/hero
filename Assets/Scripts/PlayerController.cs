using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject eggProjectile;
    [SerializeField] private Camera cam;
    [SerializeField] private float linearSpeed;
    [SerializeField] private float rotationSpeed;

    private Rigidbody2D rb;
    private bool keyboardMode = false;
    private float cooldown = 0.2f;
    private float fireRate = 0.2f;

    private GameObject currentDest;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (keyboardMode)
            UIManager.UpdateHeroModeText("Keyboard");
        else
            UIManager.UpdateHeroModeText("Mouse");
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            currentDest = WaypointManager.GetNextDestination(currentDest);
            Debug.Log(currentDest);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            keyboardMode = !keyboardMode;

            if (keyboardMode)
                UIManager.UpdateHeroModeText("Keyboard");
            else
                UIManager.UpdateHeroModeText("Mouse");
        }

        if (Input.GetKey(KeyCode.Space) && Time.time > cooldown)
        {
            Debug.Log("Shoot!");
            Instantiate(eggProjectile, transform.position, transform.rotation);
            cooldown = Time.time + fireRate;
        }
    }

    private void FixedUpdate()
    {
        if (keyboardMode)
        {
            KeyboardMovement();
        }
        else
        {
            MouseMovement();
        }
    }

    // Handles keyboard mode movement
    void KeyboardMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");


        transform.Translate(Vector2.up * vertical * linearSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, 0, 1) * -horizontal * rotationSpeed * Time.deltaTime);
    }

    // Handles mouse mode movement
    void MouseMovement()
    {
        transform.position = MousePosToWorld();
    }

    // Converts mouse screen position to world position while keeping z coordinate constant
    Vector2 MousePosToWorld()
    {
        return new Vector2(cam.ScreenToWorldPoint(Input.mousePosition).x, cam.ScreenToWorldPoint(Input.mousePosition).y);
    }
}
