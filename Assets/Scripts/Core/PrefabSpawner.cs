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

        private List<GameObject> objects;

        void Start()
        {
            startTime = Time.time + startTimeOffset;
            
            if (spawnigCapacity < 0)
            {
                spawnigCapacity = int.MaxValue;
            }

            objects = new List<GameObject>();
            for (uint i = 0; i < spawnigCapacity; ++i)
            {
                Vector3 position = transform.position;
                GameObject obj = Instantiate(prefab, position, Quaternion.identity);
                obj.transform.SetParent(transform);
                obj.SetActive(false);
                objects.Add(obj);
            }
        }

        void Update()
        {
            if (Time.time - startTime >= spawnPeriod && spawnedPrefabs < spawnigCapacity) 
            {
                objects[spawnedPrefabs].SetActive(true);
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