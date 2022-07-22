using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryField : MonoBehaviour
{
    public CharacterInventory characterInventory;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            characterInventory.itemOnTheGround.Add(other.transform.GetComponent<Item>());
            characterInventory.ItGroundUpdate();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {
            characterInventory.itemOnTheGround.Remove(other.transform.GetComponent<Item>());
            characterInventory.ItGroundUpdate();
        }
    }

}
