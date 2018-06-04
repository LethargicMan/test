using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GokUtil.UpdateManager;

public class Missile : MonoBehaviour, IUpdatable
{

    public int damage;
    public string targetTagName;
    public float speed = 5;
    public float rotatingSpeed = 200;
    public float exRadius = 0.5f;//爆発半径
    public GameObject target;
    public GameObject damageText;
    Rigidbody2D rb;
    bool death = false;
    // Use this for initialization
    void Start()
    {

        target = GameObject.FindGameObjectWithTag("Player");

        rb = GetComponent<Rigidbody2D>();


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
        if (target != null)
        {
            Vector2 point2Target = (Vector2)transform.position - (Vector2)target.transform.position;

            point2Target.Normalize();

            float value = Vector3.Cross(point2Target, transform.right).z;
            rb.angularVelocity = rotatingSpeed * value;


            rb.velocity = transform.right * speed;
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player");
            if (target == null && !death)
            {
                death = true;
                StartCoroutine(Death());
            }
        }
    }
    IEnumerator Death()
    {
        yield return new WaitForSeconds(Random.Range(2, 5));
        transform.GetChild(0).GetComponent<ExplodeDamage>().Explode(damage, exRadius);
        GetComponent<CircleCollider2D>().enabled = false;
    }
    // Update is called once per frame
    //void FixedUpdate()
    //{
    //    if (target != null)
    //    {
    //        Vector2 point2Target = (Vector2)transform.position - (Vector2)target.transform.position;

    //        point2Target.Normalize();

    //        float value = Vector3.Cross(point2Target, transform.right).z;

    //        /*
    //        if (value > 0) {

    //                rb.angularVelocity = rotatingSpeed;
    //        } else if (value < 0)
    //                rb.angularVelocity = -rotatingSpeed;
    //        else
    //                rotatingSpeed = 0;
    //*/

    //        rb.angularVelocity = rotatingSpeed * value;


    //        rb.velocity = transform.right * speed;
    //    }
    //    else
    //    {
    //        target = GameObject.FindGameObjectWithTag("Player");
    //    }


    //}



    void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.tag != targetTagName)
        {
            transform.GetChild(0).GetComponent<ExplodeDamage>().Explode(damage, exRadius);
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }


}
