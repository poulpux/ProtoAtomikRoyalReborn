using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MancheManager : MonoBehaviour
{
    [SerializeField]
    private CooldownBomb player1, player2;

    [SerializeField]
    private int nbPointForWin = 5; 

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            resetGame();

        if(player1.hp <= 0)
            MancheGagné(true);
        if(player2.hp <= 0)
            MancheGagné(false);

    }

    private void resetGame()
    {
        SceneManager.LoadScene("AmbroisePlayer");
        PlayerPrefs.SetInt("Score1", 0);
        PlayerPrefs.SetInt("Score2", 0);
    }

    private void MancheGagné(bool player1)
    {

        if (player1)
        {
            if (PlayerPrefs.GetInt("Score1") == 4)
                Victory(true);
            else
            {
                PlayerPrefs.SetInt("Score1", PlayerPrefs.GetInt("Score1") + 1);
                SceneManager.LoadScene("AmbroisePlayer");
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("Score2") == 4)
                Victory(false);
            else
            {
                PlayerPrefs.SetInt("Score2", PlayerPrefs.GetInt("Score2") + 1);
                SceneManager.LoadScene("AmbroisePlayer");
            }
        }
        SceneManager.LoadScene("AmbroisePlayer");
    }

    private void Victory(bool player1)
    {

    }
}
