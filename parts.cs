using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parts : MonoBehaviour
{

    public GameObject prefub;//出現させるやつ
    public void OnClickButton()
    {
        GameObject g = Instantiate(prefub);
        g.transform.SetParent(transform.parent.parent.GetComponent<SaveCShip>().save.transform);
        transform.parent.parent.GetComponent<SaveCShip>().currentParts = g;
        transform.parent.GetComponent<PartsCheck>().Check(g);
        transform.parent.parent.GetComponent<SaveCShip>().currentParsScript = this;
    }
    public void Complete()
    {
        transform.parent.GetComponent<PartsCheck>().save = null;
    }
}
