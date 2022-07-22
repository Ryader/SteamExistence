using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRot : MonoBehaviour
{
    [SerializeField] private Transform target;
    void Update()
    {
        Vector3 movement = Vector3.zero;
        float horInput = Input.GetAxis("Horizontal");
        float vertInut = Input.GetAxis("vertical");

        if (horInput == 0 && vertInut == 0)
        {
            return;
        }
        movement.x = horInput;
        movement.z = vertInut;
        target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
        movement = target.TransformDirection(movement);

        Quaternion tmp = target.rotation;
        target.rotation = tmp;

        transform.rotation = Quaternion.LookRotation(movement);
    }
}
