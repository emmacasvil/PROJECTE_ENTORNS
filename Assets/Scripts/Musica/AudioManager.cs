using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioSource musicSource;

    //Afegim els clips de musica
    public AudioClip Musica_DISTOPIC;
    public AudioClip Musica_NORMAL;
    public AudioClip Musica_UTOPIC;

    //el següent codi s'ha extret del esquelet que vam programar al principi de tot per facilitar la integritat i unificar els processos a l'hora de programar (així sempre tindrem les reaccions dels diferents objectes iguals)
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

    void Reaccio(int estat) //rep l'estat que hem generat amb l'event del GameManager: Canvi?.Invoke(estat)
    {
        AudioClip novaMusica = null; //aquesta varible gaurdarà quina cançó serà

        //triarem la música segons l'estat
        if (estat == GameManager.ESTAT_DISTOPIC)
            novaMusica = Musica_DISTOPIC;
        else if (estat == GameManager.ESTAT_NORMAL)
            novaMusica = Musica_NORMAL;
        else if (estat == GameManager.ESTAT_UTOPIC)
            novaMusica = Musica_UTOPIC;

        if (musicSource.clip != novaMusica) //aquest condicional el fem servir perquè, si ja està sonant la música NO la reiniciem. 
        { //això s'explica fàcil, com ho cridem a cada frame, el programa ho va fent cada cop, si no fessim això, estaria contant com que a cada frame la música és nova (encara que no hagi canviat el estat)
            musicSource.clip = novaMusica; //assigem la nova cançó a la variable principal
            musicSource.Play(); //li donem al play perquè soni
            Debug.Log("Música canviada: " + novaMusica.name); //fem un debug per comprovar que tot funcioni
        }
    }
}
