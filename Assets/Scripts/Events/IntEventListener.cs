using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityIntEvent : UnityEvent<int> { }

public class IntEventListener : MonoBehaviour
{
    [SerializeField] private IntEventSO listen;
    [SerializeField] private UnityIntEvent onEventRaised;

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

    public void Respond(int value) => onEventRaised.Invoke(value);
}
