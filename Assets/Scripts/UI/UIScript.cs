using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    [SerializeField] PlayerStats scriptPlayer;
    [SerializeField] private Image healthBarplayer1;
    [SerializeField] private TMP_Text textHPPlayer1;


    private void Update()
    {
        textHPPlayer1.text = scriptPlayer._pv.ToString();
        healthBarplayer1.fillAmount = scriptPlayer._pv / scriptPlayer._pvSAVE;
    }
}
