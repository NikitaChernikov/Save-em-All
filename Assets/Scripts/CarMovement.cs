using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] private List<AxleInfo> axleInfos;
    //[SerializeField] private Joystick joystick;
    [SerializeField] private float maxMotorTorque = 1000;
    [SerializeField] private float maxSteeringAngle = 110;
    [SerializeField] private float breakForce = 3000;
    [SerializeField] private TrailRenderer leftTrail;
    [SerializeField] private TrailRenderer rightTrail;
    [SerializeField] private Light[] lights;
    [SerializeField] private Vector3 centerOfMass;

    private bool turnLeft;
    private bool turnRight;
    private Rigidbody rb;
    private float motor;
    private float steering;
    private Transform visualWheel;
    Vector3 position;
    Quaternion rotation;
    private bool isBreak;
    private float pitch;
    private AudioSource motorSound;

    private void Awake()
    {
        motorSound = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.centerOfMass = centerOfMass;
    }

    public void BrakeOn()
    {
        isBreak = true;
    }

    public void BrakeOff()
    {
        isBreak = false;
    }

    private void VisualWheels(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }
        visualWheel = collider.transform.GetChild(0);
        collider.GetWorldPose(out position, out rotation);
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public float GetSpeed()
    {
        return rb.velocity.magnitude;
    }

    private void FixedUpdate()
    {
        //pitch = Mathf.Lerp(0.6f, 1.6f, joystick.Vertical);
        //motorSound.pitch = Mathf.Lerp(GetComponent<AudioSource>().pitch, pitch, 0.01f);

        //motor = maxMotorTorque * joystick.Vertical * 1.5f; //ускорение
        //steering = maxSteeringAngle * joystick.Horizontal; //угол поворота
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = -motor; 
                axleInfo.rightWheel.motorTorque = -motor; 
            }
            if (!isBreak)
            {
                axleInfo.leftWheel.brakeTorque = 0;
                axleInfo.rightWheel.brakeTorque = 0;
                leftTrail.emitting = false;
                rightTrail.emitting = false;
                foreach (Light light in lights)
                {
                    light.enabled = false;
                }
            }
            else
            {
                axleInfo.leftWheel.brakeTorque = breakForce;
                axleInfo.rightWheel.brakeTorque = breakForce;
                leftTrail.emitting = true;
                rightTrail.emitting = true;
                foreach (Light light in lights)
                {
                    light.enabled = true;
                }
            }
            VisualWheels(axleInfo.leftWheel);
            VisualWheels(axleInfo.rightWheel);
        }
    }

    public void MoveForward()
    {
        motor = maxMotorTorque;
    }
    public void DontMoveForward()
    {
        motor = 0;
    }

    public void MoveBackwords()
    {
        motor = -maxMotorTorque;
    }

    public void DontMoveBackwords()
    {
        motor = 0;
    }

    public void TurnLeft()
    {
        steering = -maxSteeringAngle;
    }

    public void TurnRight()
    {
        steering = maxSteeringAngle;
    }

    public void DontTurn()
    {
        steering = 0;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.CompareTag("Fence") || collision.transform.CompareTag("Building"))
    //    {
    //        rb.AddForce(-transform.position * 100, ForceMode.Impulse);
    //    }
    //}
}

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;  
    public WheelCollider rightWheel; 
    public bool motor; // присоединено ли колесо к мотору?
    public bool steering; // поворачивает ли это колесо?
}
