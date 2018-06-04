using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GokUtil.UpdateManager;
public class DamageSlider : MonoBehaviour, IUpdatable
{
    private Image slider;
    private GameObject target;

    void Start()
    {
        slider = transform.Find("Fill").GetComponent<Image>();
        gameObject.SetActive(false);
        transform.localScale = new Vector3(0.4f, 0.05f, 1);
    }
    void OnEnable()
    {

        UpdateManager.AddUpdatable(this);
    }
    void OnDisable()
    {
        UpdateManager.RemoveUpdatable(this);
    }
    public void UpdateSlider(GameObject target, float amount)
    {
        gameObject.SetActive(true);
        slider.fillAmount = amount;
        this.target = target;
        StartCoroutine(Rate(1));
    }
    IEnumerator Rate(float a)
    {
        yield return new WaitForSeconds(a);
        gameObject.SetActive(false);
    }

    public void UpdateMe()
    {
        if (target != null)
            transform.position = target.transform.position;
    }
}
