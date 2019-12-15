using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource audioSource;
    private AudioClip[] clips;
    private bool clipIsPlayed;
    private void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
        Resources.LoadAll<AudioClip>("Clips/");
    }

    public void PlaySound(int index)
    {
        if (clipIsPlayed)
            return;
        audioSource.clip = clips[index];
        audioSource.Play();
        clipIsPlayed = true;
        StartCoroutine(WaitTillSoundIsPlayed(clips[index].length));

    }

    private IEnumerator WaitTillSoundIsPlayed(float time)
    {
        yield return new WaitForSeconds(time);
        clipIsPlayed = false;
    }
}
