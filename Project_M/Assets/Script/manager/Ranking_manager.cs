using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using MySql.Data.MySqlClient;
using TMPro;

public class Ranking_manager : Manager
{
    public GameObject scorelist;
    public GameObject input_list;

    public GameObject[] rankinglist;
    public GameObject[] input_field;

    string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    string setting_nickname = "";

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
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    public override void Manager_Update()
    {
        if (setting_nickname.Length < 3)
            Set_Nickname();
        else
            view_rank();
    }

    void Set_Nickname()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            alpha_cnt = 0;
            setting_nickname += input_field[enter_cnt].GetComponent<TextMeshProUGUI>().text;
            input_field[enter_cnt + 3].GetComponent<Animator>().enabled = false;
            input_field[enter_cnt + 3].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
            enter_cnt++;

            if (enter_cnt == 3)
            {
                insert_rank(setting_nickname);
                input_list.SetActive(false);
            }
            else
                input_field[enter_cnt + 3].GetComponent<Animator>().enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            alpha_cnt++;
            if (alpha_cnt > 25)
                alpha_cnt = 0;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            alpha_cnt--;
            if (alpha_cnt < 0)
                alpha_cnt = 25;
        }
        input_field[enter_cnt].GetComponent<TextMeshProUGUI>().text = alphabet[alpha_cnt].ToString();
    }

    //게임이 끝나면 스코어를 데이터 베이스에 저장하기위한 함수
    void insert_rank(string nickname) //가제
    {
        try
        {
            if (sqlconn.State == ConnectionState.Open)
                sqlconn.Close();

            string query = "insert into ranking(nickname,score) value('" + nickname + "'," +GameManager.instance.P_score.ToString() + ");";


            MySqlCommand cmd = new MySqlCommand(query,sqlconn);

            sqlconn.Open();
            cmd.ExecuteNonQuery();
            sqlconn.Close();

        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
        }
    }

    //현재 랭킹을 보여주기 위한 함수
    void view_rank()
    {
        try
        {
            scorelist.SetActive(true);

            if (sqlconn.State == ConnectionState.Open)
                sqlconn.Close();

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
            }
            sqlconn.Close();
            GameManager.instance.P_game_end = true;
        }
        catch(System.Exception e)
        {
            Debug.Log(e.ToString());
        }
    }
}
