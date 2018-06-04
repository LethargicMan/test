using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateShip : SingletonMonoBehaviour<CreateShip>
{
    public GameObject referencePrefub;//プレハブの情報から作るからセット
    public ShipBase[] shipBase;
    public ModuleData[] moduleData;
    public GameObject shipReference;//親クラスの参照用

    public void Creat()
    {
        ShipDataToModuleData(referencePrefub.GetComponent<ShipData>().shipData, referencePrefub.GetComponent<ShipData>().shipID);

    }
    /// <summary>
    /// 引数にResourcesのプレハブを設定すればそれを作るよ
    /// </summary>
    /// <param name="reference"></param>
    public void Creat(GameObject reference)
    {
        ShipDataToModuleData(reference.GetComponent<ShipData>().shipData, reference.GetComponent<ShipData>().shipID);
    }
    public void Creat(int i)
    {
        GameObject a = (GameObject)Resources.Load("ShipSaveData/ShipData" + i);
        referencePrefub = a;
        ShipDataToModuleData(a.GetComponent<ShipData>().shipData, a.GetComponent<ShipData>().shipID);
    }
    public GameObject CreateTemporary(int i)
    {
        GameObject a = (GameObject)Resources.Load("ShipSaveData/ShipData" + i);
        ShipDataToModuleDataTemp(a.GetComponent<ShipData>().shipData, a.GetComponent<ShipData>().shipID);
        return shipReference;
    }

    private void ShipDataToModuleData(Data[] shipData, int shipID)
    {
        for (int i = 0; i < shipBase.Length; i++)
        {
            if (shipID == shipBase[i].id)
            {
                GameObject g = Instantiate(shipBase[i].instance);//船体を作る
                shipReference = g;
                g.GetComponent<Health>().health = referencePrefub.GetComponent<ShipData>().health;
                g.tag = "Player";
                g.name = "Player";
            }
        }

        for (int i = shipData.Length - 1; i >= 0; i--)
        {
            for (int j = moduleData.Length - 1; j >= 0; j--)
            {
                if (shipData[i].moduleID == moduleData[j].id)
                {

                    GameObject g = Instantiate(moduleData[j].instance);
                    g.transform.SetParent(shipReference.transform);
                    g.transform.position = shipData[i].position;
                    g.transform.rotation = shipData[i].rotation;
                    g.tag = "Player";
                }
            }

        }



        //for (int i = 0; i < shipData.Length; i++)
        //{
        //    for (int j = 0; j < moduleData.Length; j++)
        //    {
        //        if (shipData[i].moduleID == moduleData[j].id)
        //        {
        //            GameObject g = Instantiate(moduleData[j].instance);
        //            g.transform.SetParent(shipReference.transform);
        //            g.transform.position = shipData[i].position;
        //            g.transform.rotation = shipData[i].rotation;
        //            g.tag = "Player";
        //        }
        //    }

        //}
    }
    private void ShipDataToModuleDataTemp(Data[] shipData, int shipID)
    {
        for (int i = 0; i < shipBase.Length; i++)
        {
            if (shipID == shipBase[i].id)
            {
                GameObject g = Instantiate(shipBase[i].instance);//船体を作る
                SceneManager.Instance.GrageTempShip = g;
                shipReference = g;
                g.GetComponent<Health>().enabled = false;
                g.GetComponent<PlayerController>().enabled = false;
                g.tag = "Player";
                g.name = "Player";
            }
        }

        for (int i = shipData.Length - 1; i >= 0; i--)
        {
            for (int j = moduleData.Length - 1; j >= 0; j--)
            {
                if (shipData[i].moduleID == moduleData[j].id)
                {

                    GameObject g = Instantiate(moduleData[j].instance);
                    g.transform.SetParent(shipReference.transform);
                    g.transform.position = shipData[i].position;
                    g.transform.rotation = shipData[i].rotation;
                    g.tag = "Player";
                }
            }

        }
    }


}


[System.Serializable]
public class ModuleData
{
    public string name;
    public int id;//これをもとに生成されるよ(部品)
    public GameObject instance;
}
[System.Serializable]
public class ShipBase
{
    public string name;
    public int id;//これをもとに生成されるよ(船)
    public GameObject instance;
}
