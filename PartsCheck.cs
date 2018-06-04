using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsCheck : MonoBehaviour
{

    public GameObject save;

    public void Check(GameObject target)
    {
        if (save != null)
        {
            Destroy(save.gameObject);
        }

        save = target;
    }
}
