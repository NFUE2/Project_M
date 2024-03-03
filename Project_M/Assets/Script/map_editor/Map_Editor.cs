using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//빌드런 에러로 인한 에디터 전체 주석처리함

public class Map_Editor : EditorWindow
{
    #region Variable
    Object[] TileList; //불러올 타일 오브젝트 변수배열
    int choice = 0; //선택한 타일의 번호
    GameObject select_tile; //선택한 타일 게임오브젝트
    Stack<GameObject> created_tile; //생성한 타일들을 스택으로 담음 뒤로가기 기능을 위해서 스택을 사용
    GameObject maplist; //부모 게임오브젝트 찾기


    string[] button_list = { "create", "back", "skip", "up", "reset" }; //버튼으로 사용할 문자열

    float offset_x = 0.0f; //x값의 기본위치
    float offset_y = 0.0f; //y값의 기본위치
    #endregion

    private static Map_Editor instance; //싱글톤 패턴을 위한 인스턴스 생성
    public static Map_Editor Getinstance { get { return instance; } }
    private void Awake()
    {
        instance = this; //인스턴스 생성
        created_tile = new Stack<GameObject>(); //타일 관리를 위한 스택 선언
        set_tile_in_stack(); //함수작동
    }

    private void OnFocus() //윈도우 창을 클릭했을경우 작동, 가끔 윈도우 창을 벗어났다가 작업을하면 타일을 인식을 못해서 사용했음
    {
        if (created_tile == null) //타일배열이 사라졌다면 작동
        {
            created_tile = new Stack<GameObject>(); //스택을 새로선언
            set_tile_in_stack(); //함수작동
        }
    }

    [MenuItem("MapEditor/MapEditor")] //윈도우 상단에 생성
    static void showWindow()
    {
        GetWindow(typeof(Map_Editor)).Show(); //윈도우 창 생성
    }

    void ViewTile()
    {
        Texture[] thumnail = new Texture[TileList.Length]; //썸네일 사진을 불러올 타일갯수만큼 선언

        for (int i = 0; i < TileList.Length; i++) //반복문을 이용하여 미리보기 썸네일을 생성
            thumnail[i] = AssetPreview.GetAssetPreview(TileList[i]); //유니티 프로젝트창에서 보이는 미리보기를 담습니다.

        choice = GUILayout.SelectionGrid(choice, thumnail, 4, GUILayout.MaxHeight(150.0f), GUILayout.MaxWidth(500.0f)); //사각형의 틀을 이용하여 미리보기를 보여줍니다.
        select_tile = (GameObject)TileList[choice]; //선택한 번호를 생성할 게임오브젝트로 저장
    }
    private void OnGUI()
    {
        TileList = Resources.LoadAll<Object>("Tile"); //폴더에서 오브젝트를 불러옴
        ViewTile(); //오브젝트를 보여주는 함수 작동

        EditorGUILayout.Space(50.0f); //ui사이의 공간 생성

        #region Set_Button

        EditorGUILayout.BeginHorizontal(); //정렬
        for (int i = 0; i < button_list.Length; i++) //생성할 버튼의 수만큼 반복
        {
            if (GUILayout.Button(button_list[i], GUILayout.MaxHeight(50.0f), GUILayout.MaxWidth(100.0f))) //버튼의 크기 및 문자열을 정하고 생성
            {
                //누른버튼의 번호에 따라 작동합니다.
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
        EditorGUILayout.EndHorizontal(); //정렬 종료

        #endregion

        EditorGUILayout.Space(50.0f);

        //정렬하는 코드
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

    void Create() //생성하는 함수
    {
        if (GameObject.Find("map") == null) //만약 부모오브젝트가 없다면 생성해줍니다.
            new GameObject("map");

        maplist = GameObject.Find("map"); //만약 있다면 찾아서 입력합니다.

        GameObject Tile = Instantiate(select_tile); //선택한 게임오브젝트를 복사합니다.
        created_tile.Push(Tile); //스택에 타일을 입력합니다.
        Tile.transform.parent = maplist.transform; //게임오브젝트의 부모를 설정합니다.

        Tile.transform.position = new Vector3(offset_x, offset_y, 0.0f); //게임오브젝트의 위치를 설정합니다
        Tile.transform.rotation = Quaternion.Euler(0f, 90f, 0f); //게임오브젝트의 각도를 조정합니다.

        offset_x += 5.0f; //생성되었으면 다음 생성할 위치로 이동합니다.
    }

    void Back() //잘못생성한 타일의 경우 되돌리는 함수
    {
        if (created_tile.Count == 0) //스택이 비었으면 작동하지않습니다.
            return;

        Vector3 last_Pos = created_tile.Peek().transform.position; //마지막 생성한 위치를 가져옵니다.
        //마지막 생성한 위치를 생성할 위치로 저장합니다.
        offset_x = last_Pos.x;
        offset_y = last_Pos.y;
        
        //마지막 생성한 게임오브젝트를 삭제합니다.
        DestroyImmediate(created_tile.Peek());
        created_tile.Pop(); //스택에서 마지막에 생성했던 게임오브젝트를 삭제합니다.
    }

    void set_tile_in_stack() //맵이 생성되어있을때 스택의 정보가 사라질경우 사용하는 함수입니다.
    {
        for (int i = 0; i < maplist.transform.childCount; i++) //생성한 게임오브젝트를 maplist의 자식으로 넣었기때문에 자식들의 수만큼 불러옵니다.
            created_tile.Push(maplist.transform.GetChild(i).gameObject); //스택에 반복하여 입력합니다.
    }
}
