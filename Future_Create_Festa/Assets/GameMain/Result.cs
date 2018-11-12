using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour {
    GameSystem End_Manager;
    public bool Red=false;

	// Use this for initialization
	void Start () {
        End_Manager = GameObject.Find("GameSystemManager").GetComponent<GameSystem>();
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(5).gameObject.SetActive(false);
        transform.GetChild(6).gameObject.SetActive(false);
        transform.GetChild(7).gameObject.SetActive(false);
        transform.GetChild(8).gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (End_Manager.Get_GameSet())
        {
            if (Red == !End_Manager.Get_WINNER())
            {
                transform.GetChild(0).gameObject.SetActive(true);

            }
            else
            {  
                transform.GetChild(1).gameObject.SetActive(true);
            }
        }
    }

    public void Set_Win(int red,int blue)
    {
        if (red > 1)
        {
            transform.GetChild(5).gameObject.SetActive(true);
            transform.GetChild(6).gameObject.SetActive(true);
        }
        else if(red >0)
        {
            transform.GetChild(5).gameObject.SetActive(true);

        }
        if (blue > 1)
        {
            transform.GetChild(7).gameObject.SetActive(true);
            transform.GetChild(8).gameObject.SetActive(true);
        }
        else if (blue > 0)
        {
            transform.GetChild(7).gameObject.SetActive(true);

        }
    }
}
