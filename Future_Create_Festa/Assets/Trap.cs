using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour {
    [SerializeField]GameObject Creater;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Delete_Function()
    {
        Creater.GetComponent<PL>().Trap_Recv();

    }
    public void Set_Creater(GameObject parent)
    {
        Creater = parent;
    }
}
