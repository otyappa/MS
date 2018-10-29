using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;
public class PL : MonoBehaviour {
    float cool_time;
    Vector3 Start_Player_Position;
    Create_SpecialItem C_Manager;
    public bool Red_Player;
    public GameObject ReSporn_Point;
    public GameObject Coll_Point;
    public GameObject Trap_Point;
    public GameObject Couple;
    public GameObject SPECIAL_SHOT;
    public float Move_Speed=1;
   public  GameObject Player_Model;
    Animator Player_Anm;
    public bool TonT; 
    GameObject Player_HintModel;
    int Make_Trap_Time = 0;
    bool SP_MODE = false;
    TimeCtl TimeBar;
    [SerializeField]
    [Range(1, 10)]
    int max_trap = 1;
    int trap_count;
    bool Trap_now=false;
    int Flash_Time;
    int flash_now;
   public enum Vec{
        top=0,
        right,
        buttom,
        left
    }
    Vec now = 0;
	// Use this for initialization
	void Start () {
        trap_count = 0;
      //  max_trap = 1;
		Start_Player_Position = this.transform.position;
        TimeBar = GetComponent<TimeCtl>();
        C_Manager = GameObject.Find("Stage").GetComponent<Create_SpecialItem>();
        Player_Model = transform.GetChild(2).gameObject;
        Player_Anm = Player_Model.GetComponent<Animator>();
        
   }

    // Update is called once per frame
    void Update()
    {
        if (Trap_now)
        {
            if (flash_now > 15)
            {
                Player_Model.SetActive(!Player_Model.activeSelf);
                flash_now = 0;
                Flash_Time++;
            }
            if (Flash_Time > 8)
            {
                Re_Start();
                Flash_Time = 0;
                Trap_now = false;
                Player_Model.SetActive(true);
            }
            flash_now++;
        }
        else
        {
            if (Red_Player)
            {
                Move_R();
            }
            else
            {
                Move_B();
            }
        }
    }

