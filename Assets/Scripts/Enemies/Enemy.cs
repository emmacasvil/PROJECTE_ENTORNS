using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public SpawnPoints puntSpawn; //cada cuc recorda quin spawn ocupa

    public static event Action EnemicHaMort; //hem creat un event que es dispara quan l'enemic mort, això serveix perquè altres scripts es puguin subscriure a aquest event i sapiguen quan ha mort l'enemic

    public void Morir() 
    {
        if (puntSpawn != null) //alliberem l'espai, així evitem que un mateix cuc sempre bloquegi aquell punt encara estan mort
            puntSpawn.ocupat = false;

        EnemicHaMort?.Invoke();
        Destroy(gameObject); //destruim el gameobject
    }
}
