using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeCtl : MonoBehaviour {
    public GameObject SliderUI;
    Slider SliderManager;
    float count;
    float wari;
	// Use this for initialization
	void Start () {
        SliderManager = SliderUI.GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Set_Pasent(float _set)
    {
        wari = _set;
    }

    public void Bar_Update()
    {
        SliderManager.value = count / wari;
        count++;
    }
    public void Reset()
    {
        wari = 0;
        count = 0;
        SliderManager.value = 0;
    }
}
