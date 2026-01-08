using UnityEngine;

public class SpawnCucs : MonoBehaviour
{
    public float spawnRateDystopic = 0.5f;   // més ràpid
    public float spawnRateNormal = 1.5f;     // mig
    public float spawnRateUtopic = 3f;       // més lent

    private float spawnRateActual;
    private float timer = 0f;

    void OnEnable()
    {
        GameManager.Instance.Canvi += Reaccio;

        // Inicialitzar segons l'estat actual
        Reaccio(GameManager.Instance.estatActual);
    }

    void OnDisable()
    {
        GameManager.Instance.Canvi -= Reaccio;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnRateActual)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    void SpawnEnemy()
    {
        Debug.Log("[Spawner] Enemy spawned!");
        // Aquí instancies l’enemic real
    }

    void Reaccio(int estat)
    {
        if (estat == GameManager.ESTAT_DISTOPIC)
        {
            spawnRateActual = spawnRateDystopic;
        }
        else if (estat == GameManager.ESTAT_NORMAL)
        {
            spawnRateActual = spawnRateNormal;
        }
        else if (estat == GameManager.ESTAT_UTOPIC)
        {
            spawnRateActual = spawnRateUtopic;
        }

        Debug.Log("[Spawner] Nou spawnRate = " + spawnRateActual);
    }
}
