using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GokUtil.UpdateManager;
using UnityStandardAssets.CrossPlatformInput;

public class EditMove : MonoBehaviour, IUpdatable
{
    public float speed;
    public float turningSpeed;
    Rigidbody2D r2;
    void OnEnable()
    {
        UpdateManager.AddUpdatable(this);
        r2 = GetComponent<Rigidbody2D>();
    }
    void OnDisable()
    {
        UpdateManager.RemoveUpdatable(this);
    }
    public void UpdateMe()
    {
        //移動
        float x = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        float y = CrossPlatformInputManager.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(x, y);
        r2.AddForce(direction * speed);

        //回転処理
        float vx = CrossPlatformInputManager.GetAxisRaw("VirtualX");
        float vy = CrossPlatformInputManager.GetAxisRaw("VirtualY");
        Vector3 rotationVec = new Vector3(vx, vy);
        if (rotationVec == Vector3.right)
        {
            transform.eulerAngles = new Vector3(0, 0, -90);
        }
        else if (rotationVec == Vector3.up)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (rotationVec == Vector3.left)
        {
            transform.eulerAngles = new Vector3(0, 0, 90);
        }
        else if (rotationVec == Vector3.down)
        {
            transform.eulerAngles = new Vector3(0, 0, 180);
        }
        else
        {
            r2.AddTorque(turningSpeed * -vx);
        }


    }

}
