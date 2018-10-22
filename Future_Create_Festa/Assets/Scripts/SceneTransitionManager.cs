using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public enum SceneType{
        Title = 0,
        ModeSelect,
        StageSelect,
        GameMain,
    }

    [Tooltip("現在のシーン")]
    public SceneType NowScene;
    [Tooltip("次に遷移するシーン")]
    public SceneType NextScene;
    [Tooltip("シーン遷移フラグ")]
    public bool isTransition;

    // 現在存在しているオブジェクト実体の記憶領域
    static SceneTransitionManager _instance = null;

    // オブジェクト実体の参照（初期参照時、実体の登録も行う）
    static SceneTransitionManager instance
    {
        get { return _instance ?? (_instance = FindObjectOfType<SceneTransitionManager>()); }
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
    void Start()
    {

        NowScene = SceneTransitionManager.SceneType.Title;

        DontDestroyOnLoad(this.gameObject);

    }


    // Update is called once per frame
    void Update()
    {

        switch (NowScene)
        {
            case SceneType.Title:
                NextScene = SceneType.ModeSelect;
                SceneTransition();
                break;

            case SceneType.ModeSelect:
                NextScene = SceneType.StageSelect;
                SceneTransition();
                break;

            case SceneType.StageSelect:
                NextScene = SceneType.Title;
                SceneTransition();
                break;

            case SceneType.GameMain:
                break;

        }
    }

    // シーンの遷移
    void SceneTransition()
    {
        if (isTransition)
        {
            isTransition = false;
            NowScene = NextScene;
            SceneManager.LoadScene(NextScene.ToString());
        }
    }

    // 遷移する条件
    public void CheckTransition()
    {
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
                break;

            case SceneType.StageSelect:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    isTransition = true;
                }
                break;

            case SceneType.GameMain:
                break;

        }
    }
}