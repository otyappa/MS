using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hint : MonoBehaviour {
   public float Translucent_time=100;
    // Use this for initialization
    void Start () {
        Set_false();
	}
	
	// Update is called once per frame
	void Update () {
        if (Translucent_time < 2)
        {
            Translucent_time += Time.deltaTime;
            if (Translucent_time >= 2)
            {
                Set_false();
            }
        }
	}

    public void Set_Active()
    {
        gameObject.SetActive(true);
        Translucent_time = 0;
    }

    void Set_false()
    {
        gameObject.SetActive(false);
    }
}
