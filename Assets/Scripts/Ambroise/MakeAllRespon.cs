using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MakeAllRespon : MonoBehaviour
{
    [SerializeField]
    GameObject objet;

    [SerializeField]
    private GameObject save;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("Game");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.SetInt("Score1", 0);
            PlayerPrefs.SetInt("Score2", 0);
            SceneManager.LoadScene("AmbroisePlay");
            SceneManager.LoadScene("AmbroisePlay");
        }
    }
}
