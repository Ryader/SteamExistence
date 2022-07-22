using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhotonPlayerFFA : MonoBehaviour
{

    public static PhotonPlayerFFA pp;
    private PhotonView photonView;
    public GameObject localAvatar;

    int spawnPicker;

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();


        spawnPicker = Random.Range(0, GameSetupFFA.gameSetup.spawnPositions.Length);
        if (photonView.IsMine)
        {
            localAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"),
               GameSetupFFA.gameSetup.spawnPositions[spawnPicker].position, GameSetupFFA.gameSetup.spawnPositions[spawnPicker].rotation, 0);
            GameSetupFFA.gameSetup.maincamera.SetActive(false);
        }

    }
}