    //青チーム側の行動処理
    void Move_B()
	{
		Vector3 Move_Transform = this.transform.position;
		Vector3 Move_LR = new Vector3 (Move_Speed/20, 0.0f, 0.0f);//
		Vector3 Move_UD = new Vector3 (0.0f, 0.0f, Move_Speed / 20);//

        if (SP_MODE)
        {
            B_Special_Mode();
        }
        else
        {
            if (TonT)
            {
                GamepadState state = GamePad.GetState(GamePad.Index.Three);

                //罠を仕掛ける
                if (GamePad.GetButtonDown(GamePad.Button.B, GamePad.Index.Three) && trap_count < max_trap && Make_Trap_Time <= 60)
                {
                    TimeBar.Set_Pasent(60);
                    //敵に見える
                    transform.GetChild(0).GetComponent<Hint>().Set_Active();
                }
                if (GamePad.GetButton(GamePad.Button.B, GamePad.Index.Three) && trap_count < max_trap && Make_Trap_Time <= 60)
                {
                    if (Make_Trap_Time == 60)
                    {
                        Set_Trap();
                    }
                    else
                    {
                        TimeBar.Bar_Update();
                        Make_Trap_Time++;
                    }
                }
                else
                {
                    if (GamePad.GetButtonUp(GamePad.Button.B, GamePad.Index.Three))
                    {
                        TimeBar.Reset();
                        Make_Trap_Time = 0;
                    }
                    //左右移動
                    if (state.dPadAxis.x > 0.2f)
                    {
                        Player_Anm.SetTrigger("walk");
                        now = Vec.right;
                        Move_Transform += Move_LR;
                        Rotate_Model();
                    }
                    else if (state.dPadAxis.x < -0.2f)
                    {
                        Player_Anm.SetTrigger("walk");
                        now = Vec.left;
                        Move_Transform -= Move_LR;
                        Rotate_Model();
                    }//end_if

                    //上下移動
                    if (state.dPadAxis.y > 0.2f)
                    {
                        Player_Anm.SetTrigger("walk");
                        now = Vec.top;
                        Move_Transform += Move_UD;
                        Rotate_Model();
                    }
                    else if (state.dPadAxis.y < -0.2f)
                    {
                        Player_Anm.SetTrigger("walk");
                        now = Vec.buttom;
                        Move_Transform -= Move_UD;
                        Rotate_Model();
                    }//end_if

                    this.transform.position = Move_Transform;
                }


                //カップルを呼ぶ
                if (state.A)
                {

                    Coll_Couple();
                }

            }
            else//1対１
            {
                GamepadState state = GamePad.GetState(GamePad.Index.One);

                //罠を仕掛ける
                if (GamePad.GetButtonDown(GamePad.Button.B, GamePad.Index.One) && trap_count < max_trap && Make_Trap_Time <= 60)
                {

                    TimeBar.Set_Pasent(60);
                    //敵に見える
                    transform.GetChild(0).GetComponent<Hint>().Set_Active();
                }
                if (GamePad.GetButton(GamePad.Button.B, GamePad.Index.One) && trap_count < max_trap && Make_Trap_Time <= 60)
                {
                    if (Make_Trap_Time == 60)
                    {
                        Set_Trap();
                    }
                    else
                    {
                        TimeBar.Bar_Update();
                        Make_Trap_Time++;
                    }
                }
                else
                {
                    if (GamePad.GetButtonUp(GamePad.Button.B, GamePad.Index.One))
                    {
                        TimeBar.Reset();
                        Make_Trap_Time = 0;
                    }
                    //左右移動
                    if (state.dPadAxis.x > 0.2f)
                    {
                        Player_Anm.SetTrigger("walk");
                        now = Vec.right;
                        Move_Transform += Move_LR;
                        Rotate_Model();
                    }
                    else if (state.dPadAxis.x < -0.2f)
                    {
                        Player_Anm.SetTrigger("walk");
                        now = Vec.left;
                        Move_Transform -= Move_LR;
                        Rotate_Model();
                    }//end_if

                    //上下移動
                    if (state.dPadAxis.y > 0.2f)
                    {
                        Player_Anm.SetTrigger("walk");
                        now = Vec.top;
                        Move_Transform += Move_UD;
                        Rotate_Model();
                    }
                    else if (state.dPadAxis.y < -0.2f)
                    {
                        Player_Anm.SetTrigger("walk");
                        now = Vec.buttom;
                        Move_Transform -= Move_UD;
                        Rotate_Model();
                    }//end_if

                    this.transform.position = Move_Transform;
                }


                //カップルを呼ぶ
                if (state.A)
                {

                    Coll_Couple();
                }

            }
        }
	}
    //赤チーム側の行動処理
	void Move_R()
	{
		Vector3 Move_Transform = this.transform.position;
		Vector3 Move_LR = new Vector3 (Move_Speed / 20, 0.0f, 0.0f);
		Vector3 Move_UD = new Vector3 (0.0f, 0.0f, Move_Speed / 20);
        if (SP_MODE)
        {
            R_Special_Mode();
        }
        else
        {
            if (TonT)
            {
                GamepadState state = GamePad.GetState(GamePad.Index.Four);
               

                //罠を仕掛ける
                if (GamePad.GetButtonDown(GamePad.Button.B, GamePad.Index.Four) && trap_count < max_trap && Make_Trap_Time <= 60 || (Input.GetKeyDown(KeyCode.E) && trap_count < max_trap && Make_Trap_Time <= 60))
                {
                    TimeBar.Set_Pasent(60);
                    //敵に見える
                    transform.GetChild(0).GetComponent<Hint>().Set_Active();
                }
                if ((GamePad.GetButton(GamePad.Button.B, GamePad.Index.Four) && trap_count < max_trap && Make_Trap_Time <= 60) || (Input.GetKey(KeyCode.E) && trap_count < max_trap && Make_Trap_Time <= 60))
                {
                    TimeBar.Bar_Update();
                    if (Make_Trap_Time == 60)
                    {
                        Set_Trap();
                    }
                    else
                    {
                        Make_Trap_Time++;
                    }
                }
                else
                {
                    if (Input.GetKeyUp(KeyCode.E) || GamePad.GetButtonUp(GamePad.Button.B, GamePad.Index.Four))
                    {

                        TimeBar.Reset();
                        Make_Trap_Time = 0;
                    }
                    Player_Anm.SetBool("walk", false);
                    //左右移動
                    if (Input.GetKey(KeyCode.D) || state.dPadAxis.x>0.2f)
                    {
                        Player_Anm.SetBool("walk",true);
                        now = Vec.right;
                        Move_Transform += Move_LR;
                        Rotate_Model();
                    }
                    else if (Input.GetKey(KeyCode.A) || state.dPadAxis.x < -0.2f)
                    {
                        Player_Anm.SetBool("walk", true);
                        now = Vec.left;
                        Move_Transform -= Move_LR;
                        Rotate_Model();
                    }//左右移動ここまで

                    //上下移動
                    if (Input.GetKey(KeyCode.W) || state.dPadAxis.y > 0.2f)
                    {
                        Player_Anm.SetBool("walk", true);
                        now = Vec.top;
                        Move_Transform += Move_UD;
                        Rotate_Model();
                    }
                    else if (Input.GetKey(KeyCode.S) || state.dPadAxis.y < -0.2f)
                    {
                        Player_Anm.SetBool("walk", true);
                        now = Vec.buttom;
                        Move_Transform -= Move_UD;
                        Rotate_Model();
                    }//上下移動ここまで

                    this.transform.position = Move_Transform;

                }

                //カップルを呼ぶ
                if (Input.GetKeyUp(KeyCode.Q) || state.A)
                {

                    Coll_Couple();
                }

            }
            else
            {
                GamepadState state = GamePad.GetState(GamePad.Index.Two);

                //罠を仕掛ける
                if (GamePad.GetButtonDown(GamePad.Button.B, GamePad.Index.Two) && trap_count < max_trap && Make_Trap_Time <= 60 )
                {
                    TimeBar.Set_Pasent(60);
                    //敵に見える
                    transform.GetChild(0).GetComponent<Hint>().Set_Active();
                }
                if ((GamePad.GetButton(GamePad.Button.B, GamePad.Index.Two) && trap_count < max_trap && Make_Trap_Time <= 60))
                {
                    TimeBar.Bar_Update();
                    if (Make_Trap_Time == 60)
                    {
                        Set_Trap();
                    }
                    else
                    {
                        Make_Trap_Time++;
                    }
                }
                else
                {
                    if (GamePad.GetButtonUp(GamePad.Button.B, GamePad.Index.Two))
                    {

                        TimeBar.Reset();
                        Make_Trap_Time = 0;
                    }
                    //左右移動
                    if (state.dPadAxis.x > 0.2f)
                    {
                        Player_Anm.SetTrigger("walk");
                        now = Vec.right;
                        Move_Transform += Move_LR;
                        Rotate_Model();
                    }
                    else if (state.dPadAxis.x < -0.2f)
                    {
                        Player_Anm.SetTrigger("walk");
                        now = Vec.left;
                        Move_Transform -= Move_LR;
                        Rotate_Model();
                    }//左右移動ここまで

                    //上下移動
                    if (state.dPadAxis.y > 0.2f)
                    {
                        Player_Anm.SetTrigger("walk");
                        now = Vec.top;
                        Move_Transform += Move_UD;
                        Rotate_Model();
                    }
                    else if (state.dPadAxis.y < -0.2f)
                    {
                        Player_Anm.SetTrigger("walk");
                        now = Vec.buttom;
                        Move_Transform -= Move_UD;
                        Rotate_Model();
                    }//上下移動ここまで

                    this.transform.position = Move_Transform;

                }

                //カップルを呼ぶ
                if (state.A)
                {

                    Coll_Couple();
                }
            }
        }
    }

