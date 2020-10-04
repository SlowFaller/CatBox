using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace LD47.UI
{
    public class StartButton : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // load's Jake's scene
        public void OnClick()
        {
            Debug.Log("Starting game");
            SceneManager.LoadScene(2);
        }
    }
}
