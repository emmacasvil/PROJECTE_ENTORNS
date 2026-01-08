using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector] public SpawnPoint puntSpawn;

    // Crida això quan el jugador mati el cuc
    public void Morir()
    {
        if (puntSpawn != null)
            puntSpawn.ocupat = false;

        Destroy(gameObject);
    }
}
