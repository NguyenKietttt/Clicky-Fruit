using System.Collections;
using UnityEngine;

public class RipplePostProcessor : MonoBehaviour
{
    [Header("Configs")]
    [SerializeField] private RippleSO rippleSO;

    [Header("Validation")]
	[SerializeField] private bool isFailedConfig;

    private float amount = 0f;


    private void OnValidate() 
    {
        CustomLogs.Instance.Warning(rippleSO == null, "rippleSO is missing!!!");

        isFailedConfig = rippleSO == null;
    }

    private void OnEnable() 
    {
        if (isFailedConfig)
            enabled = false;
    }

    private void Update()
    {
        rippleSO.RippleMaterial.SetFloat("_Amount", amount);
        amount *= rippleSO.Friction;
    }

    
    /// <summary>
    ///  Raise by OnBadTargetClick Event from TargetOnClick
    /// </summary>
    public void RippleEffect(VFXData vFXData)
    {
        if (isFailedConfig)
            return;

        amount = rippleSO.MaxAmount;

        rippleSO.RippleMaterial.SetFloat("_CenterX", vFXData.Position.x);
        rippleSO.RippleMaterial.SetFloat("_CenterY", vFXData.Position.y);

        StartCoroutine(SlowTime());
    }

    IEnumerator SlowTime()
    {
        Time.timeScale = 0.5f;

        yield return new WaitForSeconds(rippleSO.Durable);
        Time.timeScale = 1.0f;
    }

    private void OnRenderImage(RenderTexture src, RenderTexture dst)
    {
        Graphics.Blit(src, dst, rippleSO.RippleMaterial);
    }
}
