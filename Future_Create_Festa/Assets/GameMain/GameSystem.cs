using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSystem : MonoBehaviour {

    bool GameSet=false;
    bool Blue_Win=false;
    GameObject blue_2;
    GameObject red_2;
    bool two_on_two;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.F12))
        {
            SceneManager.LoadScene("Main");
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
    public bool Get_WINNER()
    {
        return Blue_Win;
    }

}
