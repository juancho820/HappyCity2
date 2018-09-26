using System.Collections;
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
    public static bool agacharTren = false;
    public static bool subirTren = false;
    public bool normal = false;

    public bool IsMoving { set; get; }

    public void Start()
    {
        agachar = false;
        subir = false;
        agacharTren = false;
        subirTren = false;
    }

    private void LateUpdate()
    {
        if (!IsMoving)
        {
            return;
        }
        if (agachar == true)
        {
            Vector3 desirePosition = lookAt.position + offset2;
            desirePosition.x = lookAt.transform.position.x;
            transform.position = Vector3.Lerp(transform.position, desirePosition, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), 0.1f);
            StartCoroutine(Resetiar());
        }
        if (subir == true)
        {
            Vector3 desirePosition = lookAt.position + offset3;
            desirePosition.x = lookAt.transform.position.x;
            transform.position = Vector3.Lerp(transform.position, desirePosition, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), 0.1f);
            StartCoroutine(Resetiar());
        }
        if (agacharTren == true)
        {
            Debug.Log("Bajada");
            Vector3 desirePosition = lookAt.position + offset2;
            desirePosition.x = lookAt.transform.position.x;
            transform.position = Vector3.Lerp(transform.position, desirePosition, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), 0.1f);
            //StartCoroutine(Resetiar());
        }
        if(subirTren == true)
        {
            agacharTren = false;
            normal = true;
            StartCoroutine(Resetiar());
        }
        if(normal == true)
        {
            Vector3 desirePosition = lookAt.position + offset;
            desirePosition.x = lookAt.transform.position.x;
            transform.position = Vector3.Lerp(transform.position, desirePosition, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), 0.1f);
        }
        else
        {
            Vector3 desirePosition = lookAt.position + offset;
            desirePosition.x = lookAt.transform.position.x;
            transform.position = Vector3.Lerp(transform.position, desirePosition, 0.1f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(rotation), 0.1f);
        }
    }

    public void Cinematic()
    {
        if(PlayerPrefs.GetInt("ReplayTuto") == 0)
        {
            GameManagerTutorial.cinematica = false;
            GameManagerTutorial.Instance.tutorialCards.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("ReplayTuto", 0);
        }
    }
    private IEnumerator Resetiar()
    {
        yield return new WaitForSeconds(1.2f);
        agachar = false;
        subir = false;
        agacharTren = false;
        subirTren = false;
        normal = false;
    }
}
