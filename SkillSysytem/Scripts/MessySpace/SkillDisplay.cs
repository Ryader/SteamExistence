using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


    public class SkillDisplay : MonoBehaviour
    {

        public Skills skill;
        public Text SkillName;
        public Text SkillDescription;
        public Text SkillLevel;
        public Text SkillXPNeeded;
        public Text SkillAttribure;
        public Text SkillAttrAmount;
        public Image SkillIcon;

        [SerializeField]
        private PlayerStats m_PlayerHandler;
        // Start is called before the first frame update
        void Start()
        {
            m_PlayerHandler = this.GetComponentInParent<SkillPanelController>().Player;
            m_PlayerHandler.onXPChange += ReactToChange;
            m_PlayerHandler.onLevelChange += ReactToChange;
            if (skill)
            {
                skill.SetValues(this.gameObject, m_PlayerHandler);
            }
        }

        public void EnableSkills()
        {
            if (m_PlayerHandler && skill && skill.EnableSkill(m_PlayerHandler))
            {
                TurnOnSkillIcon();
            }

            else if (m_PlayerHandler && skill.CheckSkills(m_PlayerHandler))
            {
                this.GetComponent<Button>().interactable = true;
                this.transform.Find("SkillIcon").Find("Disabled").gameObject.SetActive(false);
            }
            else
            {
                TurnOffSkillIcon();
            }
        }

        private void OnEnable()
        {
            EnableSkills();
        }

        public void GetSkill()
        {
            if (skill.GetSkill(m_PlayerHandler))
            {
                TurnOnSkillIcon();
            }
        }

        private void TurnOnSkillIcon()
        {
            this.GetComponent<Button>().interactable = false;
            this.transform.Find("SkillIcon").Find("Available").gameObject.SetActive(false);
            this.transform.Find("SkillIcon").Find("Disabled").gameObject.SetActive(false);
        }

        private void TurnOffSkillIcon()
        {
            this.GetComponent<Button>().interactable = false;
            this.transform.Find("SkillIcon").Find("Available").gameObject.SetActive(true);
            this.transform.Find("SkillIcon").Find("Disabled").gameObject.SetActive(true);
        }

        void ReactToChange()
        {
            EnableSkills();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

