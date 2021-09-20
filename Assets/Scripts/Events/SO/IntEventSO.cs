using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Int Event", menuName = "SciptableObject/Event/Int Game Event")] 
public class IntEventSO : ScriptableObject
{
	public UnityAction<int> OnEventRaised;
    
	public void RaiseEvent(int value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}
