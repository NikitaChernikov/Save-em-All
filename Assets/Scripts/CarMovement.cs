using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private FixedJoystick joystick;
    [SerializeField] private float moveSpeed;

    public static bool PointerDown = false;

    private Rigidbody rb;
    private float verticalDirection;
    private float horizontalDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        verticalDirection = -joystick.Horizontal * moveSpeed;
        horizontalDirection = -joystick.Vertical * moveSpeed;

        //rotation
        float hAxis = -verticalDirection;
        float vAxis = -horizontalDirection;
        float yAxis = Mathf.Atan2(hAxis, vAxis) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0f, yAxis, 0f);
    }

    private void FixedUpdate()
    {
        if (PointerDown)
        { 
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 0.01f);
        }
        else
        {
            rb.velocity = Vector3.Lerp(rb.velocity, new Vector3(verticalDirection, 0f, horizontalDirection), Time.deltaTime);
            //rb.MovePosition(rb.position + new Vector3(verticalDirection, 0f, horizontalDirection) * Time.deltaTime);
        }
    }
}