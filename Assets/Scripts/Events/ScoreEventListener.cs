using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityScoreEvent : UnityEvent<ScoreFloatData> { }

public class ScoreEventListener : MonoBehaviour
{
    [SerializeField] private ScoreEventSO listen;
    [SerializeField] private UnityScoreEvent onEventRaised;

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