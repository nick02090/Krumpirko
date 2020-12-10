using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    Animator anim;
    NavMeshAgent agent;
    EnemyState currentState;

    // public Transform player;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();

        GameObject player = GameObject.FindWithTag("Player");

        currentState = new StateIdle(this.gameObject, agent, anim, player.transform);
    }

    void Update()
    {
        currentState = currentState.Process();
    }
}
