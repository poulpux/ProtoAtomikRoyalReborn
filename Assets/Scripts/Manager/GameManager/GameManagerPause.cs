using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public partial class GameManager
{
    State pause = new State();
    private void onPauseEnter()
    {
        Time.timeScale = 0f;
    }
    private void onPauseUpdate()
    {

    }

    private void onPauseExit()
    {
        Time.timeScale = 1f;
    }

    public void GoToPause()
    {
        ChangeState(pause);
    }

    public void QuitPause()
    {
        ChangeState(game);
    }

}
