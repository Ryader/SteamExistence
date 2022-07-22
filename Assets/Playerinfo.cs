using UnityEngine;

public class Playerinfo : MonoBehaviour
{
    public static Transform Maneken;

    void Start()
    {
        FindManeken();
    }

    public static void FindManeken()
    {
        Maneken = GameObject.FindGameObjectWithTag("Maneken").transform;
    }
}
