using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayGame : MonoBehaviour
{
    [SerializeField]
    private RawImage image;
    private bool goToFondu;

    AnimatingCurve animatingCurve = new AnimatingCurve(Vector3.zero, new Vector3(1f,1f,1f),0.7f, GRAPH.EASESIN, INANDOUT.IN, LOOP.CLAMP);
    public void PlayGameFonction()
    {
        goToFondu = true;
        image.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (goToFondu)
        {
            Vector3 pop =new Vector3( image.color.a,0f,0f);
            Tools.PlayCurve(ref animatingCurve,ref pop);
            image.color = new Color(0, 0, 0, pop.x);
            if (Tools.isCurveFinish(animatingCurve))
            {
                image.color = new Color(0, 0, 0, 1);
                GameManager.Instance.GoInGame();
            }
        }
    }
}
