using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    public GameObject Weapon1;
    public GameObject Weapon2;
    public GameObject Weapon3;
    public GameObject Weapon4;
    public int MaxWeapon = 4;
    private int ScrolInt;

    void Update()
    {
        if (ScrolInt == 0)
        {
            Weapon1.SetActive(true);
            Weapon2.SetActive(false);
            Weapon3.SetActive(false);
            Weapon4.SetActive(false);
        }

        if (ScrolInt == 1)
        {
            Weapon1.SetActive(false);
            Weapon2.SetActive(true);
            Weapon3.SetActive(false);
            Weapon4.SetActive(false);
        }

        if (ScrolInt == 2)
        {
            Weapon1.SetActive(false);
            Weapon2.SetActive(false);
            Weapon3.SetActive(true);
            Weapon4.SetActive(false);
        }

        if (ScrolInt == 3)
        {
            Weapon1.SetActive(false);
            Weapon2.SetActive(false);
            Weapon3.SetActive(false);
            Weapon4.SetActive(true);

        }

        if (ScrolInt >= MaxWeapon)
        {
            ScrolInt = MaxWeapon;
        }

        if (ScrolInt <= 0)
        {
            ScrolInt = 0;
        }

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            ScrolInt += 1;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            ScrolInt -= 1;
        }
    }
}