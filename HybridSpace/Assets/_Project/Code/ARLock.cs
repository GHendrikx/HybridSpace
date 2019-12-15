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

    private bool unlocked = false;

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
        if (!unlocked && key.CurrentStatus == unlockStatus)
            Unlock();
    }

    private void OnDisable()
    {
        Unlock();
    }

    public void Lock()
    {
        objectToUnlock.enabled = unlocked = false;
        Debug.Log($"{objectToUnlock.name} Locked");
    }

    public void Unlock()
    {
        objectToUnlock.enabled = unlocked = true;
        Debug.Log($"{objectToUnlock.name} Unlocked");
    }
}
