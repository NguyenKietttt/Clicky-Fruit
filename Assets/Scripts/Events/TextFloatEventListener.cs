using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityTextFloatEvent : UnityEvent<ScoreFloatData> { }

public class TextFloatEventListener : MonoBehaviour
{
    [SerializeField] private TextFloatEventSO listen;
    [SerializeField] private UnityTextFloatEvent onEventRaised;

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

    public void Respond(ScoreFloatData scoreData) => onEventRaised.Invoke(scoreData);
}
