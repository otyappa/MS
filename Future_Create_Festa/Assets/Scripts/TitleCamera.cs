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
    [Tooltip("フェードインフラグ")]
    public bool fadeInFlg;
    [Tooltip("フェードアウトフラグ")]
    public bool fadeOutFlg;
    [Tooltip("フェードの時間"), SerializeField, Range(0.1f, 4)]
    public float fadeTime;

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
        fadeImage.SetMaterialAlpha(1);

    }

    // Update is called once per frame
    void Update () {

        this.transform.LookAt(lookObj.transform);


        if (fadeInFlg)
        {
            Debug.Log("fadein");
            if (!fadeImage.GetIsFadingIn())
            {
                GlobalCoroutine.Go(fadeImage.MaterialFadeIn(rend, fadeTime));
            }
        }
        if (fadeOutFlg)
        {
            Debug.Log("fadeout");
            if (!fadeImage.GetIsFadingOut())
            {
                GlobalCoroutine.Go(fadeImage.MaterialFadeOut(rend, fadeTime));
            }
        }
    }
}
