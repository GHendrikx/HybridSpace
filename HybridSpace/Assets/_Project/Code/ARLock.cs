using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ARLock : MonoBehaviour
{
    [SerializeField]
    private ImageTargetBehaviour objectToUnlock = null;
    [SerializeField]
    private ImageTargetBehaviour key = null;
    [SerializeField]
    private TrackableBehaviour.Status unlockStatus = TrackableBehaviour.Status.TRACKED;

    private void Awake()
    {
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(Lock);
    }

    private void OnEnable()
    {
        Lock();
    }

    private void Update()
    {
        if (key.CurrentStatus == unlockStatus)
            Unlock();
    }

    private void OnDisable()
    {
        Unlock();
    }

    public void Lock()
    {
        objectToUnlock.enabled = false;
        Debug.Log($"{objectToUnlock.name} Locked");
    }

    public void Unlock()
    {
        objectToUnlock.enabled = true;
        Debug.Log($"{objectToUnlock.name} Unlocked");
    }
}
