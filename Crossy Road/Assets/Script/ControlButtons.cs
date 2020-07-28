using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlButtons : MonoBehaviour
{

    public GameObject buttons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void pressPlay()
    {
        Application.LoadLevel("Game");
    }

    public void pressExit()
    {
        Application.Quit();
    }

}
