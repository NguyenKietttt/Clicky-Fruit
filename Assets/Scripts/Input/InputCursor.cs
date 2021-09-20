using UnityEngine;

public class InputCursor : StateBase
{
    [Header("Configs")]
    [SerializeField] private InputSO intputSO;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(intputSO == null, "intputSO is missing!!!");

        isFailedConfig = intputSO == null;
    }

    /// <summary>
    /// Raise by TitleMenuState Event from StateManager
    /// </summary>
    public override void OnTitleMenu()
    {
        if (isFailedConfig)
            return;

        ChangeCursor(0);
    }

    /// <summary>
    /// Raise by GameplayState Event from StateManager
    /// </summary>
    public override void OnGameplay()
    {
        if (isFailedConfig)
            return;

        ChangeCursor(1);
    }

    /// <summary>
    /// Raise by GameOverState Event from StateManager
    /// </summary>
    public override void OnGameOver()
    {
        if (isFailedConfig)
            return;

        ChangeCursor(0);
    }

    /// <summary>
    /// <para>0 - Arrow</para>
    /// <para>1 - Click</para>
    /// </summary>
    /// <param name="cursorIndex"></param>
    public void ChangeCursor(int cursorIndex)
    {
        Texture2D cursorTexture = intputSO.Cursors.Find(p => p.Index == cursorIndex).Texture;
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
    }
}