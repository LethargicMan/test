using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsStatus : MonoBehaviour
{
    public int moduleID;//実際の部品をつけるときの為のID
    public int health;//本体に追加するHP
    public int weight;//この部品の重さ
    public int horcePow;//エンジン以外の部品は0(特殊品としても良い)

    public void Add()
    {
        if (transform.parent.GetComponent<ShipData>().CheckWeight(weight))//重さをクリアしたら
        {
            transform.parent.GetComponent<ShipData>().UpDateShipData(health, weight, horcePow);//足す
            transform.parent.GetComponent<ShipData>().AddModules(gameObject);
        }
        else//クリアしなかったら
        {
            StartCoroutine(transform.parent.GetComponent<ShipData>().OverWeight(weight));
        }

    }
    public bool AddCheck()
    {
        if (transform.parent.GetComponent<ShipData>().CheckWeight(weight))//重さをクリアしたら
        {
            return true;
        }
        else//クリアしなかったら
        {
            StartCoroutine(transform.parent.GetComponent<ShipData>().OverWeight(weight));
        }
        return false;
    }
}
