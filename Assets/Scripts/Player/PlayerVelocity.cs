using UnityEngine;
using System;

public class PlayerVelocity : MonoBehaviour
{
    PlayerMovement playerMovement;

    public float velocitatBase = 1.5f;   // comença lent
    public float increment = 0.15f;      // +0.10f per cada valorEstat

    float velocitatTARGET;
    public float velocitatCanvi = 2f;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

        playerMovement.speed = velocitatBase;
        velocitatTARGET = velocitatBase;
    }

    void OnEnable()
    {
        GameManager.Instance.ValorModificat += ReaccioGradual;
    }

    void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.ValorModificat -= ReaccioGradual;
    }

    void Update()
    {
        playerMovement.speed =
            Mathf.Lerp(playerMovement.speed, velocitatTARGET, Time.deltaTime * velocitatCanvi);
    }

    void ReaccioGradual(float valorEstat)
    {
        velocitatTARGET = velocitatBase + valorEstat * increment;

        Debug.Log($"[VELOCITAT] valorEstat={valorEstat} → velocitatTARGET={velocitatTARGET}");
    }
}
