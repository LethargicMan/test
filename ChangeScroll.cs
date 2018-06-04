using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeScroll : MonoBehaviour
{
    //UpDown以外のみ
    public GameObject[] scrolls;//0:Ship 1:weapon 2:armor 3:engine
    public GameObject scrollRoot;
    public SaveCShip destroyParts;//違う画面に行こうとしたら今選択中のsaveを削除するため
    //Updownのみ
    public GameObject root;
    public bool beBase = false;//船体が出ていれば他の項目に移動しても良い
    private bool enable;

    public void SwichScroll()
    {

        switch (gameObject.name)
        {
            case "ShipBase":
                Change(0);
                break;
            case "Weapon":
                Change(1);
                break;
            case "Armor":
                Change(2);
                break;
            case "EngineA":
                Change(3);
                break;
            case "UpDown":
                enable = !enable;
                float s = Screen.height / 14;
                if (enable)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, -180));
                    root.GetComponent<RectTransform>().position += new Vector3(0, s, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(Vector3.zero);
                    root.GetComponent<RectTransform>().position -= new Vector3(0, s, 0);
                }
                break;
            default:
                break;

        }

    }
    void Change(int a)
    {
        for (int i = 0; i < 4; i++)
        {
            scrolls[i].SetActive(false);
        }
        if (destroyParts.currentParts != null)
        {
            Destroy(destroyParts.currentParts);
        }

        scrolls[a].SetActive(true);
        scrollRoot.GetComponent<ScrollRect>().content = scrolls[a].GetComponent<RectTransform>();
    }
}
