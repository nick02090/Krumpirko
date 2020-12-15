using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Enemy
{
    public class EnemyState
    {
        public enum STATE
        {
            IDLE, CHASE, EATING, WANDER, ENTER
        };

        public enum EVENT
        {
            ENTER, UPDATE, EXIT
        };

        // To store the name of the STATE
        public STATE name;
        // To store the stage the EVENT is in
        protected EVENT stage;
        // To store the NPC game object
        protected GameObject enemy;
        // To store the Animator component
        protected Animator anim;
        // To store the transform of the player
        protected Transform player;
        // The state that gets to run after the one currently running
        protected EnemyState nextState;
        // To store the enemy NavMeshAgent component
        protected NavMeshAgent agent;

        protected float hearDist = 3.0f;
        protected float visDist = 10.0f;
        protected float visAngle = 45.0f;
        protected float stealDist = 2.0f;

        public EnemyState(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player)
        {
            enemy = _enemy;
            agent = _agent;
            anim = _anim;
            player = _player;

            stage = EVENT.ENTER;
        }

        public virtual void Enter() { stage = EVENT.UPDATE; }
        public virtual void Update() { stage = EVENT.UPDATE; }
        public virtual void Exit() { stage = EVENT.EXIT; }

        public EnemyState Process()
        {
            if (stage == EVENT.ENTER) Enter();
            if (stage == EVENT.UPDATE) Update();
            if (stage == EVENT.EXIT)
            {
                Exit();
                return nextState;
            }
            return this;
        }

        protected bool CanSensePlayer() 
        {
            Vector3 direction = player.position - enemy.transform.position;
            float angle = Vector3.Angle(direction, enemy.transform.forward);

            if((direction.magnitude < visDist && angle < visAngle) || direction.magnitude < hearDist)
            {
                return true;
            }
            return false;
        }

        protected bool CanSeePlayer()
        {
            Vector3 direction = player.position - enemy.transform.position;
            float angle = Vector3.Angle(direction, enemy.transform.forward);

            if(direction.magnitude < visDist && angle < visAngle)
            {
                return true;
            }
            return false;
        }

        protected bool CanStealFries()
        {
            Vector3 direction = player.position - enemy.transform.position;

            if(direction.magnitude < stealDist)
            {
                return true;
            }
            return false;
        }

        protected void SetRandomDestination(float low, float high)
        {
            Vector3 randomOffset = new Vector3(Random.Range(low, high), 0f, Random.Range(low, high));
            agent.SetDestination(enemy.transform.position + randomOffset); 
        }

    }
}