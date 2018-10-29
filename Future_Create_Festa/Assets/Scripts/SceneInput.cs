using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneInput : SceneTransitionManager {

    [Tooltip("回転速度")]
    public float rotSpeed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        switch (NowScene)
        {
            case SceneType.Title:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    isTransition = true;
                }

                break;

            case SceneType.ModeSelect:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    isTransition = true;
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {

                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {

                }

                break;

            case SceneType.StageSelect:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    isTransition = true;
                }

                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {

                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {

                }

                break;

            case SceneType.Main:
                break;

        }


    }
}
