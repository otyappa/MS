using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GamepadInput;

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

    [Tooltip("タイトルUI")]
    public GameObject titleUI;
    [Tooltip("モードセレクトUI")]
    public GameObject modeSelectUI;
    [Tooltip("ステージセレクトUI")]
    public GameObject stageSelectUI;
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

    private GamepadState gpState;

    private GameObject[] modeSelectFrameObj;

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
        titleUI = GameObject.Find("TitleUI");
        modeSelectUI = GameObject.Find("ModeSelectUI");
        stageSelectUI = GameObject.Find("StageSelectUI");
        titleStage = GameObject.Find("TitleStage");
        titleCamera = GameObject.Find("TitleCamera").GetComponent<TitleCamera>();
        modeSelectFrameObj = new GameObject[2];
        modeSelectFrameObj[0] = GameObject.Find("selectFrame_1v1");
        modeSelectFrameObj[1] = GameObject.Find("selectFrame_2v2");

        oneTimeFadeOut = false;

        NowScene = SceneTransitionManager.SceneType.Title;
        titleUI.SetActive(true);
        modeSelectUI.SetActive(false);
        stageSelectUI.SetActive(false);
        //NowScene = SceneTransitionManager.SceneType.StageSelect;

        choseMode = ModeType.VALUE_MAX;
        choseStage = 1;

        Sound.LoadBgm("bgm", "BGM/BGM_Title");
        Sound.LoadSe("enter", "SE/SE_Enter");
        Sound.LoadSe("select", "SE/SE_Select");
        Sound.LoadSe("cancel", "SE/SE_Cancel");
        Sound.LoadSe("modeSelectIn", "SE/SE_ModeSelect_Start");

        DontDestroyOnLoad(this.gameObject);

    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log(modelTrans);
        //NowScene = (SceneType)SceneManager.GetActiveScene().rootCount;
        gpState = GamePad.GetState(GamePad.Index.One);
        testinput();

        switch (NowScene)
        {
            case SceneType.Title:
                if (!oneTimeFadeOut)
                {
                    //Sound.LoadBgm("TitleBgm", "Title_TestBgm");
                    //Sound.LoadSe("TitleSe", "Title_TestSe");
                    //Sound.PlayBgm("TitleBgm");
                    Sound.PlayBgm("bgm");

                    titleUI.SetActive(true);
                    modeSelectUI.SetActive(false);
                    stageSelectUI.SetActive(false);
                    GlobalCoroutine.Go(titleCamera.fadeImage.MaterialFadeOut(titleCamera.rend, titleCamera.fadeTime));
                    oneTimeFadeOut = true;
                }
                CheckTransition();
                SceneTransition();
                break;

            case SceneType.ModeSelect:
                if (!oneTimeFadeOut)
                {
                    //Sound.LoadBgm("ModeSelectBgm", "ModeSelect_TestBgm");
                    //Sound.LoadSe("ModeSelectSe", "ModeSelect_TestSe");
                    //Sound.PlayBgm("ModeSelectBgm");

                    choseMode = ModeType.OneToOne;

                    Sound.PlaySe("modeSelectIn");

                    titleUI.SetActive(false);
                    modeSelectUI.SetActive(true);
                    stageSelectUI.SetActive(false);
                    modeSelectFrameObj[0].SetActive(true);
                    modeSelectFrameObj[1].SetActive(false);
                    GlobalCoroutine.Go(titleCamera.fadeImage.MaterialFadeOut(titleCamera.rend, titleCamera.fadeTime));
                    oneTimeFadeOut = true;
                }
                CheckTransition();
                SceneTransition();
                break;

            case SceneType.StageSelect:
                if (!oneTimeFadeOut)
                {
                    //Sound.LoadBgm("StageSelectBgm", "StageSelect_TestBgm");
                    //Sound.LoadSe("StageSelectSe", "StageSelect_TestSe");
                    //Sound.PlayBgm("StageSelectBgm");

                    titleUI.SetActive(false);
                    modeSelectUI.SetActive(false);
                    stageSelectUI.SetActive(true);
                    GlobalCoroutine.Go(titleCamera.fadeImage.MaterialFadeOut(titleCamera.rend, titleCamera.fadeTime));
                    oneTimeFadeOut = true;
                }
                if (modelTrans == null)
                {
                    modelTrans = GameObject.Find("SelectPanelParent").transform;
                }
                CheckTransition();
                SceneTransition();
                break;

            case SceneType.Main:
                if (!oneTimeFadeOut)
                {
                    oneTimeFadeOut = true;
                    //Sound.LoadBgm("StageSelectBgm", "StageSelect_TestBgm");
                    //Sound.LoadSe("StageSelectSe", "StageSelect_TestSe");
                    //Sound.PlayBgm("StageSelectBgm");

                    titleUI.SetActive(false);
                    modeSelectUI.SetActive(false);
                    stageSelectUI.SetActive(false);
                    titleStage.SetActive(false);
                    titleCamera.gameObject.SetActive(false);
                    GlobalCoroutine.Go(titleCamera.fadeImage.MaterialFadeOut(titleCamera.rend, titleCamera.fadeTime));

                }

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
                if (Input.GetKeyDown(KeyCode.Return) || IsInputGp_ABXY())
                {
                    NextScene = SceneType.ModeSelect;
                    //Sound.StopBgm();
                    Sound.PlaySe("enter");
                    isTransition = true;
                    GlobalCoroutine.Go(titleCamera.fadeImage.MaterialFadeIn(titleCamera.rend, titleCamera.fadeTime));
                }
                break;

            case SceneType.ModeSelect:
                if (Input.GetKeyDown(KeyCode.Return) || gpState.X)
                {
                    NextScene = SceneType.StageSelect;
                    //Sound.StopBgm();
                    Sound.PlaySe("enter");
                    isTransition = true;
                    GlobalCoroutine.Go(titleCamera.fadeImage.MaterialFadeIn(titleCamera.rend, titleCamera.fadeTime));
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow) || gpState.Left || gpState.Up)
                {
                    Sound.PlaySe("select");
                    modeSelectFrameObj[0].SetActive(true);
                    modeSelectFrameObj[1].SetActive(false);
                    choseMode = ModeType.OneToOne;
                }
                if (Input.GetKeyDown(KeyCode.RightArrow) || gpState.Right || gpState.Down)
                {
                    Sound.PlaySe("select");
                    modeSelectFrameObj[0].SetActive(false);
                    modeSelectFrameObj[1].SetActive(true);
                    choseMode = ModeType.TwoToTwo;
                }
                break;

            case SceneType.StageSelect:

                // モード選択に戻る
                if(Input.GetKeyDown(KeyCode.Backspace) || gpState.B)
                {
                    //Sound.StopBgm();
                    Sound.PlaySe("cancel");
                    NextScene = SceneType.ModeSelect;
                    isTransition = true;
                    GlobalCoroutine.Go(titleCamera.fadeImage.MaterialFadeIn(titleCamera.rend, titleCamera.fadeTime));

                }

                if (Input.GetKeyDown(KeyCode.Return) || gpState.X)
                {
                    NextScene = SceneType.Main;
                    Sound.StopBgm();
                    Sound.PlaySe("enter");
                    isTransition = true;
                    GlobalCoroutine.Go(titleCamera.fadeImage.MaterialFadeIn(titleCamera.rend, titleCamera.fadeTime));
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow) || gpState.Left && !isRotation)
                {
                    Sound.PlaySe("select");
                    choseStage++;
                    if ((StageType)choseStage > StageType.Stage4)
                    {
                        choseStage = (int)StageType.Stage1;
                    }
                    //GlobalCoroutine.Go(RotationModel(modelTrans, stageSelectRotationSpeed, true));
                    GlobalCoroutine.Go(testRotationModel(modelTrans, stageSelectRotationSpeed, true));
                }
                if (Input.GetKeyDown(KeyCode.RightArrow) || gpState.Right && !isRotation)
                {
                    Sound.PlaySe("select");
                    choseStage--;
                    if ((StageType)choseStage < StageType.Stage1)
                    {
                        choseStage = (int)StageType.Stage4;
                    }
                    //GlobalCoroutine.Go(RotationModel(modelTrans, stageSelectRotationSpeed, false));
                    GlobalCoroutine.Go(testRotationModel(modelTrans, stageSelectRotationSpeed, false));
                }

                break;

            case SceneType.Main:
                NextScene = SceneType.Title;

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

            //trans.transform.eulerAngles = new Vector3(trans.rotation.x, tmpRotY, trans.rotation.z);
            //trans.transform.rotation = Quaternion.Euler(trans.rotation.x, tmpRotY, trans.rotation.z);
            trans.Rotate(0, 1, 0);

            yield return true;

        }

        isRotation = false;
    }


    [SerializeField] float test;
    private IEnumerator testRotationModel(Transform trans, float rotTime, bool leftRot)
    {

        // 排他制御
        if (isRotation)
        {
            yield break;
        }

        isRotation = true;

        Debug.Log("A");

        float speed = rotTime;
        float rotAngle = 90f;
        float variation;
        float rot;

        if (leftRot)
        {
            variation = rotAngle / speed * -1;
        }
        else
        {
            variation = rotAngle / speed;
        }

        rot = 0f;
        //trans.transform.localRotation = Quaternion.Euler(0, 0, 0);

        float startRotY = trans.transform.rotation.y;
        float endRotY;

        //while (rot <= rotAngle)
        while (leftRot ? rot >= -rotAngle : rot <= rotAngle)
        {
            //if (leftRot)
            //{
            //    trans.transform.Rotate(0, variation * Time.deltaTime * -1, 0);
            //}
            //else
            //{
            //    trans.transform.Rotate(0, variation * Time.deltaTime, 0);
            //}
            trans.transform.Rotate(0, variation * Time.deltaTime, 0);

            rot += variation * Time.deltaTime;

            test = rot;

            yield return true;

        }

        rot = Mathf.Floor(rot);
        //  Debug.Log("")
        rot = Mathf.Round(rot / 10);
        rot = Mathf.Floor(rot);
        rot = rot * 10;


        //rot = Mathf.Floor((trans.transform.rotation.y / 10) * 10);


        //trans.transform.localRotation = Quaternion.Euler(0, rot, 0);

        isRotation = false;

    }

    // ボタンのどれかが押されたら
    private bool IsInputGp_ABXY()
    {
        if (gpState.A) return true;
        if (gpState.B) return true;
        if (gpState.X) return true;
        if (gpState.Y) return true;

        return false;
    }

    private void testinput()
    {
        if (gpState.A) Debug.Log("A");
        if (gpState.B) Debug.Log("B");
        if (gpState.X) Debug.Log("X");
        if (gpState.Y) Debug.Log("Y");
        if (gpState.Back) Debug.Log("Back");
        if (gpState.Start) Debug.Log("Start");
        if (gpState.Up) Debug.Log("Up");
        if (gpState.Down) Debug.Log("Down");
        if (gpState.Left) Debug.Log("Left");
        if (gpState.Right) Debug.Log("Right");

    }

}
