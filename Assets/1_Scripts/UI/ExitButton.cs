using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
