//Author: Richard Pieterse
//Date: 16 May 2013
//Email: Merrik44@live.com

using UnityEngine;
using System.Collections;
using GamepadInput;

public class DemoScript : MonoBehaviour
{


    void Examples()
    {
        GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.One);
        GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One);
        GamePad.GetTrigger(GamePad.Trigger.RightTrigger, GamePad.Index.One);

        GamepadState state = GamePad.GetState(GamePad.Index.One);

        print("A: " + state.A);
 
        //if()

    }

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 20, Screen.width, Screen.height));

        GUILayout.BeginHorizontal();

        GUILayout.FlexibleSpace();

        DrawLabels();

        for (int i = 0; i < 5; i++)
            DrawState((GamePad.Index)i);
        GUILayout.FlexibleSpace();

        GUILayout.EndHorizontal();

        GUILayout.EndArea();
        var playerNo = GamePad.Index.One;
        Debug.Log(playerNo);
        //var keyState = GamePad.GetState(playerNo, false);
        if (Input.GetKey(KeyCode.Joystick2Button0))
        {
            Debug.Log("D");
        }
        if (Input.GetKey(KeyCode.Joystick2Button1))
        {
            Debug.Log("x");
        }
        if (Input.GetKey(KeyCode.Joystick2Button2))
        {
            Debug.Log("o");
        }
        if (Input.GetKey(KeyCode.Joystick2Button3))
        {
            Debug.Log("A");
        }
        if (Input.GetButtonDown("Square_1"))
        {
            Debug.Log("1");
        }
        if (Input.GetButtonDown("Square_2"))
        {
            Debug.Log("2");
        }
        if (Input.GetButtonDown("Square_3"))
        {
            Debug.Log("3");
        }
        if (Input.GetButtonDown("Square"))
        {
            Debug.Log("4");
        }



    }


    void DrawState(GamePad.Index controller)
    {
        GUILayout.Space(45);

        GUILayout.BeginVertical();
        
        
        GamepadState state = GamePad.GetState(controller);

        // buttons
        GUILayout.Label("Gamepad " + controller);
        GUILayout.Label("" + state.A);
        GUILayout.Label("" + state.B);
        GUILayout.Label("" + state.X);
        GUILayout.Label("" + state.Y);
        GUILayout.Label("" + state.Start);
        GUILayout.Label("" + state.Back);
        GUILayout.Label("" + state.LeftShoulder);
        GUILayout.Label("" + state.RightShoulder);
        GUILayout.Label("" + state.Left);
        GUILayout.Label("" + state.Right);
        GUILayout.Label("" + state.Up);
        GUILayout.Label("" + state.Down);
        GUILayout.Label("" + state.LeftStick);
        GUILayout.Label("" + state.RightStick);

        GUILayout.Label("");

        // triggers
        GUILayout.Label("" + System.Math.Round(state.LeftTrigger, 2));
        GUILayout.Label("" +  System.Math.Round(state.RightTrigger, 2));

        GUILayout.Label("");

        // Axes
        GUILayout.Label("" + state.LeftStickAxis);
        GUILayout.Label("" + state.rightStickAxis);
        GUILayout.Label("" + state.dPadAxis);
        

        //GUILayout.EndArea();
        GUILayout.EndVertical();

    }

    void DrawLabels()
    {
        //GUILayout.BeginArea(new Rect(30, 0, width - 30, Screen.height));

        GUILayout.BeginVertical();
        // buttons
        GUILayout.Label(" ", GUILayout.Width(80));
        GUILayout.Label("A");
        GUILayout.Label("B");
        GUILayout.Label("X");
        GUILayout.Label("Y");
        GUILayout.Label("Start");
        GUILayout.Label("Back");
        GUILayout.Label("Left Shoulder");
        GUILayout.Label("Right Shoulder");
        GUILayout.Label("Left");
        GUILayout.Label("Right");
        GUILayout.Label("Up");
        GUILayout.Label("Down");
        GUILayout.Label("LeftStick");
        GUILayout.Label("RightStick");

        GUILayout.Label("");

        GUILayout.Label("LeftTrigger");
        GUILayout.Label("RightTrigger");

        GUILayout.Label("");

        GUILayout.Label("LeftStickAxis");
        GUILayout.Label("rightStickAxis");
        GUILayout.Label("dPadAxis");

        GUILayout.EndVertical();

    }
}
