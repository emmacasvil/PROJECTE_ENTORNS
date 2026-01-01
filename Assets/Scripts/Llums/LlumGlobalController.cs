using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class LlumGlobalController : MonoBehaviour
{ //la llum global es la que il·lumina tota l'escena, aquest codi fa que la intensitat de la llum vagi creixent poc a poc
    public Light2D llumGlobal;
    public float intensitatBase = 0.4f; //aquesta és la intensitat amb la que començarà a brillar la llum
    public float increment = 0.1f; //cada valorEstat +1f, s'incrementarà la intensitat per 0.1
    public float velocitatCanvi = 2f; //com hem explicat en el PlayerVelocity, això és la rapidesa del Lerp

    float intensitatTarget;

    void Start() { 
        
        intensitatTarget = intensitatBase;  //al principi la intensitat base serà 0.4, la que s'ha establert en la variable intensitatBase
        
        llumGlobal.intensity = intensitatBase;  //aquí li estem dient que la variable intensitat en l'inspector és la intensitatBase
       
        Debug.Log("[LLUM GLOBAL] Start executat"); //hem hagut de fer un debug perquè
      
        if (GameManager.Instance != null) { GameManager.Instance.ValorModificat += ReaccioGradual; 
            Debug.Log("[LLUM GLOBAL] Subscrit correctament des de Start()"); 
        } 

        else { 
            Debug.LogWarning("[LLUM GLOBAL] GameManager.Instance és NULL a Start()"); 
        } 
    }

    void Update()
    { //mirar el codi de playerVelocity per entendre el funcionament d'aquest Lerp.
        llumGlobal.intensity = Mathf.Lerp(llumGlobal.intensity, intensitatTarget, Time.deltaTime * velocitatCanvi);
    }


    public void ReaccioGradual(float valorEstat)
    {
        intensitatTarget = intensitatBase + valorEstat * increment; //exactament la mateixa formula que fem servir en el PlayerVelcity
        Debug.Log($"[LLUM GLOBAL] valorEstat={valorEstat} → intensitatTarget={intensitatTarget}");
    }
}
