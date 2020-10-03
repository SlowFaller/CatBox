using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        SceneManager.LoadScene(1);
    }
}
