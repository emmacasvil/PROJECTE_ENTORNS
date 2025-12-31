using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class VignetteControl : MonoBehaviour
{
    private Volume volume; // assignar el Volume amb el post-processing
    private Vignette vignette;

    private Coroutine _vignetteRoutine;
    public float vignetteFadeDuration = 1f;

    private void OnEnable()
    {
        GameManager.Instance.Canvi += VignetteFade;
    }

    private void OnDisable()
    {
        GameManager.Instance.Canvi -= VignetteFade;
    }

    void Start()
    { 
        volume = GetComponent<Volume>(); //agafa volume
        volume.profile.TryGet<Vignette>(out vignette); //assigna al vignette
    }

    private void Update()
    {
        VignetteFade(GameManager.Instance.estatActual);
    }

    IEnumerator FadeVignetteTo(float targetIntensity)
    {
        float startIntensity = vignette.intensity.value;
        float time = 0f;

        while (time < vignetteFadeDuration)
        {
            time += Time.deltaTime;
            float t = time / vignetteFadeDuration;

            vignette.intensity.value = Mathf.Lerp(
                startIntensity,
                targetIntensity,
                t
            );

            yield return null;
        }

        vignette.intensity.value = targetIntensity;
    }

    void VignetteFade(int estat)
    {
        float targetIntensity = 0f; 

        switch (estat)
        {
            case 0: // distopic
                targetIntensity = 0.7f;
                break;
            case 1: // normal
                targetIntensity = 0.3f;
                break;
            case 2: // utopic
                targetIntensity = 0f;
                break;
        }

        if (_vignetteRoutine != null)
            StopCoroutine(_vignetteRoutine);

        _vignetteRoutine = StartCoroutine(FadeVignetteTo(targetIntensity));
    }
}
