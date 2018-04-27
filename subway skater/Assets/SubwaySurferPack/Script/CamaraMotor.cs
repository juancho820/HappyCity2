using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMotor : MonoBehaviour
{
    public Transform lookAt; //Tin
    public Vector3 offset = new Vector3(0, 5.4f, -0.5f);
    public Vector3 offset2 = new Vector3(0, 3.5f, -0.5f);
    public Vector3 offset3 = new Vector3(0, 7, -0.5f);
    public Vector3 rotation = new Vector3(35, 0, 0);
    public static bool agachar = false;
    public static bool subir = false;

    public bool IsMoving { set; get; }

    private void LateUpdate()
    {
        if (!IsMoving)
        {
            return;
        }
        
        if(agachar == true)
        {
            Vector3 desirePosition = lookAt.position + offset2;
            desirePosition.x = lookAt.transform.position.x;
            transform.position = Vector3.Lerp(transform.position, desirePosition, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), 0.1f);
            StartCoroutine(Resetiar());
        }
        if(subir == true)
        {
            Vector3 desirePosition = lookAt.position + offset3;
            desirePosition.x = lookAt.transform.position.x;
            transform.position = Vector3.Lerp(transform.position, desirePosition, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), 0.1f);
            StartCoroutine(Resetiar());
        }
        else
        {
            Vector3 desirePosition = lookAt.position + offset;
            desirePosition.x = lookAt.transform.position.x;
            transform.position = Vector3.Lerp(transform.position, desirePosition, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), 0.1f);
        }
    }
    private IEnumerator Resetiar()
    {
        yield return new WaitForSeconds(1.2f);
        agachar = false;
        subir = false;
    }
}
