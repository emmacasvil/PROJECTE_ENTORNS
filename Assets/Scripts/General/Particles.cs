using UnityEngine;

public class Particles : MonoBehaviour
{
    public ParticleSystem _ps;

    // AFEGIT: referències als materials
    public Material principal; 
    public Material distopicMaterial;
    public Material normalMaterial;
    public Material utopicMaterial;

    void Start()
    {
        _ps = GetComponent<ParticleSystem>();
        _ps.Play();
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
        var renderer = _ps.GetComponent<ParticleSystemRenderer>();

        if (estat == GameManager.ESTAT_DISTOPIC)
        {
            principal = distopicMaterial; 
        }
        else if (estat == GameManager.ESTAT_NORMAL)
        {
            principal = normalMaterial;
        }
        else if (estat == GameManager.ESTAT_UTOPIC)
        {
            principal = utopicMaterial;
        }

        renderer.sharedMaterial = principal;
        _ps.Clear(); 
        _ps.Play();
    }
}
