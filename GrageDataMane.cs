using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrageDataMane : MonoBehaviour {

	public GameObject temporaryShip;//参照用
    public void ClearTemporary()
    {
        Destroy(temporaryShip);
    }
}
