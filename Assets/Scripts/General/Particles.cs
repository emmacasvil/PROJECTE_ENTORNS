using UnityEngine;

public class Particles : MonoBehaviour
{
    public ParticleSystem _ps; //aquesta és la variable on enllaçarem al inspector el sistema de partícules que farem servir

    //Les variables dels materials creats per als diferents estats
    public Material principal; //material base
    public Material distopicMaterial;
    public Material normalMaterial;
    public Material utopicMaterial;

    void Start()
    {
        _ps = GetComponent<ParticleSystem>(); //llegim el particle system

        principal = distopicMaterial; //farem que comenci amb el sistema de partícules del material distòpic
        var renderer = _ps.GetComponent<ParticleSystemRenderer>(); //llegim el renderer del sistema de partícules
        renderer.sharedMaterial = principal; //li assigem que el material incial serà el "material principal"

        GameManager.Instance.Canvi += Reaccio; //subscrivim aquest script al event de canvi d'estat del GameManager, quan es cridi l'event cridarem Reacció()

        _ps.Play(); //comença a emetre partícules
    }

    void OnEnable()
    {
        GameManager.Instance.Canvi += Reaccio; //Quan l’objecte s’activa, es torna a subscriure a l’event.
    }

    void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.Canvi -= Reaccio; //i al revés: quan es desactiva, es des-subscriu per evitar errors.
    }

    void Reaccio(int estat)
    {
        Debug.Log("Reaccio cridada. Estat = " + estat); //fem un debug per comprovar que la funció reacció() vagi bé (ens ha donat molts problemes)

        var renderer = _ps.GetComponent<ParticleSystemRenderer>();

        switch (estat) //fem un switch molt bàsic, quan sigui distòpic, normal i utòpic li assignarem el material de partícula corresponent
        {
            case GameManager.ESTAT_DISTOPIC:
                principal = distopicMaterial;
                break;

            case GameManager.ESTAT_NORMAL:
                principal = normalMaterial;
                break;

            case GameManager.ESTAT_UTOPIC:
                principal = utopicMaterial;
                break;
        }

        renderer.sharedMaterial = principal; //el material nou serà assignat
        _ps.Clear(); //esborrarà les partícules que hi havia (les antigues)
        _ps.Play(); //mostrarà les noves
    }

}
