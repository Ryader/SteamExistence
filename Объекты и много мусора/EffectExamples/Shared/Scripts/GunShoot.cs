using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class GunShoot : MonoBehaviour {

	public float fireRate = 0.25f;										
	public float weaponRange = 20f;
    public float damage = 100;
    public float timeout = 0.2f;
    public string[] targetTags = { "Maneken" };
    private float curTimeout;

    public Transform gunEnd;
	public ParticleSystem muzzleFlash;
	public ParticleSystem cartridgeEjection;

	public GameObject metalHitEffect;
	public GameObject sandHitEffect;
	public GameObject stoneHitEffect;
	public GameObject waterLeakEffect;
    public GameObject waterLeakExtinguishEffect;
	public GameObject[] fleshHitEffects;
	public GameObject woodHitEffect;

	private float nextFire;												
	private Animator anim;
	private GunAim gunAim;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		gunAim = GetComponentInParent<GunAim>();
	}

	void Update () 
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire && !gunAim.GetIsOutOfBounds()) 
		{
			nextFire = Time.time + fireRate;
			muzzleFlash.Play();
			cartridgeEjection.Play();
			anim.SetTrigger ("Fire");

            Vector3 rayOrigin = gunEnd.position;
			RaycastHit hit;
			if (Physics.Raycast(rayOrigin, gunEnd.forward, out hit, weaponRange))
			{
				HandleHit(hit);
            }
            hit.transform.GetComponent<EnemyHealth>().AddDamage(damage);
        }

    }

	void HandleHit(RaycastHit hit)
	{
		if(hit.collider.sharedMaterial != null)
		{
			string materialName = hit.collider.sharedMaterial.name;

			switch(materialName)
			{
				case "Metal":
					SpawnDecal(hit, metalHitEffect);
					break;
				case "Sand":
					SpawnDecal(hit, sandHitEffect);
					break;
				case  "Stone":
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