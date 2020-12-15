using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using AI;
using AI.Enemy;

namespace Controll
{
    public class EnemyController : MonoBehaviour
    {
        Animator anim;
        NavMeshAgent agent;

        FsmAI ai;

        void Start()
        {
            anim = this.GetComponent<Animator>();
            agent = this.GetComponent<NavMeshAgent>();
            GameObject player = GameObject.FindWithTag("Player");

            EnemyState startingState = new StateEnter(this.gameObject, agent, anim, player.transform);
            ai = new FsmAI(startingState);
        }

        void Update()
        {
            ai.Update();
        }
    }
}