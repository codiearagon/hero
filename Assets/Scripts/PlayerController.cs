using System.Collections;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI heroModeText;
    [SerializeField] private GameObject eggProjectile;
    [SerializeField] private Camera cam;
    [SerializeField] private float linearSpeed = 3.0f;
    [SerializeField] private float rotationSpeed = 240.0f;

    private Rigidbody2D rb;
    private bool keyboardMode = false;
    private bool firing = false;
    private bool cooldown = false;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (keyboardMode)
            heroModeText.text = "Hero Mode: Keyboard";
        else
            heroModeText.text = "Hero Mode: Mouse";
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            keyboardMode = !keyboardMode;

            if (keyboardMode)
                heroModeText.text = "Hero Mode: Keyboard";
            else
                heroModeText.text = "Hero Mode: Mouse";
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (!cooldown && !firing)
            {
                firing = true;
                StartCoroutine(FireEggs());
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            firing = false;
            StopCoroutine(FireEggs());

            if (!cooldown)
                StartCoroutine(SpamCooldown());
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

    IEnumerator FireEggs()
    {
        while (firing)
        {
            Instantiate(eggProjectile, transform.position, transform.rotation);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator SpamCooldown()
    {
        cooldown = true;
        yield return new WaitForSeconds(0.18f);
        cooldown = false;
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
