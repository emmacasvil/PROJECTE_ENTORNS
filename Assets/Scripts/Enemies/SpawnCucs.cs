using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnCucs : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    private int maxEnemics = 14;
    private Coroutine ajustarRoutine;

    void Start()
    {
        GameManager.Instance.Canvi += Reaccio;
        GameManager.Instance.ValorModificat += ActualitzarMaxEnemics;

        Reaccio(GameManager.Instance.estatActual);
        ActualitzarMaxEnemics(GameManager.Instance.valorEstat);

        SpawnInicial();
    }


    void OnDisable()
    {
        GameManager.Instance.Canvi -= Reaccio;
        GameManager.Instance.ValorModificat -= ActualitzarMaxEnemics;
    }

    // SPAWN INICIAL
    void SpawnInicial()
    {
        for (int i = 0; i < maxEnemics; i++)
            SpawnEnemy();
    }


    // SPAWN D’UN ENEMIC
    void SpawnEnemy()
    {
        List<Transform> lliures = new List<Transform>();

        foreach (Transform t in spawnPoints)
        {
            SpawnPoints sp = t.GetComponent<SpawnPoints>();
            if (sp != null && !sp.ocupat)
                lliures.Add(t);
        }

        if (lliures.Count == 0)
            return;

        Transform punt = lliures[Random.Range(0, lliures.Count)];
        SpawnPoints spPoint = punt.GetComponent<SpawnPoints>();

        spPoint.ocupat = true;

        GameObject enemic = Instantiate(enemyPrefab, punt.position, Quaternion.identity);

        Enemy e = enemic.GetComponent<Enemy>();
        e.puntSpawn = spPoint;
    }

    // CANVI D’ESTAT (no afecta res, però ho mantenim)
    void Reaccio(int estat)
    {
        // No fem res aquí
    }

    // CANVI GRADUAL DEL VALOR
    void ActualitzarMaxEnemics(float valor)
    {

        maxEnemics = Mathf.RoundToInt(Mathf.Lerp(14, 0, valor / 20f));
        if (maxEnemics < 0) maxEnemics = 0;

        Debug.Log($"[ENEMIES] Nou maxEnemics: " + maxEnemics);

        AjustarEnemics();
    }


    // INICIA O REINICIA LA COROUTINE D’AJUST
    void AjustarEnemics()
    {
        if (ajustarRoutine != null)
            StopCoroutine(ajustarRoutine);

        ajustarRoutine = StartCoroutine(AjustGradual());
    }

    // DESPAWN / SPAWN GRADUAL
    IEnumerator AjustGradual()
    {
        while (true)
        {
            Enemy[] enemics = FindObjectsOfType<Enemy>();
            int actuals = enemics.Length;
            Debug.Log($"[ENEMIES] Enemics actuals: {actuals} | Max permès: {maxEnemics}");


            // Si sobren enemics → eliminar un
            if (actuals > maxEnemics)
            {
                enemics[0].Morir();
                yield return new WaitForSeconds(0.4f);
                continue;
            }

            // Si en falten enemics → crear un
            if (actuals < maxEnemics)
            {
                SpawnEnemy();
                yield return new WaitForSeconds(0.4f);
                continue;
            }

            // Si coincideix → aturar
            break;
        }

        ajustarRoutine = null;
    }
}
