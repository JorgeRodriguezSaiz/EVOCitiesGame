using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {
    public bool firstGame = true;
    public GameObject panelJugando;
    public GameObject panelTuto;
    public Text textoTuto;
    private string auxString;
    public string[] text;
    public int textNumber = 0;
    public float tiempoLetras = 0.1f;
    public bool finAnimate = true;
    public bool click = false;
    public bool skip = false;
	// Use this for initialization
	void Start ()
    {
        if (firstGame)
        {
            StartCoroutine(Tuto(textNumber));
        }
    }
    IEnumerator Tuto(int auxInt)
    {
        panelJugando.SetActive(false);
        panelTuto.SetActive(true);
        if (finAnimate)
        {
            finAnimate = false;
            StartCoroutine(AnimateText(text[auxInt]));
        }
        firstGame = false;
        yield return null;        
    }
    IEnumerator AnimateText(string strComplete)
    {
        auxString = "";
        for (int i = 0; i < strComplete.Length; i++)
        {
            if (i == 4)
            {
                IntroducirName();
            }
            auxString += strComplete[i];
            textoTuto.text = auxString;
            yield return new WaitForSeconds(tiempoLetras);
        }
        finAnimate = true;
        click = true;
        yield return null;
    }
    IEnumerator IntroducirName()
    {
        TouchScreenKeyboard.Open("");
        yield return null;
    }
    public void Click()
    {
        if (click)
        {
            click = false;
            if (textNumber < text.Length-1)
            {
                textNumber++;
            }
            StartCoroutine(Tuto(textNumber));
        }
        else
        {
            StopAllCoroutines();
            textoTuto.text = text[textNumber];
            click = true;
            finAnimate = true;
        }
    }
}
