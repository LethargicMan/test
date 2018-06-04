using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SBB : MonoBehaviour
{

    public GameObject prefub;//出現させるやつ
    public void OnClickButton()
    {
        GameObject g = Instantiate(prefub);
        transform.parent.GetComponent<SSBCheck>().Check(g);
    }
}
