using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEditor;

public class ShipData : MonoBehaviour
{
    public int shipID;//この船のID(実機作るときに必要だよ)
    public Data[] shipData;
    public GameObject[] Modules;//追加する装備
    public int maxWeight;//限界の重さ
    public int currentWeight;//現在の重さ
    public int health;//ここに各モジュールのHPが加算され、最終的に自身のHPとなる
    public int horcePow;//船の出力

    public Text healthText;
    public Text weightText;
    public Text horcePowText;
    public Text horceRatioText;
    // Use this for initialization

    void Start()
    {
        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        weightText = GameObject.Find("WeightText").GetComponent<Text>();
        horcePowText = GameObject.Find("EngineText").GetComponent<Text>();
        horceRatioText = GameObject.Find("EngineRatioText").GetComponent<Text>();
        healthText.text = health.ToString();
        weightText.text = currentWeight.ToString() + " / " + maxWeight.ToString();
    }
    public void AddModules(GameObject module)
    {
        GameObject[] a = Modules;
        Modules = new GameObject[Modules.Length + 1];
        for (int i = 0; i < Modules.Length - 1; i++)
        {
            Modules[i] = a[i];
        }
        Modules[Modules.Length - 1] = module;//最後に追加
    }

    /// <summary>
    /// これをセーブボタンが呼ぶことでセーブされてるよ
    /// </summary>
    /// <param name="saveCount">何番目のセーブデータに上書きするのか</param>
    public void SaveEdit(int saveCount)
    {
        shipData = new Data[Modules.Length];
        for (int i = 0; i < Modules.Length; i++)
        {
            shipData[i] = new Data();
            shipData[i].moduleID = Modules[i].GetComponent<PartsStatus>().moduleID;
            shipData[i].position = Modules[i].transform.position;
            shipData[i].rotation = Modules[i].transform.rotation;
        }
        GameObject savePrefub = (GameObject)Resources.Load("ShipSaveData/ShipData"+saveCount.ToString());
        savePrefub.GetComponent<ShipData>().shipData = shipData;////プレハブ内容を変えて
        savePrefub.GetComponent<ShipData>().shipID = shipID;
        savePrefub.GetComponent<ShipData>().health = health;
       // AssetDatabase.SaveAssets();//アセット全てをセーブ
    }
    public bool CheckWeight(int weight)//PartsStatusにて
    {
        int a = currentWeight + weight;
        if (a > maxWeight) return false;
        return true;

    }

    public void UpDateShipData(int h, int w, int p)//CheckWeightをクリアしていれば PartsStatusにて
    {
        health += h;
        currentWeight += w;
        horcePow += p;
        healthText.text = health.ToString();
        weightText.text = currentWeight.ToString() + " / " + maxWeight.ToString();
        horcePowText.text = horcePow.ToString();
        if (horcePow >= 1)
        {
            float f = (float)horcePow / (float)currentWeight;
            horceRatioText.text = f.ToString();
        }
    }
    public IEnumerator OverWeight(int w)//重量オーバー時のPartsStatusにて
    {
        weightText.color = Color.red;
        weightText.text = (currentWeight + w).ToString() + " / " + maxWeight.ToString();
        yield return new WaitForSeconds(1);
        weightText.color = Color.black;
        weightText.text = currentWeight.ToString() + " / " + maxWeight.ToString();
    }


}
[System.Serializable]
public class Data
{
    public int moduleID;//部品ID
    public Vector3 position;//部品位置
    public Quaternion rotation;//部品回転
    public Data()
    {
        moduleID = 0;
        position = Vector3.zero;
        rotation = Quaternion.identity;
    }
}