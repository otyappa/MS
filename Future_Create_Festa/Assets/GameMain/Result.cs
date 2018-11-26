using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result : MonoBehaviour {
    GameSystem End_Manager;
    TotalGameManager TotalManager;
    public bool Red=false;
    GameObject Blue_Win_IMage;
    GameObject Red_Win_Image;
    GameObject hert1;
    GameObject hert2;
    GameObject hert3;
    GameObject hert4;
    // Use this for initialization
    void Start () {
        Sound.LoadBgm("Result", "BGM/Result");
        End_Manager = GameObject.Find("GameSystemManager").GetComponent<GameSystem>();
      TotalManager = GameObject.Find("TotalManager").GetComponent<TotalGameManager>();
        Red_Win_Image = transform.GetChild(0).gameObject;
        Blue_Win_IMage = transform.GetChild(1).gameObject;
        hert1 = transform.GetChild(9).gameObject;
        hert2 = transform.GetChild(10).gameObject;
        hert3 = transform.GetChild(11).gameObject;
        hert4 = transform.GetChild(12).gameObject;
        Red_Win_Image.SetActive(false);
        Blue_Win_IMage.SetActive(false);
        hert1.SetActive(false);
        hert2.SetActive(false);
        hert3.SetActive(false);
        hert4.SetActive(false);
        Set_Win(TotalManager.Red_WinCount, TotalManager.Blue_WinCount);
    }

    // Update is called once per frame
    void Update () {
        if (End_Manager.Get_GameSet())
        {
            if (Red == !End_Manager.Get_WINNER())
            {
                if (Red_Win_Image.activeSelf == false)
                {
                    Red_Win_Image.SetActive(true);
                    Set_Win(TotalManager.Red_WinCount, TotalManager.Blue_WinCount);
                }
            }
            else if (Blue_Win_IMage.activeSelf == false)
            {
                Blue_Win_IMage.SetActive(true);
                Set_Win(TotalManager.Red_WinCount, TotalManager.Blue_WinCount);
            }
        }
    }

    public void Set_Win(int red,int blue)
    {
        if (red > 1)
        {
            hert1.SetActive(true);
            hert2.SetActive(true);
            Sound.StopBgm();
            Sound.PlayBgm("Result");
        }
        else if(red >0)
        {
            hert1.SetActive(true);

        }
        if (blue > 1)
        {
            hert3.SetActive(true);
            hert4.SetActive(true);
            Sound.StopBgm();
            Sound.PlayBgm("Result");
        }
        else if (blue > 0)
        {
            hert3.SetActive(true);

        }
    }
}
