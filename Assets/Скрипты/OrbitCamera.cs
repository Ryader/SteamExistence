using UnityEngine;
public class OrbitCamera : MonoBehaviour
{
    public Transform target;
    public Transform pivot;
    public Transform Player; 
    public Transform mTransform;

    public PlayerConfig playerConfig;
    public CameraConfig cameraConfig;
    public bool leftPivot;

    public float delta; 
    public float mouseX;
    public float mouseY;
    public float smoothX;
    public float smoothY;
    public float smoothXvelocity;
    public float smoothYvelocity;
    public float loocAngle;
    public float titleAngle;

    void Update()
    {
        FixedTick();
    }

    void FixedTick()
    {
        delta = Time.deltaTime;

        HandlePosition();
        HandleRotation();

        Vector3 targetPosition = Vector3.Lerp(mTransform.position, Player.position, 1);
        mTransform.position = targetPosition;
    }

    void HandlePosition()
    {
        float targetX = cameraConfig.normalX, targetY = cameraConfig.normalY, targetZ = cameraConfig.normalZ;

        if (playerConfig.isAiming)
        {
            targetX = cameraConfig.aimX;
            targetZ = cameraConfig.aimZ;
        }

        if (leftPivot)
        {
            targetX = -targetX;
        }

        Vector3 newPivotPosition = pivot.localPosition;
        newPivotPosition.x = targetX; 
        newPivotPosition.y = targetY;

        Vector3 newCameraPosition = target.localPosition;
        newCameraPosition.z = targetZ;

        float t = delta * cameraConfig.pivotSpeed;
        pivot.localPosition = Vector3.Lerp(pivot.localPosition, newPivotPosition, t);
        target.localPosition = Vector3.Lerp(target.localPosition, newCameraPosition, t);
    }

    void HandleRotation()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if (cameraConfig.turnSmooth > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, mouseX, ref smoothXvelocity, cameraConfig.turnSmooth);
            smoothY = Mathf.SmoothDamp(smoothY, mouseY, ref smoothXvelocity, cameraConfig.turnSmooth);
        }
        else
        {
            smoothX = mouseX;
            smoothY = mouseY;
        }

        loocAngle += smoothX * cameraConfig.Y_rot_speed;
        Quaternion targetRot = Quaternion.Euler(0, loocAngle, 0);
        mTransform.rotation = targetRot;

        titleAngle -= smoothY * cameraConfig.Y_rot_speed;
        titleAngle = Mathf.Clamp(titleAngle, cameraConfig.minAngle, cameraConfig.maxAngle);
        pivot.localRotation = Quaternion.Euler(titleAngle,0,0);
    }
}
