using Photon.Pun;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;


public class WeaponScriptForMp : MonoBehaviourPunCallbacks
{

    public float fireRate = 2f;
    public float weaponRange = 20f;
    public float current_damage;
    public float timeout = 0.2f;
    public string[] targetTags = { "Player" };
    private float curTimeout;

    public LayerMask layerMask;

    private RaycastHit hit;

    public float AmmoTotal = 1000;
    public float AmmoClip = 30;
    public float AmmoNow = 30;

    private GameObject PlayCamera;


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

    private string forKillBar;

    private float nextFire;

    private PhotonView photonView;
    private PlayerCondition condition;


    void Start()
    {
        photonView = GetComponent<PhotonView>();
        condition = GetComponent<PlayerCondition>();
        Fire_Point_Start = Fire_Point;
        PlayCamera = GameObject.FindWithTag("MainCamera");


    }

    IEnumerator ReloadingWhithinTime()
    {
        yield return new WaitForSeconds(3);

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
        if (photonView.IsMine)
        {
            Debug.DrawRay(Fire_Point.position, Fire_Point.TransformDirection(Vector3.forward) * 10f, Color.yellow);
            GameSetup.gameSetup.AmmoTotalText.text = "" + AmmoTotal;
            GameSetup.gameSetup.AmmoTotalText.color = Color.white;
            
            GameSetup.gameSetup.AmmoNowText.text = "" + AmmoNow;
            GameSetup.gameSetup.AmmoNowText.color = Color.white;
            if (AmmoNow == 0 || Input.GetKeyDown(KeyCode.R) && AmmoNow != AmmoClip)
            {
                StartCoroutine(ReloadingWhithinTime());
            }
            if (Input.GetMouseButtonDown(0) && AmmoNow != 0)
            {
                RPC_Shooting();
            }
        }



    }

    void RPC_Shooting()
    {
        float dist;

        AmmoNow--;

        nextFire = Time.time + fireRate;
        muzzleFlash.Play();

        Vector3 fwd = Fire_Point.TransformDirection(Vector3.forward);
        if (Physics.Raycast(Fire_Point.position, fwd, out hit, weaponRange, layerMask))
        {
            HandleHit(hit);
            Instantiate(muzzleFlash, hit.point + hit.normal * 0.012f, Quaternion.FromToRotation(Vector3.forward, PlayCamera.transform.forward));

            if (hit.transform.tag == "Player")
            {
                dist = Vector3.Distance(transform.position, hit.collider.transform.position);
                current_damage = condition.playerDamage / (dist / 100); //Формула снижение урона от 100 метров
                if (current_damage > condition.playerDamage)
                {
                    current_damage = condition.playerDamage;
                }

                PhotonView pView = hit.transform.GetComponent<PhotonView>();
                if (pView)
                {
                    
                    string killerName = photonView.Owner.NickName;
                    killerName = PhotonNetwork.NickName;
                    killerName = PlayerPrefs.GetString("PlayerPrefKey");
                    pView.RPC("GetDamage", RpcTarget.All, current_damage, killerName);
                }
            }
        }





        if (AmmoNow <= 0)
        {
            AmmoNow = 0;
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