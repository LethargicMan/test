using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GokUtil.UpdateManager;
using UnityStandardAssets.CrossPlatformInput;

public class CanonTurning : MonoBehaviour, IUpdatable
{
    public float turningSpeed = 50;//旋回速度
    public float restrictionTurn = 180;//旋回角度制限(180で360度旋回可能)(通常角度制限つけるときは90以下に、それ以外は180で)
    public bool isMobile = false;
    public canon[] canons;

    void OnEnable()
    {
        UpdateManager.AddUpdatable(this);
        isMobile = UpdateManager.isMobile;
    }
    void OnDisable()
    {
        UpdateManager.RemoveUpdatable(this);
    }
    public void Edit()
    {
        for (int i = 0; i < canons.Length; i++)
        {
            canons[i].enabled = false;
        }
        this.enabled = false;
    }

    public void UpdateMe()
    {
        if (isMobile)
        {
            float x = CrossPlatformInputManager.GetAxisRaw("VirtualX");
            float y = CrossPlatformInputManager.GetAxisRaw("VirtualY");
            Vector3 vec = new Vector3(x, y);
            if (vec != Vector3.zero)
            {

                float value = Vector3.Cross(transform.up, -vec.normalized).z;
                if (vec.magnitude >= 0.5f)
                {
                    for (int i = 0; i < canons.Length; i++)
                    {
                        canons[i].SetShot(true);
                    }
                }
                else
                {
                    for (int i = 0; i < canons.Length; i++)
                    {
                        canons[i].SetShot(false);
                    }
                }

                if (Mathf.Abs(value) >= 0.1f)
                {
                    if (value <= 0)
                    {
                        transform.Rotate(Vector3.forward * turningSpeed * Time.deltaTime);
                    }
                    else if (value >= 0)
                    {
                        transform.Rotate(Vector3.forward * -turningSpeed * Time.deltaTime);
                    }
                }
            }
            else
            {
                for (int i = 0; i < canons.Length; i++)
                {
                    canons[i].SetShot(false);
                }
            }

        }
        else
        {
            Vector3 vec = Input.mousePosition;
            vec.z = -Camera.main.transform.position.z;
            vec = Camera.main.ScreenToWorldPoint(vec);
            vec = vec - transform.root.transform.position;
            float value = Vector3.Cross(transform.up, -vec.normalized).z;
            if (Mathf.Abs(value) >= 0.1f)
            {
                if (value <= 0)
                {
                    transform.Rotate(Vector3.forward * turningSpeed * Time.deltaTime);
                }
                else if (value >= 0)
                {
                    transform.Rotate(Vector3.forward * -turningSpeed * Time.deltaTime);
                }
            }
        }

    }


    /// <summary>
    /// 未完成だから上の通り回転制御無しでやってるよ
    /// 完成させて
    /// </summary>
    //void test()
    //{
    //    Vector3 vec = Input.mousePosition;
    //    vec.z = -Camera.main.transform.position.z;
    //    vec = Camera.main.ScreenToWorldPoint(vec);
    //    vec = vec - transform.root.transform.position;
    //    float value = Vector3.Cross(transform.up, -vec.normalized).z;
    //    if (Mathf.Abs(value) >= 0.1f)
    //    {
    //        float a = Vector3.Angle(transform.up, firstUp.transform.position);
    //        Debug.Log(a);
    //        if (a <= restrictionTurn)
    //        {

    //            if (value <= 0)
    //            {
    //                transform.Rotate(Vector3.forward * turningSpeed * Time.deltaTime);
    //            }
    //            else if (value >= 0)
    //            {
    //                transform.Rotate(Vector3.forward * -turningSpeed * Time.deltaTime);
    //            }
    //        }
    //        else
    //        {
    //            if (value <= 0)
    //            {
    //                a = Vector3.Angle(transform.up, firstUp.transform.position);
    //                if (a <= restrictionTurn)
    //                {
    //                    transform.Rotate(Vector3.forward * turningSpeed * Time.deltaTime);
    //                }

    //            }
    //            else if (value >= 0)
    //            {
    //                a = Vector3.Angle(transform.up, firstUp.transform.position);
    //                if (a <= restrictionTurn)
    //                {
    //                    transform.Rotate(Vector3.forward * -turningSpeed * Time.deltaTime);
    //                }
    //            }
    //        }
    //    }
    //}
}
