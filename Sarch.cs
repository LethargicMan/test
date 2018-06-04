using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sarch : MonoBehaviour
{
    public GameObject returnObject;//当たりを返す

    void Start()
    {
        GetComponent<CircleCollider2D>().radius = transform.parent.GetComponent<NormalFort>().sarchRadius;
    }
    void OnTriggerEnter2D(Collider2D colider)
    {
        if (colider.gameObject.name == "Player")
        {
            returnObject.GetComponent<NormalFort>().enterPlayer = true;
            returnObject.GetComponent<NormalFort>().target = colider.gameObject;//ターゲット登録
        }
    }
    void OnTriggerExit2D(Collider2D colider)
    {
        if (colider.gameObject.name == "Player")
        {
            returnObject.GetComponent<NormalFort>().enterPlayer = false;
            returnObject.GetComponent<NormalFort>().target = null;//攻撃できなくする
        }
    }
}
