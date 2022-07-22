using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move2 : MonoBehaviour
{
    public Controller charControl;
    public PlayerAnim playerAnim;
    public PlayerInput playerInput;

    public void Update()
    {
        charControl.MoveUpdate();
        playerAnim.AnimationUpdate();
        playerInput.InputUpdate();
    }
}
