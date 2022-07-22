using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    private PhotonView photonView;
    private PlayerCondition condition;
    public Transform ray;


    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
        condition = GetComponent<PlayerCondition>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        Debug.DrawRay(ray.position, ray.TransformDirection(Vector3.forward) *10f , Color.yellow);
        if (Input.GetMouseButtonDown(0))
        {
            photonView.RPC("RPC_Shooting", RpcTarget.All);
        }
    }

    //[PunRPC]
    //void RPC_Shooting()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(ray.position, ray.TransformDirection(Vector3.forward), out hit, 1000))
    //    {
    //        Debug.DrawRay(ray.position, ray.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

    //        if (hit.transform.tag == "Player")
    //        {
    //            hit.transform.gameObject.GetComponent<PlayerCondition>().playerHealth -= condition.playerDamage;
               
    //            Debug.Log("Hit!");
    //        }
    //    }
    //    else
    //    {
    //        Debug.DrawRay(ray.position, ray.TransformDirection(Vector3.forward) * hit.distance, Color.white);
    //        Debug.Log("Not Hit");
    //    }
    //}
}
