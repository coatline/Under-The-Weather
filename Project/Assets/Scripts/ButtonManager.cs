using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public void ChangeMaterialSelection(int matNum)
    {
        switch (matNum)
        {
            case 1: C.currentMaterial = C.Material.wood; break;
            case 2: C.currentMaterial = C.Material.stone; break;
            case 3: C.currentMaterial = C.Material.concrete; break;
        }

        C.InConnectMode = false;
    }

    public void ChangeMaterialLength(int length)
    {
        C.size = length;
    }

    public void ChangeScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
}
