using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//���己 ������ ���� ������ ��ü �ּ�ó����

public class Map_Editor : EditorWindow
{
    #region Variable
    Object[] TileList; //�ҷ��� Ÿ�� ������Ʈ �����迭
    int choice = 0; //������ Ÿ���� ��ȣ
    GameObject select_tile; //������ Ÿ�� ���ӿ�����Ʈ
    Stack<GameObject> created_tile; //������ Ÿ�ϵ��� �������� ���� �ڷΰ��� ����� ���ؼ� ������ ���
    GameObject maplist; //�θ� ���ӿ�����Ʈ ã��


    string[] button_list = { "create", "back", "skip", "up", "reset" }; //��ư���� ����� ���ڿ�

    float offset_x = 0.0f; //x���� �⺻��ġ
    float offset_y = 0.0f; //y���� �⺻��ġ
    #endregion

    private static Map_Editor instance; //�̱��� ������ ���� �ν��Ͻ� ����
    public static Map_Editor Getinstance { get { return instance; } }
    private void Awake()
    {
        instance = this; //�ν��Ͻ� ����
        created_tile = new Stack<GameObject>(); //Ÿ�� ������ ���� ���� ����
        set_tile_in_stack(); //�Լ��۵�
    }

    private void OnFocus() //������ â�� Ŭ��������� �۵�, ���� ������ â�� ����ٰ� �۾����ϸ� Ÿ���� �ν��� ���ؼ� �������
    {
        if (created_tile == null) //Ÿ�Ϲ迭�� ������ٸ� �۵�
        {
            created_tile = new Stack<GameObject>(); //������ ���μ���
            set_tile_in_stack(); //�Լ��۵�
        }
    }

    [MenuItem("MapEditor/MapEditor")] //������ ��ܿ� ����
    static void showWindow()
    {
        GetWindow(typeof(Map_Editor)).Show(); //������ â ����
    }

    void ViewTile()
    {
        Texture[] thumnail = new Texture[TileList.Length]; //����� ������ �ҷ��� Ÿ�ϰ�����ŭ ����

        for (int i = 0; i < TileList.Length; i++) //�ݺ����� �̿��Ͽ� �̸����� ������� ����
            thumnail[i] = AssetPreview.GetAssetPreview(TileList[i]); //����Ƽ ������Ʈâ���� ���̴� �̸����⸦ ����ϴ�.

        choice = GUILayout.SelectionGrid(choice, thumnail, 4, GUILayout.MaxHeight(150.0f), GUILayout.MaxWidth(500.0f)); //�簢���� Ʋ�� �̿��Ͽ� �̸����⸦ �����ݴϴ�.
        select_tile = (GameObject)TileList[choice]; //������ ��ȣ�� ������ ���ӿ�����Ʈ�� ����
    }
    private void OnGUI()
    {
        TileList = Resources.LoadAll<Object>("Tile"); //�������� ������Ʈ�� �ҷ���
        ViewTile(); //������Ʈ�� �����ִ� �Լ� �۵�

        EditorGUILayout.Space(50.0f); //ui������ ���� ����

        #region Set_Button

        EditorGUILayout.BeginHorizontal(); //����
        for (int i = 0; i < button_list.Length; i++) //������ ��ư�� ����ŭ �ݺ�
        {
            if (GUILayout.Button(button_list[i], GUILayout.MaxHeight(50.0f), GUILayout.MaxWidth(100.0f))) //��ư�� ũ�� �� ���ڿ��� ���ϰ� ����
            {
                //������ư�� ��ȣ�� ���� �۵��մϴ�.
                switch (i)
                {
                    case 0:
                        Create();
                        break;

                    case 1:
                        Back();
                        break;

                    case 2:
                        offset_x += 5.0f;
                        break;

                    case 3:
                        offset_y = 2.0f;
                        break;
                    case 4:
                        offset_x = 0.0f;
                        offset_y = 0.0f;
                        break;
                }
            }
        }
        EditorGUILayout.EndHorizontal(); //���� ����

        #endregion

        EditorGUILayout.Space(50.0f);

        //�����ϴ� �ڵ�
        #region Set_Offset 
        GUILayout.BeginHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("offset_X");
        offset_x = EditorGUILayout.FloatField(offset_x, GUILayout.MaxWidth(150.0f));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("offset_Y");
        offset_y = EditorGUILayout.FloatField(offset_y, GUILayout.MaxWidth(150.0f));
        EditorGUILayout.EndHorizontal();
        GUILayout.EndHorizontal();
        #endregion
    }

    void Create() //�����ϴ� �Լ�
    {
        if (GameObject.Find("map") == null) //���� �θ������Ʈ�� ���ٸ� �������ݴϴ�.
            new GameObject("map");

        maplist = GameObject.Find("map"); //���� �ִٸ� ã�Ƽ� �Է��մϴ�.

        GameObject Tile = Instantiate(select_tile); //������ ���ӿ�����Ʈ�� �����մϴ�.
        created_tile.Push(Tile); //���ÿ� Ÿ���� �Է��մϴ�.
        Tile.transform.parent = maplist.transform; //���ӿ�����Ʈ�� �θ� �����մϴ�.

        Tile.transform.position = new Vector3(offset_x, offset_y, 0.0f); //���ӿ�����Ʈ�� ��ġ�� �����մϴ�
        Tile.transform.rotation = Quaternion.Euler(0f, 90f, 0f); //���ӿ�����Ʈ�� ������ �����մϴ�.

        offset_x += 5.0f; //�����Ǿ����� ���� ������ ��ġ�� �̵��մϴ�.
    }

    void Back() //�߸������� Ÿ���� ��� �ǵ����� �Լ�
    {
        if (created_tile.Count == 0) //������ ������� �۵������ʽ��ϴ�.
            return;

        Vector3 last_Pos = created_tile.Peek().transform.position; //������ ������ ��ġ�� �����ɴϴ�.
        //������ ������ ��ġ�� ������ ��ġ�� �����մϴ�.
        offset_x = last_Pos.x;
        offset_y = last_Pos.y;
        
        //������ ������ ���ӿ�����Ʈ�� �����մϴ�.
        DestroyImmediate(created_tile.Peek());
        created_tile.Pop(); //���ÿ��� �������� �����ߴ� ���ӿ�����Ʈ�� �����մϴ�.
    }

    void set_tile_in_stack() //���� �����Ǿ������� ������ ������ �������� ����ϴ� �Լ��Դϴ�.
    {
        for (int i = 0; i < maplist.transform.childCount; i++) //������ ���ӿ�����Ʈ�� maplist�� �ڽ����� �־��⶧���� �ڽĵ��� ����ŭ �ҷ��ɴϴ�.
            created_tile.Push(maplist.transform.GetChild(i).gameObject); //���ÿ� �ݺ��Ͽ� �Է��մϴ�.
    }
}
