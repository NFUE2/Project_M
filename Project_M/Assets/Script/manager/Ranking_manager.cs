using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using MySql.Data.MySqlClient;
using TMPro;

public class Ranking_manager : Manager
{
    public GameObject[] rankinglist;
    public GameObject[] input_field;

    string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    string set_nickname = "";

    int enter_cnt = 0;
    int alpha_cnt = 0;


    #region SQL
    public static MySqlConnection sqlconn;
    static string ipAdress = "Server = 127.0.0.1;";
    static string db_name = "Database = project;";
    static string db_port = "Port = 3307;";
    static string db_id = "Uid = admin;";
    static string db_pw = "Pwd = 1234;";

    string strconn = ipAdress + db_name + db_port + db_id + db_pw;
    #endregion

    public override void Manager_Start()
    {
        try
        {
            sqlconn = new MySqlConnection(strconn);
            view_rank();
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    public override void Manager_Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            alpha_cnt = 0;
            set_nickname += input_field[enter_cnt].GetComponent<TextMeshProUGUI>().text;
            enter_cnt++;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            input_field[enter_cnt].GetComponent<TextMeshProUGUI>().text = alphabet[alpha_cnt].ToString();
        }
        else if(Input.GetKeyDown(KeyCode.DownArrow))
        {

        }

    }

    //������ ������ ���ھ ������ ���̽��� �����ϱ����� �Լ�
    void insert_rank(string nickname) //����
    {
        string query = "insert into ranking(nickname,score) value(" + nickname + GameManager.instance.P_score.ToString() + ");";
        MySqlCommand cmd = new MySqlCommand(query,sqlconn);

        sqlconn.Open();
        cmd.ExecuteNonQuery();
        sqlconn.Close();
    }

    //���� ��ŷ�� �����ֱ� ���� �Լ�
    void view_rank()
    {
        try
        {
            sqlconn.Open();

            string query = "select * from ranking order by score desc";
            MySqlCommand cmd = new MySqlCommand(query, sqlconn);

            MySqlDataReader reader = cmd.ExecuteReader();

            int i = 0;
            while(reader.Read())
            {
                if (i == 10) break;

                rankinglist[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = reader[1].ToString();
                rankinglist[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = reader[2].ToString();
                rankinglist[i].SetActive(true);
                i++;
                //Debug.Log(string.Format("{0},{1},{2}", reader[0], reader[1], reader[2]));
            }

            sqlconn.Close();
        }
        catch(System.Exception e)
        {
            Debug.Log("����");
        }
    }

    

    
}
