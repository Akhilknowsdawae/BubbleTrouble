using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpace : MonoBehaviour
{
    bool bHasTower = false;

    public void SetHasTower(bool inHasTower)
    {
        bHasTower = inHasTower;
    }

    public bool GetHasTower()
    {
        return bHasTower;
    }
}
