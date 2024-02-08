using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    [SerializeField] private Image _imageTimerDash;
    [SerializeField] PlayerSlide _playerSlide;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void UpdateViewTimerDash()
    {
        _imageTimerDash.fillAmount = _playerSlide._timerCanDash / _playerSlide._timerCanDashSAVE;
    }
}
