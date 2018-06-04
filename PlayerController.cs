using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GokUtil.UpdateManager;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour, IUpdatable
{
    public float speed = 10;
    public float turningSpeed = 1;
    public bool isMobile = false;
    public GameObject m;
    public GameObject[] particle;

    private Rigidbody2D r2;
    // Use this for initialization
    void Start()
    {
        r2 = GetComponent<Rigidbody2D>();
        isMobile = UpdateManager.isMobile;
        if (!isMobile) speed *= 20;
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
        if (isMobile) { MobileInput(); }
        else { PCInput(); }


    }
    void MobileInput()
    {
        float x = CrossPlatformInputManager.GetAxisRaw("Horizontal");
        float y = CrossPlatformInputManager.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(x, y);
        if (direction == Vector3.zero) direction = Vector3.up;
        float a = Vector3.Angle(Vector3.up, direction);
        a *= Mathf.Sign(x);

        //移動制御
        if (y > 0)
        {
            float s = (90 - Mathf.Abs(a)) * speed * direction.magnitude;
            r2.AddForce(transform.up * s);
            CheckPlayPerticle(0);
            CheckStopPerticle(3);
            CheckStopPerticle(4);
        }
        if (y < 0)
        {
            float s = Mathf.Abs(a) * (speed / 5) * direction.magnitude;
            r2.AddForce(transform.up * -s);
            CheckStopPerticle(0);
            CheckPlayPerticle(3);
            CheckPlayPerticle(4);
        }
        //回転制御
        if (a > 5 && Mathf.Abs(a) <= 170)
        {
            float s = Mathf.Abs(a) / 180;
            r2.AddTorque(-turningSpeed * s);
            CheckPlayPerticle(2);
            CheckStopPerticle(1);
        }
        if (a < -5 && Mathf.Abs(a) <= 170)
        {
            float s = Mathf.Abs(a) / 180;
            r2.AddTorque(turningSpeed * s);
            CheckPlayPerticle(1);
            CheckStopPerticle(2);
        }
        if (a == 0 || Mathf.Abs(a) >= 170)
        {
            CheckStopPerticle(0);
            CheckStopPerticle(1);
            CheckStopPerticle(2);
        }
        if (a == 0)
        {
            CheckStopPerticle(3);
            CheckStopPerticle(4);
        }

    }

    void PCInput()
    {
        if (Input.GetKey(KeyCode.W))
        {
            r2.AddForce(transform.up * speed);
            CheckPlayPerticle(0);
        }
        else
        {
            CheckStopPerticle(0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            r2.AddTorque(turningSpeed);
            CheckPlayPerticle(1);
        }
        else
        {
            CheckStopPerticle(1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            r2.AddTorque(-turningSpeed);
            CheckPlayPerticle(2);
        }
        else
        {
            CheckStopPerticle(2);
        }
        if (Input.GetKey(KeyCode.S))
        {
            r2.AddForce(transform.up * (-speed / 2));
            CheckPlayPerticle(3);
            CheckPlayPerticle(4);
        }
        else
        {
            CheckStopPerticle(3);
            CheckStopPerticle(4);
        }
        if (Input.GetKey(KeyCode.C))
        {
            GameObject g = Instantiate(m);
            float a = Random.Range(0.1f, 1);
            g.GetComponent<Missile>().exRadius = a;
            float s = g.GetComponent<Missile>().damage * (1f - a);
            g.GetComponent<Missile>().damage = (int)s;
        }
    }
    void CheckPlayPerticle(int a)
    {
        if (!particle[a].GetComponent<ParticleSystem>().isPlaying)
            particle[a].GetComponent<ParticleSystem>().Play();
    }
    void CheckStopPerticle(int a)
    {
        if (!particle[a].GetComponent<ParticleSystem>().isStopped)
            particle[a].GetComponent<ParticleSystem>().Stop();
    }
}
