using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : Manager
{
    AudioSource audio;

    public AudioClip[] BGM;
    int music_num;

    public override void Manager_Start()
    {
        audio = GetComponent<AudioSource>();
    }

    public override void Manager_Update()
    {
        //현재 씬에 따라 재생되는 배경음
        switch(SceneManager.GetActiveScene().buildIndex)
        {
            case 1:
                SoundPlay(0,true);
                break;
            case 2:
                SoundPlay(1, true);
                break;
            case 3:
                if (GameManager.instance.P_boss && GameManager.instance.P_player_survive)
                    SoundPlay(3, true);

                else if (!GameManager.instance.P_player_survive)
                    SoundPlay(BGM.Length - 2, false);

                else if (GameManager.instance.P_stage_clear)
                {
                    SoundPlay(BGM.Length - 1, false);
                }
                else
                    SoundPlay(2, true);
                break;
        }
    }

    //씬이 이동되면 음악을 변경하는 함수
    void SoundPlay(int num,bool loop)
    {
        if (audio.isPlaying && num == music_num) return;
        else
        {
            audio.clip = BGM[num];
            audio.loop = loop;
            audio.volume = 1.0f;
            audio.Play();
            music_num = num;
        }
    }
}
