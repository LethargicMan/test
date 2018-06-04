using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCShip : MonoBehaviour
{

    public GameObject save;
    public GameObject currentParts;
    public parts currentParsScript;

    public void Complete()
    {
        if (currentParts != null)
        {
            if (currentParts.GetComponent<PartsStatus>().AddCheck())
            {
                currentParts.GetComponent<PolygonCollider2D>().enabled = true;//停止時のコライダーに切り替え
                currentParts.GetComponent<BoxCollider2D>().enabled = false;
                currentParts.GetComponent<EditMove>().enabled = false;
                Destroy(currentParts.GetComponent<Rigidbody2D>());
                currentParsScript.Complete();
                Color a = currentParts.GetComponent<SpriteRenderer>().color;
                currentParts.GetComponent<SpriteRenderer>().color = new Color(a.r, a.g, a.b, 255);//透明度を戻して確定を知らせる
                currentParts.GetComponent<PartsStatus>().Add();
                currentParts = null;
            }
        }

    }
}
