using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;



public class SceneManager : SingletonMonoBehaviour<SceneManager>//シングルトン
{
    public GameObject gameManager;
    public GameObject MainCanvas;

    public GameObject GrageTempShip;//参照用
    public int RealShipSaveNumber = 1;//各ボタンから数字を変えてどのデータをロードするか

    public Situation currentSituation;//現在のシーン状況
    // Use this for initialization
    void Start()
    {
        currentSituation = Situation.START;

    }

    public void Progress(Situation situation)
    {
        switch (situation)
        {
            case Situation.START:
                currentSituation = Situation.START;
                break;
            case Situation.GAMEPLAY:
                currentSituation = Situation.GAMEPLAY;
                GameManager.Instance.Initialize();
                StartCoroutine(WaitFade("ONBUTTLEUI"));
                //StartCoroutine(WaitCreate(1, RealShipSaveNumber));
                if (GrageTempShip != null) Destroy(GrageTempShip);
                break;
            case Situation.GAMEOVER:
                currentSituation = Situation.GAMEOVER;
                StartCoroutine(WaitFade("ONGRAGEUI"));
                break;
            case Situation.RESULT:
                currentSituation = Situation.RESULT;
                StartCoroutine(WaitFade("ONRESULTUI"));
                break;
            case Situation.GRAGE:
                currentSituation = Situation.GRAGE;
                StartCoroutine(WaitFade("ONGRAGEUI"));
                break;
            case Situation.EDIT:
                currentSituation = Situation.EDIT;
                StartCoroutine(WaitFade("ONEDITUI"));
                Camera.main.transform.position = new Vector3(0, 0, -10);
                Camera.main.orthographicSize = 5;
                if (GrageTempShip != null) Destroy(GrageTempShip);
                break;
            default:
                break;
        }
    }
    public void GameStart(int difficury)
    {
        currentSituation = Situation.GAMEPLAY;
        GameManager.Instance.Initialize();
        StartCoroutine(WaitFade("ONBUTTLEUI"));
        StartCoroutine(WaitCreate(1, RealShipSaveNumber, difficury));
        if (GrageTempShip != null) Destroy(GrageTempShip);
    }

    public enum Situation
    {
        START, GAMEPLAY, GAMEOVER, RESULT,
        GRAGE, EDIT,
    }

    //先にフェードで見えなくしてからするため
    private IEnumerator WaitFade(string situation)
    {
        CanvasManager.Instance.HideCanvas(true);
        Fade.Instance.FadeIn(1);
        yield return new WaitForSeconds(1);
        switch (situation)
        {
            case "ONBUTTLEUI":
                CanvasManager.Instance.SwitchUI(CanvasManager.CanvasSituation.ONBATTLEUI);//戦闘UIへ
                break;
            case "ONGRAGEUI":
                CanvasManager.Instance.SwitchUI(CanvasManager.CanvasSituation.ONGRAGEUI);
                break;
            case "ONEDITUI":
                CanvasManager.Instance.SwitchUI(CanvasManager.CanvasSituation.ONEDITUI);
                break;
            case "ONRESULTUI":
                CanvasManager.Instance.SwitchUI(CanvasManager.CanvasSituation.ONRESALTUI);
                break;

        }
        yield return new WaitForSeconds(0.1f);
        Fade.Instance.FadeOut(1);
        yield return new WaitForSeconds(1);
        CanvasManager.Instance.HideCanvas(false);
    }

    private IEnumerator WaitCreate(float a, int ship, int difficury)
    {
        yield return new WaitForSeconds(a);
        GameManager.Instance.GameStart(ship, difficury);
    }
}

