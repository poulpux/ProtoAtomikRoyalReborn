using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public partial class GameManager
{
    private State menu = new State();

    private void onMenuEnter()
    {
        SceneManager.LoadScene("Menu");
    }
    private void onMenuUpdate()
    {
       
    }

    private void onMenuExit()
    {

    }
}
