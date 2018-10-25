using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSystem : MonoBehaviour {

    bool GameSet=false;
    bool Blue_Win=false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();

        }
	}

    public void BlueWin()
    {
        if (GameSet == false)
        {
            GameSet = true;
            Blue_Win = true;
        }
    }
    public void RedWin()
    {
        if (GameSet == false)
        {
            GameSet = true;
        }
    }
    public bool Get_GameSet()
    {
        return GameSet;
    }

}
