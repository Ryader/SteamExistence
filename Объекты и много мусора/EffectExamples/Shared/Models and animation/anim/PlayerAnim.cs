using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public Animator anim;
    public PlayerConfig playerConfig;
    public Controller controller;

    public void AnimationUpdate()
    {
       // anim.SetBool("sprint", playerConfig.isSprint);
        anim.SetBool("aiming", playerConfig.isAiming);

        if (!playerConfig.isAiming)
            AnimationNormal();
        else
            AnimationAiming();
    }

    void AnimationNormal()
    {
        anim.SetFloat("Vertical", controller.moveAmount, 0.15f, Time.deltaTime);
    }

    void AnimationAiming()
    {
        float v = controller.Vertical;
        float h = controller.Horizontal;

        anim.SetFloat("Vertical", v, 0.15f, Time.deltaTime);
        anim.SetFloat("Horizontal", h, 0.15f, Time.deltaTime);
    }
}
