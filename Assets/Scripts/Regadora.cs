using UnityEngine;
using System.Collections;


public class Regadora : MonoBehaviour
{
    [Header("Configuració de reg")]
    [SerializeField] private float distanciaReg = 1f;

    //[SerializeField] ho posem perquè així poguem veure a l'inspector de Unity encara que sigui una variable privada
    [SerializeField] private float tempsDistopic = 2.5f; //quan esdistòpic la velocitat en la que es rega és més lenta
    [SerializeField] private float tempsNormal = 1.5f;
    [SerializeField] private float tempsUtopic = 1f;//quan es utopic va una mica més ràpid

    private bool regant = false; //de primeres el jugador NO està regant

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !regant) //la tecla de REGAR SERÀ LA F, 
        {
            IntentarRegar(); //quan l'usuari prem la tecla F mirem si podem regar 
        }
    }

    void IntentarRegar()
    {
        RaycastHit hit; 

        if (Physics.Raycast(transform.position, transform.forward, out hit, distanciaReg)) //enviem un raycast perquè així sapiguem si el jugador està davant d'una flor, ja que sinó podriem regar una paret, arbre, etc . . .
        { //hem fet que el jugador hagi d'estar relativament a prop de la flor, ja que sino regaries a distància

            FlowerState flor = hit.collider.GetComponent<FlowerState>();  //llegim el collider i mirem que l'objecte sigui una flor gràcies al codi de FlowerState

            if (flor != null) //Això comprova si ha tocat o no una flor, si ho ha fet comencem la corutina, sino, no fem res
            {
                StartCoroutine(RegarFlor(flor));
            }
        }
    }

    IEnumerator RegarFlor(FlowerState flor)
    {
        regant = true; //quant hagi començat a regar, el jugador no podrà premer un altre cop F, així evitem Spammers

        float tempsReg = ObtenirTempsSegonsEstat(); //aquí entra el GameManager, depenent de l'estat en que es troba el joc es tardarà més o menys en regar

        yield return new WaitForSeconds(tempsReg); //sempre posarem un yield return en una corutina

        flor.AtendreFlor(); //LI PASSEM EL MISSATGE DE QUE LA FLOR HA ESTAT ATESA gràcies a la funció creada en el script FlowerState

        regant = false; //tornem a posar l'acció en false, ara el jugador ja podrà regar un altre cop
    }

    float ObtenirTempsSegonsEstat()
    {
        switch (GameManager.Instance.estatActual) //segons el GameManager, el temps de fer l'acció de regar canviarà:
        {
            case GameManager.ESTAT_NORMAL:

                return tempsNormal;
 
            case GameManager.ESTAT_UTOPIC:
                return tempsUtopic;

            default:
                return tempsDistopic; //predeterminat serà el temps distòpic ja que el joc sempre començarà en distòpic

        }
    }
}
