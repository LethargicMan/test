using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CanvasManager : SingletonMonoBehaviour<CanvasManager>
{

    public GameObject ButtleUI;
    public GameObject MiniMap;
    public GameObject EditUI;
    public GameObject GrageUI;
    public GameObject ResaltUI;
    public GameObject ClearUI;
    public GameObject Mask;//フェードしている間にボタンをさわられないように

    public void SwitchUI(CanvasSituation cs)
    {
        switch (cs)//単純に場面によってMainUI変えるよってだけ
        {
            case CanvasSituation.ONBATTLEUI:
                ButtleUI.SetActive(true);
                MiniMap.SetActive(true);
                EditUI.SetActive(false);
                GrageUI.SetActive(false);
                ResaltUI.SetActive(false);
                break;

            case CanvasSituation.ONEDITUI:
                ButtleUI.SetActive(false);
                MiniMap.SetActive(false);
                EditUI.SetActive(true);
                GrageUI.SetActive(false);
                ResaltUI.SetActive(false);
                break;
            case CanvasSituation.ONGRAGEUI:
                ButtleUI.SetActive(false);
                MiniMap.SetActive(false);
                EditUI.SetActive(false);
                GrageUI.SetActive(true);
                ResaltUI.SetActive(false);
                break;
            case CanvasSituation.ONRESALTUI:
                 ButtleUI.SetActive(false);
                MiniMap.SetActive(false);
                EditUI.SetActive(false);
                GrageUI.SetActive(false);
                ResaltUI.SetActive(true);
                break;
            default:
                break;
        }
    }
    public void Clear()
    {
        StartCoroutine(ClearI(1));
    }
    IEnumerator ClearI(float f)
    {
        ClearUI.SetActive(true);
        yield return new WaitForSeconds(f);
        ClearUI.SetActive(false);
    }
    public void HideCanvas(bool active)
    {
        Mask.SetActive(active);
    }

    public enum CanvasSituation
    {
        ONBATTLEUI,
        ONEDITUI,
        ONGRAGEUI,
        ONRESALTUI,
    }
}
