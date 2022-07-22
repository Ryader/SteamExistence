using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAtack : MonoBehaviour
{
    public float damage = 200;
    public GameObject triggerDamage;
 
    void Update()
    {
        if(Input.GetKey(KeyCode.Mouse0))
        {
            gameObject.GetComponent<Animator>().SetTrigger("Atack1");
            triggerDamage.SetActive(true);
        }      
    }  
}
