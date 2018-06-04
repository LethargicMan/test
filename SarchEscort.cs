using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SarchEscort : MonoBehaviour
{
    public GameObject returnobj;
    public float sarchRadius=10;
    void Start()
    {
        GetComponent<CircleCollider2D>().radius = sarchRadius;
    }
    void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.gameObject.name == "Player")
        {
            returnobj.GetComponent<Pursuit>().SetTarget(colider.gameObject);
        }
    }
    void OnTriggerExit2D(Collider2D colider)
    {
        if (colider.gameObject.name == "Player")
        {
            returnobj.GetComponent<Pursuit>().SetTarget(GameManager.Instance.ReturnFortTransform());//どっかのFortに行先を設定
        }
    }
}
