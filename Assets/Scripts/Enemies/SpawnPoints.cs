using UnityEngine;

public class SpawnPoints : MonoBehaviour
{
    [HideInInspector] public bool ocupat = false;

    void Start() //quan l'escena s'engega, tots els punts estan buits
    {
        ocupat = false;
    }
}
