﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSystem : MonoBehaviour {

    bool GameSet = false;
    bool Blue_Win = false;
    public GameObject blue_1;
    public GameObject red_1;
    public GameObject blue_2;
    public GameObject red_2;
    Couple_Bar TimeUpWinner;
    SceneTransitionManager scene_mana;
    bool two_on_two;
    TotalGameManager TotalManager;
    public GameObject Stage1;
    public GameObject Stage2;
    public GameObject Stage3;
    public GameObject Stage4;

    // Use this for initialization

        void Awake()
    {
        Init();
    }


    void Start() {
     //   Init();
        TotalManager = GameObject.Find("TotalManager").GetComponent<TotalGameManager>();
    }

    // Update is called once per frame
    void Update() {

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
            TotalManager.AddWin(false);
        }
    }
    public void RedWin()
    {
        if (GameSet == false)
        {
            GameSet = true;
            TotalManager.AddWin(true);
        }
    }

    void Draw()
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

    public void TimeUp()
    {
        switch (TimeUpWinner.RedWin())
        {
            case 0:
                RedWin();
                break;
            case 1:
                BlueWin();
                break;
            case 2:
                Draw();
                break;
        }
    }

    void Init()
    {
        scene_mana = GameObject.Find("SceneTransitionManager").GetComponent<SceneTransitionManager>();
        blue_1 = GameObject.Find("Blue_Player");
        blue_2 = GameObject.Find("Blue_Player2");
        red_1 = GameObject.Find("Red_Player");
        red_2 = GameObject.Find("Red_Player2");
        switch ((int)scene_mana.choseStage)
        {
            case 1:
                blue_1.GetComponent<PL>().SetUp(6, 12);
                blue_2.GetComponent<PL>().SetUp(2, 12);
                red_1.GetComponent<PL>().SetUp(6, 2);
                red_2.GetComponent<PL>().SetUp(2, 2);
                GameObject.Find("Red_Couple").GetComponent<Couple>().SetUp(4,0);
                GameObject.Find("Blue_Couple").GetComponent<Couple>().SetUp(4, 14);
               GameObject stage = Instantiate(Stage1, new Vector3(0,0,-4.5f), Quaternion.identity);
                stage.name = "Stage";

                break;
            case 2:
                blue_1.GetComponent<PL>().SetUp(6, 12);
                blue_2.GetComponent<PL>().SetUp(2, 12);
                red_1.GetComponent<PL>().SetUp(6, 2);
                red_2.GetComponent<PL>().SetUp(2, 2);
                GameObject.Find("Red_Couple").GetComponent<Couple>().SetUp(4, 0);
                GameObject.Find("Blue_Couple").GetComponent<Couple>().SetUp(4, 14);
                GameObject stage2 = Instantiate(Stage2, new Vector3(0, 0, -4.5f), Quaternion.identity);
                stage2.name = "Stage";
                break;
            case 3:
                blue_1.GetComponent<PL>().SetUp(6, 12);
                blue_2.GetComponent<PL>().SetUp(2, 12);
                red_1.GetComponent<PL>().SetUp(6, 2);
                red_2.GetComponent<PL>().SetUp(2, 2);
                GameObject.Find("Red_Couple").GetComponent<Couple>().SetUp(4, 0);
                GameObject.Find("Blue_Couple").GetComponent<Couple>().SetUp(4, 14);
                GameObject stage3 = Instantiate(Stage3, new Vector3(0, 0, -4.5f), Quaternion.identity);
                stage3.name = "Stage";
                break;
            case 4:
                blue_1.GetComponent<PL>().SetUp(6, 12);
                blue_2.GetComponent<PL>().SetUp(2, 12);
                red_1.GetComponent<PL>().SetUp(6, 2);
                red_2.GetComponent<PL>().SetUp(2, 2);
                GameObject.Find("Red_Couple").GetComponent<Couple>().SetUp(4, 0);
                GameObject.Find("Blue_Couple").GetComponent<Couple>().SetUp(4, 14);
                GameObject stage4 = Instantiate(Stage4, new Vector3(0, 0, -4.5f), Quaternion.identity);
                stage4.name = "Stage";
                break;                
                   
            default:
                blue_1.GetComponent<PL>().SetUp(6, 12);
                blue_2.GetComponent<PL>().SetUp(2, 12);
                red_1.GetComponent<PL>().SetUp(6, 2);
                red_2.GetComponent<PL>().SetUp(2, 2);
                GameObject.Find("Red_Couple").GetComponent<Couple>().SetUp(4, 0);
                GameObject.Find("Blue_Couple").GetComponent<Couple>().SetUp(4, 14);
                break;

        }


        TimeUpWinner = GetComponent<Couple_Bar>();
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
}
