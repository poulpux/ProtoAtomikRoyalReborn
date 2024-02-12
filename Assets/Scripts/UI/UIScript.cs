using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIScript : MonoBehaviour
{
    [SerializeField] PlayerStat _playerStat;
    [SerializeField] Image _imageHealthBarGreen;
    [SerializeField] private TMP_Text _textHealth;

    void Start()
    {
        _textHealth.text = _playerStat._pv.ToString();
    }

    public void UpdateViewHealthBarGreen()
    {
        _textHealth.text = "" + _playerStat._pv;
        _imageHealthBarGreen.fillAmount = _playerStat._pv / _playerStat._pvSAVE;
    }
}