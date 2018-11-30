using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TimeCtl : MonoBehaviour {
    public GameObject SliderUI;
    public Vector3 test;
    float count;
    float wari;
    public Vector3 UIScale;
	// Use this for initialization
	void Start () {
        UIScale = SliderUI.transform.lossyScale;
       // UIScale = new Vector3(0, 0, 0);
        Reset();
	}
	
	// Update is called once per frame
	void Update () {
        test = SliderUI.transform.lossyScale;
	}

    public void Set_Pasent(float _set)
    {
        wari = _set;
    }

    public void Bar_Update()
    {
        SliderUI.transform.localScale = new Vector3(count / wari * 1.95f, UIScale.y, UIScale.z);
        SliderUI.transform.localPosition = new Vector3(-2.5f+(count / wari * 2.5f), 0, -0.01f);

        count += Time.deltaTime;
    }
    public void Reset()
    {
        wari = 0;
        count = 0;
        SliderUI.transform.localPosition = new Vector3(-2.5f, 0, -0.01f);
        SliderUI.transform.localScale = UIScale;
    }
}
