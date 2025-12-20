using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //en un principi teniem 1 audiomanager, però clar, els canvis de música NO eren gradual, per fer-ho gradual NECESSITAREM 2 audiomanage, així els podrem transicionar entre ells
    public AudioSource sourceA; //per enllaçar els dos audiomanagers al inspector de unity
    public AudioSource sourceB;

    //declarem les variables
    public AudioClip Musica_DISTOPIC;
    public AudioClip Musica_NORMAL;
    public AudioClip Musica_UTOPIC;

    void Start()
    {
        //comencem amb música distòpica
        sourceA.clip = Musica_DISTOPIC; 
        sourceA.volume = 1f; //posem que el volum estigui al màxim (el volum va de 0 a 1)
        sourceA.Play(); //comença a sonar

        sourceB.volume = 0f; //el volum del source B NO s'escoltarà

        GameManager.Instance.ValorModificat += OnValorModificat; //subscrivim aquest Script al event que vam crear al GameManager
    }

    void OnValorModificat(float valor) //aquesta funció és la que fa tota la feina de triar quina música sona i amb quina altra ha de transicionar
    {
        AudioClip fromClip = null; //creem una variable per guardar la variable de la música actual
        AudioClip toClip = null; //creem una altre variable per guardar la varibale de la següent cançó
        float t = 0f; //és una variable auxiliar per la barreja dels dos canals d'àudio

        //Aquest switch determinem quin parell de músiques s'han de barrejar
        switch (GameManager.Instance.estatActual)
        {
            case GameManager.ESTAT_DISTOPIC: //quan el GameManager estigui en utòpic
                fromClip = Musica_DISTOPIC; //la música transiciona de distòpica --> a normal
                toClip = Musica_NORMAL;
                t = valor / 7f; //establim que desde que el valor del estat és de 0 fins al 7 façi aquesta transició, fem que per exemple de 0/7, es zero, per tant NO s'escolta la música normal, en el següent valor d'estat: 1/7, ja es comença a escoltar la música normal, 2/7, 3/7 . . . fins arribar a 7/7 que és 1 i és el màxim´de volum, aquí ja s'escolta per complet la música normal 
                break;

                //la resta de cases funcionen igual
            case GameManager.ESTAT_NORMAL: //quan sigui normal el mateix
                fromClip = Musica_NORMAL;
                toClip = Musica_UTOPIC;
                t = (valor - 7f) / 7f; //aquí li estem dient que de 7 → 14 faci aquest case
                break;

            case GameManager.ESTAT_UTOPIC:
                fromClip = Musica_UTOPIC;
                toClip = Musica_UTOPIC; //aquí no ha d'anar a cap clip, s'ha de quedar  igual
                t = 1f;
                break;
        }

        //aquest troç comprova si el clip que hauria de sonar és diferent del que està sonant ara. Si és diferent, l’actualitza i el reprodueix.
        if (sourceA.clip != fromClip) //això ho fem per evitar de que, si fessim play() cada vegada que el valor d'estat canviés, la música es reiniciaria constanment, i no sonaria bé
        {
            sourceA.clip = fromClip; //bàsicament: Si el clip actual del sourceA NO és el clip “d’origen” que hauria de sonar…
            sourceA.Play(); //l'actualitzem i li donem al play
        }

        if (sourceB.clip != toClip)
        {
            sourceB.clip = toClip;
            sourceB.Play();
        }

        // Crossfade, això es per acabar de que el volum s'ajusti correctament i s'escolti fluid
        //quant t = 0: sourceA.volume = 1, sourceB.volume = 0 → només es sent la música origen.
        // quant t = 0.5: s'escolten les dos per igual
        //quant t = 1: sourceA.volume = 0, sourceB.volume = 1 → només es sent la música transicionada.
        sourceA.volume = 1f - t;
        sourceB.volume = t;
    }

    void OnDestroy()
    {
        GameManager.Instance.ValorModificat -= OnValorModificat;
    }
}
