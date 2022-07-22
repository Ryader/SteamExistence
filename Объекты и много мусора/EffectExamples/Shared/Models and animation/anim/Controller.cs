using UnityEngine;

public class Controller : MonoBehaviour
{
    public Transform CameraTransform;
    public PlayerConfig playerConfig;
    public Animator anim;

    public float Horizontal;
    public float Vertical;
    public float moveAmount;
    public float rotationSpeed;

    public Vector3 rotationDirection, moveDirection;

    public void MoveUpdate() 
    {
        Vertical = Input.GetAxis("Vertical");
        Horizontal = Input.GetAxis("Horizontal");
        moveAmount = Mathf.Clamp01(Mathf.Abs(Vertical) + Mathf.Abs(Horizontal));

       

        Vector3 moveDir = CameraTransform.forward * Vertical;
        moveDir += CameraTransform.right * Horizontal;
        moveDir.Normalize();
        moveDirection = moveDir;
        rotationDirection = CameraTransform.forward;

        RotationNormal();
        playerConfig.isGround = Ground();
    }

    public void RotationNormal()
    {
        if (!playerConfig.isAiming)
        {
            rotationDirection = moveDirection;
        }

        Vector3 targetDir = rotationDirection;
        targetDir.y = 0;

        if (targetDir == Vector3.zero)
            targetDir = transform.forward;

        Quaternion lookDir = Quaternion.LookRotation(targetDir);
        Quaternion targetRot = Quaternion.Slerp(transform.rotation, lookDir, rotationSpeed);
        transform.rotation = targetRot;
    }
    public bool Ground()
    {
        Vector3 origin = transform.position;
        origin.y += 0.6f;
        Vector3 dir = -Vector3.up;
        float dis = 0.7f;
        RaycastHit hit;
        if(Physics.Raycast(origin, dir, out hit,dis ))
        {
            Vector3 tp = hit.point;
            transform.position = tp;
            return true;
        }
        return false;
    }

}
