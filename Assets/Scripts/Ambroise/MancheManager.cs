using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MancheManager : MonoBehaviour
{
    [SerializeField]
    private CooldownBomb player1, player2;

    [SerializeField]
    private TextMeshProUGUI pointJ1, pointJ2, winTxt;
    [SerializeField]
    private int nbPointForWin = 5; 

    void Start()
    {
        pointJ1.text = PlayerPrefs.GetInt("Score2").ToString();
        pointJ2.text = PlayerPrefs.GetInt("Score1").ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            resetGame();

        if (player1.hp <= 0)
            StartCoroutine(DelayedMancheGagn�(true));
        if(player2.hp <= 0)
            StartCoroutine(DelayedMancheGagn�(false));
           

    }

    private void resetGame()
    {
        PlayerPrefs.SetInt("Score1", 0);
        PlayerPrefs.SetInt("Score2", 0);
        DestroyAll();
        UnityEngine.SceneManagement.SceneManager.LoadScene("AmbroisePlay");
        //SceneManager.LoadScene("AmbroisePlay");
    }

    IEnumerator DelayedMancheGagn�(bool player1)
    {
        Tools.TimeScale(0.2f);
        // Attendez pendant 3 secondes
        yield return new WaitForSeconds(0.75f);

        // Appelez la fonction MancheGagn� avec un joueur fictif (par exemple, le joueur 1)
        MancheGagn�(player1);
    }


    private void MancheGagn�(bool player1)
    {

        if (player1)
        {
            if (PlayerPrefs.GetInt("Score1") == 4)
                Victory(true);
            else
            {
                PlayerPrefs.SetInt("Score1", PlayerPrefs.GetInt("Score1") + 1);
                DestroyAll();
                UnityEngine.SceneManagement.SceneManager.LoadScene("AmbroisePlay");
                //SceneManager.LoadScene("AmbroisePlay");
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("Score2") == 4)
                Victory(false);
            else
            {
                PlayerPrefs.SetInt("Score2", PlayerPrefs.GetInt("Score2") + 1);
                DestroyAll();
                UnityEngine.SceneManagement.SceneManager.LoadScene("AmbroisePlay");
                //SceneManager.LoadScene("AmbroisePlay");
            }
        }
    }

    private void Victory(bool player1)
    {
        if (player1)
            winTxt.text = "Player 2 Victory !!!";
        else
            winTxt.text = "Player 1 Victory !!!";
    }

    private void DestroyAll()
    {
        GameObject[] objectsToDestroy = GameObject.FindObjectsOfType<GameObject>();

        foreach (var obj in objectsToDestroy)
        {
            if (obj != gameObject)
            {
                Destroy(obj);
            }
        }
    }
}