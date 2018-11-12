using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotalGameManager : MonoBehaviour {
    int Round_Count = 0;
    int Red_WinCount = 0;
    int Blue_WinCount = 0;
    Result WinCountUI1;
    Result WinCountUI2;
    // 現在存在しているオブジェクト実体の記憶領域
    static TotalGameManager _instance = null;

    // オブジェクト実体の参照（初期参照時、実体の登録も行う）
    static TotalGameManager instance
    {
        get { return _instance ?? (_instance = FindObjectOfType<TotalGameManager>()); }
    }

    void Awake()
    {

        // ※オブジェクトが重複していたらここで破棄される

        // 自身がインスタンスでなければ自滅
        if (this != instance)
        {
            Destroy(gameObject);
            return;
        }

        // 以降破棄しない
        DontDestroyOnLoad(gameObject);

    }

    void OnDestroy()
    {

        // ※破棄時に、登録した実体の解除を行なっている

        // 自身がインスタンスなら登録を解除
        if (this == instance) _instance = null;

    }



    // Use this for initialization
    void Start () {
        WinCountUI1 = GameObject.Find("Blue_Canvas").GetComponent<Result>();
        WinCountUI2 = GameObject.Find("Red_Canvas").GetComponent<Result>();

    }

    // Update is called once per frame
    void Update () {
		
	}

    public void AddWin(bool red)
    {
        if (red)
        {
            Red_WinCount++;
        }
        else
        {
            Blue_WinCount++;
        }
        Round_Count++;
        WinCountUI1.Set_Win(Red_WinCount, Blue_WinCount);
        WinCountUI2.Set_Win(Red_WinCount, Blue_WinCount);
    }

}
