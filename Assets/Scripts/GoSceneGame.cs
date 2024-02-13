using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoSceneGame : MonoBehaviour
{


    private void OnCollisionEnter(Collision collision)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("PlayTestClement");
    }
}
