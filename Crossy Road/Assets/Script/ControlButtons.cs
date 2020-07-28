using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlButtons : MonoBehaviour
{

    public void pressPlay()
    {
        Application.LoadLevel("Game");
    }

    public void pressExit()
    {
        Application.Quit();
    }

}
