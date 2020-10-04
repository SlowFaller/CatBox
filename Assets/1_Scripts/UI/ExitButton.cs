using UnityEngine;
using UnityEngine.UI;

namespace LD47.UI
{
    public class ExitButton : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // quits the game
        public void OnClick()
        {
            Debug.Log("Quitting the game");
            Application.Quit();
        }

        // Update is called once per frame
    }
}
