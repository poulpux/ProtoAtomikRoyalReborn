using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> menu, option;
    [SerializeField]
    private List<Image> sliderVisu;

    private bool goToOption, goToMenu;
    AnimatingCurve animatingCurve = new AnimatingCurve(Vector3.zero, new Vector3(1f, 1f, 1f), 0.3f, GRAPH.EASESIN, INANDOUT.IN, LOOP.CLAMP);

    void Update()
    {
        if(goToOption)
            TransitToOption();

        if(goToMenu)
            TransitToMainMenu();
    }
    private void TransitToOption()
    {
        if (Tools.isCurveFinish(animatingCurve))
        {
            goToOption = false;
            foreach (var item in menu)
                item.gameObject.SetActive(false);
        }
        Vector3 opacity = new Vector3(option[0].color.a, 0f, 0f);
        Tools.PlayCurve(ref animatingCurve, ref opacity);
        foreach (var item in menu)
            item.color = new Color(item.color.r, item.color.g, item.color.b, 1f - opacity.x);

        foreach (var item in option)
            item.color = new Color(item.color.r, item.color.g, item.color.b, opacity.x);

        foreach (var item in sliderVisu)
            item.color = new Color(item.color.r, item.color.g, item.color.b, opacity.x);
    }
    private void TransitToMainMenu()
    {
        if (Tools.isCurveFinish(animatingCurve))
        {
            goToMenu = false;
            foreach (var item in sliderVisu)
                item.gameObject.SetActive(false);

            foreach (var item in option)
                item.gameObject.SetActive(false);
        }
        Vector3 opacity = new Vector3(menu[0].color.a, 0f, 0f);
        Tools.PlayCurve(ref animatingCurve, ref opacity);
        foreach (var item in option)
            item.color = new Color(item.color.r, item.color.g, item.color.b, 1f - opacity.x);

        foreach (var item in sliderVisu)
            item.color = new Color(item.color.r, item.color.g, item.color.b, 1f - opacity.x);

        foreach (var item in menu)
            item.color = new Color(item.color.r, item.color.g, item.color.b, opacity.x);
    }
    public void GoToOption()
    {
        if (!goToMenu)
        {
            goToOption = true;
            animatingCurve.timeSinceBegin = 0f;
            foreach (var item in option)
                item.gameObject.SetActive(true);

            foreach (var item in sliderVisu)
                item.gameObject.SetActive(true);
        }
    }
    
    public void GoToMenu()
    {
        if (!goToOption)
        {
            goToMenu = true;
            animatingCurve.timeSinceBegin = 0f;
            foreach (var item in menu)
                item.gameObject.SetActive(true);
        }
    }
}
