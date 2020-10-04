using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD47.Core
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] [Range(0, 10)] float levelRestartDelay = 3f;

        public void LoadLevel(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void LoadLevel()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextScene = ++currentSceneIndex % SceneManager.sceneCountInBuildSettings;

            SceneManager.LoadScene(nextScene);
        }

        public void ReloadLevel()
        {
            StartCoroutine(ReloadScene());
        }

        IEnumerator ReloadScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            yield return new WaitForSeconds(levelRestartDelay);

            SceneManager.LoadScene(currentSceneIndex);
        }
    }

}