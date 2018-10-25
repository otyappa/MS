using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Create_SpecialItem : MonoBehaviour {
    public Vector3 Create_Pos1;
    public Vector3 Create_Pos2;
    public GameObject Item;
   public  int Create_Count=0;
   public  int Re_Create = 0;
    // Use this for initialization
    void Start () {
        Random.seed = (int)Time.time;
	}

    // Update is called once per frame
    void Update() {
        if (Create_Count == 0)
        {
            if (Re_Create > 180)
            {
                if (Random.value % 2 == 0)
                {
                    GameObject shot = Instantiate(Item, Create_Pos1, Quaternion.identity);
                    Create_Count++;
                }
                else
                {
                    GameObject shot = Instantiate(Item, Create_Pos2, Quaternion.identity);
                    Create_Count++;
                }
            }
            Re_Create++;
        }
    }
    public void Delete_Item()
    {
        if(Create_Count>0)
        Create_Count--;
        Re_Create = 0;
    }
}
