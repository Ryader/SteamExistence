using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharacterMovement : MonoBehaviour
{

    public Transform CameraTransform;
    public CharacterStatus characterStatus;
    public Animator anim;

    public float vertical;
    public float horizontal;
    public float moveAmount;
    public float rotationSpeed;

    public float maxStamina;
    public float currentStamina = 100;
    public RectTransform staminaTransform;
    public Image Stamina;
    private float minXValue;
    private float cachedY;
    private float maxXValue;
    private bool onCD;
    public float collDown;
    private float TimeDelayST;

    public Vector3 rotationDirection;
    public Vector3 moveDirection;


    private float СurrentStamina
    {
        get { return currentStamina; }
        set
        {
            currentStamina = value;
        }
    }


    void Start()
    {
        cachedY = staminaTransform.position.y;
        maxXValue = staminaTransform.position.x;
        minXValue = staminaTransform.position.x - staminaTransform.rect.width;
        currentStamina = maxStamina;

        anim = GetComponent<Animator>();
    }

    public void FixedUpdate()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        moveAmount = Mathf.Clamp01(Mathf.Abs(vertical) + Mathf.Abs(horizontal));

        Vector3 moveDir = CameraTransform.forward * vertical;
        moveDir += CameraTransform.right * horizontal;
        moveDir.Normalize();
        moveDirection = moveDir;
        rotationDirection = CameraTransform.forward;

        RotationNormal();
        characterStatus.isGround = Ground();

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            anim.speed = 1.5f;

            StartCoroutine(CollDownStm());
            currentStamina -= 0.8f;

            if (currentStamina <= 0)
            {
                currentStamina = 0;
                anim.speed = 1f;
            }
            HandleStamina();
        }
        else
        {
            anim.speed = 1f;

            if (currentStamina < maxStamina)
            {
                StartCoroutine(CollDownStm());
                currentStamina += 0.33f;
            }

            if (currentStamina < maxStamina)
            {

                TimeDelayST += Time.deltaTime;
                if (currentStamina >= maxStamina)
                {
                    currentStamina = maxStamina;
                }
                HandleStamina();
            }
        }
    }



    public void RotationNormal()
    {
        if (!characterStatus.isAiming)
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
        if (Physics.Raycast(origin, dir, out hit, dis))
        {
            Vector3 tp = hit.point;
            transform.position = tp;
            return true;
        }
        return false;
    }


    IEnumerator CollDownStm()
    {
        onCD = true;
        yield return new WaitForSeconds(collDown);
        onCD = false;
    }

    private void HandleStamina()
    {
        float currentXValue = MapValues(currentStamina, 0, maxStamina, minXValue, maxXValue);
        staminaTransform.position = new Vector3(currentXValue, cachedY);
    }
    private float MapValues(float x, float inMin, float inMax, float outMin, float outMax)
    {
        return (x - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
