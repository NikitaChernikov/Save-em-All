using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 10f;
    [SerializeField] private Vector3 offset;

    void FixedUpdate()
    {
        Vector3 desiredPosition = target.transform.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);
    }
}
