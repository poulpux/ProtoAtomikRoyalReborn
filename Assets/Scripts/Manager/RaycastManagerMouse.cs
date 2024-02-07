using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RaycastManagerMouse : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;

    //Events


    [SerializeField]
    private GraphicRaycaster UIRaycaster;

    [SerializeField]
    EventSystem eventSystem;
    PointerEventData pointerEventData;

    private int _currentScene;
    private static RaycastManagerMouse instance;
    public static RaycastManagerMouse Instance { get { return instance; } }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        pointerEventData = new PointerEventData(eventSystem);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            RaycastHit(Input.mousePosition);
    }

    //----------------------------------------------------------------------------------------------------

    private void RaycastHit(Vector3 pos)
    {
        if (_currentScene == 0)
        {
            if(MenuUI(pos) == false)
                Menu(pos);
        }
        else if (_currentScene == 1)
        {
            if (GameUI(pos) == false)
                Game(pos);
        }
    }

    private bool MenuUI(Vector3 pos)
    {
        pointerEventData.position = pos;
        List<RaycastResult> results = new List<RaycastResult>();
        UIRaycaster.Raycast(pointerEventData, results);

        if (results.Count > 0)
        {
            //Your UnityEvent
            return true;
        }

        return false;
    }

    private void Menu(Vector3 pos)
    {
        Ray ray = _camera.ScreenPointToRay(pos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            //Your UnityEvent
        }
    }

    private bool GameUI(Vector3 pos)
    {
        pointerEventData.position = pos;
        List<RaycastResult> results = new List<RaycastResult>();
        UIRaycaster.Raycast(pointerEventData, results);

        if (results.Count > 0)
        {
            //Your UnityEvent
            return true;
        }

        return false;
    }

    private void Game(Vector3 pos)
    {
        Ray ray = _camera.ScreenPointToRay(pos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "YourTag")
            {
                //Your UnityEvent
            }
        }
    }

}
