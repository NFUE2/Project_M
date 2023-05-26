using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Data;
using MySql.Data.MySqlClient;
using TMPro;

public class Ranking_manager : Manager
{
    public GameObject scorelist; //��ŷ ���
    public GameObject input_list; //�Է��ϴ� UI���� �θ�

    public GameObject[] rankinglist; //��ŷ����Ʈ�� ����� ���ӿ�����Ʈ
    public GameObject[] input_field; //��ŷ�� �Է��Ҷ� ����ϴ� UI��

    string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; //�г����� ���Ҷ� ����ϴ� ���ĺ�
    string setting_nickname = ""; //������ �г���

    int enter_cnt = 0; //���͸� ���� Ƚ��
    int alpha_cnt = 0; //���� ���ĺ��� ����

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
            sqlconn = new MySqlConnection(strconn); //SQL����
        }
        catch (System.Exception e) //������н� �۵�
        {
            Debug.Log(e.ToString());
        }
    }

    public override void Manager_Update()
    {
        if (setting_nickname.Length < 3) //���� �г����� 3���� ���϶�� �г��� �����Ϸ���
            Set_Nickname();
        else  
            view_rank(); //�ƴҰ�� ��ŷ����Ʈ�� ������
    }

    void Set_Nickname() //�г��� �����ϱ�
    {
        //3���ڰ� �Ǳ������� �г����� ���ϴ� �Լ�,�������� �г����ѱ��ھ� ���Ҽ� ������ ���͸� ������ �������� ��ĭ�� �̵�
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            alpha_cnt = 0;
            setting_nickname += input_field[enter_cnt].GetComponent<TextMeshProUGUI>().text;
            input_field[enter_cnt + 3].GetComponent<Animator>().enabled = false;
            input_field[enter_cnt + 3].GetComponent<TextMeshProUGUI>().color = new Color(1, 1, 1, 1);
            enter_cnt++;

            if (enter_cnt == 3) //3���ڰ� �Ǿ������ �Է��ϴ� â�� �����.
            {
                insert_rank(setting_nickname);
                input_list.SetActive(false);
            }
            else
                input_field[enter_cnt + 3].GetComponent<Animator>().enabled = true; //���� ���� �Է��ϰ��ִ� ������ ��ġ�� �˼��ְ� �ִϸ��̼� �۵�
        }

        //����Ű�� �Է¿����� ���ĺ��� �ٲ�
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

        input_field[enter_cnt].GetComponent<TextMeshProUGUI>().text = alphabet[alpha_cnt].ToString(); //���� �����ϰ��ִ� ���ĺ��� �������� ������
    }

    //������ ������ ���ھ ������ ���̽��� �����ϱ����� �Լ�
    void insert_rank(string nickname) //����
    {
        try
        {
            if (sqlconn.State == ConnectionState.Open) //SQL�� ����ؼ� �����ִ� ���װ� �߻��Ͽ� �ۼ�,���� ������ �����ִٸ� �ݾ��ݴϴ�.
                sqlconn.Close();

            //������ �ۼ�
            string query = "insert into ranking(nickname,score) value('" + nickname + "'," +GameManager.instance.P_score.ToString() + ");";

            //���� ����� ������,Ŀ�ǵ� �Է�
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

    //���� ��ŷ�� �����ֱ� ���� �Լ�
    void view_rank()
    {
        try
        {
            scorelist.SetActive(true); //��ŷ�� �����ݴϴ�.

            if (sqlconn.State == ConnectionState.Open) //���׷����� �ۼ�
                sqlconn.Close();

            sqlconn.Open();
            string query = "select * from ranking order by score desc"; //������
            MySqlCommand cmd = new MySqlCommand(query, sqlconn); //Ŀ�ǵ� ����

            MySqlDataReader reader = cmd.ExecuteReader(); //������ ���� ������ �����ɴϴ�.

            int i = 0; //Ƚ���� ī��Ʈ�մϴ�.

            while(reader.Read())
            {
                if (i == 10) break; //���� ��ŷ�� �����ִ� Ƚ���� 10�� ������ �ݺ��� ��������

                //���������� ���� �������� �г��Ӱ� ���ھ �����ɴϴ�.
                rankinglist[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = reader[1].ToString(); 
                rankinglist[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = reader[2].ToString();
                rankinglist[i].SetActive(true); //������ ������� ����մϴ�.
                i++;
            }
            sqlconn.Close();

            GameManager.instance.P_game_end = true; //������ �Ϻ��ϰ� �����ٰ� �����մϴ�.
        }
        catch(System.Exception e) //������ �۵�
        {
            Debug.Log(e.ToString());
        }
    }
}
