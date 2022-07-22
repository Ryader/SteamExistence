using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPosition : MonoBehaviour
{

    public EnemyStat enemyStat;
    public int multiplication;


    public void Damage(int damage)
    {
        enemyStat.TakeAwayHealth(damage * multiplication);
    }
}
