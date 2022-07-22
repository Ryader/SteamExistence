using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkillPanelController : MonoBehaviour
{

    public PlayerStats Player;
    public GameObject SkillPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            if (!SkillPanel.activeSelf)
            {
                SkillPanel.SetActive(true);
            }
            else
            {
                SkillPanel.SetActive(false);
            }
    }

}

