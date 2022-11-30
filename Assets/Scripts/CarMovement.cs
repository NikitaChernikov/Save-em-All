using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private float moveSpeed;

    public static bool PointerDown = false;

    private Rigidbody rb;
    private float verticalDirection;
    private float horizontalDirection;

    //engine sound
    private AudioSource engineSound;
    private float pitch;

    private void Awake()
    {
        engineSound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, -0.23f, transform.position.z);
        verticalDirection = -joystick.Horizontal * moveSpeed;
        horizontalDirection = -joystick.Vertical * moveSpeed;
        pitch = Mathf.Max(Mathf.Lerp(0.6f, 2f, Mathf.Abs(joystick.Vertical)), Mathf.Lerp(0.6f, 2f, Mathf.Abs(joystick.Horizontal)));
        engineSound.pitch = Mathf.Lerp(engineSound.pitch, pitch, 0.01f);

        //rotation
        float hAxis = -verticalDirection;
        float vAxis = -horizontalDirection;
        float yAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f, yAxis, 0f);
    }

    public float GetSpeed()
    {
        return rb.velocity.magnitude;
    }

    private void FixedUpdate()
    {
        if (PointerDown)
        { 
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 0.02f);
        }
        else
        {
            rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(verticalDirection, 0f, horizontalDirection), Time.deltaTime);
            //rb.MovePosition(rb.position + new Vector3(verticalDirection, 0f, horizontalDirection) * Time.deltaTime);
        }
    }
}