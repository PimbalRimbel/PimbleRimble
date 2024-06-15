using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public List<GameObject> hearts;

    public void LoseLife(int vida){
        hearts[vida].SetActive(false);
    }
}
