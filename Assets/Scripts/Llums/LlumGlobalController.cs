using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class LlumGlobalController : MonoBehaviour
{
    public Light2D llumGlobal;
    public float intensitatBase = 0.5f;
    public float increment = 0.7f;
    public float velocitatCanvi = 2f;

    float intensitatTarget;

    void Start() { intensitatTarget = intensitatBase; llumGlobal.intensity = intensitatBase; 
        Debug.Log("[LLUM GLOBAL] Start executat"); 
        if (GameManager.Instance != null) { GameManager.Instance.ValorModificat += ReaccioGradual; 
            Debug.Log("[LLUM GLOBAL] Subscrit correctament des de Start()"); } 
        else { Debug.LogWarning("[LLUM GLOBAL] GameManager.Instance és NULL a Start()"); } }

    void Update()
    {
        llumGlobal.intensity = Mathf.Lerp(llumGlobal.intensity, intensitatTarget, Time.deltaTime * velocitatCanvi);
    }


    public void ReaccioGradual(float valorEstat)
    {
        intensitatTarget = intensitatBase + valorEstat * increment;
        Debug.Log($"[LLUM GLOBAL] valorEstat={valorEstat} → intensitatTarget={intensitatTarget}");
    }
}
