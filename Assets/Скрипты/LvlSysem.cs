using System;

[System.Serializable]
public class LvlSysem
{
    public int EXP;
    public int currentLevel;
    public Action OnLevelUp;

    public int MAX_EXP;
    public int _maxLevel = 99;

    public LvlSysem(int level, Action OneLevUp)
    {
        MAX_EXP = GetXPForLevel(_maxLevel);
        currentLevel = level;
        EXP = GetXPForLevel(level);
        OneLevUp = OneLevUp;
    }

    public int GetXPForLevel(int level)
    {
        if (level > _maxLevel)
        {
            return 0;
        }
        int firstPass = 0;
        int secondPass = 0;

        for (int levelCycle = 1; levelCycle < level; levelCycle++)
        {
            firstPass += (int)Math.Floor(levelCycle + (300.0f * Math.Pow(2.0f, levelCycle / 7.0f)));
            secondPass = firstPass / 4;
        }
        if (secondPass > MAX_EXP && MAX_EXP != 0)
        {
            return MAX_EXP;
        }
        if (secondPass < 0)
        {
            return MAX_EXP;
        }
        return secondPass;
    }

    public int GetLevelForXP(int exp)
    {
        if (exp > MAX_EXP)
        {
            return MAX_EXP;
        }
        int firstPass = 0;
        int secondPass = 0;

        for (int levelCycle = 1; levelCycle <= MAX_EXP; levelCycle++)
        {
            firstPass += (int)Math.Floor(levelCycle + (300.0f * Math.Pow(2.0f, levelCycle / 7.0f)));
            secondPass = firstPass / 4;

            if (secondPass > exp)
            {
                return levelCycle;
            }
        }
        if (exp > secondPass)
        {
            return _maxLevel;
        }
        return 0;
    }

    public bool AddExp(int amount)
    {
        if (amount + EXP < 0 || EXP > MAX_EXP)
        {
            if (EXP > MAX_EXP)
            {
                EXP = MAX_EXP;
            }
            return false;
        }
        int oldLevel = GetLevelForXP(EXP);
        EXP += amount;
        if (oldLevel < GetLevelForXP(EXP))
        {
            if (currentLevel < GetLevelForXP(EXP))
            {
                currentLevel = GetLevelForXP(EXP);
                if (OnLevelUp != null)
                {
                    OnLevelUp.Invoke();
                }
                return true;
            }
        }
        return false;
    }
}