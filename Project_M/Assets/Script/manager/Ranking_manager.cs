using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using MySql.Data.MySqlClient;
using TMPro;

public class Ranking_manager : MonoBehaviour
{
    public GameObject[] rankinglist;


    #region SQL
    public static MySqlConnection sqlconn;
    static string ipAdress = "Server = 127.0.0.1;";
    static string db_name = "Database = project;";
    static string db_port = "Port = 3307;";
    static string db_id = "Uid = admin;";
    static string db_pw = "Pwd = 1234;";

    string strconn = ipAdress + db_name + db_port + db_id + db_pw;
    #endregion

    private void Start()
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

    //게임이 끝나면 스코어를 데이터 베이스에 저장하기위한 함수
    void insert_rank(string nickname) //가제
    {
        string query = "insert into ranking(nickname,score) value(" + nickname + GameManager.instance.P_score.ToString() + ");";
        MySqlCommand cmd = new MySqlCommand(query,sqlconn);

        sqlconn.Open();
        cmd.ExecuteNonQuery();
        sqlconn.Close();
    }

    //현재 랭킹을 보여주기 위한 함수
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
            Debug.Log("에러");
        }
    }

}
