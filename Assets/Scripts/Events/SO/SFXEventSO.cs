using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New SFX Event", menuName = "SciptableObject/Event/SFX Game Event")] 
public class SFXEventSO : ScriptableObject
{
    public UnityAction<AudioClip> OnEventRaised;
    
	public void RaiseEvent(AudioClip clip)
	{
		if (OnEventRaised != null)
			OnEventRaised.Invoke(clip);
	}
}
