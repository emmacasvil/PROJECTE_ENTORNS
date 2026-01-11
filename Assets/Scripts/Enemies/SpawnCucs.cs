using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//la idea d'aquest script és que quane l món sigui distòpic, hi hagin molts cucs, poc a poc aniran desapareixent a mesura que el món es torna utòpic
public class SpawnCucs : MonoBehaviour
{
    public GameObject enemyPrefab; //llegim a través del inspector el prefab del enemic
    public Transform[] spawnPoints;

    private int maxEnemics = 14; //hem establert que el màxim d'enemics siguin 14
    private Coroutine ajustarRoutine; //guardem la corutina

    void Start()
    {
        GameManager.Instance.Canvi += Reaccio; //ens subscrivim al event GameManager  
        GameManager.Instance.ValorModificat += ActualitzarMaxEnemics;

        Reaccio(GameManager.Instance.estatActual); 
        ActualitzarMaxEnemics(GameManager.Instance.valorEstat);

        SpawnInicial(); //omplim el mapa de cucs amb aquesta funció
    }


    void OnDisable()
    {
        GameManager.Instance.Canvi -= Reaccio; //quan l'objecte mor es desuscriu del event reaccio
        GameManager.Instance.ValorModificat -= ActualitzarMaxEnemics; //s'actualitza els enemics
    }

    // SPAWN INICIAL
    void SpawnInicial() //spawnegem tants cucs com permet el estatActual
    { //exemple: maxEnemics = 6 --> només spawnegen 6
        for (int i = 0; i < maxEnemics; i++)
            SpawnEnemy();
    }


    // SPAWN D’UN ENEMIC
    void SpawnEnemy()
    {
        List<Transform> lliures = new List<Transform>(); //tota la llista del inspector de tots els spawnPoints

        foreach (Transform t in spawnPoints) //recorrem tots els spawnpoints
        {
            SpawnPoints sp = t.GetComponent<SpawnPoints>(); //llegim els spawpoints
            if (sp != null && !sp.ocupat) //mirem si estan ocupats o no
                lliures.Add(t); //aquesta part serveix perque els cucs no spawnegin un sobre l'altre
        }

        if (lliures.Count == 0) //si no hi ha lloc no spawnegem cap cuc
            return;

        Transform punt = lliures[Random.Range(0, lliures.Count)]; //triem un punt del mapa random
        SpawnPoints spPoint = punt.GetComponent<SpawnPoints>();

        spPoint.ocupat = true; //marquem el punt de spawn com a ocupat

        GameObject enemic = Instantiate(enemyPrefab, punt.position, Quaternion.identity); //instanciem el cuc, quan es mori el cuc el lloc s'alliberarà

        Enemy e = enemic.GetComponent<Enemy>();
        e.puntSpawn = spPoint;
    }

    void Reaccio(int estat)
    {
        // No fem res aquí
    }

    // CANVI GRADUAL DEL VALOR
    void ActualitzarMaxEnemics(float valor)
    {

        maxEnemics = Mathf.RoundToInt(Mathf.Lerp(14, 0, valor / 20f)); //el lerp funciona per calcular quants cucs hi ha d'haver en cada moment, recordem que quan ValorEstat = 20, no hi haurà cap cuc
        if (maxEnemics < 0) maxEnemics = 0;

        Debug.Log($"[ENEMIES] Nou maxEnemics: " + maxEnemics);

        AjustarEnemics(); //cada vegada que canvia el món mirem quants enemics hi han
    }


    // INICIA O REINICIA LA COROUTINE D’AJUST
    void AjustarEnemics()
    {
        if (ajustarRoutine != null)
            StopCoroutine(ajustarRoutine);

        ajustarRoutine = StartCoroutine(AjustGradual());
    }

    // DESPAWN - SPAWN GRADUAL
    IEnumerator AjustGradual()
    {
        while (true)
        {
            Enemy[] enemics = FindObjectsOfType<Enemy>(); //comptem quants enemics hi han en l'escena
            int actuals = enemics.Length;
            Debug.Log($"[ENEMIES] Enemics actuals: {actuals} | Max permès: {maxEnemics}");


            // Si sobren enemics → eliminar un
            if (actuals > maxEnemics)
            {
                enemics[0].Morir(); //matem un enemic
                yield return new WaitForSeconds(0.4f); //esperem
                continue; //tornem a mirar si en sobren o no
            }

            // Si en falten enemics → crear un
            if (actuals < maxEnemics)
            {
                SpawnEnemy(); //generem un
                yield return new WaitForSeconds(0.4f);
                continue;
            }

            // Si coincideix → aturar
            break;
        }

        ajustarRoutine = null;
    }
}
