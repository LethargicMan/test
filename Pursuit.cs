using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GokUtil.UpdateManager;

public class Pursuit : MonoBehaviour, IUpdatable
{

    public GameObject target;//これを追跡するよ(PlayerがNullならGameManagerからFortの場所にいくよ)
    //移動系
    public float rotatingSpeed = 1;
    public float speed = 1;
    private Rigidbody2D rb;

    //攻撃系
    public GameObject bullet;
    public int damage;
    public float bulletSpeed;
    [Range(0.025f, 100)]
    public float shotRate = 0.1f;
    public float lifeOverTime = 3;
    [Range(0, 1)]
    public float accuracy = 0.1f;
    public GameObject effect;
    public Transform[] shotPoints;

    public GameObject cage;
    private AudioSource au;
    private float shotRateRange;
    private float rateTime;
    void OnEnable()
    {
        UpdateManager.AddUpdatable(this);
        rb = GetComponent<Rigidbody2D>();
        cage = GameObject.Find("BulletCage");
        au = GetComponent<AudioSource>();
        au.Stop();
        shotRateRange = shotRate;
        target = GameManager.Instance.ReturnFortTransform();
    }
    void OnDisable()
    {
        UpdateManager.RemoveUpdatable(this);
    }

    public void UpdateMe()
    {

        //回転＆移動
        if (target != null)
        {
            if (target.gameObject.name == "Player")
            {
                //攻撃
                if (rateTime <= Time.time)
                {
                    rateTime = Time.time + shotRateRange;
                    shotRateRange = Random.Range(shotRate * 0.75f, shotRate * 1.25f);
                    Shot();
                }

                //移動回転
                Vector2 point2Target = (Vector2)transform.position - (Vector2)target.transform.position;

                point2Target.Normalize();

                float value = Vector3.Cross(point2Target, transform.up).z;
                rb.angularVelocity = rotatingSpeed * value;


                rb.velocity = transform.up * speed;
            }
            else
            {
                Vector2 point2Target = (Vector2)transform.position - (Vector2)target.transform.position;
                float a = Vector3.Distance(transform.position, target.transform.position);
                if (a <= 10)//到着したら
                {
                    target = GameManager.Instance.ReturnFortTransform();//違う要塞へ
                }
                point2Target.Normalize();

                float value = Vector3.Cross(point2Target, transform.up).z;
                rb.angularVelocity = rotatingSpeed * value;


                rb.velocity = transform.up * speed;
            }

        }
    }

    public void SetTarget(GameObject g)
    {
        target = g;
    }
    public void ClearTarget()
    {
        target = null;
    }

    public void Shot()
    {
        for (int i = 0; i < shotPoints.Length; i++)
        {
            //ビーム生成
            GameObject g = Instantiate(bullet, shotPoints[i].position, Quaternion.identity);
            Vector3 direction = target.transform.position - shotPoints[i].position;
            direction.Normalize();
            Vector3 vec = new Vector3(direction.x + Random.Range(-accuracy, accuracy), direction.y + Random.Range(-accuracy, accuracy), 0);
            g.GetComponent<Rigidbody2D>().AddForce(vec.normalized * bulletSpeed);
            g.GetComponent<canonBullet>().SetBulletStatus(damage, gameObject.tag, lifeOverTime);
            g.transform.SetParent(cage.transform);
            float a = transform.localScale.x;
            g.transform.localScale = new Vector3(g.transform.localScale.x * a, g.transform.localScale.y * a, g.transform.localScale.z * a);
            g.transform.rotation = Quaternion.FromToRotation(Vector3.up, vec);

            //エフェクト生成
            GameObject s = Instantiate(effect, shotPoints[i].position, Quaternion.identity);
            s.transform.localScale = transform.localScale * 2f;
            // s.transform.SetParent(g.transform);
            Destroy(s, 2);
        }
        //音再生
        au.Stop();//
        au.Play();
    }
}