    //プレイヤーのリスポーン
    void Re_Start()
    {
        this.transform.position = ReSporn_Point.transform.position;
    }
    //カップルを呼ぶ
    void Coll_Couple()
    {
        //プレイヤーの現在位置にColl_Pointを設置
        Coll_Point.transform.position = this.transform.position;
        //カップルを移動させる　
        Couple.GetComponent<Couple>().root_serch(Coll_Point.transform);

        //敵に見える
        transform.GetChild(0).GetComponent<Hint>().Set_Active();
    }
    //罠を仕掛ける
    void Set_Trap()
    {
        Make_Trap_Time++;
        trap_count++;
        //落とし穴作成
        GameObject trap= Instantiate(Trap_Point, this.transform.position, Quaternion.identity);
        trap.GetComponent<Trap>().Set_Creater(this.gameObject);

    }

    //何かにぶつかったとき
    void OnTriggerEnter(Collider it)
    {
        if (it.transform.tag == "Trap")
        {
            //自分のレイヤーとぶつかったもののレイヤーが同じならなにもしない
            if (this.gameObject.layer + 4 == it.transform.gameObject.layer)
            {
                return;
            }

            Trap_now = true;
            
            it.GetComponent<Trap>().Delete_Function();
            Destroy(it.gameObject);
        }
        if (it.transform.tag == "Player")
        {
            //敵に見える
            transform.GetChild(0).GetComponent<Hint>().Set_Active();
        }
        if (it.transform.tag == "S.P.E.C.I.A.L")
        {
            SP_MODE = true;
            C_Manager.Delete_Item();


            Destroy(it.gameObject);
        }
        
        if (it.transform.tag == "Restart_Shot")
        {
            Re_Start();
            Destroy(it.gameObject);
        }

    }

