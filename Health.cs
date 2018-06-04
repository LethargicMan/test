using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GokUtil.UpdateManager;

public class Health : MonoBehaviour, IUpdatable
{

    //耐久値のある全ての装備はこれをつける
    public int scrap;//部品
    public int gold;//金
    public int health;
    public GameObject damageText;
    public Image playerSlider;//playerの場合のみ
    private int firstHealth;//初期
    public bool cure = false;//自動回復あり無し
    [Range(0, 1000)]
    public float cureAmount;//回復量(自身の最大HPからの割合回復)

    private int savingDamage;
    private int count;
    private int cureCount;
    void Start()
    {

        firstHealth = health;
        if (gameObject.name == "Player")
        {
            playerSlider = GameObject.Find("PlayerSlider").GetComponent<Image>();
            playerSlider.fillAmount = 1;
        }
    }

    public void Damage(int damage, Vector3 position)
    {
        float final = damage * Random.Range(0.8f, 1.25f);
        int result = Mathf.FloorToInt(final);
        health -= result;

        GameObject.Find("DamageCanvas").GetComponent<DamageCanvas>().CallDamage(position, damage);
        if (health <= 0)
        {

            Destroy(gameObject);
        }


    }
    //public void Damage(int damage, GameObject target)
    //{
    //    float final = damage * Random.Range(0.8f, 1.25f);
    //    int result = Mathf.FloorToInt(final);
    //    health -= result;
    //    GameObject.Find("DamageCanvas").GetComponent<DamageCanvas>().CallDamage(target.transform.position, damage);
    //    GameObject.Find("DamageCanvas").GetComponent<DamageCanvas>().CallSlider(target, firstHealth, health);
    //    if (health <= 0)
    //    {

    //        alive = false;
    //        Destroy(gameObject);
    //    }
    //}
    public void Damage(int damage, GameObject target)
    {
        float final = damage * Random.Range(0.8f, 1.25f);
        int result = Mathf.FloorToInt(final);
        health -= result;
        savingDamage += result;
        if (playerSlider != null)
        {
            playerSlider.fillAmount = (float)health / (float)firstHealth;
        }

        if (health <= 0)
        {
            GameManager.Instance.AddReward(scrap, gold);//
            switch (gameObject.name)
            {
                case "Missile":

                    break;
                case "Player":
                    GameManager.Instance.PlayerDeath();
                    Destroy(gameObject);
                    break;
                default:

                    Destroy(gameObject);
                    break;

            }

        }


    }
    void Cure()
    {
        float s = firstHealth * (cureAmount / 1000);
        s = Mathf.FloorToInt(s);
        health += (int)s;
        if (health > firstHealth) health = firstHealth;
        GameObject.Find("DamageCanvas").GetComponent<DamageCanvas>().CallCureText(transform.position, (int)s);
        if (playerSlider != null)
        {
            playerSlider.fillAmount = (float)health / (float)firstHealth;
        }
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
        count++;

        if (count >= 60)
        {
            count = 0;
            if (savingDamage >= 1)
            {
                GameObject.Find("DamageCanvas").GetComponent<DamageCanvas>().CallDamage(transform.position, savingDamage);
                GameObject.Find("DamageCanvas").GetComponent<DamageCanvas>().CallSlider(gameObject, firstHealth, health);
                savingDamage = 0;
            }
            if (health < firstHealth && cure)
            {
                Cure();
            }
        }
    }
}
