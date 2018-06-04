using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherButton : MonoBehaviour
{
    public void OnClickButton()
    {
        switch (gameObject.name)
        {
            case "Save1":
                GameObject.Find("SelectScrollView").GetComponent<SaveCShip>().save.GetComponent<ShipData>().SaveEdit(1);//1番目を上書きセーブ
                transform.parent.parent.gameObject.SetActive(false);
                break;
            case "Save2":
                GameObject.Find("SelectScrollView").GetComponent<SaveCShip>().save.GetComponent<ShipData>().SaveEdit(2);//2番目を上書きセーブ
                transform.parent.parent.gameObject.SetActive(false);
                break;
            case "Save3":
                GameObject.Find("SelectScrollView").GetComponent<SaveCShip>().save.GetComponent<ShipData>().SaveEdit(3);//3番目を上書きセーブ
                transform.parent.parent.gameObject.SetActive(false);
                break;
            case "Save4":
                GameObject.Find("SelectScrollView").GetComponent<SaveCShip>().save.GetComponent<ShipData>().SaveEdit(4);//4番目を上書きセーブ
                transform.parent.parent.gameObject.SetActive(false);
                break;
            case "Save5":
                GameObject.Find("SelectScrollView").GetComponent<SaveCShip>().save.GetComponent<ShipData>().SaveEdit(5);//5番目を上書きセーブ
                transform.parent.parent.gameObject.SetActive(false);
                break;
            case "Save6":
                GameObject.Find("SelectScrollView").GetComponent<SaveCShip>().save.GetComponent<ShipData>().SaveEdit(6);//6番目を上書きセーブ
                transform.parent.parent.gameObject.SetActive(false);
                break;
            case "Exit":
                Application.Quit();//アプリ終了
                break;
            case "Level1"://レベル１の星域へ
                SceneManager.Instance.GameStart(1);
                break;
            case "Level2"://２へ
                SceneManager.Instance.GameStart(2);
                break;
            case "Level3"://3へ
                SceneManager.Instance.GameStart(3);
                break;
            case "GoToDock"://編集画面へ

                SceneManager.Instance.Progress(SceneManager.Situation.EDIT);
                break;
            case "GoToGrage":
                Destroy(GameObject.Find("SelectScrollView").GetComponent<SaveCShip>().save);
                SceneManager.Instance.Progress(SceneManager.Situation.GRAGE);
                break;
            case"ResaltToGrage":
                SceneManager.Instance.Progress(SceneManager.Situation.GAMEOVER);
                break;
            default:
                break;
        }
    }
}
