using UnityEngine;

public abstract class StateBase : MonoBehaviour
{   
    public virtual void OnTitleMenu() { }

    public virtual void OnGameplay() { }

    public virtual void OnGamePause() { }

    public virtual void OnGameOver() { }
}
