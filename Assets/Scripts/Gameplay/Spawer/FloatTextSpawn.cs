using System;
using DG.Tweening;
using UnityEngine;

public class FloatTextSpawn : StateBase
{
    [Header("Configs")]
    [SerializeField] private TextFloatSO scoreFloatSO;
    [SerializeField] private TextFloatSO lifeFloatSO;

    [Header("Events")]
    [SerializeField] private IntEventSO onTargetClickEvent;
    [SerializeField] private VoidEventSO reduceLifeEvent;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;

    private bool isGameover;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(scoreFloatSO == null, "scoreFloatSO is missing!!!");
        CustomLogs.Instance.Warning(lifeFloatSO == null, "lifeFloatSO is missing!!!");

        CustomLogs.Instance.Warning(onTargetClickEvent == null, "onTargetClickEvent is missing!!!");
        CustomLogs.Instance.Warning(reduceLifeEvent == null, "reduceLifeEvent is missing!!!");

        isFailedConfig = scoreFloatSO == null || scoreFloatSO == null || onTargetClickEvent == null
            || reduceLifeEvent == null;
    }

    public override void OnGameplay()
    {
        isGameover = false;
    }

    public override void OnGameOver()
    {
        isGameover = true;
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
        FadeOut(scoreFloatObj, meshRenderer, scoreFloatData, scoreFloatSO, 
            () => CompleteScoreFade(scoreFloatData.Score, scoreFloatObj));
    }

    /// <summary>
    /// Raise by LifeFloat Event from TargetOnClick
    /// </summary>
    public void SpawnFloatLife(ScoreFloatData scoreFloatData)
    {
        if (isFailedConfig)
            return;

        if (isGameover)
            return;

        var scoreFloatObj = Instantiate(scoreFloatSO.Prefab, scoreFloatData.Position, Quaternion.identity);
        var textMesh = scoreFloatObj.GetComponent<TextMesh>();
        var meshRenderer = scoreFloatObj.GetComponent<MeshRenderer>();

        ChangeLife(textMesh);
        FadeOut(scoreFloatObj, meshRenderer, scoreFloatData, lifeFloatSO,
            () => CompleteLifeFade(scoreFloatObj));
    } 

    private void ChangeScore(TextMesh textMesh, ScoreFloatData scoreFloatData)
    {
        textMesh.text = "+" + scoreFloatData.Score;
        textMesh.color = scoreFloatData.Color;
    }

    private void ChangeLife(TextMesh textMesh)
    {
        textMesh.text = "-1";
        textMesh.color = Color.white;
    }

    private void FadeOut(GameObject scoreFloatObj, MeshRenderer meshRenderer, 
        ScoreFloatData scoreFloatData, TextFloatSO textFloatSO, Action method)
    {
        var cachedTransform = scoreFloatObj.transform;

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(cachedTransform.DOMove(scoreFloatData.Position + Vector3.up, textFloatSO.MoveUpTime))
            .Append(cachedTransform.DOMove(textFloatSO.EndPos, textFloatSO.MoveToMainScoreTime))
            .Append(meshRenderer.material.DOFade(textFloatSO.Intensity, textFloatSO.FadeTime))
            .OnComplete(() => method());
    }

    private void CompleteScoreFade(int point, GameObject scoreFloatObj)
    {
        onTargetClickEvent.RaiseEvent(point);
        Destroy(scoreFloatObj);
    }

    private void CompleteLifeFade(GameObject scoreFloatObj)
    {
        reduceLifeEvent.RaiseEvent();
        Destroy(scoreFloatObj);
    }
}
