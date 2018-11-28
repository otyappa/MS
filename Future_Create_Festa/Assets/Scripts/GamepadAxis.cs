using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class GamepadAxis : MonoBehaviour {

    public bool DpadLeftTrigger = false;
    public bool DpadRightTrigger = false;
    public bool DpadTriggerFlg = false;
    public bool LeftStickLeftTrigger = false;
    public bool LeftStickRightTrigger = false;
    public bool LeftStickTriggerFlg = false;

    public Vector2 Dpad;
    public Vector2 LeftStick;

    private GamepadState gpState;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        gpState = GamePad.GetState(GamePad.Index.One);

        SetParam();
        SetState();

	}

    void SetParam()
    {
        Dpad = GamePad.GetAxis(GamePad.Axis.Dpad, GamePad.Index.One);
        LeftStick = GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.One);
    }


    void SetState()
    {
        // Dpad
        if(Dpad.x < -0.9f)
        {
            DpadLeftTrigger = true;
        }
        else
        {
            DpadLeftTrigger = false;
        }
        if (Dpad.x > 0.9f)
        {
            DpadRightTrigger = true;
        }
        else
        {
            DpadRightTrigger = false;
        }

        // LeftStick
        if (LeftStick.x < -0.4f)
        {
            LeftStickLeftTrigger = true;
        }
        else
        {
            LeftStickLeftTrigger = false;
        }
        if (LeftStick.x > 0.4f)
        {
            LeftStickRightTrigger = true;
        }
        else
        {
            LeftStickRightTrigger = false;
        }
    }

    public bool DpadTriggerLeft()
    {
        if (!DpadTriggerFlg)
        {
            SetState();
            if(DpadLeftTrigger)
            {
                DpadTriggerFlg = true;
            }
            if (DpadTriggerFlg)
            {
                return true;
            }
        }
        else
        {
            if(!DpadLeftTrigger && !DpadRightTrigger)
            {
                DpadTriggerFlg = false;
            }
        }

        return false;
    }

    public bool DpadTriggerRight()
    {
        if (!DpadTriggerFlg)
        {
            SetState();
            if (DpadRightTrigger)
            {
                DpadTriggerFlg = true;
            }
            if (DpadTriggerFlg)
            {
                return true;
            }
        }
        else
        {
            if (!DpadLeftTrigger && !DpadRightTrigger)
            {
                DpadTriggerFlg = false;
            }
        }

        return false;
    }

    public bool LeftStickTriggerLeft()
    {
        if (!LeftStickTriggerFlg)
        {
            SetState();
            if (LeftStickLeftTrigger)
            {
                LeftStickTriggerFlg = true;
            }
            if (LeftStickTriggerFlg)
            {
                return true;
            }
        }
        else
        {
            if (!LeftStickLeftTrigger && !LeftStickRightTrigger)
            {
                LeftStickTriggerFlg = false;
            }
        }

        return false;
    }

    public bool LeftStickTriggerRight()
    {
        if (!LeftStickTriggerFlg)
        {
            SetState();
            if (LeftStickRightTrigger)
            {
                LeftStickTriggerFlg = true;
            }
            if (LeftStickTriggerFlg)
            {
                return true;
            }
        }
        else
        {
            if (!LeftStickLeftTrigger && !LeftStickRightTrigger)
            {
                LeftStickTriggerFlg = false;
            }
        }

        return false;
    }


}
