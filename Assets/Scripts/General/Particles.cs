using UnityEngine;

public class Particles : MonoBehaviour
{
    public ParticleSystem _ps;

    // Materials per als diferents estats
    public Material principal;
    public Material distopicMaterial;
    public Material normalMaterial;
    public Material utopicMaterial;

    void Start()
    {
        _ps = GetComponent<ParticleSystem>();

        principal = distopicMaterial;
        var renderer = _ps.GetComponent<ParticleSystemRenderer>();
        renderer.sharedMaterial = principal;

        GameManager.Instance.Canvi += Reaccio;

        _ps.Play();
    }

    void OnEnable()
    {
        GameManager.Instance.Canvi += Reaccio;
    }

    void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.Canvi -= Reaccio;
    }

    void Reaccio(int estat)
    {
        Debug.Log("Reaccio cridada. Estat = " + estat);

        var renderer = _ps.GetComponent<ParticleSystemRenderer>();

        switch (estat)
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

        renderer.sharedMaterial = principal;
        _ps.Clear();
        _ps.Play();
    }

}
