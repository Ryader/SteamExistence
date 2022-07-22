using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WeaponScript : MonoBehaviour
{
    public float MIN_Random_Soot = -6;
    public float MAX_Random_Soot = 6;

    public float fireRate = 0.25f;
    public float weaponRange = 20f;
    public float damage;
    public float current_damage;
    public float timeout = 0.2f;
    public string[] targetTags = { "Maneken", "Default" };
    private float curTimeout;

    public LayerMask layerMask;

    private RaycastHit hit;

    public float AmmoTotal = 1000;
    public float AmmoClip = 30;
    public float AmmoNow = 30;

    private GameObject PlayCamera;

    public Text AmmoTotalText;
    public Text AmmoNowText;


    public Transform Fire_Point;
    private Transform Fire_Point_Start;
    public ParticleSystem muzzleFlash;


    public GameObject metalHitEffect;
    public GameObject sandHitEffect;
    public GameObject stoneHitEffect;
    public GameObject waterLeakEffect;
    public GameObject waterLeakExtinguishEffect;
    public GameObject[] fleshHitEffects;
    public GameObject woodHitEffect;
    public Animator anim;

    private float nextFire;


    void Start()
    {
        Fire_Point_Start = Fire_Point;
        PlayCamera = GameObject.FindWithTag("MainCamera");
        anim = GetComponent<Animator>();
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

    void Update()
    {
        float mob_dist;

        AmmoTotalText.text = "" + AmmoTotal;
        AmmoNowText.text = "" + AmmoNow;
        Debug.DrawRay(Fire_Point.position, Fire_Point.TransformDirection(Vector3.forward) * 10f, Color.yellow);
        if (Input.GetButton("Fire1") && Time.time > nextFire && AmmoNow > 0)
        {
            AmmoNow--;

            nextFire = Time.time + fireRate;
            muzzleFlash.Play();
            for (int bullet_counter = 1; bullet_counter > 0; bullet_counter--) // Цикл с формулой разброса
            {
                Fire_Point.localRotation = Quaternion.identity;
                Fire_Point.localRotation = Quaternion.Euler(Fire_Point_Start.localRotation.x + Random.Range(MIN_Random_Soot, MAX_Random_Soot),
                Fire_Point_Start.localRotation.y + Random.Range(MIN_Random_Soot, MAX_Random_Soot), Fire_Point_Start.localRotation.z + Random.Range
                (MIN_Random_Soot, MAX_Random_Soot));
                Vector3 fwd = Fire_Point.TransformDirection(Vector3.forward);
                if (Physics.Raycast(Fire_Point.position, fwd, out hit, weaponRange, layerMask))
                {
                    HandleHit(hit);
                    Instantiate(muzzleFlash, hit.point + hit.normal * 0.01f, Quaternion.FromToRotation(Vector3.forward, PlayCamera.transform.forward));

                    if (hit.collider.tag == "Maneken")
                    {
                        hit.transform.GetComponent<EnemyHealth>().AddDamage(damage);
                    }
                    
                }
            }

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

    void HandleHit(RaycastHit hit) // Список декалей
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



    void SpawnDecal(RaycastHit hit, GameObject prefab)// Добавление "Декаля" при попадании
    {
        GameObject spawnedDecal = GameObject.Instantiate(prefab, hit.point, Quaternion.LookRotation(hit.normal));
        spawnedDecal.transform.SetParent(hit.collider.transform);
    }
}