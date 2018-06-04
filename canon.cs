using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GokUtil.UpdateManager;

public class canon : MonoBehaviour, IUpdatable
{

    public GameObject bullet;//発射する弾
    public int damage;//ダメージ
    public float bulletSpeed;//弾速
    [Range(0.025f, 100)]
    public float shotRate = 0.1f;//発射レート
    public float lifeOverTime = 3f;//生存時間
    [Range(0, 1)]
    public float accuracy = 0;//精度
    public float turningSpeed = 50;
    public GameObject effect;//発射エフェクト
    public Transform shotPoint;
    public bool ismobile = false;
    public bool isShot = false;//撃てるかどうか
    private GameObject cage;//格納用
    private Rigidbody2D rb;
    private AudioSource au;

    private float rateTime;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        au = GetComponent<AudioSource>();
        au.Stop();
        if (cage == null)
        {
            cage = GameObject.Find("BulletCage");
        }
        ismobile = UpdateManager.isMobile;

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
        if (ismobile)
        {
            if (isShot)
            {
                if (rateTime <= Time.time)
                {
                    rateTime = Time.time + shotRate;
                    Shot();
                }
            }

        }
        else
        {
            if (Input.GetKey(KeyCode.F))
            {
                if (rateTime <= Time.time)
                {
                    rateTime = Time.time + shotRate;
                    Shot();
                }
            }
        }

    }
    void Shot()
    {
        //ビーム生成
        GameObject g = Instantiate(bullet, shotPoint.position, Quaternion.identity);
        Vector3 vec = new Vector3(transform.up.x + Random.Range(-accuracy, accuracy), transform.up.y + Random.Range(-accuracy, accuracy), 0);
        g.GetComponent<Rigidbody2D>().AddForce((transform.up + vec).normalized * bulletSpeed);
        g.GetComponent<canonBullet>().SetBulletStatus(damage, gameObject.tag, lifeOverTime);
        g.transform.SetParent(cage.transform);
        float a = transform.localScale.x;
        g.transform.localScale = new Vector3(g.transform.localScale.x * a, g.transform.localScale.y * a, g.transform.localScale.z * a);
        g.transform.rotation = Quaternion.FromToRotation(Vector3.up, transform.up + vec);

        //エフェクト生成
        GameObject s = Instantiate(effect, shotPoint.position, Quaternion.identity);
        s.transform.localScale = transform.localScale * 2f;
        // s.transform.SetParent(g.transform);
        Destroy(s, 2);

        //音再生
        au.Stop();//
        au.Play();

    }
    public void SetShot(bool a)
    {
        isShot = a;
    }


}
