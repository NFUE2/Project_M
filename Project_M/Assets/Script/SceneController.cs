using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{

    private void Update()
    {
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 0 : case 1 :
                if (Input.anyKeyDown)
                    Scene_Change();
                break;
        }






    }

    public void Scene_Change()
    {
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
