using UnityEngine;

public class deleteprefs : MonoBehaviour
{
	public void borrar()
    {
        PlayerPrefs.DeleteAll();
    }
}
