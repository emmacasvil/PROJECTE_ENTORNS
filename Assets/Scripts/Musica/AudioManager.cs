using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource musicSource;

    public AudioClip Musica_DISTOPIC;
    public AudioClip Musica_NORMAL;
    public AudioClip Musica_UTOPIC;

    void Start()
    {
        Reaccio(GameManager.Instance.estatActual);
    }

    void OnEnable()
    {
        GameManager.Instance.Canvi += Reaccio;
    }

    void OnDisable()
    {
        GameManager.Instance.Canvi -= Reaccio;
    }

    void Reaccio(int estat)
    {
        Debug.Log("Reaccio cridada amb estat: " + estat + " | valorEstat: " + GameManager.Instance.valorEstat); //NO FUNCIONA HELP

        AudioClip novaMusica = null;

        if (estat == GameManager.ESTAT_DISTOPIC)
            novaMusica = Musica_DISTOPIC;
        else if (estat == GameManager.ESTAT_NORMAL)
            novaMusica = Musica_NORMAL;
        else if (estat == GameManager.ESTAT_UTOPIC)
            novaMusica = Musica_UTOPIC;

        if (musicSource.clip != novaMusica)
        {
            musicSource.clip = novaMusica;
            musicSource.Play();
            Debug.Log("Música canviada: " + novaMusica.name);
        }
    }
}
