using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LD47.Core
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] [Range(1, 30)] float levelLoadDelay = 3f;

        void LoadNextScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextScene = ++currentSceneIndex % SceneManager.sceneCountInBuildSettings;

            SceneManager.LoadScene(nextScene);
        }

        void ReloadScene()
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.LoadScene(currentSceneIndex);
        }

        public void ReloadLevel()
        {
            Invoke("ReloadScene", levelLoadDelay);
        }

        public void LoadNextLevel()
        {
            Invoke("LoadNextScene", levelLoadDelay);
        }
    }

}