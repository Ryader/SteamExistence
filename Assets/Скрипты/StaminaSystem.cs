using UnityEngine;

public class StaminaSystem : MonoBehaviour
{
    public float moveSpeed = 6.0f;
    public float maxStamina = 100;
    public float currentStamina;
    private CharacterController charController;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;
        float horInput = Input.GetAxis("Horizontal");
        float vertIpnut = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = 10.0f;
            movement.x = horInput * moveSpeed;
            movement.z = vertIpnut * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed);
        }

    }
}
