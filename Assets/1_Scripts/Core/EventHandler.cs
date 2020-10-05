using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LD47.Core
{
    public class EventHandler : MonoBehaviour
    {
        SceneLoader obj_sceneLoader;

        // Start is called before the first frame update
        void Start()
        {
            obj_sceneLoader = FindObjectOfType<SceneLoader>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void callReloadLevel()
        {
            obj_sceneLoader.ReloadLevel();
        }
    }
}
