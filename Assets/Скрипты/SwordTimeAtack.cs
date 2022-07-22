using UnityEngine;

public class SwordTimeAtack : MonoBehaviour
{
    public float timeStart;
    public float timeEnd;

    void Update()
    {
        timeStart += 1;
        if (timeStart >= timeEnd)
        {
            timeStart = 0;
            gameObject.SetActive(false);
        }
    }
}
