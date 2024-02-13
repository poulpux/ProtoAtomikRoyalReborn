using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoSceneGame : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game1Player 2");
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Game1Player");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PlayTestClement");
    }
}
