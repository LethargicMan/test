using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GokUtil.UpdateManager;

public class FortEscortCreate : MonoBehaviour, IUpdatable
{
    public Escorts[] escorts;
    public GameObject[] enemyReference;
    public bool isEscortBoss = false;
    private int count;
    private int w;
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
        w++;
        if (w > 600)
        {
            w = 0;
            CheckEscort();
        }
    }
    void Start()
    {
        if (isEscortBoss)
        {
            escorts[0].count = Random.Range(0, 3);
        }

        Create();
    }
    public void Create()
    {
        count = 0;
        int a = 0;
        for (int i = 0; i < escorts.Length; i++)
        {
            a += escorts[i].count;
        }
        enemyReference = new GameObject[a];
        for (int i = 0; i < escorts.Length; i++)
        {
            for (int j = 0; j < escorts[i].count; j++)
            {

                GameObject g = Instantiate(escorts[i].escort);
                g.transform.position = transform.position;
                enemyReference[count] = g;
                GameManager.Instance.AddEnemyUnFort(g);//GameManagerに雑魚を登録
                count++;
            }
        }

    }

    public void CheckEscort()
    {
        int a = 0;
        for (int i = 0; i < enemyReference.Length; i++)
        {
            if (enemyReference[i] != null)
            {
                a++;
            }
        }

        for (int i = 0; i < count - a; i++)
        {
            for (int j = 0; j < enemyReference.Length; j++)
            {
                if (enemyReference[j] == null)
                {
                    GameObject g = Instantiate(escorts[0].escort);
                    g.transform.position = transform.position;
                    enemyReference[j] = g;
                    GameManager.Instance.AddEnemyUnFort(g);//GameManagerに雑魚を登録
                }
            }

        }
    }

    [System.Serializable]
    public class Escorts
    {
        public string name;
        public GameObject escort;
        public int count;
    }
}
