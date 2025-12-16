using UnityEngine;
using System.Collections;

public class Regadora : MonoBehaviour
{
    private int direccio = 1; // 1 = dreta, -1 = esquerra

    [SerializeField] private float distanciaReg = 2.5f;

    //[SerializeField] ho posem perquè així poguem veure a l'inspector de Unity encara que sigui una variable privada
    [SerializeField] private float tempsDistopic = 2.5f; //quan esdistòpic la velocitat en la que es rega és més lenta
    [SerializeField] private float tempsNormal = 1.5f;
    [SerializeField] private float tempsUtopic = 1f;//quan es utopic va una mica més ràpid
    [SerializeField] private GameObject asset_regadora; //creo un gameobject buit perquè NO es veigui la regadora

    private bool regant = false; //de primeres el jugador NO està regant

    void Start()
    {
        asset_regadora.SetActive(false); //l'asset de la regadora NO es veurà en un principi
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        if (moveX != 0)
        {
            direccio = (int)Mathf.Sign(moveX);
        }

        if (Input.GetKeyDown(KeyCode.F) && !regant) //la tecla de REGAR SERÀ LA F, 
        {
            //  Debug.Log("S'ha premut la tecla F");
            IntentarRegar(); //quan l'usuari prem la tecla F mirem si podem regar 
        }
    }

    void IntentarRegar()
    {
        Vector2 origen = transform.position;
        Vector2 direccioRay = Vector2.right * direccio;

        int mask = LayerMask.GetMask("FLOWER");

        RaycastHit2D hit = Physics2D.Raycast(origen, direccioRay, distanciaReg, mask);


        if (hit.collider != null)
        {
          //  Debug.Log("Raycast ha tocat: " + hit.collider.name); //fem un debug per veure que el Raycast hagi tocat al collider de la flor

            FlowerState flor = hit.collider.GetComponent<FlowerState>();

            if (flor != null)
            {
                StartCoroutine(RegarFlor(flor));
            }
        }
    }

    IEnumerator RegarFlor(FlowerState flor)
    {
        regant = true; //quant hagi començat a regar, el jugador no podrà premer un altre cop F, així evitem Spammers
        asset_regadora.SetActive(true);

        float tempsReg = ObtenirTempsSegonsEstat(); //aquí entra el GameManager, depenent de l'estat en que es troba el joc es tardarà més o menys en regar

        yield return new WaitForSeconds(tempsReg); //sempre posarem un yield return en una corutina

        flor.AtendreFlor(); //LI PASSEM EL MISSATGE DE QUE LA FLOR HA ESTAT ATESA gràcies a la funció creada en el script FlowerState

        asset_regadora.SetActive(false); //desactiva el asset de la regadora, ja NO es veurà per pantalla

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
