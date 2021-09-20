using DG.Tweening;
using UnityEngine;

public class FloatScoreSpawn : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private ScoreFloatSO scoreFloatSO;

    [Header("Events")]
    [SerializeField] private IntEventSO onTargetClickEvent;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(scoreFloatSO == null, "scoreFloatSO is missing!!!");
        CustomLogs.Instance.Warning(onTargetClickEvent == null, "onTargetClickEvent is missing!!!");

        isFailedConfig = scoreFloatSO == null || onTargetClickEvent == null;
    }
    

    /// <summary>
    /// Raise by ScoreFloat Event from TargetOnClick
    /// </summary>
    public void SpawnFloatScore(ScoreFloatData scoreFloatData)
    {
        if (isFailedConfig)
            return;

        var scoreFloatObj = Instantiate(scoreFloatSO.Prefab, scoreFloatData.Position, Quaternion.identity);
        var textMesh = scoreFloatObj.GetComponent<TextMesh>();
        var meshRenderer = scoreFloatObj.GetComponent<MeshRenderer>();

        ChangeScore(textMesh, scoreFloatData);
        FadeOut(scoreFloatObj, meshRenderer, scoreFloatData);
    }

    private void ChangeScore(TextMesh textMesh, ScoreFloatData scoreFloatData)
    {
        textMesh.text = "+" + scoreFloatData.Score;
        textMesh.color = scoreFloatData.Color;
    }

    private void FadeOut(GameObject scoreFloatObj, MeshRenderer meshRenderer, ScoreFloatData scoreFloatData)
    {
        var cachedTransform = scoreFloatObj.transform;

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(cachedTransform.DOMove(scoreFloatData.Position + Vector3.up, scoreFloatSO.MoveUpTime))
            .Append(cachedTransform.DOMove(scoreFloatSO.MainScorePos, scoreFloatSO.MoveToMainScoreTime))
            .Append(meshRenderer.material.DOFade(scoreFloatSO.Intensity, scoreFloatSO.FadeTime))
            .OnComplete(() => CompleteFade(scoreFloatData.Score, scoreFloatObj));
    }

    private void CompleteFade(int point, GameObject scoreFloatObj)
    {
        onTargetClickEvent.RaiseEvent(point);
        Destroy(scoreFloatObj);
    }
}
