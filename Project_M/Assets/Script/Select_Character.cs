using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Select_Character : MonoBehaviour
{
    #region Variable
    public Object[] character = new Object[3];
    public Image[] thumnail = new Image[3];
    public GameObject choice;

    int choice_num = 0;

    public Vector2 canvas_Pos;

    #endregion 
    private void Start()
    {
        canvas_Pos = gameObject.transform.parent.GetComponent<CanvasScaler>().referenceResolution;

        for (int i = 0; i < 3; i++)
        {
            Texture2D texture = AssetPreview.GetAssetPreview(character[i]);
            thumnail[i].sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        }
        choice.GetComponent<RectTransform>().position += new Vector3(-500f, 0, 0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
            GameManager.instance.player_obj = (GameObject)character[choice_num];

        if (GameManager.instance.player_obj != null)
            return;

        if(Input.GetKeyDown(KeyCode.LeftArrow) && choice_num > 0)
        {
            choice.GetComponent<RectTransform>().position += new Vector3(-500f, 0, 0);
            choice_num--;
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow) && choice_num < 2)
        {
            choice.GetComponent<RectTransform>().position += new Vector3(500f, 0, 0);
            choice_num++;
        }
    }
}
