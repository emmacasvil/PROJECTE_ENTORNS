using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource sourceA;
    public AudioSource sourceB;

    public AudioClip Musica_DISTOPIC;
    public AudioClip Musica_NORMAL;
    public AudioClip Musica_UTOPIC;

    void Start()
    {
        // Inicialitzem amb música distòpica
        sourceA.clip = Musica_DISTOPIC;
        sourceA.volume = 1f;
        sourceA.Play();

        sourceB.volume = 0f;

        GameManager.Instance.ValorModificat += OnValorModificat;
    }

    void OnValorModificat(float valor)
    {
        AudioClip fromClip = null;
        AudioClip toClip = null;
        float t = 0f;

        // Determinem quin parell de músiques s'han de barrejar
        switch (GameManager.Instance.estatActual)
        {
            case GameManager.ESTAT_DISTOPIC:
                fromClip = Musica_DISTOPIC;
                toClip = Musica_NORMAL;
                t = valor / 7f; // 0 → 7
                break;

            case GameManager.ESTAT_NORMAL:
                fromClip = Musica_NORMAL;
                toClip = Musica_UTOPIC;
                t = (valor - 7f) / 7f; // 7 → 14
                break;

            case GameManager.ESTAT_UTOPIC:
                fromClip = Musica_UTOPIC;
                toClip = Musica_UTOPIC; // ja estem al màxim
                t = 1f;
                break;
        }

        // Assegurem que els clips estan assignats
        if (sourceA.clip != fromClip)
        {
            sourceA.clip = fromClip;
            sourceA.Play();
        }

        if (sourceB.clip != toClip)
        {
            sourceB.clip = toClip;
            sourceB.Play();
        }

        // Crossfade
        sourceA.volume = 1f - t;
        sourceB.volume = t;
    }

    void OnDestroy()
    {
        GameManager.Instance.ValorModificat -= OnValorModificat;
    }
}
