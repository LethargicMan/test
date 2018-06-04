using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrageSaveData : MonoBehaviour
{


    public GameObject createData;
    public GameObject healthText;
    void OnEnable()
    {
        createData = GameObject.Find("CreatData");
        switch (gameObject.name)
        {
            case "SaveData1":
                UpdataText(1);
                break;
            case "SaveData2":
                UpdataText(2);
                break;
            case "SaveData3":
                UpdataText(3);
                break;
            case "SaveData4":
                UpdataText(4);
                break;
            case "SaveData5":
                UpdataText(5);
                break;
            case "SaveData6":
                UpdataText(6);
                break;
            default:
                break;
        }

    }
    public void OnClickLoad()
    {
        switch (gameObject.name)
        {
            case "SaveData1":
                temporary(1);
                SceneManager.Instance.RealShipSaveNumber = 1;
                break;
            case "SaveData2":
                temporary(2);
                SceneManager.Instance.RealShipSaveNumber = 2;
                break;
            case "SaveData3":
                temporary(3);
                SceneManager.Instance.RealShipSaveNumber = 3;
                break;
            case "SaveData4":
                temporary(4);
                SceneManager.Instance.RealShipSaveNumber = 4;
                break;
            case "SaveData5":
                temporary(5);
                SceneManager.Instance.RealShipSaveNumber = 5;
                break;
            case "SaveData6":
                temporary(6);
                SceneManager.Instance.RealShipSaveNumber = 6;
                break;
            default:
                break;
        }
    }
    private void temporary(int c)
    {
        if (transform.parent.GetComponent<GrageDataMane>().temporaryShip == null)
        {
            transform.parent.GetComponent<GrageDataMane>().temporaryShip = createData.GetComponent<CreateShip>().CreateTemporary(c);
        }
        else
        {
            transform.parent.GetComponent<GrageDataMane>().ClearTemporary();//元のをなくし
            transform.parent.GetComponent<GrageDataMane>().temporaryShip = createData.GetComponent<CreateShip>().CreateTemporary(c);
        }
    }
    private void UpdataText(int i)
    {

        GameObject g = (GameObject)Resources.Load("ShipSaveData/ShipData" + i);
        healthText.GetComponent<Text>().text = g.GetComponent<ShipData>().health.ToString();
    }
}
