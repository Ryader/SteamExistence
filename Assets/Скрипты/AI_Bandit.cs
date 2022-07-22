using UnityEngine;

public class AI_Bandit : MonoBehaviour
{
    private float timer;
    public GameObject Player;
    public ParticleSystem muzzleFlash;

    [SerializeField] float speed = 10f;
    [SerializeField] float waitTime = 3f;
    float currentWaitTime = 0f;
    float minX, maxX, minZ, maxZ;
    Vector3 moveSpot;

    private bool OnTrigger { get; set; }

    void Start()
    {
        GetGroundSize();
        moveSpot = GetNewPosition();
    }

    void Update()
    {
        if (OnTrigger == false)
        {
            WatchYourStep();
            GetToStepping();
        }

    }
    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Player")
        {
            OnTrigger = true;
            Player = col.gameObject;
            gameObject.GetComponent<Animator>().SetBool("Fire", true);
            transform.LookAt(col.transform.position);
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            OnFire();
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            OnTrigger = false;
            gameObject.GetComponent<Animator>().SetBool("Fire", false);
        }
    }

    void OnFire()
    {
        timer += 1 * Time.deltaTime;
        if (timer >= 1.4f)
        {
            muzzleFlash.Play();
            Player.GetComponent<HealthPlayer>().currentHealth -= 1;
            timer = 0;
        }

    }

    private void GetGroundSize()
    {
        GameObject ground = GameObject.FindWithTag("WayZone");
        Renderer groundSize = ground.GetComponent<Renderer>();
        minX = (groundSize.bounds.center.x - groundSize.bounds.extents.x);
        maxX = (groundSize.bounds.center.x + groundSize.bounds.extents.x);
        minZ = (groundSize.bounds.center.z - groundSize.bounds.extents.z);
        maxZ = (groundSize.bounds.center.z + groundSize.bounds.extents.z);
    }

    Vector3 GetNewPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);
        Vector3 newPosition = new Vector3(randomX, transform.position.y, randomZ);
        return newPosition;

    }

    void GetToStepping()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpot, speed * Time.deltaTime);
        gameObject.GetComponent<Animator>().SetBool("Move", true);
        if (Vector3.Distance(transform.position, moveSpot) <= 0.2f)
        {

            if (currentWaitTime <= 0)
            {
                moveSpot = GetNewPosition();
                currentWaitTime = waitTime;

            }
            else
            {
                currentWaitTime -= Time.deltaTime;
                gameObject.GetComponent<Animator>().SetBool("Move", false);

            }
        }
    }

    void WatchYourStep()
    {
        Vector3 targetDirection = moveSpot - transform.position;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 0.3f, 0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
    }
}
