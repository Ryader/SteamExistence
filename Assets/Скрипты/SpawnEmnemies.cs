using System.Collections;
using UnityEngine;

public class SpawnEmnemies : MonoBehaviour
{
    public GameObject Enemy;
    public int xPos;
    public int zPos;
    public int EnemyCount;

    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while (EnemyCount < 10)
        {
            xPos = Random.Range(1, 50);
            zPos = Random.Range(1, 31);
            Instantiate(Enemy, new Vector3(6150, 98, 6508), Quaternion.identity);
            yield return new WaitForSeconds(5.0f);
            EnemyCount += 1;
        }
    }
}
