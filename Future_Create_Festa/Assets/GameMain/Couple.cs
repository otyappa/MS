using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Couple : MonoBehaviour {
  [SerializeField]  Vector3 goal;
    [SerializeField]
    Vector3 Goal_Direction;
    [SerializeField]
    GameSystem GameSystemManager;
    [SerializeField]
    bool coal_flg;
    [SerializeField]
    bool Stan_flg=false;
    [SerializeField]
    bool walk_flg=true;
    [SerializeField]
    Vector3 target;
    [SerializeField]
    public bool Red_Player=false;
    //[SerializeField]
    //public GameObject Goal_Point;
    [SerializeField]
    public float move_speed=1;
    [SerializeField]
    int heal_Time=0;
    [SerializeField]
    Vector3 start_pos;
    Couple_Bar Set_bar;
    public struct stage
    {
      public  float x;
      public float z;
      public int jitucost;
      public int suiteicost;
      public int score;
      public int status;
      public int parent_x;
      public int parent_y;
    }
    [SerializeField]
    stage[,]teststage = new stage[9, 15];
    bool coll;
    int old_heal_Time = 0;
    bool Trap_now = false;
    int Flash_Time;
    int flash_now;
    Renderer flash_mesh;
    Animator Couple_Anm;
    GameObject Couple_Model;
    Sound test;
    public SceneTransitionManager stage_No;
    // Use this for initialization
    void Start () {
        GameSystemManager = GameObject.Find("GameSystemManager").GetComponent<GameSystem>();
        Set_bar = GameObject.Find("GameSystemManager").GetComponent<Couple_Bar>();
        stage_No = GameObject.Find("SceneTransitionManager").GetComponent<SceneTransitionManager>();
        CreateStage();

        Couple_Model = transform.GetChild(0).gameObject;
        Couple_Anm = Couple_Model.GetComponent<Animator>();
        flash_mesh = GetComponent<Renderer>();
        start_pos = transform.position;
        Sound.LoadSe("Hit_Item", "SE/Couple_Hit_Item");
        Sound.LoadSe("Hit_Trap", "SE/Couple_Hit_Trap");
        //Sound.PlaySe("start");
        //Sound.SetVolumeSe()
        //Sound.StopBgm();
    }
	
	// Update is called once per frame
	void Update () {
        if (!GameSystemManager.Get_GameSet())
        {
            if (Stan_flg==true)
            {
                if (flash_now > 15)
                {
                    //this.renderer.
                    flash_mesh.enabled = !flash_mesh.enabled;
                    flash_now = 0;
                    Flash_Time++;
                }
                if (Flash_Time > 8)
                {
                    //Re_Start();
                   // Flash_Time = 0;
                    //Trap_now = false;
                    //Player_Model.SetActive(true);
                }
                flash_now++;
            }
            else if (coal_flg == true)
            {
                Move();
                Set_bar.SetSlider(transform.position.x, Red_Player);
               
            }
        }
    }

    public void root_serch(Transform Goal)
    {
        if (Stan_flg == false)
        {
            CreateStage();
            //walk_flg = true;
            goal = Goal.position;
            int g_x = (int)((Goal.transform.position.z-0.75f) / -1.5f);
            int g_z = (int)((Goal.transform.position.x + 0.75f) / 1.5f);
            int t_x = (int)((transform.position.z) / -1.5f);
            int t_z = (int)((transform.position.x) / 1.5f);
            teststage[g_x, g_z].status = 2;
            teststage[g_x, g_z].jitucost = 0;
            teststage[g_x, g_z].suiteicost = (t_x - g_x) + (t_z - g_z);
            teststage[g_x, g_z].score = teststage[g_x, g_z].jitucost + teststage[g_x, g_z].suiteicost;
            teststage[g_x, g_z].parent_x = 99;
            if (g_x < 8)
            {
                if (teststage[g_x + 1, g_z].status == 0)
                {
                    teststage[g_x + 1, g_z].status = 0;
                    teststage[g_x + 1, g_z].parent_x = g_x;
                    teststage[g_x + 1, g_z].parent_y = g_z;
                    A_Star(1, g_x + 1, g_z);

                }

            }
            if (g_z < 14)
            {
                if (teststage[g_x, g_z + 1].status == 0)
                {
                    teststage[g_x, g_z + 1].status = 0;
                    teststage[g_x, g_z + 1].parent_x = g_x;
                    teststage[g_x, g_z + 1].parent_y = g_z;
                    A_Star(1, g_x, g_z + 1);
                }
            }
            if (g_x > 0)
            {
                if (teststage[g_x - 1, g_z].status == 0)
                {
                    teststage[g_x - 1, g_z].status = 0;
                    teststage[g_x - 1, g_z].parent_x = g_x;
                    teststage[g_x - 1, g_z].parent_y = g_z;
                    A_Star(1, g_x - 1, g_z);

                }
            }
            if (g_z > 0)
            {

                if (teststage[g_x, g_z - 1].status == 0)
                {
                    teststage[g_x, g_z - 1].status = 0;
                    teststage[g_x, g_z - 1].parent_x = g_x;
                    teststage[g_x, g_z - 1].parent_y = g_z;
                    A_Star(1, g_x, g_z - 1);

                }
            }



            int count = 1;
            int parent_x = g_x;
            int parent_y = g_z;
            while (true)
            {
                int min = 10000;
                int min_x = g_x;
                int min_y = g_z;

                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 15; j++)
                    {
                        if (min > teststage[i, j].score && teststage[i, j].status == 1)
                        {
                            
                            min = teststage[i, j].score;
                            min_x = i;
                            min_y = j;
                        }
                    }
                }
                teststage[min_x, min_y].status = 2;
                parent_x = min_x;
                parent_y = min_y;
                if (min_x == t_x && min_y == t_z)
                {
                    coal_flg = true;
                    break;
                }
                count++;
                if (min_x < 8)
                {

                    if (teststage[min_x+1, min_y].status == 0)
                    {
                        teststage[min_x + 1, min_y].parent_x = min_x;
                        teststage[min_x + 1, min_y].parent_y = min_y;
                        A_Star(count, min_x + 1, min_y);
                    }
                }
                if (min_y < 14)
                {

                    if (teststage[min_x, min_y + 1].status == 0)
                    {
                        teststage[min_x, min_y + 1].parent_x = min_x;
                        teststage[min_x, min_y + 1].parent_y = min_y;
                        A_Star(count, min_x, min_y + 1);
                    }
                }
                if (min_x > 0)
                {

                    if (teststage[min_x - 1, min_y].status == 0)
                    {
                        teststage[min_x - 1, min_y].parent_x = min_x;
                        teststage[min_x - 1, min_y].parent_y = min_y;
                        A_Star(count, min_x - 1, min_y);
                    }
                }
                if (min_y > 0)
                {

                    if (teststage[min_x, min_y-1].status == 0)
                    {
                        teststage[min_x, min_y - 1].parent_x = min_x;
                        teststage[min_x, min_y - 1].parent_y = min_y;
                        A_Star(count, min_x, min_y - 1);
                    }
                }


                if (count > 135)
                {
                    break;
                }


            }
           
        }
    }
    void A_Star(int a,int x,int z)
    {
        int t_x = (int)((this.transform.position.x) / 1.5f);
        int t_z = (int)((this.transform.position.z) / 1.5f);
        if (teststage[x, z].score == 2)
        {
            return;
        } else if (teststage[x, z].status==0)
        {
            teststage[x, z].status = 1;
            teststage[x, z].jitucost = a;
            teststage[x, z].suiteicost = (t_x - x) + (t_z - z);
            teststage[x, z].score = teststage[x, z].jitucost + teststage[x, z].suiteicost;
            
        }
    }

    void CreateStage()
    {
        for (int i = 0; i < 9; i++)
        {
            for (int j = 0; j < 15; j++)
            {
                teststage[i, j].x = (j * 1.5f);
                teststage[i, j].z = (i * -1.5f);
                teststage[i, j].status = 0;
                teststage[i, j].jitucost = 0;
                teststage[i, j].parent_x = 0;
                teststage[i, j].parent_y = 0;
                teststage[i, j].score = 0;
                teststage[i, j].suiteicost = 0;
            }
        }
        switch ((int)stage_No.choseStage)
        { case 1:

                    teststage[0, 5].status = 2;
                    teststage[0, 6].status = 2;
                    teststage[0, 7].status = 2;
                    teststage[0, 8].status = 2;
                    teststage[0, 9].status = 2;

                    teststage[1, 1].status = 2;
                    teststage[1, 2].status = 2;
                    teststage[1, 5].status = 2;
                    teststage[1, 6].status = 2;
                    teststage[1, 7].status = 2;

                    teststage[1, 8].status = 2;
                    teststage[1, 9].status = 2;
                    teststage[1, 12].status = 2;
                    teststage[1, 13].status = 2;
                    teststage[2, 1].status = 2;

                    teststage[2, 13].status = 2;
                    teststage[4, 3].status = 2;
                    teststage[4, 4].status = 2;
                    teststage[4, 5].status = 2;
                    teststage[4, 6].status = 2;

                    // teststage[4, 7].status = 2;
                    teststage[4, 8].status = 2;
                    teststage[4, 9].status = 2;
                    teststage[4, 10].status = 2;
                    teststage[4, 11].status = 2;

                    teststage[6, 1].status = 2;
                    teststage[6, 13].status = 2;
                    teststage[7, 1].status = 2;
                    teststage[7, 2].status = 2;
                    teststage[7, 5].status = 2;

                    teststage[7, 6].status = 2;
                    teststage[7, 7].status = 2;
                    teststage[7, 8].status = 2;
                    teststage[7, 9].status = 2;
                    teststage[7, 12].status = 2;

                    teststage[7, 13].status = 2;
                    teststage[8, 5].status = 2;
                    teststage[8, 6].status = 2;
                    teststage[8, 7].status = 2;
                    teststage[8, 8].status = 2;

                    teststage[8, 9].status = 2;

                break;
            case 2:

                teststage[2, 1].status = 2;
                teststage[2, 5].status = 2;
                teststage[2, 6].status = 2;
                teststage[2, 7].status = 2;
                teststage[2, 8].status = 2;

                teststage[2, 9].status = 2;
                teststage[2, 13].status = 2;
                teststage[3, 1].status = 2;
                teststage[3, 2].status = 2;
                teststage[3, 12].status = 2;

                teststage[3, 13].status = 2;
                teststage[4, 7].status = 2;
                teststage[5, 1].status = 2;
                teststage[5, 2].status = 2;
                teststage[5, 12].status = 2;

                teststage[5, 13].status = 2;
                teststage[6, 1].status = 2;
                teststage[6, 5].status = 2;
                teststage[6, 6].status = 2;
                teststage[6, 7].status = 2;

                // teststage[4, 7].status = 2;
                teststage[6, 8].status = 2;
                teststage[6, 9].status = 2;
                teststage[6, 13].status = 2;

                break;
            case 3:
                teststage[2, 2].status = 2;
                teststage[2, 3].status = 2;
                teststage[2, 4].status = 2;
                teststage[2, 5].status = 2;
                teststage[2, 6].status = 2;

                teststage[2, 7].status = 2;
                teststage[2, 8].status = 2;
                teststage[2, 9].status = 2;
                teststage[2, 10].status = 2;
                teststage[2, 11].status = 2;

                teststage[2, 12].status = 2;
                teststage[3, 7].status = 2;
                teststage[4, 5].status = 2;
                teststage[4, 6].status = 2;
                teststage[4, 7].status = 2;

                teststage[4, 8].status = 2;
                teststage[4, 9].status = 2;
                teststage[5, 7].status = 2;
                teststage[6, 2].status = 2;
                teststage[6, 3].status = 2;
            
                teststage[6, 4].status = 2;
                teststage[6, 5].status = 2;
                teststage[6, 6].status = 2;
                teststage[6, 7].status = 2;

                teststage[6, 8].status = 2;
                teststage[6, 9].status = 2;
                teststage[6, 10].status = 2;
                teststage[6, 11].status = 2;
                teststage[6, 12].status = 2;

                break;
            case 4:
                teststage[0, 0].status = 2;
                teststage[0, 1].status = 2;
                teststage[0, 2].status = 2;
                teststage[0, 3].status = 2;
                teststage[0, 4].status = 2;

                teststage[1, 0].status = 2;
                teststage[1, 1].status = 2;
                teststage[1, 2].status = 2;
                teststage[1, 3].status = 2;
                teststage[1, 4].status = 2;

                teststage[1, 6].status = 2;
                teststage[1, 7].status = 2;
                teststage[1, 8].status = 2;
                teststage[1, 9].status = 2;
                teststage[1, 10].status = 2;

                teststage[1, 11].status = 2;
                teststage[2, 0].status = 2;
                teststage[2, 1].status = 2;
                teststage[3, 4].status = 2;
                teststage[3, 5].status = 2;

                teststage[3, 6].status = 2;
                teststage[3, 13].status = 2;
                teststage[4, 1].status = 2;
                teststage[4, 13].status = 2;

                teststage[5, 1].status = 2;
                teststage[5, 8].status = 2;
                teststage[5, 9].status = 2;
                teststage[5, 10].status = 2;
                teststage[6, 13].status = 2;

                teststage[6, 14].status = 2;
                teststage[7, 3].status = 2;
                teststage[7, 4].status = 2;
                teststage[7, 5].status = 2;
                teststage[7, 6].status = 2;

                teststage[7, 7].status = 2;
                teststage[7, 8].status = 2;
                teststage[7, 10].status = 2;
                teststage[7, 11].status = 2;
                teststage[7, 12].status = 2;

                teststage[7, 13].status = 2;
                teststage[7, 14].status = 2;
                teststage[8, 10].status = 2;
                teststage[8, 11].status = 2;
                teststage[8, 12].status = 2;

                teststage[8, 13].status = 2;
                teststage[8, 14].status = 2;



                break;
            default:
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 11; j++)
                {
                    teststage[i, j].x = (j * 1.5f);
                    teststage[i, j].z = (i * -1.5f);
                    teststage[i, j].status = 0;
                    teststage[i, j].jitucost = 0;
                    teststage[i, j].parent_x = 0;
                    teststage[i, j].parent_y = 0;
                    teststage[i, j].score = 0;
                    teststage[i, j].suiteicost = 0;
                }
            }
                teststage[0, 0].status = 2;
                teststage[0, 5].status = 2;
                teststage[0, 10].status = 2;
                teststage[1, 0].status = 2;
                teststage[1, 2].status = 2;
                teststage[1, 3].status = 2;
                teststage[1, 7].status = 2;
                teststage[1, 8].status = 2;
                teststage[1, 10].status = 2;
                teststage[2, 5].status = 2;
                teststage[3, 5].status = 2;
                teststage[4, 0].status = 2;
                teststage[4, 2].status = 2;
                teststage[4, 3].status = 2;
                teststage[4, 7].status = 2;
                teststage[4, 8].status = 2;
                teststage[4, 10].status = 2;
                teststage[5, 0].status = 2;
                teststage[5, 5].status = 2;
                teststage[5, 10].status = 2;
                break;
        }


    }

    void Move()
    {

        if (Vector3.MoveTowards(transform.position, target, move_speed / 20) == target)
        {
            Couple_Anm.SetBool("walk", false);
            walk_flg = true;

        }
        if (walk_flg == true)
        {
            int x = (int)((transform.position.z) / -1.5f);
            int z = (int)((transform.position.x) / 1.5f);

            if (teststage[x, z].parent_x == 99)
            {
                coal_flg = false;
                return;
            }

            target = new Vector3(teststage[teststage[x, z].parent_x, teststage[x, z].parent_y].x, 1.55f, teststage[teststage[x, z].parent_x, teststage[x, z].parent_y].z);
            walk_flg = false;


        }

        if (!walk_flg)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, move_speed / 20);
            Couple_Anm.SetBool("walk", true);
            Vector3 diff = transform.position - target;
            if (diff.magnitude > 0.01f) //ベクトルの長さが0.01fより大きい場合にプレイヤーの向きを変える処理を入れる(0では入れないので）
            {
                transform.rotation = Quaternion.LookRotation(diff);  //ベクトルの情報をQuaternion.LookRotationに引き渡し回転量を取得しプレイヤーを回転させる
                float step = 5 * Time.deltaTime;
                //Debug.Log((float)now);
                

               // Couple_Model.transform.rotation = Quaternion.Slerp(Couple_Model.transform.rotation, Quaternion.Euler(0, (float)now * 90.0f, 0), step);


            }
        }
    }

    //何かにぶつかったとき
    void OnTriggerEnter(Collider it)
    {
        //トラップ
        if (it.transform.tag == "Trap")
        {
            if (it.transform.gameObject.layer == 12 && Red_Player == true)
            {
                Sound.PlaySe("Hit_Trap");
                Stan_flg = true;
                coal_flg = false;
                CreateStage();
                it.GetComponent<Trap>().Delete_Function();
                Destroy(it.gameObject);
            }
            else if (it.transform.gameObject.layer == 13 && Red_Player == false)
            {
                Sound.PlaySe("Hit_Trap");
                Stan_flg = true;
                coal_flg = false;
                CreateStage();
                it.GetComponent<Trap>().Delete_Function();
                Destroy(it.gameObject);
            }
        }
        else if (it.transform.gameObject.layer == 8 && Red_Player == false && Stan_flg)
        {
            it.GetComponent<PL>().Set_Time(120);

        }
        else if (it.transform.gameObject.layer == 9 && Red_Player == true && Stan_flg)
        {
            it.GetComponent<PL>().Set_Time(120);

        }

        if (it.transform.tag == "Restart_Shot")
        {
            Sound.PlaySe("Hit_Item");
            Re_Start();
            Destroy(it.gameObject);
        }

        if (it.transform.tag == "RedGoal" && Red_Player == true)
        {
            GameSystemManager.RedWin();
        }
        else if (it.transform.tag == "BlueGoal" && Red_Player == false)
        {
            GameSystemManager.BlueWin();
        }



    }

    void OnTriggerStay(Collider it)
    {

        if (it.transform.gameObject.layer == 8 && Red_Player == false && Stan_flg)
        {
            if (heal_Time == 120)
            {
                Stan_flg = false;
                heal_Time = 0;
                it.GetComponent<PL>().End_Time();
            }
            else
            {
                it.GetComponent<PL>().Update_Time();
                heal_Time++;
            }
        }
        else if (it.transform.gameObject.layer == 9 && Red_Player == true && Stan_flg)
        {
            if (heal_Time == 120)
            {
                Stan_flg = false;
                flash_mesh.enabled = true;
                heal_Time = 0;
                it.GetComponent<PL>().End_Time();
            }
            else
            {
                it.GetComponent<PL>().Update_Time();
                heal_Time++;
            }
        }
    }

    void OnTriggerRelease(Collider it)
    {
        Debug.Log(it);
        if (it.transform.gameObject.layer == 8 && Red_Player == false && !Stan_flg)
        {
            it.GetComponent<PL>().End_Time();
        }
        else if (it.transform.gameObject.layer == 9 && Red_Player == true && !Stan_flg)
        {
            it.GetComponent<PL>().End_Time();
        }
    }
    void Re_Start()
    {
        transform.position = start_pos;
        Set_bar.SetSlider(transform.position.x, Red_Player);

        coal_flg = false;
        walk_flg = true;
    }

    public void SetUp(int x, int z)
    {
        transform.position = new Vector3(1.5f * z, 1.55f, -1.5f * x);
        start_pos = transform.position;
    }

}
