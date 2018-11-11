using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour {
    public Text Blue_TextTime;
    public Text Red_TextTime;
    public float max_time;
    float now_time;
    GameSystem GameSystemManager;
	// Use this for initialization
	void Start () {
        now_time = 0;
        GameSystemManager = GameObject.Find("GameSystemManager").GetComponent<GameSystem>();

    }

    // Update is called once per frame
    void Update () {
        if(!GameSystemManager.Get_GameSet())
        now_time += Time.deltaTime;
        int time=(int)max_time-(int)now_time;
        Blue_TextTime.text = time.ToString();
        Red_TextTime.text = time.ToString();

        if (time == 0)
        {
            GameSystemManager.TimeUp();
                
        }
	}
}
