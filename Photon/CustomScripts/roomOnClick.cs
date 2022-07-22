using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class roomOnClick : MonoBehaviour
{

    public Text nameText;
    public Text sizeText;
    public string roomName;
    public string roomSize;

    public void SetRoom()
    {
        nameText.text = roomName;
        sizeText.text = roomSize.ToString();
    }

    public void JoinRoomOnClick()
    {
        PhotonNetwork.JoinRoom(roomName);
    }
}
