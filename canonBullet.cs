using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canonBullet : MonoBehaviour
{

    public int damage;
    public string targetTagName;
    public GameObject damageText;
    public GameObject effect;
    /// <summary>
    /// 外部呼出し
    /// </summary>
    /// <param name="damage">この弾のダメージ</param>
    /// <param name="tag">この弾を撃った奴のタグ</param>
    public void SetBulletStatus(int damage, string tag)
    {
        this.damage = damage;
        targetTagName = tag;
        Destroy(gameObject, 2);
    }
    //生存時間を指定したい場合
    public void SetBulletStatus(int damage, string tag, float suvive)
    {
        this.damage = damage;
        targetTagName = tag;
        Destroy(gameObject, suvive);
    }

    /// <summary>
    /// ダメージメソッド
    /// </summary>
    /// <param name="damage"></param>
    /// <param name="target">ダメージを与える相手の</param>
    void Damage(int damage, GameObject target)
    {
        if (target.tag != "Trigger")
        {
            target.GetComponent<Health>().Damage(damage, target);
            GameObject g = Instantiate(effect, transform.position, Quaternion.identity);
            g.transform.Rotate(transform.up);
            Destroy(gameObject);
            Destroy(g, 2);
        }

    }
    void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.tag != targetTagName)
        {
            Damage(this.damage, colision.gameObject);

        }
    }
    /*
    void OnCollisionEnter2D(Collision2D colision)
    {

        if (colision.collider.tag != targetTagName)
        {

            Damage(this.damage, colision.collider.gameObject);
            Destroy(gameObject);

        }

        //switch (targetTagName)
        //{
        //    case "Player":
        //        Debug.Log("");
        //        if (colider.gameObject.tag != targetTagName)
        //        {

        //            Damage(this.damage, colider.gameObject);
        //            Destroy(gameObject);

        //        }
        //        break;
        //    case "Enemy":

        //        break;
        //    default:
        //        break;
        //}
    }*/
}
