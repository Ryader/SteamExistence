using UnityEngine;
using UnityEngine.AI;

public class Running_Enemy : MonoBehaviour
{
    private NavMeshAgent agent;
    public GameObject Player;
    public float EnemyDistanceRun = 4.0f;
    public Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        float distance = Vector3.Distance(transform.position, Player.transform.position);

        if (distance < EnemyDistanceRun)
        {
            Vector3 dirToPlayer = transform.position - Player.transform.position;
            Vector3 newPosition = transform.position + dirToPlayer;
            agent.SetDestination(newPosition);

            anim.SetTrigger("walk");
        }
    }
}