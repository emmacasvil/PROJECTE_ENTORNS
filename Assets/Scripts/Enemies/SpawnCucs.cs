using UnityEngine;
using System.Collections.Generic;

public class SpawnCucs : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    public float spawnRateDystopic = 0.5f;
    public float spawnRateNormal = 1.5f;
    public float spawnRateUtopic = 3f;

    private float spawnRateActual;
    private float timer = 0f;

    void OnEnable()
    {
        GameManager.Instance.Canvi += Reaccio;
        Reaccio(GameManager.Instance.estatActual); // inicialitzar
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
        // Buscar punts lliures
        List<Transform> lliures = new List<Transform>();

        foreach (Transform t in spawnPoints)
        {
            SpawnPoints sp = t.GetComponent<SpawnPoints>();
            if (!sp.ocupat)
                lliures.Add(t);
        }

        // Si no hi ha punts lliures, no fem spawn
        if (lliures.Count == 0)
            return;

        // Triar un punt lliure aleatori
        Transform punt = lliures[Random.Range(0, lliures.Count)];
        SpawnPoints spPoint = punt.GetComponent<SpawnPoints>();

        // Marcar-lo com ocupat
        spPoint.ocupat = true;

        // Instanciar enemic
        GameObject enemic = Instantiate(enemyPrefab, punt.position, Quaternion.identity);

        // Assignar el punt al cuc
        enemic.GetComponent<Enemy>().puntSpawn = spPoint;

        Debug.Log($"[SPAWN_CUCS] Enemic nou");
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
    }
}
