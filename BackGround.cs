using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GokUtil.UpdateManager;

public class BackGround : MonoBehaviour, IUpdatable
{

    public GameObject player;
    public float speed;
    public float oderInLayer;
    private Renderer re;
    void Start()
    {
        re = GetComponent<Renderer>();
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
        if (player != null)
        {
            transform.position = player.transform.position + new Vector3(0, 0, 5 - oderInLayer); ;
            Vector2 offset = player.transform.position * -speed * 0.001f;
            re.sharedMaterial.SetTextureOffset("_MainTex", offset);
        }
        else
        {
            player = GameObject.Find("Player");
        }
    }
}