    //何かにぶつかったとき
    void OnCollisionEnter(Collision it)
    {
        if (it.transform.tag == "Player")
        {
            //敵に見える
            transform.GetChild(0).GetComponent<Hint>().Set_Active();
        }

    }

    //罠の使用回数を回復
    public void Trap_Recv()
    {
        if (trap_count > 0)
        {
            trap_count--;
        }
    }

    //時間をUIに渡す
    public void Set_Time(float t)
    {
        TimeBar.Set_Pasent(t);
    }
    //時間を進める
    public void Update_Time()
    {
        TimeBar.Bar_Update();
    }
    //バーの経過時間をリセットする
    public void End_Time()
    {
        TimeBar.Reset();
    }
    //球を取った時の行動青バージョン
    void B_Special_Mode()
    {
        Vector3 Move_Transform = this.transform.position;
        Vector3 Move_LR = new Vector3(Move_Speed / 20, 0.0f, 0.0f);//
        Vector3 Move_UD = new Vector3(0.0f, 0.0f, Move_Speed / 20);//
        if (TonT)
        {
            GamepadState state = GamePad.GetState(GamePad.Index.Three);

            if (GamePad.GetButtonUp(GamePad.Button.B, GamePad.Index.Three))
            {
                Vector3 create_pos = this.transform.position;
                switch (now)
                {
                    case Vec.top:
                        create_pos.z += 1.5f;
                        break;
                    case Vec.right:
                        create_pos.x += 1.5f;
                        break;
                    case Vec.buttom:
                        create_pos.z -= 1.5f;
                        break;
                    case Vec.left:
                        create_pos.x -= 1.5f;
                        break;
                }
                GameObject shot = Instantiate(SPECIAL_SHOT, create_pos, Quaternion.identity);
                shot.GetComponent<Special_Item>().Set_vector((int)now);
                SP_MODE = false;
            }
            else
            {

                //左右移動
                if (state.dPadAxis.x > 0.2f)
                {
                    Player_Anm.SetTrigger("walk");
                    now = Vec.right;
                    Move_Transform += Move_LR;
                    Rotate_Model();
                }
                else if (state.dPadAxis.x < -0.2f)
                {
                    Player_Anm.SetTrigger("walk");
                    now = Vec.left;
                    Move_Transform -= Move_LR;
                    Rotate_Model();
                }//end_if

                //上下移動
                if (state.dPadAxis.y > 0.2f)
                {
                    Player_Anm.SetTrigger("walk");
                    now = Vec.top;
                    Move_Transform += Move_UD;
                    Rotate_Model();
                }
                else if (state.dPadAxis.y < -0.2f)
                {
                    Player_Anm.SetTrigger("walk");
                    now = Vec.buttom;
                    Move_Transform -= Move_UD;
                    Rotate_Model();
                }//end_if

                this.transform.position = Move_Transform;
            }


            //カップルを呼ぶ
            if (state.A)
            {

                Coll_Couple();
            }

        }
        else
        {
            GamepadState state = GamePad.GetState(GamePad.Index.One);

            if (GamePad.GetButtonDown(GamePad.Button.B, GamePad.Index.One))
            {
                Vector3 create_pos = this.transform.position;
                switch (now)
                {
                    case Vec.top:
                        create_pos.z += 1.5f;
                        break;
                    case Vec.right:
                        create_pos.x += 1.5f;
                        break;
                    case Vec.buttom:
                        create_pos.z -= 1.5f;
                        break;
                    case Vec.left:
                        create_pos.x -= 1.5f;
                        break;
                }
                GameObject shot = Instantiate(SPECIAL_SHOT, create_pos, Quaternion.identity);
                shot.GetComponent<Special_Item>().Set_vector((int)now);
                SP_MODE = false;
            }
            else
            {

                //左右移動
                if (state.dPadAxis.x > 0.2f)
                {
                    Player_Anm.SetTrigger("walk");
                    now = Vec.right;
                    Move_Transform += Move_LR;
                    Rotate_Model();
                }
                else if (state.dPadAxis.x < -0.2f)
                {
                    Player_Anm.SetTrigger("walk");
                    now = Vec.left;
                    Move_Transform -= Move_LR;
                    Rotate_Model();
                }//end_if

                //上下移動
                if (state.dPadAxis.y > 0.2f)
                {
                    Player_Anm.SetTrigger("walk");
                    now = Vec.top;
                    Move_Transform += Move_UD;
                    Rotate_Model();
                }
                else if (state.dPadAxis.y < -0.2f)
                {
                    Player_Anm.SetTrigger("walk");
                    now = Vec.buttom;
                    Move_Transform -= Move_UD;
                    Rotate_Model();
                }//end_if

                this.transform.position = Move_Transform;
            }


            //カップルを呼ぶ
            if (state.A)
            {

                Coll_Couple();
            }
        }
    }
    //球を取った時の行動赤バージョン
    void R_Special_Mode()
    {
        Vector3 Move_Transform = this.transform.position;
        Vector3 Move_LR = new Vector3(Move_Speed / 20, 0.0f, 0.0f);
        Vector3 Move_UD = new Vector3(0.0f, 0.0f, Move_Speed / 20);
        if (TonT)
        {
            GamepadState state = GamePad.GetState(GamePad.Index.Four);

            if (Input.GetKeyUp(KeyCode.E) || GamePad.GetButtonUp(GamePad.Button.B, GamePad.Index.Four))
            {
                Vector3 create_pos = this.transform.position;
                switch (now)
                {
                    case Vec.top:
                        create_pos.z += 1.5f;
                        break;
                    case Vec.buttom:
                        create_pos.z -= 1.5f;
                        break;
                    case Vec.right:
                        create_pos.x += 1.5f;
                        break;
                    case Vec.left:
                        create_pos.x -= 1.5f;
                        break;
                }
                GameObject shot = Instantiate(SPECIAL_SHOT, create_pos, Quaternion.identity);
                shot.GetComponent<Special_Item>().Set_vector((int)now);
                SP_MODE = false;
            }
            else
            {

                //if (Input.GetKeyUp(KeyCode.E) || Input.GetButtonUp("R2_Trap"))
                //{

                //    TimeBar.Reset();
                //    Make_Trap_Time = 0;
                //}
                //左右移動
                if (Input.GetKey(KeyCode.D) || state.dPadAxis.x > 0.2f)
                {
                    Player_Anm.SetTrigger("walk");
                    now = Vec.right;
                    Move_Transform += Move_LR;
                    Rotate_Model();
                }
                else if (Input.GetKey(KeyCode.A) || state.dPadAxis.x < -0.2f)
                {
                    Player_Anm.SetTrigger("walk");
                    now = Vec.left;
                    Move_Transform -= Move_LR;
                    Rotate_Model();
                }//左右移動ここまで

                //上下移動
                if (Input.GetKey(KeyCode.W) || state.dPadAxis.y > 0.2f)
                {
                    Player_Anm.SetTrigger("walk");
                    now = Vec.top;
                    Move_Transform += Move_UD;
                    Rotate_Model();
                }
                else if (Input.GetKey(KeyCode.S) || state.dPadAxis.y < -0.2f)
                {
                    Player_Anm.SetTrigger("walk");
                    now = Vec.buttom;
                    Move_Transform -= Move_UD;
                    Rotate_Model();
                }//上下移動ここまで


                this.transform.position = Move_Transform;
            }


            //カップルを呼ぶ
            if (state.A)
            {

                Coll_Couple();
            }


        }
        else
        {
            GamepadState state = GamePad.GetState(GamePad.Index.Two);

            if (GamePad.GetButtonDown(GamePad.Button.B, GamePad.Index.Two))
            {
                Vector3 create_pos = this.transform.position;
                switch (now)
                {
                    case Vec.top:
                        create_pos.z += 1.5f;
                        break;
                    case Vec.buttom:
                        create_pos.z -= 1.5f;
                        break;
                    case Vec.right:
                        create_pos.x += 1.5f;
                        break;
                    case Vec.left:
                        create_pos.x -= 1.5f;
                        break;
                }
                GameObject shot = Instantiate(SPECIAL_SHOT, create_pos, Quaternion.identity);
                shot.GetComponent<Special_Item>().Set_vector((int)now);
                SP_MODE = false;
            }
            else
            {

                //if (Input.GetKeyUp(KeyCode.E) || Input.GetButtonUp("R_Trap"))
                //{

                //    TimeBar.Reset();
                //    Make_Trap_Time = 0;
                //}
                //左右移動
                if (state.dPadAxis.x > 0.2f)
                {
                    Player_Anm.SetTrigger("walk");
                    now = Vec.right;
                    Move_Transform += Move_LR;
                    Rotate_Model();
                }
                else if (state.dPadAxis.x < -0.2f)
                {
                    Player_Anm.SetTrigger("walk");
                    now = Vec.left;
                    Move_Transform -= Move_LR;
                    Rotate_Model();
                }//左右移動ここまで

                //上下移動
                if (state.dPadAxis.y > 0.2f)
                {
                    Player_Anm.SetTrigger("walk");
                    now = Vec.top;
                    Move_Transform += Move_UD;
                    Rotate_Model();
                }
                else if (state.dPadAxis.y< -0.2f)
                {
                    Player_Anm.SetTrigger("walk");
                    now = Vec.buttom;
                    Move_Transform -= Move_UD;
                    Rotate_Model();
                }//上下移動ここまで


                this.transform.position = Move_Transform;
            }


            //カップルを呼ぶ
            if (state.A)
            {

                Coll_Couple();
            }
        }
    }

    //モデルを回転させる
    void Rotate_Model()
    {
        float step = 5 * Time.deltaTime;
        //Debug.Log((float)now);
        Player_Model.transform.rotation = Quaternion.Slerp(Player_Model.transform.rotation, Quaternion.Euler(0, (float)now*90.0f, 0), step);
    }

    public void Set_MaxTrap(int setter)
    {
        max_trap = setter;
    }


}
