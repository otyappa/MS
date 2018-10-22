using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Couple : MonoBehaviour {
    Vector3 goal;
    Vector3 Goal_Direction;
    GameSystem GameSystemManager;
    bool coal_flg;
    bool Stan_flg=false;
    bool walk_flg=true;
    Vector3 target;
    public bool Red_Player=false;
    public GameObject Goal_Point;
    public float move_speed=1;
    int heal_Time=0;
    Vector3 start_pos;
    struct stage
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
    stage[,] teststage = new stage[6, 11];
    bool coll;
	// Use this for initialization
	void Start () {
        CreateStage();
        GameSystemManager = GameObject.Find("GameSystemManager").GetComponent<GameSystem>();
        start_pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameSystemManager.Get_GameSet())
        {
            if (Stan_flg==true)
            {

            }else if (coal_flg == true)
            {
                Move();
            }
        }
    }

    public void root_serch(Transform Goal)
    {
        if (Stan_flg == false)
        {
            CreateStage();

            goal = Goal.position;
            int g_x = (int)((Goal.transform.position.z-0.75f) / -1.5f);
            int g_z = (int)((Goal.transform.position.x + 0.75f) / 1.5f);
            int t_x = (int)((transform.position.z) / -1.5f);
            int t_z = (int)((transform.position.x) / 1.5f);
            Debug.Log("x:");
            Debug.Log(g_x);
            Debug.Log("y:");
            Debug.Log(g_z);
            teststage[g_x, g_z].status = 2;
            teststage[g_x, g_z].jitucost = 0;
            teststage[g_x, g_z].suiteicost = (t_x - g_x) + (t_z - g_z);
            teststage[g_x, g_z].score = teststage[g_x, g_z].jitucost + teststage[g_x, g_z].suiteicost;
            teststage[g_x, g_z].parent_x = 99;
            if (g_x < 5)
            {
                if (teststage[g_x + 1, g_z].status == 0)
                {
                    Debug.Log("buttom");
                    teststage[g_x + 1, g_z].status = 0;
                    teststage[g_x + 1, g_z].parent_x = g_x;
                    teststage[g_x + 1, g_z].parent_y = g_z;
                    A_Star(1, g_x + 1, g_z);

                }

            }
            if (g_z < 10)
            {
                if (teststage[g_x, g_z + 1].status == 0)
                {
                    Debug.Log("right");
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
                    Debug.Log("top");
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
                    Debug.Log("left");
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

                for (int i = 0; i < 6; i++)
                {
                    for (int j = 0; j < 11; j++)
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
                //teststage[min_x, min_y].parent_x = parent_x;
                //teststage[min_x, min_y].parent_y = parent_y;
                parent_x = min_x;
                parent_y = min_y;
                  Debug.Log("x:");
                  Debug.Log(min_x);
                  Debug.Log("y:");
                  Debug.Log(min_y);
                Debug.Log("score");
                Debug.Log(teststage[min_x, min_y].jitucost);
                if (min_x == t_x && min_y == t_z)
                {
                    coal_flg = true;
                    Debug.Log("END");
                    break;
                }
                count++;
                if (min_x < 5)
                {

                    if (teststage[min_x+1, min_y].status == 0)
                    {
                        //teststage[g_x + 1, g_z].status = 1;
                        teststage[min_x + 1, min_y].parent_x = min_x;
                        teststage[min_x + 1, min_y].parent_y = min_y;
                        A_Star(count, min_x + 1, min_y);
                    }
                }
                if (min_y < 10)
                {

                    if (teststage[min_x, min_y + 1].status == 0)
                    {
                        //teststage[g_x, g_z + 1].status = 1;
                        teststage[min_x, min_y + 1].parent_x = min_x;
                        teststage[min_x, min_y + 1].parent_y = min_y;
                        A_Star(count, min_x, min_y + 1);
                    }
                }
                if (min_x > 0)
                {

                    if (teststage[min_x - 1, min_y].status == 0)
                    {
                        //teststage[g_x - 1, g_z].status = 1;
                        teststage[min_x - 1, min_y].parent_x = min_x;
                        teststage[min_x - 1, min_y].parent_y = min_y;
                        A_Star(count, min_x - 1, min_y);
                    }
                }
                if (min_y > 0)
                {

                    if (teststage[min_x, min_y-1].status == 0)
                    {
                        // teststage[g_x, g_z - 1].status = 1;
                        teststage[min_x, min_y - 1].parent_x = min_x;
                        teststage[min_x, min_y - 1].parent_y = min_y;
                        A_Star(count, min_x, min_y - 1);
                    }
                }


                if (count > 66)
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
        // int count = 0;
       // Debug.Log("fuuuuu");
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
    void fool()
    {
        //Vector3 Move_Transform = this.transform.position;

        //Move_Transform += Goal_Direction;
        //this.transform.position = Move_Transform;
        
    }
    void CreateStage()
    {
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

        }

    }

    void Move()
    {

      
        if (walk_flg)
        {
            int x = (int)((transform.position.z) / -1.5f);
            int z = (int)((transform.position.x) / 1.5f);
            // Debug.Log(teststage[x, z].parent_x);
            // Debug.Log(teststage[x, z].parent_y);
            Debug.Log(teststage[5, 9].parent_x);
            Debug.Log(teststage[5, 9].parent_y);
            if (teststage[x, z].parent_x == 99)
            {
                if (transform.position == Goal_Point.transform.position)
                {
                    if (Red_Player == true)
                    {
                        GameSystemManager.RedWin();
                    }
                    else
                    {
                        GameSystemManager.BlueWin();
                    }
                }
                coal_flg = false;
                return;
            }
            target = new Vector3(teststage[teststage[x, z].parent_x, teststage[x, z].parent_y].x, 1.55f, teststage[teststage[x, z].parent_x, teststage[x, z].parent_y].z);
            walk_flg = false;
        }
        //Debug.Log(Vector3.MoveTowards(transform.position, target, 0.01f));
        if (Vector3.MoveTowards(transform.position, target, move_speed/20)==target)
        {
            walk_flg = true;
           
        }
        transform.position= Vector3.MoveTowards(transform.position, target,move_speed/20);
    }

    //何かにぶつかったとき
    void OnTriggerEnter(Collider it)
    {
        // Debug.Log("hit");
        // Debug.Log(it.transform.tag);
        // Debug.Log(it.transform.gameObject.layer);
        //トラップ
        if (it.transform.tag == "Trap")
        {
            if (it.transform.gameObject.layer == 12 && Red_Player == true)
            {
                Stan_flg = true;
                coal_flg = false;
                CreateStage();
                it.GetComponent<Trap>().Delete_Function();
                Destroy(it.gameObject);
            }
            else if (it.transform.gameObject.layer == 13 && Red_Player == false)
            {
                Stan_flg = true;
                coal_flg = false;
                CreateStage();
                it.GetComponent<Trap>().Delete_Function();
                Destroy(it.gameObject);
            }
        }
        else if (it.transform.gameObject.layer == 8 && Red_Player == false && Stan_flg)
        {
            Debug.Log("test");
            it.GetComponent<PL>().Set_Time(120);

        }
        else if (it.transform.gameObject.layer == 9 && Red_Player == true && Stan_flg)
        {
            Debug.Log("test");
            it.GetComponent<PL>().Set_Time(120);

        }

        if (it.transform.tag == "Restart_Shot")
        {
            Re_Start();
            Destroy(it.gameObject);
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
        coal_flg = false;
    }
}
