using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Void Event", menuName = "SciptableObject/Event/Void Game Event")] 
public class VoidEventSO : ScriptableObject
{
	public UnityAction OnEventRaised;
    
	public void RaiseEvent()
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke();
	}
}
