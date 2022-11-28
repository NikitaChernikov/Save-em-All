using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform[] positions;

    int counter = 0;

    private void Update()
    {
        if (counter < positions.Length)
        {
            mainCamera.position = Vector3.Lerp(mainCamera.position, positions[counter].position, 0.005f);
            mainCamera.rotation = Quaternion.Lerp(mainCamera.rotation, positions[counter].rotation, 0.005f);
            if (Vector3.Distance(mainCamera.position, positions[counter].position) < 10f)
            {
                counter++;
            }
        }
        else
        {
            counter = 0;
        }
    }

}
