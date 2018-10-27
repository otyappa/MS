using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special_Item : MonoBehaviour {
    Vector3 shot_vec;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 Move_Transform = this.transform.position;
        Move_Transform += shot_vec;
        transform.position = Move_Transform;
    }
    public void Set_vector(int vec)
    {
        Vector3 muki=Vector3.zero;
        switch (vec)
        {
            case 0:
                muki = new Vector3(0, 0, 0.15f);
                break;
            case 1:
                muki = new Vector3(0.15f, 0, 0);

                break;
            case 2:
                muki = new Vector3(0, 0, -0.15f);

                break;
            case 3:
                muki = new Vector3( -0.15f, 0, 0);
                break;
        }
        shot_vec = muki;
    }

    void OnTriggerEnter(Collider it)
    {
        Debug.Log(it.transform.tag);
        if (it.transform.tag == "Stage")
        {
            Destroy(this.gameObject);
        }
    }
}
