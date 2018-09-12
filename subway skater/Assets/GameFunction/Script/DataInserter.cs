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
        new WWW("https://corretin.000webhostapp.com/InsertCodes.php");
    }
}
