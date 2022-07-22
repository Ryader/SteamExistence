using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassPicker : MonoBehaviour
{
    public void OnClick(int Classid)
    {
        if(PlayerInfo.playerInfo != null)
        {
            PlayerInfo.playerInfo.SelectedClass = Classid;
            PlayerPrefs.SetInt("myClass", Classid);
        }
    }
}
