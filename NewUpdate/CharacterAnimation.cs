using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{

    public Animator anim;
    public CharacterMovement characterMovement;
    public CharacterStatus characterStatus;


    public void AnimationUpdate()
    {
        anim.SetBool("sprint", characterStatus.isSprint);
        anim.SetBool("aiming", characterStatus.isAiming);
        anim.SetBool("aiming move", characterStatus.isAimingMove);

        if (!characterStatus.isAiming)
            AnimationNormal();
        else
            AnimationAiming();

    }

    void AnimationNormal()
    {
        anim.SetFloat("vertical", characterMovement.moveAmount, 0.15f, Time.deltaTime);
    }

    void AnimationAiming()
    {
        float v = characterMovement.vertical;
        float h = characterMovement.horizontal;

        anim.SetFloat("vertical", v, 0.15f, Time.deltaTime);
        anim.SetFloat("horizontal", h, 0.15f, Time.deltaTime);
    }
}
