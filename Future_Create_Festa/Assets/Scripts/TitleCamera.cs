using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleCamera : MonoBehaviour {

    [Tooltip("注視点オブジェクト")]
    public GameObject lookObj;
    [Tooltip("シーン遷移フェードレンダラ")]
    public Renderer rend;
    [Tooltip("遷移フェードスクリプト")]
    public FadeImage fadeImage;
    [Tooltip("フェードフラグ")]
    public bool fadeFlg;

    // 現在存在しているオブジェクト実体の記憶領域
    static TitleCamera _instance = null;

    // オブジェクト実体の参照（初期参照時、実体の登録も行う）
    static TitleCamera instance
    {
        get { return _instance ?? (_instance = FindObjectOfType<TitleCamera>()); }
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

        lookObj = GameObject.Find("TitleStage");

        fadeImage = GetComponent<FadeImage>();

        rend = GameObject.Find("FadePlane").GetComponent<Renderer>();
        fadeImage.Init(rend);
	}
	
	// Update is called once per frame
	void Update () {

        this.transform.LookAt(lookObj.transform);

        if (fadeFlg)
        {
            fadeImage.MaterialFadeIn(rend, 0.5f);
        }


    }
}
