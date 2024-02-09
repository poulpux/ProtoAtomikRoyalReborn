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
    float timer;
    void Start()
    {
        InstantiateAll();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (CanActivate())
            SetTextHelp();
        else if (!isDoingParcours)
            text.text = "";


        if (CanActivate() && Input.GetKeyDown(KeyCode.E)) 
            LauchParcours();
        if (compteARebours)
            DuringCompteARebours();
        else if (isDoingParcours)
            DuringParcours(); 
    }

    private void InstantiateAll()
    {
        foreach (var item in allColliders)
            item.SetActive(false);

        textTimer.text = "";
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

        player.transform.position = spawnPos.transform.position;
    }

    private void DuringParcours()
    {
        textTimer.text = timer.ToString("F2");
    }

    public void LevelFinish()
    {
        Debug.Log("Arrivé");
    }
}
