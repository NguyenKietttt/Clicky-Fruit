using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnitySFXEvent : UnityEvent<AudioClip> { }

public class SFXEventListener : MonoBehaviour
{
    [SerializeField] private SFXEventSO listen;
    [SerializeField] private UnitySFXEvent onEventRaised;

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

    public void Respond(AudioClip clip) => onEventRaised.Invoke(clip);
}
