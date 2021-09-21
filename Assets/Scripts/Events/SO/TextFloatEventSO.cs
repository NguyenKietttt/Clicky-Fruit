using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New TextFloat Event", menuName = "SciptableObject/Event/TextFloat Event")] 
public class TextFloatEventSO : ScriptableObject
{
    public UnityAction<ScoreFloatData> OnEventRaised;
    
	public void RaiseEvent(ScoreFloatData scoreData)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(scoreData);
	}
}
