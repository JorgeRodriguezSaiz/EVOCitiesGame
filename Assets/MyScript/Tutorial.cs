using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {
    private bool firstGame = true;
    public GameObject panelJugando;
    public GameObject panelTuto;
    public Text textoTuto;
    private string auxString;
    private string text;
    public float tiempoLetras = 0.1f;
    public bool finAnimate = true;
    public bool click = false;
	// Use this for initialization
	void Start ()
    {
        if (firstGame)
        {
            StartCoroutine(Tuto(0));
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
    }
    IEnumerator Tuto(int auxInt)
    {
            panelJugando.SetActive(false);
            panelTuto.SetActive(true);
            if (finAnimate && auxInt == 0)
            {
            finAnimate = false;
            text = "¡Bienvenido a Evo Cities! ¡Gracias por venir tan rápido!";
            StartCoroutine(AnimateText(text));
            }
           
            firstGame = false;
            /*if (click)
            {
                click = false;
                textoTuto.text = "Como puedes ver, ahora mismo nos encontramos en la Prehistoria.Pero no te preocupes, " +
                    "si trabajas duro y cuidas de tus ciudadanos pronto irás avanzando en las distintas épocas. ¡Quién sabe lo que nos depara el futuro!" +
                    "Yo voy a encargarme de enseñarte  cómo puedes comenzar a construir tu ciudad.\n" +
                    "¡Lo primero!Tienes que darle un nombre a tu ciudad, ¿cómo quieres que se llame ?";
                if (click)
                {
                    click = false;
                    yield break;
                }
            }*/
            yield return null;
        
    }
    IEnumerator AnimateText(string strComplete)
    {
        while (true)
        {
            
            //int i = 0;
            auxString = "";
           /* while (i < strComplete.Length)
            {
                auxString += strComplete[i++];
                textoTuto.text = auxString;
                yield return new WaitForSeconds(tiempoLetras);
            }*/
            //yield return new WaitForSeconds(tiempoLetras);

            for (int i = 0; i < strComplete.Length; i++)
            {
                auxString += strComplete[i];
                textoTuto.text = auxString;
                yield return new WaitForSeconds(tiempoLetras);
            }
            finAnimate = true;
            if (finAnimate)
            {
                yield break;
            }
        }
    }
    public void OnClick()
    {
        click = true;
    }
}
