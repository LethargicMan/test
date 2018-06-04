using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : SingletonMonoBehaviour<ResultManager>
{

    public Text scrapText;
    public Text goldText;
    public void UpdateResultText(int scrap, int gold)
    {
        scrapText.text = scrap.ToString();
        goldText.text = gold.ToString();
    }
}
