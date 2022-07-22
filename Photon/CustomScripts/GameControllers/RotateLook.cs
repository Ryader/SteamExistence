using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLook : MonoBehaviour
{

    public Transform camTrans;
    public Transform targetLook;

    void Update()
    {
        TargetLook();
    }
    void TargetLook()
    {
        Ray ray = new Ray(camTrans.position, camTrans.forward * 2000);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            targetLook.position = Vector3.Lerp(targetLook.position, hit.point, Time.deltaTime * 40);
        }
        else
        {
            targetLook.position = Vector3.Lerp(targetLook.position, targetLook.transform.forward * 200, Time.deltaTime * 5);
        }
    }
}
