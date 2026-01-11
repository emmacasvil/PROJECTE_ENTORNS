using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class VignetteControl : MonoBehaviour
{
    private Volume volume; // assignar el Volume amb el post-processing
    private Vignette vignette;

    public Color dystopicColor = Color.black;
    public Color normalColor = Color.black;
    public Color utopicColor = Color.green;

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

            vignette.intensity.value = Mathf.Lerp(startIntensity, targetIntensity, t);

            yield return null;
        }

        vignette.intensity.value = targetIntensity;
    }

    IEnumerator FadeVignetteColor(Color targetColor)
    {
        Color startColor = vignette.color.value;
        float time = 0f;

        while (time < vignetteFadeDuration)
        {
            time += Time.deltaTime;
            float t = time / vignetteFadeDuration;
            vignette.color.value = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        vignette.color.value = targetColor;
    }

    void VignetteFade(int estat)
    {
        float targetIntensity = 0f;
        Color targetColor = vignette.color.value;

        switch (estat)
        {
            case 0: // distopic
                targetIntensity = 0.7f;
                targetColor = dystopicColor;
                break;
            case 1: // normal
                targetIntensity = 0.3f;
                targetColor = normalColor;
                break;
            case 2: // utopic
                targetIntensity = 0.1f;
                targetColor = utopicColor;
                break;
        }
        vignette.color.value = targetColor;

        if (_vignetteRoutine != null)
            StopCoroutine(_vignetteRoutine);

        _vignetteRoutine = StartCoroutine(FadeVignetteTo(targetIntensity));
        _vignetteRoutine = StartCoroutine(FadeVignetteColor(targetColor));
    }
}
