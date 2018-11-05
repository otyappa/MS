using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{

    [Tooltip("注視点オブジェクト")]
    public GameObject lookObj;
    
    void Update()
    {

        if (lookObj == null)
        {
            lookObj = GameObject.Find("TitleCamera");
        }
        else
        {
            this.transform.LookAt(lookObj.transform);
        }


    }
}