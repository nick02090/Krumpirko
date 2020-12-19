using UnityEngine;
using UnityEngine.AI;
using Gameplay.Ailments;
using Gameplay.Characters;
using System.Collections.Generic;

namespace AI.Enemy.States
{
    public class EnemyState : IState
    {
        public enum STATE
        {
            IDLE, CHASE, EATING, WANDER, ENTER, BAIT, BURN, FEAR, HAPPY, STARVING
        };

        public enum EVENT
        {
            ENTER, UPDATE, EXIT
        };

        // To store the name of the STATE
        public STATE name;
        // To store the stage the EVENT is in
        public EVENT stage;
        // To store the NPC game object
        protected GameObject enemy;
        // To store the Animator component
        protected Animator anim;
        // To store the player game object
        protected GameObject player;
        // The state that gets to run after the one currently running
        protected EnemyState nextState;
        // To store the enemy NavMeshAgent component
        protected NavMeshAgent agent;
        // To store enemy characteristics
        protected EnemyCharacter enemyCharacter;
        // Stores AI related parameters
        protected EnemyAIParameters aiParameters;

        protected float visDist;
        protected float hearDist;
        protected float visAngle;

        protected ICharacter playerCharacter;

        public EnemyState(GameObject _enemy, NavMeshAgent _agent, Animator _anim, GameObject _player, EnemyCharacter _enemyCharacter)
        {
            enemy = _enemy;
            agent = _agent;
            anim = _anim;
            player = _player;
            enemyCharacter = _enemyCharacter;

            playerCharacter = player.GetComponent<ICharacter>();
            aiParameters = enemyCharacter.aiParameters;

            visDist = aiParameters.NormalVisDist;
            hearDist = aiParameters.NormalHearDist;
            visAngle = aiParameters.NormalVisAngle;
        }

        public virtual void Enter() { stage = EVENT.UPDATE; }
        public virtual void Update() { stage = EVENT.UPDATE; }
        public virtual void Exit() { stage = EVENT.EXIT; }

        public IState Process()
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

        protected bool canHearPlayer()
        {
            Vector3 direction = player.transform.position - enemy.transform.position;

            if(direction.magnitude <= hearDist) {
                return true;
            }
            return false;
        }

        protected void turnXTowardsY(GameObject x, GameObject y, float rotationSpeed)
        {
            Vector3 direction = (y.transform.position - x.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            x.transform.rotation = Quaternion.Slerp(x.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }

        protected void turnTowardsPlayer(float rotationSpeed)
        {
            turnXTowardsY(enemy, player, rotationSpeed);
        }

        protected bool CanSeePlayer()
        {
            Vector3 direction = player.transform.position - enemy.transform.position;
            float angle = Vector3.Angle(direction, enemy.transform.forward);

            if(direction.magnitude < visDist && angle < visAngle)
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

        protected void SetRandomDestinationFromRing(float minDist, float maxDist)
        {
            float x = Random.Range(minDist, maxDist);
            if (RandomChance(50)) 
            {
                x *= -1;
            }
            float z = Random.Range(minDist, maxDist);
            if (RandomChance(50))
            {
                z *= -1;
            }

            Vector3 randomOffset = new Vector3(x, 0f, z);
            agent.SetDestination(enemy.transform.position + randomOffset);
        }

        protected bool RandomChance(float chance)
        {
            return Random.Range(0, 100) < chance;
        }

        protected bool AreThereBaits()
        {
            return playerCharacter.GetBaits().Count != 0;
        }

        protected GameObject GetClosestBaitInRange()
        {
            List<GameObject> baits = playerCharacter.GetBaits();

            GameObject closest = null;
            float minDist = float.MaxValue;

            foreach (var bait in baits)
            {
                Vector3 direction = enemy.transform.position - bait.transform.position;

                if(direction.magnitude <= aiParameters.BaitVisDist && direction.magnitude < minDist)
                {
                    closest = bait;
                    minDist = direction.magnitude;
                }
            }

            return closest;
        }

        protected bool IsStarving()
        {
            return false;
            // return enemyCharacter.GetTimeUntilDeath() <= aiParameters.StarvationTriggerTime;
        }

        protected EnemyState GetAilmentState()
        {
            Ailment active = enemyCharacter.GetActiveAilment();
            return new StateAilmentFear(enemy, agent, anim, player, enemyCharacter, active);
        }

    }
}