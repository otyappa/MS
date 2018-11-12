using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Couple_Bar : MonoBehaviour {
    public Slider BlueSliderManager1;
    public Slider BlueSliderManager2;
    public Slider RedSliderManager1;
    public Slider RedSliderManager2;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

   public  void SetSlider(float x,bool Red)
    {
        if (Red)
        {
            RedSliderManager1.value = x / 21.0f;
            RedSliderManager2.value = x / 21.0f;
        }
        else
        {
            BlueSliderManager1.value = 1-(x / 21.0f);
            BlueSliderManager2.value = 1-(x/ 21.0f);

        }
    }

    public int RedWin()
    {
        if (RedSliderManager1.value == BlueSliderManager1.value)
        {
            return 2;
        }
        if (RedSliderManager1.value < BlueSliderManager1.value)
        {
            return 0;
        }
        else
        {
            return 1;
        }
    }
}
