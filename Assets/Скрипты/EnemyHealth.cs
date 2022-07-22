using UnityEngine;

[System.Serializable]
public class EnemyHealth : MonoBehaviour
{
    public float HP = 10;


    public void AddDamage(float damage)
    {
        HP -= damage;
        if (HP <= 0)
        {
            _ = HP <= 0;
            gameObject.SetActive(false);
            Destroy(gameObject, 500.5f);
        }
    }

}
