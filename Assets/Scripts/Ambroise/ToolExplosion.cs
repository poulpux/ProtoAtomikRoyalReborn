using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolExplosion : MonoBehaviour
{
    static public float reductionTime = 2f;

    static public void BrokeObject(GameObject objectToDestroy, Transform bombTransform, float power)
    {
        Vector3 oppositeDirection = (objectToDestroy.transform.position - bombTransform.position).normalized;
        Fracture fracture = objectToDestroy.GetComponent<Fracture>();
        fracture.ComputeFracture();

        fracture.fragmentRoot.GetComponentInChildren<Rigidbody>().AddForce(oppositeDirection * power, ForceMode.Impulse);

        Transform[] listChildren = fracture.fragmentRoot.GetComponentsInChildren<Transform>();
        // Utilisez une coroutine pour la destruction progressive
        foreach (var item in listChildren)
        {
            Debug.Log(item.name);
            CoroutineHelper.StartScaleAndDestroyCoroutine(item.gameObject);
        }
    }
}

public class CoroutineHelper : MonoBehaviour
{
    // Fonction statique pour lancer la coroutine
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
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Assurez-vous que la scale est correcte à la fin
        transform.localScale = Vector3.one * 0.1f;

        // Détruire l'objet
        Destroy(gameObject);
    }
}
