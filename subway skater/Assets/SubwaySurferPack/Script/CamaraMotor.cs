using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMotor : MonoBehaviour
{
    public Transform lookAt; //Tin
    public Vector3 offset = new Vector3(0, 5.0f, -10.0f);
    public Vector3 rotation = new Vector3(35, 0, 0);

    public bool IsMoving { set; get; }

    private void LateUpdate()
    {
        if (!IsMoving)
        {
            return;
        }
        
        Vector3 desirePosition = lookAt.position + offset;
        desirePosition.x = 0;
        transform.position = Vector3.Lerp(transform.position,desirePosition,Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), 0.1f);
    }
}
