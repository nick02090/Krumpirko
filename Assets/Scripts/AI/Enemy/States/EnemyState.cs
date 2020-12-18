using UnityEngine;
using UnityEngine.AI;
using Gameplay.Characters;

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
        // To store the transform of the player
        protected Transform player;
        // The state that gets to run after the one currently running
        protected EnemyState nextState;
        // To store the enemy NavMeshAgent component
        protected NavMeshAgent agent;
        // To store enemy characteristics
        protected EnemyCharacter enemyCharacter;
        // Stores AI related parameters
        protected EnemyAIParameters aiParameters;
        // Shared context for tracking ailments
        protected AilmentContext ailmentContext;

        protected float visDist;
        protected float stealDist;
        protected float hearDist;
        protected float visAngle;

        public EnemyState(GameObject _enemy, NavMeshAgent _agent, Animator _anim, Transform _player, EnemyCharacter _enemyCharacter)
        {
            enemy = _enemy;
            agent = _agent;
            anim = _anim;
            player = _player;
            enemyCharacter = _enemyCharacter;

            // if(_ailmentContext != null)
            //     ailmentContext = _ailmentContext;
            // else
            //     ailmentContext = new AilmentContext();

            aiParameters = enemyCharacter.aiParameters;

            visDist = aiParameters.normalVisDist;
            stealDist = aiParameters.normalStealDist;
            hearDist = aiParameters.normalHearDist;
            visAngle = aiParameters.normalVisAngle;
        }

        // enemyCharacter.AilmentHandler.onAilmentInflict += NazivMetodeUnutarAI;

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
            Vector3 direction = player.position - enemy.transform.position;

            if(direction.magnitude <= hearDist) {
                return true;
            }
            return false;
        }

        protected void turnTowardsPlayer(float rotationSpeed)
        {
            Vector3 direction = (player.position - enemy.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
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

        protected bool RandomChance(float chance)
        {
            return Random.Range(0, 100) < chance;
        }

    }
}