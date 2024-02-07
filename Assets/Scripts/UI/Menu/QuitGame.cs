using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void QuitGameFonction()
    {
        Debug.Log("Quitte la partie");
        Application.Quit();
    }
}
