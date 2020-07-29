using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlButtons : MonoBehaviour
{

    public void PressPlay()
    {
        Application.LoadLevel("Game");
    }

    public void PressExit()
    {
        Application.Quit();
    }

}
