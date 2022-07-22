using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaGun : MonoBehaviour
{

    public Rigidbody bullet;
    public Transform gunPoint;
    public int bulletSpeed = 10;
    public float timeout = 0.5f;
    private float curTimeout;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            curTimeout += Time.deltaTime;
            if (curTimeout > timeout)
            {
                curTimeout = 0;
                Rigidbody bulletInstance = Instantiate(bullet, gunPoint.position, Quaternion.identity) as Rigidbody;
                bulletInstance.velocity = gunPoint.forward * bulletSpeed;
            }
        }
        else
        {
            curTimeout = timeout + 1;
        }

    }
}
