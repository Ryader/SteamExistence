using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLvl : MonoBehaviour
{
    public LvlSysem level;
    public EnemyHealth HP;
    void Start()
    {
        level = new LvlSysem(1, OneLevelUp);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            level.AddExp(100);
        }
        //if(GetComponent<EnemyHealth>().HP<=0.0f)
       // {
          //  level.AddExp(100);
      //  }
    }

    public void OneLevelUp()
    {
        print("I Leveled up!!!!");
    }
}
