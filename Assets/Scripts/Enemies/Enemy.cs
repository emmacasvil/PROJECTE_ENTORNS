using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public SpawnPoints puntSpawn;

    public static event Action EnemicHaMort;

    public void Morir()
    {
        if (puntSpawn != null)
            puntSpawn.ocupat = false;

        EnemicHaMort?.Invoke();
        Destroy(gameObject);
    }
}
