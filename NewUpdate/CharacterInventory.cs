using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInventory : MonoBehaviour
{
    public Animator anim;
    public Transform targetLook;
    public GameObject mCamera;
    public Transform rHand;

    public GameObject inventory;
    public GameObject cell;
    public Transform parentCell;
    public Transform parentCellGround;
    public Transform firstWeaponCell;
    public Transform secondWeaponCell;

    public List<Item> item = new List<Item>();
    public List<Item> itemOnTheGround = new List<Item>();

    public Interface userInterface;

    public WeaponProperties firstWeapon;
    public WeaponProperties secondWeapon;

    public CharacterIK characterIK;
    public CharacterInput characterInput;

    public GameObject objWeapon;
    Weapon activeWeapon;
    public SphereCollider sphereCollider;

    public void InventoryUpdate()
    {
        InventoryActive();
        Ray ray = new Ray(mCamera.transform.position, mCamera.transform.forward * 5);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Item")
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (hit.collider.GetComponent<Item>().typeItem == "Weapon")
                    {
                        Instantiate(firstWeapon.itemPrefab, hit.collider.transform.position, hit.collider.transform.rotation);
                        DestroyWeapon();
                        firstWeapon = hit.collider.GetComponent<Item>().weaponPropertiesItem;
                        characterInput.SelWeapon = 2;
                        anim.SetTrigger("Select");
                        Destroy(hit.collider.gameObject);

                        SortItem();
                        ItGroundUpdate();
                    }
                    else if (hit.collider.GetComponent<Item>().typeItem == "Other")
                    {
                        item.Add(hit.collider.GetComponent<Item>());
                        Destroy(hit.transform.gameObject);

                        SortItem();
                        ItGroundUpdate();
                    }

                    sphereCollider.enabled = false;
                    itemOnTheGround.Clear();
                    sphereCollider.enabled = true;

                }
            }
        }
    }

    public void SortItem()
    {
        for (int i = 0; i < item.Count; i++)
        {
            for (int j = 1 + 1; j < item.Count; j++)
            {
                if (item[i].number > item[j].number)
                {
                    Item t = item[i];
                    item[i] = item[j];
                    item[j] = t;
                }
            }
        }

        for (int i = 0; i < parentCell.childCount; i++)
        {
            if (parentCell.transform.childCount > 0)
            {
                Destroy(parentCell.transform.GetChild(i).gameObject);
            }
        }

        if (firstWeapon == null)
        {
            if (firstWeaponCell.childCount > 0)
            {
                Destroy(firstWeaponCell.GetChild(0).gameObject);
            }
        }

        inventory.SetActive(true);

        int count = item.Count;
        for (int i = 0; i < count; i++)
        {
            Item it = item[i];

            GameObject newCell = Instantiate(cell);
            Drag drag;
            newCell.transform.SetParent(parentCell);
            newCell.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(it.SpritePath);
            drag = newCell.GetComponent<Drag>();
            drag.item = it;
            drag.typeList = "Inventory";
            newCell.transform.GetChild(1).GetComponent<Text>().text = drag.item.nameItem;
        }
    }

    public void crutchSortItem()
    {
        Invoke("SortItem", 0.01f);
    }

    public void TakeItem(Drag drag)
    {
        item.Add(drag.item);
        Destroy(drag.item.gameObject);

        sphereCollider.enabled = false;
        itemOnTheGround.Clear();
        sphereCollider.enabled = true;
        SortItem();
        ItGroundUpdate();
    }

    public void TakeWeapon(Drag drag)
    {
        Item it = drag.item;

        if (firstWeaponCell.childCount > 0)
        {
            GameObject newObj = Instantiate<GameObject>(Resources.Load<GameObject>(firstWeaponCell.GetChild(0).GetComponent<Drag>().item.PrefabPath));
            newObj.transform.position = transform.position + transform.forward + transform.up;

            Drag dragFW = firstWeaponCell.GetChild(0).GetComponent<Drag>();

            dragFW.item.typeItem = "First Weapon";
            dragFW.typeList = "Ground";

            dragFW.item = drag.item;
            dragFW.item.typeItem = "Use W";
            dragFW.typeList = "Inventory";
            firstWeapon = dragFW.item.weaponPropertiesItem;
            firstWeaponCell.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(it.SpritePath);
            Destroy(drag.item.gameObject);
            DestroyWeapon();
            SelectWeapon(2);
        }
        else
        {
            GameObject newCell = Instantiate(cell);
            newCell.transform.SetParent(firstWeaponCell);
            Drag newDrag = newCell.GetComponent<Drag>();
            newDrag.item = drag.item;
            newDrag.item.typeItem = "Use W";
            newDrag.typeList = "Inventory";
            firstWeapon = drag.item.weaponPropertiesItem;
            firstWeaponCell.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(it.SpritePath);

            Destroy(drag.item.gameObject);
        }


        sphereCollider.enabled = false;
        itemOnTheGround.Clear();
        sphereCollider.enabled = true;
        SortItem();
        ItGroundUpdate();
    }

    public void InventoryActive()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventory.activeSelf)
            {
                inventory.SetActive(false);

                for (int i = 0; i < parentCell.childCount; i++)
                {
                    if (parentCell.transform.childCount > 0)
                    {
                        Destroy(parentCell.transform.GetChild(i).gameObject);
                    }
                }
            }
            else
            {
                SortItem();
            }
        }
    }

    public void ItGroundUpdate()
    {
        for (int i = 0; i < itemOnTheGround.Count; i++)
        {
            for (int j = 1 + 1; j < itemOnTheGround.Count; j++)
            {
                if (itemOnTheGround[i].number > itemOnTheGround[j].number)
                {
                    Item t = itemOnTheGround[i];
                    itemOnTheGround[i] = itemOnTheGround[j];
                    itemOnTheGround[j] = t;
                }
            }
        }

        int count = itemOnTheGround.Count;

        for (int i = 0; i < parentCellGround.childCount; i++)
        {
            if (parentCellGround.transform.childCount > 0)
            {
                Destroy(parentCellGround.transform.GetChild(i).gameObject);
            }
        }

        for (int i = 0; i < count; i++)
        {
            Item it = itemOnTheGround[i];
            Drag drag;

            GameObject newCell = Instantiate(cell);
            newCell.transform.SetParent(parentCellGround);
            newCell.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(it.SpritePath);
            drag = newCell.GetComponent<Drag>();
            drag.item = it;
            drag.typeList = "Ground";
            newCell.transform.GetChild(1).GetComponent<Text>().text = drag.item.nameItem;
        }
    }

    public void UseItem(Drag drag)
    {
        print("use: " + drag.item.nameItem);
    }

    public void Remove(Drag drag)
    {
        Item it = drag.item;
        GameObject newObj = Instantiate<GameObject>(Resources.Load<GameObject>(it.PrefabPath));
        newObj.transform.position = transform.position + transform.forward + transform.up;
        Destroy(drag.gameObject);
        item.Remove(it);
    }

    public void SelectWeapon(int selectWeapon)
    {
        if (selectWeapon == 1)
        {
            DestroyWeapon();
        }
        if (selectWeapon == 2)
        {
            objWeapon = Instantiate(firstWeapon.weaponPrefab);
            activeWeapon = objWeapon.GetComponent<Weapon>();
            objWeapon.transform.parent = rHand;
            objWeapon.transform.localPosition = firstWeapon.Weapon_pos;
            objWeapon.transform.localRotation = Quaternion.Euler(firstWeapon.Weapon_rot);

            characterIK.r_Hand.localPosition = firstWeapon.rHandPos;
            Quaternion rotRight = Quaternion.Euler(firstWeapon.rHandRot.x, firstWeapon.rHandRot.y, firstWeapon.rHandRot.z);
            characterIK.r_Hand.localRotation = rotRight;

            activeWeapon.targetLook = targetLook;
            activeWeapon.cameraMain = mCamera;
            characterInput.weapon = activeWeapon;
            characterIK.l_Hand_Target = activeWeapon.lHandTarget;

            anim.SetBool("Weapon", true);
            anim.SetInteger("WeaponType", 2);

            UserInterface(firstWeapon.weaponPrefab.name);
        }
        if (selectWeapon == 3)
        {
            objWeapon = Instantiate(secondWeapon.weaponPrefab);
            activeWeapon = objWeapon.GetComponent<Weapon>();
            objWeapon.transform.parent = rHand;
            objWeapon.transform.localPosition = secondWeapon.Weapon_pos;
            objWeapon.transform.localRotation = Quaternion.Euler(secondWeapon.Weapon_rot);

            characterIK.r_Hand.localPosition = secondWeapon.rHandPos;
            Quaternion rotRight = Quaternion.Euler(secondWeapon.rHandRot.x, secondWeapon.rHandRot.y, secondWeapon.rHandRot.z);
            characterIK.r_Hand.localRotation = rotRight;

            activeWeapon.targetLook = targetLook;
            activeWeapon.cameraMain = mCamera;
            characterInput.weapon = activeWeapon;
            characterIK.l_Hand_Target = activeWeapon.lHandTarget;

            anim.SetBool("Weapon", true);
            anim.SetInteger("WeaponType", 1);
        }
    }

    public void UserInterface(string NameWeapon)
    {
        if (NameWeapon == "M4")
            userInterface.imageWeapon.sprite = userInterface.iconWeapon[1];
        else if (NameWeapon == "AKM")
            userInterface.imageWeapon.sprite = userInterface.iconWeapon[0];
    }

    public void DestroyWeapon()
    {
        characterInput.weapon = null;
        characterIK.l_Hand_Target = null;
        Destroy(objWeapon);
        objWeapon = null;
        anim.SetBool("Weapon", false);
        anim.SetInteger("WeaponType", 0);
    }
}
