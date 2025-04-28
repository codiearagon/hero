using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private GameObject eggProjectile;
    [SerializeField] private Camera cam;
    [SerializeField] private float linearSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Slider cdSlider;

    private Rigidbody2D rb;
    private bool keyboardMode = false;
    private float cooldown = 0.2f;
    private float fireRate = 0.2f;

    private GameObject currentDest;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        cdSlider.value = cdSlider.maxValue;

        if (keyboardMode)
            uiManager.UpdateHeroModeText("Keyboard");
        else
            uiManager.UpdateHeroModeText("Mouse");
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
                uiManager.UpdateHeroModeText("Keyboard");
            else
                uiManager.UpdateHeroModeText("Mouse");
        }

        if (Input.GetKey(KeyCode.Space) && Time.time > cooldown)
        {
            GameObject egg = Instantiate(eggProjectile, transform.position, transform.rotation);
            egg.GetComponent<Projectile>().SetUIManager(uiManager);
            cooldown = Time.time + fireRate;
            cdSlider.minValue = Time.time;
            cdSlider.value = cdSlider.minValue;
            cdSlider.maxValue = cooldown;
            StartCoroutine(CooldownBar());
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

    IEnumerator CooldownBar()
    {
        while (cdSlider.value < cdSlider.maxValue)
        {
            cdSlider.value = Time.time;
            yield return null;
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
