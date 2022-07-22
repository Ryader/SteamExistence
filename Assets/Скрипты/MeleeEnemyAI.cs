using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyAI : MonoBehaviour
{

    public float lookRadius = 10f;

    private float timer;

    public GameObject Player;

    Transform target;
    NavMeshAgent agent;

    void Start()
    {
        target = PlayerManager.instance.Player.transform;
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= lookRadius)
        {
            gameObject.GetComponent<Animator>().SetBool("Walk", true);
            gameObject.GetComponent<Animator>().SetBool("Punch", false);
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                gameObject.GetComponent<Animator>().SetBool("Punch", true);
                gameObject.GetComponent<Animator>().SetBool("Walk", false);
                timer += 1 * Time.deltaTime;
                if (timer >= 1.4f)
                {
                    Player.GetComponent<HealthPlayer>().currentHealth -= 1;
                    timer = 0;
                }
                FaceTarget();
            }
        }

    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
