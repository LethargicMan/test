using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeDamage : MonoBehaviour
{
    int damage;
    public GameObject damageText;
    public string targetTagName;
    public CircleCollider2D cc2;
    public GameObject effect;
    private bool effectCount = true;

    public void Explode(int damage, float radius)
    {
        this.damage = damage;
        cc2.radius = radius;
        cc2.enabled = true;
        StartCoroutine(death());

    }
    IEnumerator death()
    {
        yield return new WaitForSeconds(0.1f);
        if (effectCount)
        {
            GameObject g = Instantiate(effect, transform.position, Quaternion.identity);
            g.transform.localScale = new Vector3(2 * cc2.radius, 2 * cc2.radius, 2 * cc2.radius);
            Destroy(g, 2);
            effectCount = false;
        }
        Destroy(transform.parent.gameObject);
    }

    void Damage(int damage, GameObject target)
    {
        float distance = Vector3.Distance(transform.position, target.transform.position);//距離測定
        if (distance <= cc2.radius) distance = 1;
        else if (distance >= cc2.radius) distance = cc2.radius / distance;//爆発半径からのダメージ減衰
        distance = damage * distance;//ダメージに反映
        int result = Mathf.FloorToInt(distance);
        target.GetComponent<Health>().Damage(result, target);
        if (effectCount)
        {
            GameObject g = Instantiate(effect, transform.position, Quaternion.identity);
            g.transform.localScale = new Vector3(2 * cc2.radius, 2 * cc2.radius, 2 * cc2.radius);
            Destroy(g, 2);
            effectCount = false;
        }
        Destroy(transform.parent.gameObject);
        //transform.parent = null;

    }
    void OnTriggerEnter2D(Collider2D colision)
    {
       
        if (colision.tag != targetTagName)
        {
           
            Damage(damage, colision.gameObject);
        }

    }


}
