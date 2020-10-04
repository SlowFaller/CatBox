using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LD47.UI;
using LD47.Control;
using LD47.Core;

namespace LD47.Level
{
    public class ScenePortal : MonoBehaviour
    {
        const string TAG = "Player";

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] [Range(0f, 5f)] float fadeInTime = 2f;
        [SerializeField] [Range(0f, 5f)] float fadeOutTime = 2f;
        [SerializeField] [Range(0f, 5f)] float fadeWaitTime = 1f;

        Fader obj_fader;
        SceneLoader obj_sceneLoader;

        private void Start()
        {
            obj_fader = FindObjectOfType<Fader>();
            obj_sceneLoader = FindObjectOfType<SceneLoader>();
        }

        void OnDrawGizmos()
        {
            Gizmos.color = new Color(0, 0, 1,.1f);
            Gizmos.DrawCube(transform.position,GetComponent<BoxCollider>().size);
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.tag != TAG) { return; }
            if (sceneToLoad < 0) { Debug.LogError("Scene not set"); return; }

            StartCoroutine(Transition());
        }

        IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);

            yield return StartCoroutine(LeaveScene());

            obj_sceneLoader.LoadLevel(sceneToLoad);

            yield return StartCoroutine(EnterScene());

            Destroy(gameObject);
        }

        IEnumerator LeaveScene()
        {
            PlayerControlEnabled(false);
            yield return obj_fader.FadeToAlpha(1, fadeOutTime);
        }

        IEnumerator EnterScene()
        {
            yield return new WaitForSeconds(fadeWaitTime);
            yield return obj_fader.FadeToAlpha(0, fadeInTime);
            PlayerControlEnabled(true);
        }

        void PlayerControlEnabled(bool isEnabled)
        {
            GameObject.FindWithTag(TAG).GetComponent<PlayerController>().enabled = isEnabled;
        }
    }
}