using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Map_Editor : EditorWindow
{
    #region Variable
    Object[] TileList;
    int choice = 0;
    GameObject select_tile;
    Stack<GameObject> created_tile;
    GameObject maplist;

    string[] button_list = { "create", "back", "skip","up","reset" };

    float offset_x = 0.0f;
    float offset_y = 0.0f;
    #endregion

    private static Map_Editor instance;
    public static Map_Editor Getinstance { get { return instance; } }
    private void Awake()
    {
        instance = this;
        created_tile = new Stack<GameObject>();
        set_tile_in_stack();
    }

    private void OnFocus()
    {
        if(created_tile == null)
        {
            created_tile = new Stack<GameObject>();
            set_tile_in_stack();
        }
    }

    [MenuItem("MapEditor/MapEditor")]
    static void showWindow()
    {
        GetWindow(typeof(Map_Editor)).Show();
    }

    void ViewTile()
    {
        Texture[] thumnail = new Texture[TileList.Length];

        for (int i = 0; i < TileList.Length; i++)
            thumnail[i] = AssetPreview.GetAssetPreview(TileList[i]);

        choice = GUILayout.SelectionGrid(choice, thumnail, 4, GUILayout.MaxHeight(150.0f), GUILayout.MaxWidth(500.0f));
        select_tile = (GameObject)TileList[choice];
    }
    private void OnGUI()
    {
        TileList = Resources.LoadAll<Object>("Tile");
        ViewTile();

        EditorGUILayout.Space(50.0f);

        #region Set_Button

        EditorGUILayout.BeginHorizontal();
        for (int i = 0; i < button_list.Length; i++)
        {
            if(GUILayout.Button(button_list[i],GUILayout.MaxHeight(50.0f),GUILayout.MaxWidth(100.0f)))
            {
                switch(i)
                {
                    case 0 :
                        Create();
                        break;

                    case 1 :
                        Back();
                        break;

                    case 2 :
                        offset_x += 5.0f;
                        break;

                    case 3 :
                        offset_y = 2.0f;
                        break;
                    case 4 :
                        offset_x = 0.0f;
                        offset_y = 0.0f;
                        break;
                }
            }
        }
        EditorGUILayout.EndHorizontal();

        #endregion

        EditorGUILayout.Space(50.0f);

        #region Set_Offset
        GUILayout.BeginHorizontal();
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("offset_X");
        offset_x = EditorGUILayout.FloatField(offset_x,GUILayout.MaxWidth(150.0f));
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("offset_Y");
        offset_y = EditorGUILayout.FloatField(offset_y, GUILayout.MaxWidth(150.0f));
        EditorGUILayout.EndHorizontal();
        GUILayout.EndHorizontal();
        #endregion
    }
    
    void Create()
    {
        if (GameObject.Find("map") == null)
            new GameObject("map");

        maplist = GameObject.Find("map");

        GameObject Tile = Instantiate(select_tile);
        created_tile.Push(Tile);
        Tile.transform.parent = maplist.transform;

        Tile.transform.position = new Vector3(offset_x,offset_y,0.0f);
        Tile.transform.rotation = Quaternion.Euler(0f, 90f, 0f);

        offset_x += 5.0f;
    }

    void Back()
    {
        if (created_tile.Count == 0)
            return;

        Vector3 last_Pos =  created_tile.Peek().transform.position;
        offset_x = last_Pos.x;
        offset_y = last_Pos.y;

        DestroyImmediate(created_tile.Peek());
        created_tile.Pop();
    }
    void set_tile_in_stack()
    {
        for (int i = 0; i < maplist.transform.childCount; i++)
            created_tile.Push(maplist.transform.GetChild(i).gameObject);
    }
}
