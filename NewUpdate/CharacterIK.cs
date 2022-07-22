using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIK : MonoBehaviour
{

    public Animator anim;
    public CharacterMovement characterMovement;
    public CharacterInventory characterInventory;
    public CharacterStatus characterStatus;
    public Transform targetLook;

    public Transform l_Hand;
    public Transform l_Hand_Target;
    public Transform r_Hand;

    public Quaternion lh_rot;

    public float rh_Weight;
    public float lh_Weight;

    public Transform shoulder;
    public Transform aimPivot;

    void Start()
    {
        shoulder = anim.GetBoneTransform(HumanBodyBones.RightShoulder).transform;

        aimPivot = new GameObject().transform;
        aimPivot.name = "aim pivot";
        aimPivot.transform.parent = transform;

        r_Hand = new GameObject().transform;
        r_Hand.name = "right hand";
        r_Hand.transform.parent = aimPivot;

        l_Hand = new GameObject().transform;
        l_Hand.name = "left hand";
        l_Hand.transform.parent = aimPivot;
    }
    void Update()
    {
        if (l_Hand_Target != null)
        {
            lh_rot = l_Hand_Target.rotation;
            l_Hand.position = l_Hand_Target.position;
        }

        if (anim.GetInteger("WeaponType") >= 2)
        {
            lh_Weight = 1;
            if (characterStatus.isAiming)
            {
                rh_Weight += Time.deltaTime * 2;
            }
            else
            {
                rh_Weight -= Time.deltaTime * 2;
            }
            rh_Weight = Mathf.Clamp(rh_Weight, 0, 1);
        }

        else
        {
            if (characterStatus.isAiming)
            {
                rh_Weight += Time.deltaTime * 2;
                lh_Weight += Time.deltaTime * 2;
            }
            else
            {
                rh_Weight -= Time.deltaTime * 2;
                lh_Weight -= Time.deltaTime * 2;
            }

            rh_Weight = Mathf.Clamp(rh_Weight, 0, 1);
            lh_Weight = Mathf.Clamp(rh_Weight, 0, 1);
        }
    }

    void OnAnimatorIK()
    {
        aimPivot.position = shoulder.position;

        if (characterStatus.isAiming)
        {
            aimPivot.LookAt(targetLook);

            anim.SetLookAtWeight(1, 0, 1);
            anim.SetLookAtPosition(targetLook.position);

            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, lh_Weight);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, lh_Weight);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, l_Hand.position);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, lh_rot);

            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rh_Weight);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rh_Weight);
            anim.SetIKPosition(AvatarIKGoal.RightHand, r_Hand.position);
            anim.SetIKRotation(AvatarIKGoal.RightHand, r_Hand.rotation);
        }
        else
        {
            anim.SetLookAtWeight(.3f, 0, .3f);
            anim.SetLookAtPosition(targetLook.position);

            anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, lh_Weight);
            anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, lh_Weight);
            anim.SetIKPosition(AvatarIKGoal.LeftHand, l_Hand.position);
            anim.SetIKRotation(AvatarIKGoal.LeftHand, lh_rot);

            anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rh_Weight);
            anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rh_Weight);
            anim.SetIKPosition(AvatarIKGoal.RightHand, r_Hand.position);
            anim.SetIKRotation(AvatarIKGoal.RightHand, r_Hand.rotation);

        }
    }
}
