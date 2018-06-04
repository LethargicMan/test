using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageCanvas : MonoBehaviour
{
    public GameObject damageText;
    public int size5 = 5;//生成する数(二乗)

    public GameObject damageSlider;
    public int size;

    public GameObject cureText;
    public int cureSize;

    private GameObject[] contents;
    private int count;
    private GameObject[] content2;
    private int count2;
    private GameObject[] cureContents;
    private int cureCount;

    // Use this for initialization
    void Start()
    {
        contents = new GameObject[size5 * size5];
        content2 = new GameObject[size * size];
        cureContents = new GameObject[cureSize * cureSize];
        for (int i = 0; i < size5; i++)
        {
            StartCoroutine(CreateDamageText());
        }
        for (int i = 0; i < size; i++)
        {
            StartCoroutine(CreateDamageSlider());
        }
        for (int i = 0; i < cureSize; i++)
        {
            StartCoroutine(CreateCureText());
        }
    }
    IEnumerator CreateDamageText()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < size5; i++)
        {
            contents[count] = Instantiate(damageText);
            contents[count].transform.SetParent(transform);
            count++;
        }
        if (count >= size5 * size5) count = 0;
    }
    IEnumerator CreateDamageSlider()
    {        
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < size; i++)
        {
            content2[count2] = Instantiate(damageSlider);
            content2[count2].transform.SetParent(transform);
            count2++;
        }
        if (count2 >= size * size) count2 = 0;
    }
    IEnumerator CreateCureText()
    {

        yield return new WaitForEndOfFrame();
        for (int i = 0; i < cureSize; i++)
        {
            cureContents[cureCount] = Instantiate(cureText);
            cureContents[cureCount].transform.SetParent(transform);
            cureCount++;
        }
        if (cureCount >= cureSize * cureSize) cureCount = 0;
    }

    public void CallDamage(Vector3 position, int content)
    {
        contents[count].GetComponent<DamageText>().UpdateText(content.ToString(),10 + (content / 100), position); ;
        count++;
        if (count >= size5 * size5) count = 0;
    }
    public void CallSlider(GameObject target, int maxHp, int now)
    {
        content2[count2].GetComponent<DamageSlider>().UpdateSlider(target, (float)now / (float)maxHp);
        count2++;
        if (count2 >= size * size) count2 = 0;
    }
    public void CallCureText(Vector3 position, int content)
    {
        cureContents[cureCount].GetComponent<DamageText>().UpdateText(content.ToString(), position);
        cureCount++;
        if (cureCount >= cureSize * cureSize) cureCount = 0;
    }

}
