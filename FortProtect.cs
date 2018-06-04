using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GokUtil.UpdateManager;

public class FortProtect : MonoBehaviour, IUpdatable
{
    public GameObject Parent;//Fort本体
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
    public bool thisIsFort = false;//要塞本体かどうか

    public GameObject cage;
    private AudioSource au;
    private float shotRateRange;

    private float rateTime;
    void Start()
    {
        au = GetComponent<AudioSource>();
        au.Stop();
        shotRateRange = shotRate;
    }
    void OnEnable()
    {
        UpdateManager.AddUpdatable(this);
        cage = GameObject.Find("BulletCage");
        
    }
    void OnDisable()
    {
        UpdateManager.RemoveUpdatable(this);
    }
    public void UpdateMe()
    {
        if (rateTime <= Time.time)
        {
            rateTime = Time.time + shotRateRange;
            shotRateRange = Random.Range(shotRate * 0.75f, shotRate * 1.25f);
            Shot();
        }
    }

    void Shot()
    {
        if (thisIsFort)
        {
            if (GetComponent<NormalFort>().target != null)
            {
                for (int i = 0; i < shotPoints.Length; i++)
                {
                    //ビーム生成
                    GameObject g = Instantiate(bullet, shotPoints[i].position, Quaternion.identity);
                    Vector3 direction = GetComponent<NormalFort>().target.transform.position - shotPoints[i].position;
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
        else
        {
            if (Parent.GetComponent<NormalFort>().target != null)
            {
                for (int i = 0; i < shotPoints.Length; i++)
                {
                    //ビーム生成
                    GameObject g = Instantiate(bullet, shotPoints[i].position, Quaternion.identity);
                    Vector3 direction = Parent.GetComponent<NormalFort>().target.transform.position - shotPoints[i].position;
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
    }

}
