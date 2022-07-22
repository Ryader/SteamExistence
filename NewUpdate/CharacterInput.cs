using UnityEngine;

public class CharacterInput : MonoBehaviour
{
    public Animator anim;

    public CharacterStatus characterStatus;
    public CharacterInventory characterInventory;
    public Weapon weapon;
    public Transform targetLook;

    public bool debugAiming;
    public bool isAiming;

    public bool opportunityToAim;
    public float distance;

    public int SelWeapon;

    void Start()
    {

    }

    public void InputUpdate()
    {
        RayCastAiming();
        InputAiming();
        InputSelectWeapon();

    }

    public void InputAiming()
    {
        if (anim.GetBool("Weapon"))
        {
            if (Input.GetMouseButton(1) && opportunityToAim)
            {
                characterStatus.isAiming = true;
                characterStatus.isAimingMove = true;
            }
            if (Input.GetMouseButton(1) && !opportunityToAim)
            {
                characterStatus.isAiming = false;
                characterStatus.isAimingMove = true;
            }
            if (!Input.GetMouseButton(1))
            {
                characterStatus.isAiming = false;
                characterStatus.isAimingMove = false;
            }

            if (debugAiming)
            {
                characterStatus.isAiming = isAiming;
                characterStatus.isAimingMove = isAiming;
            }

            if (Input.GetMouseButtonDown(0) && Input.GetMouseButton(1) && opportunityToAim)
            {
                // weapon.Shoot();
            }
        }
    }

    public void InputSelectWeapon()
    {
        if (!anim.GetBool("aiming"))
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && SelWeapon != 1)
            {
                SelWeapon = 1;
                anim.SetTrigger("Select");
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && SelWeapon != 2)
            {
                SelWeapon = 2;
                anim.SetTrigger("Select");
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && SelWeapon != 3)
            {
                SelWeapon = 3;
                anim.SetTrigger("Select");
            }
        }
    }

    public void SelectWeapon()
    {
        characterInventory.DestroyWeapon();
        characterInventory.SelectWeapon(SelWeapon);
        SelWeapon = 0;
    }

    public void RayCastAiming()
    {

        Debug.DrawLine(transform.position + transform.up * 1.4f, targetLook.position, Color.green);

        distance = Vector3.Distance(transform.position + transform.up * 1.4f, targetLook.position);
        if (distance > 1.5f)
            opportunityToAim = true;
        else opportunityToAim = false;
    }
}
