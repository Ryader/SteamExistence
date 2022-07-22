using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class RelativeMovement : MonoBehaviour
{
    public static RelativeMovement Instance { get; private set; }

    public RectTransform staminaTransform;
    public Image Stamina;
    [SerializeField] public Transform target;
    public float moveSpeed = 3.0f;
    public float rotSpeed = 15.0f;
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float terminalVelocity = -10.0f;
    public float minFall = -1.5f;
    public CharacterController charController;
    public float vertSpeed;
    public float maxStamina;
    public float currentStamina = 100;
    public float collDown;
    private bool onCD;
    private float TimeDelayST;
    public float TimeDelay = 0.5f;
    private float cachedY;
    private float minXValue;
    private float maxXValue;
    static Animator anim;
    private ControllerColliderHit contact;


    
    

    public void Awake()
    {
        Instance = this;
    }

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
        charController = GetComponent<CharacterController>();
        currentStamina = maxStamina;

        anim = GetComponent<Animator>();
    }

    

    public void FixedUpdate ()
    {
        

        Vector3 movement = Vector3.zero;
        float horInput = Input.GetAxis("Horizontal");
        float vertIpnut = Input.GetAxis("Vertical");


        if (Input.GetKeyDown(KeyCode.R))
        {
            anim.SetTrigger("reload");
            Debug.Log("pidr");
        }

        if (Input.GetKeyDown(KeyCode.ScrollLock))
        {
            anim.SetBool("WalkingM", true);
            anim.SetBool("walking", false);
        }
        else
        {
            anim.SetBool("WalkingM", false);
            anim.SetBool("walking", true);
        }

        if (horInput != 0 || vertIpnut != 0)
        {
            anim.SetBool("walking", true);


            movement.x = horInput * moveSpeed;
            movement.z = vertIpnut * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);
            Quaternion tmp = target.rotation;
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
            movement = target.TransformDirection(movement);
            target.rotation = tmp;
            Quaternion direction = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);
            

            if (charController.isGrounded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    vertSpeed = jumpSpeed;
                }
                else
                {
                    vertSpeed = minFall;
                }
            }


            else
            {
                vertSpeed += gravity * 5 * Time.deltaTime;
                if (vertSpeed < terminalVelocity)
                {
                    vertSpeed = terminalVelocity;
                }
            }

            movement.y = vertSpeed;
            movement *= Time.deltaTime;
            charController.Move(movement);


        }
       
        else
        {
            anim.SetBool("walking", false);

        }

       

        IEnumerator CollDownStm()
        {
            onCD = true;
            yield return new WaitForSeconds(collDown);
            onCD = false;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = 12.0f;
            StartCoroutine(CollDownStm());
            currentStamina -= 0.8f;
            anim.SetBool("runing", true);
            if (currentStamina <= 0)
            {
                currentStamina = 0;
                moveSpeed = 4.0f;
            }
            HandleStamina();
           
        }
        else
        {
            moveSpeed = 4.0f;
            anim.SetBool("runing", false);
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
       

        movement.y = vertSpeed;
        movement *= Time.deltaTime;
        charController.Move(movement);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        contact = hit;
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