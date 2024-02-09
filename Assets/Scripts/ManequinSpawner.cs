using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ManequinSpawner : MonoBehaviour
{
    [SerializeField] private Manequin _manequin;
    private static ManequinSpawner instance = null;
    public static ManequinSpawner Instance { get { return instance; } }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Instantiate(_manequin, transform.position, Quaternion.identity);
    }

    public void SpawnerManequin()
    {
        Instantiate(_manequin, transform.position, Quaternion.identity);
    }

    public IEnumerator CoroutineRespawn()
    {
        yield return new WaitForSeconds(1f);
        SpawnerManequin();
    }
}