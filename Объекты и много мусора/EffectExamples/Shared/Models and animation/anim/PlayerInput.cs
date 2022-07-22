using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public PlayerConfig playerConfig;

    public bool isAiming;
    public bool debugAiming;

    public void InputUpdate()
    {
        if (!debugAiming)
            playerConfig.isAiming = Input.GetMouseButton(1);
        else
            playerConfig.isAiming = isAiming;
    }

}
