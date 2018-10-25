using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_SpecialItem : MonoBehaviour {
    public Vector3 Create_Pos1;
    public Vector3 Create_Pos2;
    public Vector3[] Create_Pos3;
    public GameObject Item;
   public  int Create_Count=0;
   public  int Re_Create = 0;
    int Re_Create_Time = 180;
    // Use this for initialization
    void Start () {
	}

    // Update is called once per frame
    void Update() {
        if (Create_Count == 0)
        {
            if (Re_Create > Re_Create_Time)
            {
                
                GameObject shot = Instantiate(Item, Create_Pos3[Random.Range(0, Create_Pos3.Length) ], Quaternion.identity);
                Create_Count++;
                Re_Create_Time = Random.Range(60, 180);
            }
            else
            {
                Re_Create++;
            }
        }
    }
    public void Delete_Item()
    {
        if(Create_Count>0)
        Create_Count--;
        Re_Create = 0;
    }
}
