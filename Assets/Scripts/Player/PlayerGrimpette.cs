using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(JumpFPS))]
public class PlayerGrimpette : MonoBehaviour
{
    [SerializeField]
    private float distanceAttrape = 1.5f;
    [SerializeField]
    private float timeToGrimpe = 1f;

    private JumpFPS jump;

    private Vector3 posToGo;
    private AnimatingCurve animCurveVertical, animCurveHorizontal;
    void Start()
    {
        jump = GetComponent<JumpFPS>();
    }

    void Update()
    {
        if(!jump.testToucheGround() && Input.GetKeyDown(KeyCode.Space))
            tryGrimpette();
        PlayCurve();
    }

    private void tryGrimpette()
    {
        Vector3 posToGo = FindPosGrimpette();
        if (posToGo != Vector3.zero)
        {
            animCurveVertical = new AnimatingCurve(transform.position,new Vector3(transform.position.x, posToGo.y, transform.position.z), timeToGrimpe /2f, GRAPH.EASESIN, INANDOUT.IN, LOOP.CLAMP);
            animCurveHorizontal = new AnimatingCurve(new Vector3(transform.position.x,posToGo.y, transform.position.z), posToGo, timeToGrimpe /2f, GRAPH.EASESIN, INANDOUT.IN, LOOP.CLAMP);
        }
    }

    private Vector3 FindPosGrimpette()
    {
        Ray ray = new Ray(transform.position - Vector3.up * 0.5f, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distanceAttrape))
        {
            if (hit.distance <= distanceAttrape)
            {
                Quaternion rotationY = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
                Vector3 rayDirection = rotationY * Vector3.forward;
                Vector3 rayOrigin = transform.position +(hit.distance + 0.1f) * rayDirection + Vector3.up * 1.2f;

                Ray ray2 = new Ray(rayOrigin, -transform.up);
                if (Physics.Raycast(ray2, out RaycastHit hit2, 2.5f))
                {
                    Debug.Log(rayOrigin + Vector3.up * (hit2.distance + 0.4f));
                    Ray ray3 = new Ray( transform.position, transform.up);
                    Ray ray4 = new Ray( transform.position+ transform.forward, transform.up);
                    if (!Physics.Raycast(ray3, out RaycastHit hit3, 3) && !Physics.Raycast(ray4, out RaycastHit hit4, 3))
                        return rayOrigin + Vector3.up * (hit2.distance+0.4f);
                }
                else
                    return Vector3.zero;
            }
        }
        return Vector3.zero;
    }

    private void PlayCurve()
    {
        if(animCurveVertical.beginValue != Vector3.zero)
        {
            if (!Tools.isCurveFinish(animCurveVertical))
            {
                Vector3 pos = transform.position;
                Tools.PlayCurve(ref animCurveVertical,ref pos);
                transform.position = pos;
            }
            else
            {
                Vector3 pos = transform.position;
                Tools.PlayCurve(ref animCurveHorizontal, ref pos);
                transform.position = pos;
            }
        }
    }
}
