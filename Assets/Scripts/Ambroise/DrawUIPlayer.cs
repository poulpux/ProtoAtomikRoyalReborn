using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrawUIPlayer : MonoBehaviour
{
    [SerializeField]
    private CooldownBomb cooldown;

    [SerializeField]
    private Image greyBackgroundMine, greyBackgroundBombe, lifeBackground;

    [SerializeField]
    private TextMeshProUGUI nbMine, nbBombe, lifeText;


    private float maxScaleLife;
    void Start()
    {
        maxScaleLife = lifeBackground.rectTransform.sizeDelta.x;
    }
    void Update()
    {
        nbBombe.text = cooldown.nbBomb.ToString();
        nbMine.text = cooldown.nbMine.ToString();

        float backgroundBombScale = cooldown.timerBomb > cooldown.cooldownBomb ? 0f : cooldown.timerBomb == 0f ? 100f : 100f- cooldown.timerBomb / cooldown.cooldownBomb * 100f;
        greyBackgroundBombe.rectTransform.sizeDelta = new Vector2(greyBackgroundBombe.rectTransform.rect.width, backgroundBombScale);

        float backgroundMineScale = cooldown.timerMine > cooldown.cooldownMine ? 0f : cooldown.cooldownMine ==0f ? 100f : 100f -  cooldown.timerMine / cooldown.cooldownMine * 100f;
        greyBackgroundMine.rectTransform.sizeDelta =new Vector2(greyBackgroundMine.rectTransform.rect.width,  backgroundMineScale);

        float lifeScale = (float)cooldown.hp /(float) cooldown.maxHp  * maxScaleLife;
        lifeBackground.rectTransform.sizeDelta = new Vector2(lifeScale, lifeBackground.rectTransform.sizeDelta.y);

        lifeText.text = cooldown.hp.ToString() + " / " + cooldown.maxHp.ToString();
    }
}
