using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler
{
    CharacterInventory characterInventory;
    public SphereCollider sphereCollider;
    public Drag drag;
    public string typeCell;

    void Start()
    {
        characterInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterInventory>();
        sphereCollider = GameObject.Find("InventoryField").GetComponent<SphereCollider>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        drag = eventData.pointerDrag.GetComponent<Drag>();

        if (drag.item.typeItem == "Other" && typeCell == "Inventory")
        {
            drag.transform.SetParent(transform.GetChild(0));

            if (drag.typeList == "Ground")
            {
                if (drag.transform.parent.name != "Content item on the ground")
                {
                    print("Перенесено в инвентарь");

                    Destroy(drag.item.gameObject);
                    characterInventory.item.Add(drag.item);
                    characterInventory.itemOnTheGround.Remove(drag.item);
                    drag.typeList = "Inventory";
                }
            }
        }
        else if (drag.item.typeItem == "Other" && typeCell == "Ground")
        {
            if (drag.typeList == "Inventory")
            {
                if (drag.transform.parent.name != "Content Inventory")
                {
                    print("Перенесено на землю");

                    GameObject newObj = Instantiate<GameObject>(Resources.Load<GameObject>(drag.item.PrefabPath));
                    newObj.transform.position = characterInventory.transform.position + characterInventory.transform.forward + characterInventory.transform.up;
                    characterInventory.item.Remove(drag.item);
                    drag.item = newObj.GetComponent<Item>();
                    drag.typeList = "Ground";

                    characterInventory.crutchSortItem();
                }
            }
        }



        if (drag.item.typeItem == "First Weapon" && drag.item.typeItem == typeCell && characterInventory.firstWeapon == null)
        {
            drag.transform.SetParent(transform);

            if (drag.typeList == "Ground")
            {
                if (drag.transform.parent.name != "Content item on the ground")
                {
                    print("Перенесено в инвентарь");

                    Destroy(drag.item.gameObject);
                    characterInventory.firstWeapon = drag.item.weaponPropertiesItem;
                    characterInventory.itemOnTheGround.Remove(drag.item);
                    drag.typeList = "Inventory";
                    drag.item.typeItem = "Use W";
                }
            }
        }
        else if (drag.item.typeItem == "Use W" && typeCell == "Ground" && characterInventory.firstWeapon != null)
        {
            if (drag.typeList == "Inventory")
            {
                if (drag.transform.parent.name != "Content Inventory")
                {
                    print("Перенесено на землю");

                    GameObject newObj = Instantiate<GameObject>(Resources.Load<GameObject>(drag.item.PrefabPath));
                    newObj.transform.position = characterInventory.transform.position + characterInventory.transform.forward + characterInventory.transform.up;
                    characterInventory.firstWeapon = null;
                    drag.item = newObj.GetComponent<Item>();
                    drag.typeList = "Ground";
                    characterInventory.SelectWeapon(1);

                }
            }
        }
        else if (drag.item.typeItem == "First Weapon" && drag.item.typeItem == typeCell && characterInventory.firstWeapon != null)
        {
            characterInventory.TakeWeapon(drag);
        }

        characterInventory.crutchSortItem();
        sphereCollider.enabled = false;
        characterInventory.itemOnTheGround.Clear();
        sphereCollider.enabled = true;
    }

}
