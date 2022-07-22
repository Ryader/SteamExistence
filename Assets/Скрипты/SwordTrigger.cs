using UnityEngine;

public class SwordTrigger : MonoBehaviour
{
    public string[] targetTags = { "Maneken" };
    public float damage = 200;
    public float fireRate = 2f;
    public float weaponRange = 1f;
    private float nextFire;
    public Transform gunEnd;

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Vector3 rayOrigin = gunEnd.position;
            RaycastHit hit;
            Physics.Raycast(rayOrigin, gunEnd.forward, out hit, weaponRange);

            hit.transform.GetComponent<EnemyHealth>().AddDamage(damage);
        }
    }
    void OnTriggerEnter(CapsuleCollider col)
    {
        if (col.tag == "Maneken")
        {
            col.gameObject.GetComponent<EnemyHealth>().AddDamage(damage);
        }
    }
}
