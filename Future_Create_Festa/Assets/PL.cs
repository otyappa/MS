using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    int Make_Trap_Time = 0;
    bool SP_MODE = false;
    TimeCtl TimeBar;
    [SerializeField]
    [Range(1, 10)]
    int max_trap = 1;
    int trap_count;

   public enum Vec{
        top=0,
        buttom,
        right,
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
    }
	
	// Update is called once per frame
	void Update () {
		if (Red_Player) {
			Move_R ();
		} else {
			Move_B ();
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
            //罠を仕掛ける
            if (Input.GetButtonDown("B_Trap") && trap_count < max_trap && Make_Trap_Time <= 60)
            {
                TimeBar.Set_Pasent(60);
                //敵に見える
                transform.GetChild(0).GetComponent<Hint>().Set_Active();
            }
            if (Input.GetButton("B_Trap") && trap_count < max_trap && Make_Trap_Time <= 60)
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
                if (Input.GetButtonUp("B_Trap"))
                {
                    TimeBar.Reset();
                    Make_Trap_Time = 0;
                }
                //左右移動
                if (Input.GetAxis("B_Right") > 0.2f)
                {

                    Move_Transform += Move_LR;
                }
                else if (Input.GetAxis("B_Left") < -0.2f)
                {

                    Move_Transform -= Move_LR;
                }//end_if

                //上下移動
                if (Input.GetAxis("B_Up") > 0.2f)
                {

                    Move_Transform += Move_UD;
                }
                else if (Input.GetAxis("B_Down") < -0.2f)
                {
                    Move_Transform -= Move_UD;
                }//end_if

                this.transform.position = Move_Transform;
            }


            //カップルを呼ぶ
            if (Input.GetButtonDown("B_Coll"))
            {

                Coll_Couple();
            }
        }
	}
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
            //罠を仕掛ける
            if (Input.GetKeyDown(KeyCode.E) && trap_count < max_trap && Make_Trap_Time <= 60)
            {
                TimeBar.Set_Pasent(60);
                //敵に見える
                transform.GetChild(0).GetComponent<Hint>().Set_Active();
            }
            if (Input.GetKey(KeyCode.E) && trap_count < max_trap && Make_Trap_Time <= 60)
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
                if (Input.GetKeyUp(KeyCode.E))
                {
                    TimeBar.Reset();
                    Make_Trap_Time = 0;
                }
                //左右移動
                if (Input.GetKey(KeyCode.D))
                {
                    Move_Transform += Move_LR;
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    Move_Transform -= Move_LR;
                }//左右移動ここまで

                //上下移動
                if (Input.GetKey(KeyCode.W))
                {
                    Move_Transform += Move_UD;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    Move_Transform -= Move_UD;
                }//上下移動ここまで

                this.transform.position = Move_Transform;

            }

            //カップルを呼ぶ
            if (Input.GetKeyUp(KeyCode.Q))
            {

                Coll_Couple();
            }
        }
    }

    //プレイヤーのリスポーン
    void Re_Start()
    {
        this.transform.position = ReSporn_Point.transform.position;
    }

    void Coll_Couple()
    {
        //プレイヤーの現在位置にColl_Pointを設置
        Coll_Point.transform.position = this.transform.position;
        //カップルを移動させる　未実装
        Couple.GetComponent<Couple>().root_serch(Coll_Point.transform);

        //敵に見える
        transform.GetChild(0).GetComponent<Hint>().Set_Active();
    }

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

            Re_Start();
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

    public void Set_Time(float t)
    {
        TimeBar.Set_Pasent(t);
    }
    public void Update_Time()
    {
        TimeBar.Bar_Update();
    }

    public void End_Time()
    {
        TimeBar.Reset();
    }

    void B_Special_Mode()
    {
        Vector3 Move_Transform = this.transform.position;
        Vector3 Move_LR = new Vector3(Move_Speed / 20, 0.0f, 0.0f);//
        Vector3 Move_UD = new Vector3(0.0f, 0.0f, Move_Speed / 20);//
        if (Input.GetButtonUp("B_Trap"))
        {
            Vector3 create_pos=this.transform.position;
            switch ((int)now)
            {
                case 0:
                    create_pos.z += 1.5f;
                    break;
                case 1:
                    create_pos.z -= 1.5f;
                    break;
                case 2:
                    create_pos.x += 1.5f;
                    break;
                case 3:
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
            if (Input.GetAxis("B_Right") > 0.2f)
            {
                now = Vec.right;
                Move_Transform += Move_LR;
            }
            else if (Input.GetAxis("B_Left") < -0.2f)
            {
                now = Vec.left;
                Move_Transform -= Move_LR;
            }//end_if

            //上下移動
            if (Input.GetAxis("B_Up") > 0.2f)
            {
                now = Vec.top;
                Move_Transform += Move_UD;
            }
            else if (Input.GetAxis("B_Down") < -0.2f)
            {
                now = Vec.buttom;
                Move_Transform -= Move_UD;
            }//end_if

            this.transform.position = Move_Transform;
        }


        //カップルを呼ぶ
        if (Input.GetButtonDown("B_Coll"))
        {

            Coll_Couple();
        }
    }

    void R_Special_Mode()
    {
        Vector3 Move_Transform = this.transform.position;
        Vector3 Move_LR = new Vector3(Move_Speed / 20, 0.0f, 0.0f);
        Vector3 Move_UD = new Vector3(0.0f, 0.0f, Move_Speed / 20);
        if (Input.GetKeyUp(KeyCode.E))
        {
            Vector3 create_pos = this.transform.position;
            switch ((int)now)
            {
                case 0:
                    create_pos.z += 1.5f;
                    break;
                case 1:
                    create_pos.z -= 1.5f;
                    break;
                case 2:
                    create_pos.x += 1.5f;
                    break;
                case 3:
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
            if (Input.GetKey(KeyCode.D))
            {
                now = Vec.right;
                Move_Transform += Move_LR;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                now = Vec.left;
                Move_Transform -= Move_LR;
            }//左右移動ここまで

            //上下移動
            if (Input.GetKey(KeyCode.W))
            {
                now = Vec.top;
                Move_Transform += Move_UD;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                now = Vec.buttom;
                Move_Transform -= Move_UD;
            }//上下移動ここまで

            this.transform.position = Move_Transform;
        }


        //カップルを呼ぶ
        if (Input.GetButtonDown("B_Coll"))
        {

            Coll_Couple();
        }
    }


}
