using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PL : MonoBehaviour {
    float cool_time;
    Vector3 Start_Player_Position;
    public bool Red_Player;
    public GameObject ReSporn_Point;
    public GameObject Coll_Point;
    public GameObject Trap_Point;
    public GameObject Couple;
    public float Move_Speed=1;
    int Make_Trap_Time = 0;
    
    [SerializeField]
    [Range(1, 10)]
    int max_trap = 1;
    int trap_count;
	// Use this for initialization
	void Start () {
        trap_count = 0;
      //  max_trap = 1;
		Start_Player_Position = this.transform.position;
		
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

        //罠を仕掛ける
        if (Input.GetKeyDown(KeyCode.RightControl) && trap_count < max_trap && Make_Trap_Time <= 60)
        {
            //敵に見える
            transform.GetChild(0).GetComponent<Hint>().Set_Active();
        }
        if (Input.GetKey(KeyCode.RightControl) && trap_count < max_trap&&Make_Trap_Time<=60)
        {
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
            if (Input.GetKeyUp(KeyCode.RightControl))
            {
                Make_Trap_Time = 0;
            }
            //左右移動
            if (Input.GetKey(KeyCode.RightArrow))
            {
                Move_Transform += Move_LR;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                Move_Transform -= Move_LR;
            }//end_if

            //上下移動
            if (Input.GetKey(KeyCode.UpArrow))
            {
                Move_Transform += Move_UD;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                Move_Transform -= Move_UD;
            }//end_if

            this.transform.position = Move_Transform;
        }


        //カップルを呼ぶ
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            Debug.Log("serch_start");
            Coll_Couple();
        }
	}
	void Move_R()
	{
		Vector3 Move_Transform = this.transform.position;
		Vector3 Move_LR = new Vector3 (Move_Speed / 20, 0.0f, 0.0f);
		Vector3 Move_UD = new Vector3 (0.0f, 0.0f, Move_Speed / 20);
        //罠を仕掛ける
        if (Input.GetKeyDown(KeyCode.E) && trap_count < max_trap && Make_Trap_Time <= 60)
        {
            //敵に見える
            transform.GetChild(0).GetComponent<Hint>().Set_Active();
        }
        if (Input.GetKey(KeyCode.E) && trap_count < max_trap&&Make_Trap_Time<=60)
        {
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

}
