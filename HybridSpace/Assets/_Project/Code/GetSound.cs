using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSound : MonoBehaviour
{
    public void PlaySound(int index)
    {
        SoundManager.Instance.PlaySound(index);
    }
}
