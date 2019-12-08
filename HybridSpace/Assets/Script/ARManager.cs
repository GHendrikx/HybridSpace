using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARManager : MonoBehaviour
{
    [SerializeField]
    private Clues[] arClues;

    private void Update()
    {
        for (int i = 0; i < arClues.Length; i++)
            if (arClues[i].arImage.activeInHierarchy && !arClues[i].isEnabled)
                arClues[i].isEnabled = !arClues[i].isEnabled;
    }
}
[System.Serializable]
public struct Clues
{
    public bool isEnabled;
    public GameObject arImage;
}
