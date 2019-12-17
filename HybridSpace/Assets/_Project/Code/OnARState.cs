using UnityEngine;
using UnityEngine.Events;
using Vuforia;

public class OnARState : MonoBehaviour
{
    [SerializeField]
    private TrackableBehaviour.Status state = TrackableBehaviour.Status.TRACKED;
    [SerializeField]
    private bool triggerOnce = false;
    [SerializeField]
    private UnityEvent onEnterState = new UnityEvent();
    [SerializeField]
    private UnityEvent onLeaveState = new UnityEvent();

    private TrackableBehaviour target;

    private bool entered = false;

    private void Start()
    {
        target = GetComponent<TrackableBehaviour>();
    }

    private void Update()
    {
        if(!entered)
        {
            if (target.CurrentStatus == state)
            {
                onEnterState.Invoke();
                entered = true;
            }
        }
        else
        {
            if (target.CurrentStatus != state)
            {
                onLeaveState.Invoke();
                entered = false;
            }

            if (triggerOnce)
                Destroy(this);
        }
    }
}
