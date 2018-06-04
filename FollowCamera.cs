using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{

    public GameObject target;//追跡対象
    public float rate = 1;//追跡を遅らせる程度
    [Range(3, 12)]
    public float range = 10;//距離
    private Vector3 vec = Vector3.zero;
    // Use this for initialization
    void Start()
    {
        if (target != null)
        {
            transform.position = target.transform.position;
        }

    }
    public void UpdataRange(float a)
    {
        range = a;
    }


    // ここはFixedUpdateのままで
    void FixedUpdate()
    {
        if (target != null)
        {
            Camera.main.orthographicSize = range;
            Vector3 b = target.transform.TransformPoint(new Vector3(0, 0, -range * 3));
            transform.position = Vector3.SmoothDamp(transform.position, b, ref vec, rate);
        }
        else
        {
            target = GameObject.Find("Player");
        }
    }
}
