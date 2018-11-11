using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameSystem : MonoBehaviour {

    bool GameSet=false;
    bool Blue_Win=false;
    public GameObject blue_1;
    public GameObject red_1;
    public GameObject blue_2;
    public GameObject red_2;
    Couple_Bar TimeUpWinner;
    SceneTransitionManager scene_mana;
    bool two_on_two;
    int Round_count = 0;
    int B_Win_Count = 0;
    int R_Win_Count = 0;
    // Use this for initialization
    // 現在存在しているオブジェクト実体の記憶領域
    static GameSystem _instance = null;

    // オブジェクト実体の参照（初期参照時、実体の登録も行う）
    static GameSystem instance
    {
        get { return _instance ?? (_instance = FindObjectOfType<GameSystem>()); }
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



    void Start () {
        Init();
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.F12))
        {
            SceneManager.LoadScene("Main");
        }
        if (Input.GetKey(KeyCode.F1))
        {
            scene_mana.choseMode = SceneTransitionManager.ModeType.OneToOne;
            SceneManager.LoadScene("Main");
        }
        if (Input.GetKey(KeyCode.F2))
        {
            scene_mana.choseMode = SceneTransitionManager.ModeType.TwoToTwo;
            SceneManager.LoadScene("Main");
        }
    }

    public void BlueWin()
    {
        if (GameSet == false)
        {
            GameSet = true;
            Blue_Win = true;
            B_Win_Count++;
            Round_count++;
        }
    }
    public void RedWin()
    {
        if (GameSet == false)
        {
            GameSet = true;
            B_Win_Count++;
            Round_count++;
        }
    }

    void Draw()
    {
        if (GameSet == false)
        {
            GameSet = true;
        }
    }
    public bool Get_GameSet()
    {
        return GameSet;
    }
    public bool Get_WINNER()
    {
        return Blue_Win;
    }

    public void TimeUp()
    {
        switch (TimeUpWinner.RedWin())
        {
            case 0 :
                RedWin();
                break;
            case 1:
                BlueWin();
                break;
            case 2:
                Draw();
                break;
        }
    }

    void Init()
    {
        scene_mana = GameObject.Find("SceneTransitionManager").GetComponent<SceneTransitionManager>();
        blue_1 = GameObject.Find("Blue_Player");
        blue_2 = GameObject.Find("Blue_Player2");
        red_1 = GameObject.Find("Red_Player");
        red_2 = GameObject.Find("Red_Player2");
        TimeUpWinner = GetComponent<Couple_Bar>();
        if ((int)scene_mana.choseMode == 0)
        {
            blue_2.gameObject.SetActive(false);
            red_2.gameObject.SetActive(false);
            blue_1.GetComponent<PL>().Set_MaxTrap(3);
            red_1.GetComponent<PL>().Set_MaxTrap(3);
        }
        else
        {

            blue_2.GetComponent<PL>().Set_MaxTrap(2);
            red_2.GetComponent<PL>().Set_MaxTrap(2);
            blue_1.GetComponent<PL>().Set_MaxTrap(2);
            red_1.GetComponent<PL>().Set_MaxTrap(2);
        }

    }
}
