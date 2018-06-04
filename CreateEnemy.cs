using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateEnemy : MonoBehaviour
{
    [Range(100, 500)]
    public float createRnage = 10;//敵を生成する範囲距離
    bool purasuMainasuX = false;//false=マイナス　true = プラス
    bool purasuMainasuY = false;//false=マイナス　true = プラス

    public Enemy[] enemy;

    public float createCout = 0;
    public float magnification = 1;

    /// <summary>
    /// プレハブは設定するものとして、その順番に対応して数を設定していくよ
    /// </summary>
    /// <param name="range"></param>
    /// <param name="counts"></param>
    public void UpdateFortStatus(float range, int[] counts)
    {
        createRnage = range;
        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i].count = counts[i];
        }
    }
    public void CreateForts()
    {
        GameManager.Instance.enemysForts=new GameObject[0];
        for (int j = 0; j < enemy.Length; j++)
        {
            for (int i = 0; i < enemy[j].count; i++)
            {
                Create2(enemy[j].fort);
            }
        }

        GameManager.Instance.SetLine();
    }

    public void Create()
    {
        float x;
        float y = 0;
        if (purasuMainasuX)
        {
            x = Random.Range(1, 1.5f);
            purasuMainasuX = !purasuMainasuX;
            if (purasuMainasuY)
            {
                y = Random.Range(1, 2f);
                purasuMainasuY = !purasuMainasuY;
            }
            else if (!purasuMainasuY)
            {
                y = Random.Range(-1.5f, -1f);
                purasuMainasuY = !purasuMainasuY;
            }
        }
        else
        {
            x = Random.Range(-1.5f, -1f);
            purasuMainasuX = !purasuMainasuX;
            purasuMainasuY = !purasuMainasuY;
            if (!purasuMainasuY)
            {
                y = Random.Range(-1.5f, -1f);
                purasuMainasuY = !purasuMainasuY;
            }
            else if (purasuMainasuY)
            {
                y = Random.Range(1, 1.5f);
                purasuMainasuY = !purasuMainasuY;
            }
        }
        createCout++;
        if (createCout > 4)
        { magnification += 1f; createCout = 0; }
        x *= createRnage;
        x *= magnification;
        y *= createRnage;
        y *= magnification;
        GameObject a = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere));
        a.transform.position = new Vector3(x, y, -10);
    }

    public void Create2(GameObject g)
    {
        float x = 0;
        float y = 0;

        x = Random.Range(-1f, 1f);
        y = Random.Range(-1f, 1f);
        x *= createRnage;
        y *= createRnage;
        Vector3 vec = new Vector3(x, y, 0);
        float distance = Vector3.Distance(Vector3.zero, vec);
        float min = distance;
        //if (GameManager.Instance.enemys.Length != 0)
        //{
        //    for (int i = 0; i < GameManager.Instance.enemys.Length; i++)
        //    {
        //        distance = Vector3.Distance(GameManager.Instance.enemys[i].transform.position, vec);
        //        if (min >= distance)
        //        {
        //            min = distance;
        //        }
        //    }
        //}


        //if (distance >= z)
        //{

        //    break;
        //}
        //else
        //{
        //    z--;
        //}

        GameObject a = Instantiate(g);
        a.transform.position = new Vector3(x, y, -10);
        GameManager.Instance.AddEnemy(a);

    }
    [System.Serializable]
    public class Enemy
    {
        public string name;
        public GameObject fort;
        public int count;
    }
}
