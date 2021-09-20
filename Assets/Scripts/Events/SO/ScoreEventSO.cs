using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Score Event", menuName = "SciptableObject/Event/Score Game Event")] 
public class ScoreEventSO : ScriptableObject
{
    public UnityAction<ScoreFloatData> OnEventRaised;
    
	public void RaiseEvent(ScoreFloatData scoreData)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(scoreData);
	}
}
