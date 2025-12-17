using UnityEngine;
using System;

public class Particles : MonoBehaviour
{

    public ParticleSystem _ps;
    void Start()
    {
        _ps = GetComponent<ParticleSystem>();
    }
    void OnEnable()
    {
        GameManager.Instance.Canvi += Reaccio;
    }

    void OnDisable()
    {
        GameManager.Instance.Canvi -= Reaccio;
    }

    void SetAmount()
    {
        float t = Time.time;
        var emission = _ps.emission;
        emission.rateOverTime = t;
    }
    void Reaccio(int estat)
    {
        if (estat == GameManager.ESTAT_DISTOPIC)
        {
            /////
        }
        else if (estat == GameManager.ESTAT_NORMAL)
        {
            ////
        }
        else if (estat == GameManager.ESTAT_UTOPIC)
        {
            ////
        }
    }
}
