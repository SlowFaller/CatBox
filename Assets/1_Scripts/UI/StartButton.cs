using UnityEngine;
using UnityEngine.UI;
using LD47.Core;

namespace LD47.UI
{
    public class StartButton : MonoBehaviour
    {
        SceneLoader obj_sceneLoader;

        // Start is called before the first frame update
        void Start()
        {
            obj_sceneLoader = FindObjectOfType<SceneLoader>();
        }

        // load's Jake's scene
        public void OnClick()
        {
            Debug.Log("Starting game");
            obj_sceneLoader.LoadLevel();
        }
    }
}
