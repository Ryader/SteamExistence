using UnityEngine;
using UnityEngine.UI;

public class ShotGunScript : MonoBehaviour
{

    public float MIN_Random_Soot = -6;
    public float MAX_Random_Soot = 6;
    public float current_damage; //Итоговый дэмадж
    public ParticleSystem muzzleFlash;

    private RaycastHit hit;
    public float Fire_Dist = 50;
    private int damage = 10;

    private float curTimeout;
    private GameObject PlayCamera; //У НАС ТАКОГО НЕТ!!!
    public Transform Fire_Point;
    private Transform Fire_Point_Start;
    public LayerMask layerMask; // НУЖНО ПЕРЕДЕЛАТЬ ПОД НАС!!!


    public float AmmoTotal = 100;
    public float AmmoClip = 6;
    public float AmmoNow = 6;

    public float fireRate = 0.25f;
    public float timeout = 0.2f;
    private float nextFire;

    public Text AmmoTotalText;
    public Text AmmoNowText;

    public GameObject metalHitEffect;
    public GameObject sandHitEffect;
    public GameObject stoneHitEffect;
    public GameObject waterLeakEffect;
    public GameObject waterLeakExtinguishEffect;
    public GameObject[] fleshHitEffects;
    public GameObject woodHitEffect;




    // Start is called before the first frame update
    void Start()
    {
        Fire_Point_Start = Fire_Point;
        PlayCamera = GameObject.FindWithTag("MainCamera");// У НАС ТАКОГО НЕТ!!!
    }

    public void Reload()
    {
        if (AmmoTotal >= AmmoClip)
        {
            AmmoTotal += AmmoNow;
            AmmoNow = AmmoClip;
            AmmoTotal -= AmmoClip;
        }
        else if (AmmoTotal - (AmmoClip - AmmoNow) < 0)
        {
            AmmoNow += AmmoTotal;
            AmmoTotal = 0;
        }
        else if (AmmoTotal < AmmoClip)
        {
            var diff = AmmoClip - AmmoNow;
            AmmoNow += diff;
            AmmoTotal -= diff;
        }

    }

    // Update is called once per frame
    void Update()
    {
        AmmoTotalText.text = "" + AmmoTotal;
        AmmoNowText.text = "" + AmmoNow;



        if (Input.GetButton("Fire1") && Time.time > nextFire && AmmoNow > 0)
        {
            Shoot();
        }

        if (AmmoNow <= 0)
        {
            AmmoNow = 0;
        }

        if (Input.GetKeyDown(KeyCode.R) && AmmoNow != AmmoClip)
        {
            Reload();
        }
    }

    public void Shoot()
    {

        AmmoNow--;
        nextFire = Time.time + fireRate;
        muzzleFlash.Play();
        float mob_dist;

        AmmoNow--;

        nextFire = Time.time + fireRate;
        for (int bullet_counter = 10; bullet_counter > 0; bullet_counter--)

        {
            Fire_Point.localRotation = Quaternion.identity;
            Fire_Point.localRotation = Quaternion.Euler(Fire_Point_Start.localRotation.x + Random.Range(MIN_Random_Soot, MAX_Random_Soot),
            Fire_Point_Start.localRotation.y + Random.Range(MIN_Random_Soot, MAX_Random_Soot), Fire_Point_Start.localRotation.z + Random.Range
               (MIN_Random_Soot, MAX_Random_Soot));
            Vector3 fwd = Fire_Point.TransformDirection(Vector3.forward);
            if (Physics.Raycast(Fire_Point.position, fwd, out hit, Fire_Dist, layerMask))
            {
                HandleHit(hit);
                Instantiate(muzzleFlash, hit.point + hit.normal * 0.01f, Quaternion.FromToRotation(Vector3.forward, PlayCamera.transform.forward));

                if (hit.collider.tag == "Maneken")
                {
                    mob_dist = Vector3.Distance(transform.position, hit.collider.transform.position);
                    current_damage = damage / mob_dist / 10;
                    hit.transform.GetComponent<EnemyHealth>().AddDamage(damage);
                }
            }
        }


    }

    void HandleHit(RaycastHit hit)
    {
        if (hit.collider.sharedMaterial != null)
        {
            string materialName = hit.collider.sharedMaterial.name;

            switch (materialName)
            {
                case "Metal":
                    SpawnDecal(hit, metalHitEffect);
                    break;
                case "Sand":
                    SpawnDecal(hit, sandHitEffect);
                    break;
                case "Stone":
                    SpawnDecal(hit, stoneHitEffect);
                    break;
                case "WaterFilled":
                    SpawnDecal(hit, waterLeakEffect);
                    SpawnDecal(hit, metalHitEffect);
                    break;
                case "Wood":
                    SpawnDecal(hit, woodHitEffect);
                    break;
                case "Meat":
                    SpawnDecal(hit, fleshHitEffects[Random.Range(0, fleshHitEffects.Length)]);
                    break;
                case "Character":
                    SpawnDecal(hit, fleshHitEffects[Random.Range(0, fleshHitEffects.Length)]);
                    break;
                case "WaterFilledExtinguish":
                    SpawnDecal(hit, waterLeakExtinguishEffect);
                    SpawnDecal(hit, metalHitEffect);
                    break;
            }
        }
    }



    void SpawnDecal(RaycastHit hit, GameObject prefab)
    {
        GameObject spawnedDecal = GameObject.Instantiate(prefab, hit.point, Quaternion.LookRotation(hit.normal));
        spawnedDecal.transform.SetParent(hit.collider.transform);
    }
}
