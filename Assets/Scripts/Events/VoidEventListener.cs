using UnityEngine;
using UnityEngine.Events;

public class VoidEventListener : MonoBehaviour
{
    [SerializeField] private VoidEventSO listen;
    [SerializeField] private UnityEvent onEventRaised;

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

    public void Respond() => onEventRaised.Invoke();
}
