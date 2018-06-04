using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GokUtil.UpdateManager;

public class DamageText : MonoBehaviour, IUpdatable
{
    public float speed = 1;//上昇スピード
    Text txt;
    float s;
    private float time;
    public float fadeTime = 1;
    float alpha = 1;
    void Start()
    {
        // Destroy(gameObject, 2);

        txt = GetComponent<Text>();
        gameObject.SetActive(false);
    }
    void OnEnable()
    {
        UpdateManager.AddUpdatable(this);
    }
    void OnDisable()
    {
        UpdateManager.RemoveUpdatable(this);
    }

    public void UpdateMe()
    {
        transform.position = transform.position + new Vector3(0, speed, 0);
        var color = txt.color;
        color.a = alpha;
        txt.color = color;
        alpha -= (1.0f / fadeTime) * Time.deltaTime;
        if (alpha <= 0) gameObject.SetActive(false);

    }

    /// <summary>
    /// 外部呼出し
    /// </summary>
    /// <param name="content">ダメージ数</param>
    /// <param name="size">定数以上に大きくしたければ</param>
    /// <param name="position">出す場所</param>
    public void UpdateText(string content, int size, Vector3 position)
    {
        gameObject.SetActive(true);
        alpha = 1;
        txt = GetComponent<Text>();
        txt.text = content;
        int a = (int)GameObject.Find("Main Camera").GetComponent<FollowCamera>().range;//カメラが離れている分文字を大きく
        txt.fontSize = a;
        txt.fontSize += size;
        gameObject.transform.position = position;
        //GameObject.Find("MainCanvas").GetComponent<CanvasManager>().AddContants(gameObject);
        //transform.SetParent(GameObject.Find("MainCanvas").transform);
        transform.localScale = Vector3.one;
        if (txt.fontSize >= a + 20) txt.fontSize = a + 20;
    }
    public void UpdateText(string content, Vector3 position)
    {
        gameObject.SetActive(true);
        alpha = 1;
        txt = GetComponent<Text>();
        txt.text = content;
        int a = (int)GameObject.Find("Main Camera").GetComponent<FollowCamera>().range;//カメラが離れている分文字を大きく
        txt.fontSize = 10+a;
        gameObject.transform.position = position;
        transform.localScale = Vector3.one;
        if (txt.fontSize >= a + 20) txt.fontSize = a + 20;
    }
    //void Update()
    //{
    //    transform.position = transform.position + new Vector3(0, speed, 0);
    //    var color = txt.color;
    //    color.a = alpha;
    //    txt.color = color;
    //    alpha -= (1.0f / fadeTime) * Time.deltaTime;

    //}
}
