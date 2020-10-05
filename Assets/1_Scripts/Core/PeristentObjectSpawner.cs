using UnityEngine;

namespace LD47.Core
{
    public class PeristentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectPrefab = null;

        static bool hasSpawed = false;

        private void Awake()
        {
            if (hasSpawed) { return; }
            if (persistentObjectPrefab == null) { return; }

            SpawnPersitentObjects();

            hasSpawed = true;
        }

        void SpawnPersitentObjects()
        {
            GameObject peristentObject = Instantiate(persistentObjectPrefab);
            DontDestroyOnLoad(peristentObject);
        }

        
    }
}