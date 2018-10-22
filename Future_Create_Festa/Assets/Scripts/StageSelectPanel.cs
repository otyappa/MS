using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectPanel : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.rotation = GameObject.Find("LookTransBaseObj").transform.rotation;

	}
}
