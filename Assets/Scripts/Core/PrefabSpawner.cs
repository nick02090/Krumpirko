using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class PrefabSpawner : MonoBehaviour
    {
        public GameObject prefab;
        public float spawnPeriod;
        public int spawnigCapacity;
        public float startTimeOffset = 0f;
        public float spawnPeriodDecay = 1f;

        private float startTime;
        private int spawnedPrefabs = 0;

        void Start()
        {
            startTime = Time.time + startTimeOffset;
            
            if (spawnigCapacity < 0)
            {
                spawnigCapacity = int.MaxValue;
            }
        }

        void Update()
        {
            if (Time.time - startTime >= spawnPeriod && spawnedPrefabs < spawnigCapacity) 
            {
                Vector3 position = transform.position;
                GameObject obj = Instantiate(prefab, position, Quaternion.identity) as GameObject;
                obj.transform.SetParent(transform);

                spawnedPrefabs++;
                if (spawnedPrefabs > spawnigCapacity)
                {
                    enabled = false;
                }

                spawnPeriod *= spawnPeriodDecay;
                startTime = Time.time;
            }        
        }

        void OnDrawGizmos() {
            Gizmos.color = new Color(1.0f, 0.0f, 0.0f);
            Gizmos.DrawWireCube(transform.position, transform.lossyScale);
        }

        void OnDrawGizmosSelected() {
            Gizmos.color = new Color(1.0f, 0.0f, 0.0f);
            Gizmos.DrawWireCube(transform.position, transform.lossyScale);
        }
    }
}