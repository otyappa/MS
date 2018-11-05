using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Timer : MonoBehaviour {
    Text TextTime;
    public float max_time;
    float now_time;
	// Use this for initialization
	void Start () {
        now_time = 0;
        TextTime = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        now_time += Time.deltaTime;
        int time=(int)max_time-(int)now_time;
        TextTime.text = time.ToString();
	}
}
