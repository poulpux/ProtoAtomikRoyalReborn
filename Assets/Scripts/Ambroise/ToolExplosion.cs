using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolExplosion : MonoBehaviour
{
    static public float reductionTime = 20f;

    static public void BrokeObject(GameObject objectToDestroy, Transform bombTransform, float power)
    {
        Fracture fracture = objectToDestroy.GetComponent<Fracture>();
        if (fracture != null)
        {
            Vector3 oppositeDirection = (objectToDestroy.transform.position - (bombTransform.position-Vector3.up)).normalized;
            fracture.ComputeFracture();
            DestroyParents(fracture.fragmentRoot);

            fracture.fragmentRoot.GetComponentInChildren<Rigidbody>().AddForce(oppositeDirection * power, ForceMode.Impulse);

            Transform[] listChildren = fracture.fragmentRoot.GetComponentsInChildren<Transform>();
            // Utilisez une coroutine pour la destruction progressive
            foreach (var item in listChildren)
            {
                StartScaleAndDestroyCoroutine(item.gameObject);
            }

            Destroy(objectToDestroy);
        }
    }

    public static void StartScaleAndDestroyCoroutine(GameObject objectToDestroy)
    {
        MonoBehaviour script = objectToDestroy.AddComponent<ScaleAndDestroy>();
        ScaleAndDestroy scaleAndDestroyScript = script as ScaleAndDestroy;

        // Assurez-vous que le script a été ajouté avec succès
        if (scaleAndDestroyScript != null)
        {
            // Lancer la coroutine depuis cet objet
            scaleAndDestroyScript.StartCoroutine(scaleAndDestroyScript.WillBeDestroy());
        }
    }

    public static void DestroyParents(GameObject fragmentMother)
    {
        ScaleAndDestroy scaleAndDestroyScript = fragmentMother.AddComponent<ScaleAndDestroy>();
        if (scaleAndDestroyScript != null)
        scaleAndDestroyScript.StartCoroutine(scaleAndDestroyScript.DestroyAll());
    }
}

public class ScaleAndDestroy : MonoBehaviour
{
    // Méthode pour la coroutine
    public IEnumerator WillBeDestroy()
    {
        yield return new WaitForSeconds(ToolExplosion.reductionTime);

        // Réduction progressive de la scale
        float elapsedTime = 0f;
        Vector3 initialScale = transform.localScale;

        Vector2 posBase = new Vector2(transform.position.x, transform.position.z);
        while (elapsedTime < ToolExplosion.reductionTime)
        {
            transform.position = new Vector3(posBase.x, transform.position.y, posBase.y);
            float t = elapsedTime / ToolExplosion.reductionTime;
            transform.localScale = Vector3.Lerp(initialScale, Vector3.one * 0.1f, t);
            elapsedTime += Time.deltaTime * 3f;
            yield return null;
        }

        // Assurez-vous que la scale est correcte à la fin
        transform.localScale = Vector3.one * 0.1f;

        //// Détruire l'objet
        //Destroy(gameObject);
    }

    public IEnumerator DestroyAll()
    {
        yield return new WaitForSeconds(ToolExplosion.reductionTime);
        float elapsedTime = 0f;

        Vector2 posBase = new Vector2(transform.position.x, transform.position.z);
        while (elapsedTime < ToolExplosion.reductionTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject);
    }
}
