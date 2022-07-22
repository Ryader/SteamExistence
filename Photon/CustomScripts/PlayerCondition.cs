using Photon.Pun;
using System.Collections;
using UnityEngine;

public class PlayerCondition : MonoBehaviourPunCallbacks
{
    private PhotonView photonView;
    public static PlayerCondition pcondition;
    public float playerHealth = 100f;
    public float playerDamage = 25f;
    public Camera localCamera;
    public Animator anim;
    public AudioListener localAL;
    [HideInInspector] public string myName;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.IsMine)
        {
            GameSetup.gameSetup.HUD.SetActive(true);
            myName = photonView.Owner.NickName;
        }
        else
        {
            Destroy(localCamera);
            Destroy(localAL);
        }
    }

    void Update()
    {
        if (photonView.IsMine)
        {
            if (GameSetup.gameSetup.matchEnded)
            {
                Time.timeScale = 0f;
            }

            if (playerHealth < 0) playerHealth = 0;

            if (playerHealth == 0)
            {
                //playerHealth = 100;
                
                anim.SetBool("dying", true);

                StartCoroutine(waiting_death());
                
            }
            GameSetup.gameSetup.hpBar.value = playerHealth;
        }
    }
    [PunRPC]
    public void GetDamage(float damage, string nameOfKiller)
    {


        if (playerHealth <= damage)
        {
            if (playerHealth > 0)
            {
                GameSetup.gameSetup.totalKillCount++;
                playerHealth = 0;
            }

        }
        playerHealth -= damage;
    }

    IEnumerator waiting_death()
    {
        yield return new WaitForSeconds(5);
        PhotonPlayer.pp.isDied = true;
    }
}
