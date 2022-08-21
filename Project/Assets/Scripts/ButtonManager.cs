using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void ChangeMaterialSelection(int matNum)
    {
        //switch (matNum)
        //{
        //    case 1: currentMaterial = BuildingMaterial.wood; break;
        //    case 2: currentMaterial = BuildingMaterial.stone; break;
        //    case 3: currentMaterial = BuildingMaterial.concrete; break;
        //}

        //InConnectMode = false;
    }

    public void ChangeMaterialLength(int length)
    {
        //size = length;
    }

    public void ChangeScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
