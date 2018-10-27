using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePadManager : MonoBehaviour {
    public int[] gamepadnum=new int[4];
    int padnum =0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void Set_GamePad(int num)
    {
        if (padnum < 4)
        {
            bool check = false;
            for (int i = 0; i < padnum; i++)
            {
                if (gamepadnum[i] == num)
                {
                    check = true;
                }

            }
            if (!check)
            {
                gamepadnum[padnum] = num;
                padnum++;
            }
        }
    }
}
