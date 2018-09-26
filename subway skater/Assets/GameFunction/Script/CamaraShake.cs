using UnityEngine;

public class CamaraShake : MonoBehaviour
{
    public Transform camTransform;
    public static float shakeDuration = 0f;
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition += Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
        }
    }
}

