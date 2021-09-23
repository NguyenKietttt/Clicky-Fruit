using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New VFX Event", menuName = "SciptableObject/Event/VFX Game Event")] 
public class VFXEventSO : ScriptableObject
{
    public UnityAction<VFXData> OnEventRaised;
    
	public void RaiseEvent(VFXData value)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(value);
	}
}
