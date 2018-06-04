using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;
public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public int toralScrap;//Healthから加算するよ
    public int totalGold;//同じく
    public GameObject[] enemysForts;//全ての敵はここから作られ、これに登録される。遷移時に破壊するための参照
    public GameObject[] enemysPlane;//要塞以外の敵(Fortに登録するのはクリア条件になるよ)
    public GameObject backGround;//色変更用にバックグラウンド

    //初期化
    public void Initialize()
    {
        toralScrap = 0;
        totalGold = 0;
        enemysPlane = new GameObject[0];
    }
    //ゲーム画面スタート時に呼ぶよ(SceneManagerから)
    public void GameStart(int shipDataNumber, int difficury)
    {
        CreateShip.Instance.Creat(shipDataNumber);
        if (difficury == 1)
        {
            int[] a = new int[3] { 5, 0, 0 };
            GetComponent<CreateEnemy>().UpdateFortStatus(100, a);
            backGround.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);

        }
        if (difficury == 2)
        {
            int[] a = new int[3] { 5, 5, 0 };
            GetComponent<CreateEnemy>().UpdateFortStatus(150, a);
            backGround.GetComponent<Renderer>().material.SetColor("_Color", Color.green);

        }
        if (difficury == 3)
        {
            int[] a = new int[3] { 5, 5, 5 };
            GetComponent<CreateEnemy>().UpdateFortStatus(200, a);
            backGround.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }

        GetComponent<CreateEnemy>().CreateForts();//敵の要塞を作成
    }
    public void AddReward(int scrap, int gold)
    {
        toralScrap += scrap;
        totalGold += gold;
        StartCoroutine(wait2());
    }
    public void Progress(SceneManager.Situation situation)
    {
        SceneManager.Instance.Progress(situation);//シーンを変えるよ
    }
    /// <summary>
    /// プレイヤーが死んだらHealthからよぶ
    /// </summary>
    public void PlayerDeath()
    {
        StartCoroutine(Wait(2));
        ClearLine();
    }
    IEnumerator Wait(float f)
    {
        yield return new WaitForSeconds(f);
        SceneManager.Instance.Progress(SceneManager.Situation.RESULT);
        yield return new WaitForSeconds(1f);
        ResultManager.Instance.UpdateResultText(toralScrap, totalGold);
        DestroyEnemys();
    }
    IEnumerator wait2()
    {
        yield return new WaitForSeconds(0.5f);
        if (CheckGameClear() == true)//敵の何かが殺されるとこAddRewardが呼ばれる為ここでクリア判定するよ
        {
            StartCoroutine(GameClear(1));
        }
    }
    IEnumerator GameClear(float f)
    {
        yield return new WaitForSeconds(f);
        CanvasManager.Instance.Clear();
        ClearLine();
        yield return new WaitForSeconds(3);
        Destroy(GameObject.Find("Player"));//プレイヤー壊して
        SceneManager.Instance.Progress(SceneManager.Situation.RESULT);
        DestroyEnemys();
        yield return new WaitForSeconds(1f);
        ResultManager.Instance.UpdateResultText(toralScrap, totalGold);

    }
    /// <summary>
    /// 配列更新
    /// </summary>
    /// <param name="g"></param>
    public void AddEnemy(GameObject g)
    {
        GameObject[] a = enemysForts;
        enemysForts = new GameObject[a.Length + 1];
        for (int i = 0; i < a.Length; i++)
        {
            enemysForts[i] = a[i];
        }
        enemysForts[enemysForts.Length - 1] = g;
    }
    public void AddEnemyUnFort(GameObject g)
    {
        GameObject[] a = enemysPlane;
        enemysPlane = new GameObject[a.Length + 1];
        for (int i = 0; i < a.Length; i++)
        {
            enemysPlane[i] = a[i];
        }
        enemysPlane[enemysPlane.Length - 1] = g;
    }
    /// <summary>
    ///Scene上に出ているすべての敵を削除するよ
    /// </summary>
    public void DestroyEnemys()
    {

        for (int i = 0; i < enemysForts.Length; i++)
        {
            if (enemysForts[i] != null)
            {
                Destroy(enemysForts[i]);
            }
        }
        for (int i = 0; i < enemysPlane.Length; i++)
        {
            if (enemysPlane[i] != null)
            {
                Destroy(enemysPlane[i]);
            }
        }
    }
    public void SetLine()
    {
        GetComponent<LineRenderer>().enabled = true;
        Vector3[] a = new Vector3[enemysForts.Length];
        GetComponent<LineRenderer>().SetPositions(a);
        for (int i = 0; i < enemysForts.Length; i++)
        {
            a[i] = enemysForts[i].transform.position;
        }
        GetComponent<LineRenderer>().SetPositions(a);
    }
    public void ClearLine()
    {
        Vector3[] a = new Vector3[enemysForts.Length];
        GetComponent<LineRenderer>().SetPositions(a);
        GetComponent<LineRenderer>().enabled = false;
    }

    public bool CheckGameClear()
    {
        int count = 0;
        for (int i = 0; i < enemysForts.Length; i++)
        {
            if (enemysForts[i].gameObject != null)
            {
                count++;
            }
        }
        if (count >= 1)
        {
            return false;
        }
        else
        {
            return true;
        }

        //foreach (UnityEngine.Object obj in enemysForts)
        //{
        //    if (obj == null)
        //    {
        //        continue;
        //    }
        //    if (obj.name == "Deprecated EditorExtensionImpl")
        //    {
        //        continue;
        //    }

        //    SerializedObject sobj = new SerializedObject(obj);
        //    SerializedProperty property = sobj.GetIterator();

        //    while (property.Next(true))
        //    {
        //        if (property.propertyType == SerializedPropertyType.ObjectReference
        //            && property.objectReferenceValue == null
        //            && property.objectReferenceInstanceIDValue != 0)
        //        {

        //        }
        //        else
        //        {
        //            count++;
        //        }
        //    }
        //}
        //if (count >= 1)
        //{
        //    Debug.Log("true");
        //    return true;
        //}
        //else
        //{
        //    Debug.Log("false");
        //    return false;
        //}

    }

    /// <summary>
    /// 配列からnullを削除してくれるよ（使えないよ）
    /// </summary>
    public void NullDelete()
    {

        if (enemysForts.Length >= 1)
        {
            GameObject[] g = new GameObject[0];
            int count = 0;
            for (int i = 0; i < enemysForts.Length; i++)
            {
                if (enemysForts[i] != null)
                {
                    count++;
                    g = new GameObject[count];
                    for (int j = 0; j < g.Length - 1; j++)
                    {
                        g[j] = enemysForts[j];
                    }
                    g[count - 1] = enemysForts[i];
                }
            }
            enemysForts = g;
        }
    }


    /// <summary>
    /// SurchEscortから呼び出し生存しているFortのどこかの場所が選ばれるよ。全滅してるとvector.zeroだよ
    /// </summary>
    /// <returns></returns>
    public GameObject ReturnFortTransform()
    {
        GameObject s=null;
        int count = 0;
        while (true)
        {
            int a = Random.Range(0, enemysForts.Length);
            if (enemysForts[a] != null)
            {
                s = enemysForts[a];
            }
            count++;
            if (count > 20)
            {
                break;
            }
        }
        return s;
    }
}
