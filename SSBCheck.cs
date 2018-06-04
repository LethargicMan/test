using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSBCheck : MonoBehaviour
{
    public bool beShip = false;
    public GameObject save;

    public void Check(GameObject target)
    {
        if (beShip == false)
        {
            beShip = true;
            save = target;
            transform.parent.GetComponent<SaveCShip>().save = save;
        }
        else
        {
            Destroy(save.gameObject);
            save = target;
            transform.parent.GetComponent<SaveCShip>().save = save;
        }
    }
}
