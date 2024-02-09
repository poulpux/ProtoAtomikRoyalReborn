using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParcoursDuCombattantLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject player, spawnPos;
    [SerializeField]
    private Collider triggerFinishZone;
    [SerializeField]
    private List<GameObject> allColliders;
    [SerializeField]
    private TextMeshProUGUI text, textTimer;
    private bool isDoingParcours, compteARebours;
    [SerializeField]
    private GameObject Confeti;
    float timer;
    void Start()
    {
        Debug.Log(PlayerPrefs.GetFloat("BestScore").ToString("F2"));
        InstantiateAll();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (CanActivate())
            SetTextHelp();
        else if (!isDoingParcours && timer > 3f)
            text.text = "";

        if (Vector3.Distance(transform.position, player.transform.position) < 2f && Input.GetKeyDown(KeyCode.E)) 
            LauchParcours();
        if (compteARebours)
            DuringCompteARebours();
        else if (isDoingParcours)
            DuringParcours(); 
    }

    private void InstantiateAll()
    {
        timer = 0f;
        if (PlayerPrefs.GetFloat("BestScore") == 0f)
            PlayerPrefs.SetFloat("BestScore", 99f);
        //PlayerPrefs.SetFloat("BestScore", 99f);
        isDoingParcours = false;
        foreach (var item in allColliders)
            item.SetActive(false);

        textTimer.text = "Record : "+ PlayerPrefs.GetFloat("BestScore").ToString("F2");
        text.text = "";
        triggerFinishZone.gameObject.SetActive(false);
    }

    private void SetTextHelp()
    {
        text.text = "Press E To Start the obstacle course";
        text.fontSize = 80f;
    }

    private bool CanActivate()
    {
        return Vector3.Distance(transform.position, player.transform.position) < 2f && !isDoingParcours;
    }

    private void LauchParcours()
    {
        text.fontSize = 330f;
        foreach (var item in allColliders)
            item.SetActive(true);
        isDoingParcours = true;
        compteARebours = true;
        timer = 0;
        player.GetComponent<PlayerThr>().mine = false;
    }

    private void DuringCompteARebours()
    {
        if (timer > 3f)
        {
            triggerFinishZone.gameObject.SetActive(true);
            text.text = "";
            compteARebours = false;
            timer = 0;
        }
        else if (timer > 2f)
            text.text = "1";
        else if (timer > 1f)
            text.text = "2";
        else
            text.text = "3";

        player.transform.position = spawnPos.transform.position+Vector3.up*0.5f;
    }

    private void DuringParcours()
    {
        textTimer.text = timer.ToString("F2");
    }

    public void LevelFinish()
    {
        float timerSafe = timer;
        InstantiateAll();
        if (timerSafe < PlayerPrefs.GetFloat("BestScore"))
        {
            PlayerPrefs.SetFloat("BestScore", timerSafe);
            StartCoroutine(NouveauScore(true));
        }
        else
            StartCoroutine(NouveauScore(false, timerSafe));

    }

    private IEnumerator NouveauScore(bool hightscore, float timerSave = 0f)
    {
        Debug.Log("passe");
        text.text = hightscore ? "NEW HIGHTSCORE !!! : " + PlayerPrefs.GetFloat("BestScore").ToString("F2") : "Score : " + timerSave.ToString("F2");
        text.fontSize = 80f;

        yield return new WaitForSeconds(3f);

        text.text = "";
    }
}
