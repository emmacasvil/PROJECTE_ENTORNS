using UnityEngine;
using System; 

// AQUEST CODI ES EL QUE CONTROLA ELS ESTATS GENERALS DEL JOC (UTOPIC, DISTOPIC I NORMAL) //

public class GameManager : MonoBehaviour
{

    //static vol dir que nomes hi ha una copia de l'objecte. Instance es una referencia global al GameManager. 
    public static GameManager Instance;

    //Crea un event; una llista de funcions que s'executen quan hi ha un canvi. 
    public event Action<int> Canvi;

    //Declaracio dels estats
    public const int ESTAT_DISTOPIC = 0;
    public const int ESTAT_NORMAL = 1;
    public const int ESTAT_UTOPIC = 2;

    public float valorEstat = 0f; //això només és una variable per indicar en quin número està el món

    public int estatActual 
    {
        get //rang de 0 a 20, sent 20 el màxim que es pot assolir
        {
            if (valorEstat <= 7) return ESTAT_DISTOPIC;
            else if (valorEstat <= 14) return ESTAT_NORMAL;
            else return ESTAT_UTOPIC;
        }
    }

    //Aqui s'indica que el Game Manager sera la instancia global del joc. S'executa quan es crea l'objecte abans de l'start. 
    private void Awake()
    {
        Instance = this;
    }

    //la funció Modifier seria algo així: li passem el valor de l'acció del jugador que ha fet perquè l'estat del món canvii,
    //li sumem aquest valor al valor del estat, mirem que NO passi dels límits establerts i crida la funció CanviarEstat().
    public void Modifier(float valor) //això ens servirà per implementar que els altres objectes sumin o restin punts
    {
        valorEstat += valor;

        if (valorEstat < 0) valorEstat = 0; //fem això per assegurar-nos de que el joc NO arribi a números negatius
        if (valorEstat > 20) valorEstat = 20; //el mateix, el màxim és 20

        CanviarEstat();
    }

    //Funcio per canviar d'estat. 
    public void CanviarEstat()
    {
        int nouEstat = estatActual; // obté l'estat actual a partir de valorEstat

        Debug.Log("EL MÓN HA CANVIAT A ESTAT: " + nouEstat + " | valorEstat: " + valorEstat); //això és per comprovar que canvii correctament l'estat

        Canvi?.Invoke(nouEstat);

    }
}