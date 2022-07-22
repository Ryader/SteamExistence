using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    public static PlayerInfo playerInfo;

    public int SelectedClass;
    public GameObject[] Classes;

    private void OnEnable()
    {
        if (PlayerInfo.playerInfo == null)
        {
            PlayerInfo.playerInfo = this;
        }
        else
        {
            if (PlayerInfo.playerInfo != this)
            {
                Destroy(PlayerInfo.playerInfo.gameObject);
                PlayerInfo.playerInfo = this;
            }
        }
        DontDestroyOnLoad(this.gameObject);
    }
    
    void Start()
    {
        if (PlayerPrefs.HasKey("myClass"))
        {
            SelectedClass = PlayerPrefs.GetInt("myClass");
        }
        else
        {
            SelectedClass = 0;
            PlayerPrefs.SetInt("myClass", SelectedClass);
        }
    }
}
