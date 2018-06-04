using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteButton : MonoBehaviour {
    public SaveCShip scs;
   public void OnClick()
    {
        scs.Complete();
    }
}
