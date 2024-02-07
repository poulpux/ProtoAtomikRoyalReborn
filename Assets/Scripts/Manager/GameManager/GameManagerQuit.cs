using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class GameManager
{
    State quit = new State();

    private void onQuitEnter()
    {
        Application.Quit();
    }
}
