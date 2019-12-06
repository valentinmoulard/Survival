using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        transform.LookAt(target);
        transform.RotateAround(target.position, Vector3.up, Input.GetAxis("Mouse X") * 2);
        transform.RotateAround(target.position, Vector3.right, Input.GetAxis("Mouse Y") * -2);
    }
}
