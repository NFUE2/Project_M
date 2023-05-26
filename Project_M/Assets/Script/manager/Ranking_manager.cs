using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using MySql.Data.MySqlClient;
using TMPro;

public class Ranking_manager : Manager
{
    public GameObject scorelist; //랭킹 목록
    public GameObject input_list; //입력하는 UI들의 부모

    public GameObject[] rankinglist; //랭킹리스트를 출력할 게임오브젝트
    public GameObject[] input_field; //랭킹을 입력할때 사용하는 UI들

    string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; //닉네임을 정할때 사용하는 알파벳
    string setting_nickname = ""; //설정된 닉네임

    int enter_cnt = 0; //엔터를 누른 횟수
    int alpha_cnt = 0; //현재 알파벳의 숫자

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
            sqlconn = new MySqlConnection(strconn); //SQL연결
        }
        catch (System.Exception e) //연결실패시 작동
        {
            Debug.Log(e.ToString());
        }
    }

    public override void Manager_Update()
    {
        if (setting_nickname.Length < 3) //만약 닉네임이 3글자 이하라면 닉네임 설정하러감
            Set_Nickname();
        else  
            view_rank(); //아닐경우 랭킹리스트를 보여줌
    }

    void Set_Nickname() //닉네임 설정하기
    {
        //3글자가 되기전까지 닉네임을 정하는 함수,좌측부터 닉네임한글자씩 정할수 있으며 엔터를 누르면 우측으로 한칸씩 이동
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            alpha_cnt = 0;
            setting_nickname += input_field[enter_cnt].GetComponent<TextMeshProUGUI>().text;
            input_field[enter_cnt + 3].GetComponent<Animator>().enabled = false;
            input_field[enter_cnt + 3].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
            enter_cnt++;

            if (enter_cnt == 3) //3글자가 되었을경우 입력하는 창을 감춘다.
            {
                insert_rank(setting_nickname);
                input_list.SetActive(false);
            }
            else
                input_field[enter_cnt + 3].GetComponent<Animator>().enabled = true; //현재 내가 입력하고있는 글자의 위치를 알수있게 애니메이션 작동
        }

        //방향키의 입력에따라 알파벳이 바뀜
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

        input_field[enter_cnt].GetComponent<TextMeshProUGUI>().text = alphabet[alpha_cnt].ToString(); //현재 설정하고있는 알파벳이 무엇인지 보여줌
    }

    //게임이 끝나면 스코어를 데이터 베이스에 저장하기위한 함수
    void insert_rank(string nickname) //가제
    {
        try
        {
            if (sqlconn.State == ConnectionState.Open) //SQL이 계속해서 열려있는 버그가 발생하여 작성,만약 서버가 열려있다면 닫아줍니다.
                sqlconn.Close();

            //쿼리문 작성
            string query = "insert into ranking(nickname,score) value('" + nickname + "'," +GameManager.instance.P_score.ToString() + ");";

            //현재 연결된 서버에,커맨드 입력
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
            scorelist.SetActive(true); //랭킹을 보여줍니다.

            if (sqlconn.State == ConnectionState.Open) //버그로인한 작성
                sqlconn.Close();

            sqlconn.Open();
            string query = "select * from ranking order by score desc"; //쿼리문
            MySqlCommand cmd = new MySqlCommand(query, sqlconn); //커맨드 생성

            MySqlDataReader reader = cmd.ExecuteReader(); //쿼리로 얻어온 정보를 가져옵니다.

            int i = 0; //횟수를 카운트합니다.

            while(reader.Read())
            {
                if (i == 10) break; //만약 랭킹을 보여주는 횟수가 10이 넘으면 반복문 빠져나감

                //쿼리문으로 얻어온 정보들의 닉네임과 스코어를 가져옵니다.
                rankinglist[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = reader[1].ToString(); 
                rankinglist[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = reader[2].ToString();
                rankinglist[i].SetActive(true); //정보가 있을경우 출력합니다.
                i++;
            }
            sqlconn.Close();

            GameManager.instance.P_game_end = true; //게임이 완벽하게 끝났다고 설정합니다.
        }
        catch(System.Exception e) //오류시 작동
        {
            Debug.Log(e.ToString());
        }
    }
}
