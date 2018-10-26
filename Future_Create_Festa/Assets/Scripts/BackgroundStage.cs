using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundStage : MonoBehaviour {

    [Tooltip("回転速度"), SerializeField, Range(0.1f, 1.0f)]
    public float rotSpeed;

    
    // 現在存在しているオブジェクト実体の記憶領域
    static BackgroundStage _instance = null;

    // オブジェクト実体の参照（初期参照時、実体の登録も行う）
    static BackgroundStage instance
    {
        get { return _instance ?? (_instance = FindObjectOfType<BackgroundStage>()); }
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


	}
	
	// Update is called once per frame
	void Update () {

        this.gameObject.transform.Rotate(0, rotSpeed, 0);

    }
}
