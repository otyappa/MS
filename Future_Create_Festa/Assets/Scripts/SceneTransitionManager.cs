using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionManager : MonoBehaviour
{
    public enum SceneType
    {
        Title = 0,
        ModeSelect,
        StageSelect,
        Main,
        VALUE_MAX
    }

    public enum ModeType
    {
        OneToOne = 0,
        TwoToTwo,
        VALUE_MAX
    }

    public enum StageType
    {
        Stage1 = 1,
        Stage2,
        Stage3,
        Stage4,

    }

    [Tooltip("タイトルロゴ")]
    public GameObject titleLogo;
    [Tooltip("モードセレクトロゴ")]
    public GameObject modeSelectLogo;
    [Tooltip("ステージセレクトロゴ")]
    public GameObject stageSelectLogo;
    [Tooltip("背景ステージ")]
    public GameObject titleStage;

    [Tooltip("現在のシーン")]
    public SceneType NowScene;
    [Tooltip("次に遷移するシーン")]
    public SceneType NextScene;
    [Tooltip("シーン遷移フラグ")]
    public bool isTransition;

    [Tooltip("モード選択結果")]
    public ModeType choseMode;
    [Tooltip("ステージ選択結果")]
    public int choseStage;
    [Tooltip("ステージセレクト回転速度")]
    public float stageSelectRotationSpeed;
    [Tooltip("Rotationフラグ")]
    public bool rotFlg;
    [Tooltip("Rotation中フラグ")]
    public bool isRotation;
    [Tooltip("回転させるモデル")]
    public Transform modelTrans;

    public TitleCamera titleCamera;
    public bool oneTimeFadeOut;

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

        titleLogo = GameObject.Find("TitleLogo");
        modeSelectLogo = GameObject.Find("ModeSelectLogo");
        stageSelectLogo = GameObject.Find("StageSelectLogo");
        titleStage = GameObject.Find("TitleStage");
        titleCamera = GameObject.Find("TitleCamera").GetComponent<TitleCamera>();
        oneTimeFadeOut = false;

        NowScene = SceneTransitionManager.SceneType.Title;
        //NowScene = SceneTransitionManager.SceneType.StageSelect;

        choseMode = ModeType.VALUE_MAX;
        choseStage = 1;

        DontDestroyOnLoad(this.gameObject);

    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log(modelTrans);

        switch (NowScene)
        {
            case SceneType.Title:
                if (!oneTimeFadeOut)
                {
                    Sound.LoadBgm("TitleBgm", "Title_TestBgm");
                    Sound.LoadSe("TitleSe", "Title_TestSe");

                    Sound.PlayBgm("TitleBgm");

                    titleLogo.SetActive(true);
                    modeSelectLogo.SetActive(false);
                    stageSelectLogo.SetActive(false);
                    GlobalCoroutine.Go(titleCamera.fadeImage.MaterialFadeOut(titleCamera.rend, titleCamera.fadeTime));
                    oneTimeFadeOut = true;
                }
                NextScene = SceneType.ModeSelect;
                SceneTransition();
                CheckTransition();
                break;

            case SceneType.ModeSelect:
                if (!oneTimeFadeOut)
                {
                    Sound.LoadBgm("ModeSelectBgm", "ModeSelect_TestBgm");
                    Sound.LoadSe("ModeSelectSe", "ModeSelect_TestSe");

                    Sound.PlayBgm("ModeSelectBgm");

                    titleLogo.SetActive(false);
                    modeSelectLogo.SetActive(true);
                    stageSelectLogo.SetActive(false);
                    GlobalCoroutine.Go(titleCamera.fadeImage.MaterialFadeOut(titleCamera.rend, titleCamera.fadeTime));
                    oneTimeFadeOut = true;
                }
                NextScene = SceneType.StageSelect;
                SceneTransition();
                CheckTransition();
                break;

            case SceneType.StageSelect:
                if (!oneTimeFadeOut)
                {
                    Sound.LoadBgm("StageSelectBgm", "StageSelect_TestBgm");
                    Sound.LoadSe("StageSelectSe", "StageSelect_TestSe");

                    Sound.PlayBgm("StageSelectBgm");

                    titleLogo.SetActive(false);
                    modeSelectLogo.SetActive(false);
                    stageSelectLogo.SetActive(true);
                    GlobalCoroutine.Go(titleCamera.fadeImage.MaterialFadeOut(titleCamera.rend, titleCamera.fadeTime));
                    oneTimeFadeOut = true;
                }
                NextScene = SceneType.Main;
                if (modelTrans == null)
                {
                    modelTrans = GameObject.Find("SelectPanelParent").transform;
                }
                SceneTransition();
                CheckTransition();
                break;

            case SceneType.Main:
                if (!oneTimeFadeOut)
                {
                    oneTimeFadeOut = true;
                    Sound.LoadBgm("StageSelectBgm", "StageSelect_TestBgm");
                    Sound.LoadSe("StageSelectSe", "StageSelect_TestSe");

                    Sound.PlayBgm("StageSelectBgm");

                    titleLogo.SetActive(false);
                    modeSelectLogo.SetActive(false);
                    stageSelectLogo.SetActive(false);
                    titleStage.SetActive(false);
                    titleCamera.gameObject.SetActive(false);
                    GlobalCoroutine.Go(titleCamera.fadeImage.MaterialFadeOut(titleCamera.rend, titleCamera.fadeTime));

                }
                NextScene = SceneType.Title;

                break;

        }
    }

    // シーンの遷移
    void SceneTransition()
    {
        if (isTransition && !titleCamera.fadeImage.GetIsFadingIn())
        {
            oneTimeFadeOut = false;
            isTransition = false;
            NowScene = NextScene;
            SceneManager.LoadScene(NextScene.ToString());
            Debug.Log(NextScene.ToString());
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
                    Sound.StopBgm();
                    Sound.PlaySe("TitleSe");
                    isTransition = true;
                    GlobalCoroutine.Go(titleCamera.fadeImage.MaterialFadeIn(titleCamera.rend, titleCamera.fadeTime));
                }
                break;

            case SceneType.ModeSelect:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Sound.StopBgm();
                    Sound.PlaySe("ModeSelectSe");
                    isTransition = true;
                    GlobalCoroutine.Go(titleCamera.fadeImage.MaterialFadeIn(titleCamera.rend, titleCamera.fadeTime));
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    choseMode = ModeType.OneToOne;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    choseMode = ModeType.TwoToTwo;
                }
                break;

            case SceneType.StageSelect:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Sound.StopBgm();
                    Sound.PlaySe("StageSelectSe");
                    isTransition = true;
                    GlobalCoroutine.Go(titleCamera.fadeImage.MaterialFadeIn(titleCamera.rend, titleCamera.fadeTime));
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow) && !isRotation)
                {
                    choseStage++;
                    if ((StageType)choseStage > StageType.Stage4)
                    {
                        choseStage = (int)StageType.Stage1;
                    }
                    GlobalCoroutine.Go(RotationModel(modelTrans, stageSelectRotationSpeed, true));
                }
                if (Input.GetKeyDown(KeyCode.RightArrow) && !isRotation)
                {
                    choseStage--;
                    if ((StageType)choseStage < StageType.Stage1)
                    {
                        choseStage = (int)StageType.Stage4;
                    }
                    GlobalCoroutine.Go(RotationModel(modelTrans, stageSelectRotationSpeed, false));
                }

                break;

            case SceneType.Main:

                break;

        }
    }

    // フェードイン
    private IEnumerator RotationModel(Transform trans, float rotTime, bool leftRot)
    {
        // 排他制御
        if (isRotation)
        {
            yield break;
        }

        isRotation = true;

        float nowTime = 0.0f;
        float startRotY = 0.0f;
        float endRotY = 0.0f;
        float tmpRotY = 0.0f;
        float rotAngle = 0.0f;

        Debug.Log(leftRot);

        switch (choseStage)
        {
            case 1:
                if (leftRot)
                {
                    startRotY = 90;
                    endRotY = 0;
                }
                else
                {
                    startRotY = 270;
                    endRotY = 360;
                }
                break;
            case 2:
                if (leftRot)
                {
                    startRotY = 180;
                    endRotY = 90;
                }
                else
                {
                    startRotY = 0;
                    endRotY = 90;
                }
                break;
            case 3:
                if (leftRot)
                {
                    startRotY = 270;
                    endRotY = 180;
                }
                else
                {
                    startRotY = 90;
                    endRotY = 180;
                }
                break;
            case 4:
                if (leftRot)
                {
                    startRotY = 360;
                    endRotY = 270;
                }
                else
                {
                    startRotY = 180;
                    endRotY = 270;
                }
                break;
        }

        rotAngle = startRotY - endRotY;

        while (nowTime < rotTime)
        {
            nowTime += Time.deltaTime;
            tmpRotY = nowTime / rotTime * rotAngle + startRotY;

            trans.transform.eulerAngles = new Vector3(trans.rotation.x, tmpRotY, trans.rotation.z);
            //trans.transform.rotation = Quaternion.Euler(trans.rotation.x, tmpRotY, trans.rotation.z);

            yield return true;

        }

        isRotation = false;
    }
}