using System.Collections;
using System.Collections.Generic;
using UnityEngine;




    [CreateAssetMenu(menuName = "Skills/Create Skill")]
    public class Skills : ScriptableObject
    {
        public string Description;
        public Sprite Icon;
        public int LevelNeeded;
        public int XPNeeded;

        public List<PlayerAttributes> AffectedAttributes = new List<PlayerAttributes>();


        public void SetValues(GameObject SkillDisplayObject, PlayerStats PLayer)
        {
            if (SkillDisplayObject)
            {
                SkillDisplay SD = SkillDisplayObject.GetComponent<SkillDisplay>();
                SD.SkillName.text = name;
                if (SD.SkillDescription)
                {
                    SD.SkillDescription.text = Description;
                }

                if (SD.SkillIcon)
                {
                    SD.SkillIcon.sprite = Icon;
                }

                if (SD.SkillLevel)
                {
                    SD.SkillLevel.text = LevelNeeded.ToString();
                }

                if (SD.SkillXPNeeded)
                {
                    SD.SkillXPNeeded.text = XPNeeded.ToString() + "XP";
                }

                if (SD.SkillAttribure)
                {
                    SD.SkillAttribure.text = AffectedAttributes[0].attributes.ToString();
                }

                if (SD.SkillAttrAmount)
                {
                    SD.SkillAttrAmount.text = AffectedAttributes[0].amount.ToString();
                }

            }
        }
            public bool CheckSkills(PlayerStats Player)
            {
                if (Player.PlayerLVL < LevelNeeded)
                {
                    return false;
                }
                if (Player.PlayerXP < XPNeeded)
                {
                    return false;
                }

                return true;
            }

            public bool EnableSkill(PlayerStats Player)
            {
                List<Skills>.Enumerator skills = Player.PLayerSkills.GetEnumerator();
                while (skills.MoveNext())
                {
                    var CurrSkill = skills.Current;
                    if (CurrSkill.name == this.name)
                    {
                        return true;
                    }
                }
                return false;
            }

            public bool GetSkill(PlayerStats Player)
            {
                int i = 0;

                List<PlayerAttributes>.Enumerator attributes = AffectedAttributes.GetEnumerator();
                while (attributes.MoveNext())
                {

                    List<PlayerAttributes>.Enumerator PlayerAttributes = AffectedAttributes.GetEnumerator();
                    while (PlayerAttributes.MoveNext())
                    {
                        if (attributes.Current.attributes.name.ToString() == PlayerAttributes.Current.attributes.name.ToString())
                        {
                            PlayerAttributes.Current.amount += attributes.Current.amount;
                            i++;
                        }
                    }
                    if (i > 0)
                    {
                        Player.PlayerXP -= this.XPNeeded;
                        Player.PLayerSkills.Add(this);
                        return true;
                    }
                }
                return false;
            }

        

    }


