using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSystem : MonoBehaviour {

    bool GameSet=false;
    bool Blue_Win=false;
    public GameObject blue_1;
    public GameObject red_1;
    public GameObject blue_2;
    public GameObject red_2;
    SceneTransitionManager scene_mana;
    bool two_on_two;
	// Use this for initialization
	void Start () {
        scene_mana = GameObject.Find("SceneTransitionManager").GetComponent<SceneTransitionManager>();
        if ((int)scene_mana.choseMode == 0)
        {
            blue_2.gameObject.SetActive(false);
            red_2.gameObject.SetActive(false);
            blue_1.GetComponent<PL>().Set_MaxTrap(3);
            red_1.GetComponent<PL>().Set_MaxTrap(3);
        }
        else
        {

            blue_2.GetComponent<PL>().Set_MaxTrap(2);
            red_2.GetComponent<PL>().Set_MaxTrap(2);
            blue_1.GetComponent<PL>().Set_MaxTrap(2);
            red_1.GetComponent<PL>().Set_MaxTrap(2);
        }
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
        if (Input.GetKey(KeyCode.F1))
        {
            scene_mana.choseMode = SceneTransitionManager.ModeType.OneToOne;
            SceneManager.LoadScene("Main");
        }
        if (Input.GetKey(KeyCode.F2))
        {
            scene_mana.choseMode = SceneTransitionManager.ModeType.TwoToTwo;
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
