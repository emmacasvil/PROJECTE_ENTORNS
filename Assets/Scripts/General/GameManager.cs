using UnityEngine;
using System;

// AQUEST CODI ES EL QUE CONTROLA ELS ESTATS GENERALS DEL JOC (UTOPIC, DISTOPIC I NORMAL) //

public class GameManager : MonoBehaviour
{

    //static vol dir que nomes hi ha una copia de l'objecte. Instance es una referencia global al GameManager. 
    public static GameManager Instance;

    //Crea un event; una llista de funcions que s'executen quan hi ha un canvi. 
    public event Action<int> Canvi;
    public event Action<float> ValorModificat; //hem hagut de crear-ne un altre perquè aquesta serà per fer CANVIS GRADUALS, osigui detectarà cada increment o decrement en la variable valorEstat (+1f o -1f per exemple)

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

    /*private void Update() //solució merda temporal perquè sembla que el game manager no acaba de controlar el joc(?)
    {
        Canvi?.Invoke(estatActual);//crida l'estat actual a cada update
    }*/

    //la funció Modifier seria algo així: li passem el valor de l'acció del jugador que ha fet perquè l'estat del món canvii,
    //li sumem aquest valor al valor del estat, mirem que NO passi dels límits establerts i crida la funció CanviarEstat().
    public void Modifier(float valor) //això ens servirà per implementar que els altres objectes sumin o restin punts
    {
        Debug.Log($"[GM] Modifier cridat amb valor: {valor}");

        valorEstat += valor; //bàsicament ens serveix per sumar o restar, dependrà de les accions del jugador, com per exemple regar -> +1f, si és mort una planta -> -1f

        if (valorEstat < 0) valorEstat = 0; //fem això per assegurar-nos de que el joc NO arribi a números negatius
        if (valorEstat > 20) valorEstat = 20; //el mateix, el màxim és 20

        Debug.Log($"[GM] valorEstat després de clamp: {valorEstat}");

        ValorModificat?.Invoke(valorEstat); //aquest event és MOLT important, fa que tots els scripts subscrits com (l'àudio o el tilemap) rebin el nou valor del estat, així aquests podran anar canviant A POC A POC 

        CanviEstat(); //cridem el canvi d'estat
    }

     private int estatAnterior = -1;

    //NOVA FUNCIÓ DE CANVI D'ESTAT -- l'he hagut de modificar perquè l'antiga no funcionava correctament --> Xènia :)
    void CanviEstat()
    {
        int nouEstat = estatActual; // obté l'estat actual a partir de valorEstat
        Debug.Log($"[GM] Comprovant canvi d'estat. nouEstat = {nouEstat}, estatAnterior = {estatAnterior}");

        if (nouEstat != estatAnterior) //aquí diem que si el nou estat és diferent faci el if, si no, no fa res.
        { //el estatAnterior = -1 el posem perquè així ens assegurem de que el primer estat (serà 0 perquè és distòpic) si o si el façi, si li possessim EstatAnterior = 0, NO s'executaria el condicional, faria 0 != 0
            estatAnterior = nouEstat; //actualitzem el nou estat
            Canvi?.Invoke(nouEstat); //avisem dels canvis als objectes del món així aquests també poden canviar 
        }

        Debug.Log("EL MÓN HA CANVIAT A ESTAT: " + nouEstat + " | valorEstat: " + valorEstat); //això és per comprovar que canvii correctament l'estat
    }
}