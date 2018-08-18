using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataInserter : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateUser();
        }  
    }

    public void CreateUser()
    {
        WWW itemsData = new WWW("http://localhost/Corre_Tin/InsertCodes.php");
    }
}
