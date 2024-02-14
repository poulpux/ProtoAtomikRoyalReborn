using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeAllRespon : MonoBehaviour
{
    [SerializeField]
    GameObject objet;

    [SerializeField]
    private GameObject save;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Destroy(save);
            save = Instantiate(objet);
        }
    }
}
