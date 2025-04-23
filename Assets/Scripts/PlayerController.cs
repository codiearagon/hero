using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject eggProjectile;
    [SerializeField] private Camera cam;
    [SerializeField] private float linearSpeed = 3.0f;
    [SerializeField] private float rotationSpeed = 240.0f;
    [SerializeField] private float fireRate = 0.2f;

    private Rigidbody2D rb;
    private bool keyboardMode = false;
    private float cooldown = 0.2f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (keyboardMode)
            UIManager.updateHeroModeText("Keyboard");
        else
            UIManager.updateHeroModeText("Mouse");
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            keyboardMode = !keyboardMode;

            if (keyboardMode)
                UIManager.updateHeroModeText("Keyboard");
            else
                UIManager.updateHeroModeText("Mouse");
        }

        if (Input.GetKey(KeyCode.Space) && Time.time > cooldown)
        {
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
