using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD47.Core
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] [Range(1, 30)] float levelLoadDelay = 3f;

        IEnumerator LoadScene(int sceneIndex)
        {
            yield return new WaitForSeconds(levelLoadDelay);

            SceneManager.LoadScene(sceneIndex);
        }

        IEnumerator LoadScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextScene = ++currentSceneIndex % SceneManager.sceneCountInBuildSettings;

            yield return new WaitForSeconds(levelLoadDelay);

            SceneManager.LoadScene(nextScene);
        }

        IEnumerator ReloadScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            yield return new WaitForSeconds(levelLoadDelay);

            SceneManager.LoadScene(currentSceneIndex);
        }

        public void ReloadLevel()
        {
            StartCoroutine(ReloadScene());
        }

        public void LoadLevel(int sceneToLoad)
        {
            StartCoroutine(LoadScene(sceneToLoad));
        }

        public void LoadLevel()
        {
            StartCoroutine(LoadScene());
        }
    }

}