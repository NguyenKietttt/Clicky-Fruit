using UnityEngine;
using DG.Tweening;

public class FloatText : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private TextMesh floatText;


    public void Run(ScoreData scoreData)
    {
        if (scoreData.Id == gameObject.GetInstanceID())
        {
            floatText.text = "+" + scoreData.Score;
            floatText.color = scoreData.Color;

            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(transform.DOMove(transform.position + Vector3.up, 0.5f))
                .Append(meshRenderer.material.DOFade(0.0f, 0.2f))
                .OnComplete(() => Destroy(gameObject));
        }
    }
}
