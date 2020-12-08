using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent agent;

    void Start()
    {
        agent.updateRotation = false;
    }

    void Update()
    {
        agent.SetDestination(player.position);
    }
}
