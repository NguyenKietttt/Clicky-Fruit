using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityVFXEvent : UnityEvent<VFXData> { }

public class VFXEventListener : MonoBehaviour
{
    [SerializeField] private VFXEventSO listen;
    [SerializeField] private UnityVFXEvent onEventRaised;

    private void OnEnable()
    {
        if (listen != null)
			listen.OnEventRaised += Respond;
    }

    private void OnDisable()
    {
        if (listen != null)
			listen.OnEventRaised -= Respond;
    }

    public void Respond(VFXData value) => onEventRaised.Invoke(value);
}
