using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {
    public bool firstGame = true;
    public GameObject panelJugando;
    public GameObject panelTuto;
    public GameObject panelConstuccion;
    public GameObject arbol;
    public Text textoTuto;
    private string auxString;
    public string[] text;
    public int textNumber = 0;
    public float tiempoLetras = 0.1f;
    public bool finAnimate = true;
    public bool click = false;
    public bool skip = false;
    public int fasesTuto = 0;
    public InputField halfonso;
    public string nombre;
    public GameObject[] textos;
    public GameObject ayto;
    public GameObject silo;
	// Use this for initialization
	void OnEnable ()
    {
        if (firstGame)
        {
            StartCoroutine(Tuto(textNumber));           
        }
    }
    void Update()
    {

        switch (textNumber)
        {
            case 0:
                GameObject.Find("Main Camera").GetComponent<SmoothCamera2d>().enabled = false;
                GameObject.Find("Main Camera").GetComponent<PinchZoom>().enabled = false;
                textos[0].SetActive(true);
                break;
            case 1:
                textos[0].SetActive(false);
                textos[1].SetActive(true);
                break;
            case 2:
                textos[1].SetActive(false);
                textos[2].SetActive(true);
                break;
            case 3:
                textos[2].SetActive(false);
                textos[3].SetActive(true);
                break;
            case 4:
                textos[3].SetActive(false);
                textos[4].SetActive(true);
                halfonso.gameObject.SetActive(true);
                text[5] = "Mmm..." + nombre + ", ¿es correcto? Éste es el nombre que quieres, ¿verdad? ";
                break;
            case 5:
                textos[4].SetActive(false);
                textos[5].SetActive(true);
                halfonso.gameObject.SetActive(false);
                break;
            case 6:
                textos[5].SetActive(false);
                textos[6].SetActive(true);
                break;
            case 7:
                GameObject.Find("Main Camera").GetComponent<Transform>().position = new Vector3(ayto.transform.position.x, ayto.transform.position.y,
                            GameObject.Find("Main Camera").GetComponent<Transform>().position.z);                
                textos[6].SetActive(false);
                textos[7].SetActive(true);
                break;
            case 8:
                GameObject.Find("Main Camera").GetComponent<Transform>().position = new Vector3(silo.transform.position.x-3, silo.transform.position.y,
                            GameObject.Find("Main Camera").GetComponent<Transform>().position.z);
                textos[7].SetActive(false);
                textos[8].SetActive(true);
                break;
            case 9:
                textos[8].SetActive(false);
                textos[9].SetActive(true);
                break;
            case 10:
                textos[9].SetActive(false);
                textos[10].SetActive(true);
                break;
            case 11:
                textos[10].SetActive(false);
                textos[11].SetActive(true);
                break;
            case 12:
                panelConstuccion.SetActive(true);
                textos[11].SetActive(false);
                textos[12].SetActive(true);
                break;
            case 13:
                textos[12].SetActive(false);
                textos[13].SetActive(true);
                break;
            case 14:

                textos[13].SetActive(false);
                textos[14].SetActive(true);
                break;
            case 15:
                panelConstuccion.SetActive(false);
                textos[14].SetActive(false);
                textos[15].SetActive(true);
                break;
            case 16:
                
                textos[15].SetActive(false);
                textos[16].SetActive(true);
                break;
            case 17:
                GameObject.Find("Main Camera").GetComponent<Transform>().position = new Vector3(arbol.transform.position.x, arbol.transform.position.y,
                            GameObject.Find("Main Camera").GetComponent<Transform>().position.z);
                textos[16].SetActive(false);
                textos[17].SetActive(true);
                break;
            case 18:
                textos[17].SetActive(false);
                textos[18].SetActive(true);
                break;
            case 19:
                textos[18].SetActive(false);
                textos[19].SetActive(true);
                break;
            case 20:
                textos[19].SetActive(false);
                textos[20].SetActive(true);
                break;
            case 21:
                textos[20].SetActive(false);
                textos[21].SetActive(true);
                break;
            case 22:
                textos[21].SetActive(false);
                textos[22].SetActive(true);
                break;
            case 23:
                textos[22].SetActive(false);
                textos[23].SetActive(true);
                break;

        }
        /*if (textNumber == 3)
        {
            halfonso.gameObject.SetActive(true);
        }*/
    }
    IEnumerator Tuto(int auxInt)
    {
        //panelJugando.SetActive(false);
        //panelTuto.SetActive(true);
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
            auxString += strComplete[i];
            textos[textNumber].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = auxString;
            yield return new WaitForSeconds(tiempoLetras);
        }
        finAnimate = true;
        click = true;
        yield return null;
    }
    public void Click()
    {
        if (click)
        {
            click = false;
            if (textNumber < text.Length)
            {
                textNumber++;
            }
            StartCoroutine(Tuto(textNumber));
        }
        else
        {
            StopAllCoroutines();
            textos[textNumber].transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<Text>().text = text[textNumber];
            click = true;
            finAnimate = true;
        }
    }
    public void devolverControl()
    {
        panelTuto.SetActive(false);
        panelJugando.SetActive(true);
        GameObject.Find("Main Camera").GetComponent<SmoothCamera2d>().enabled = true;
        GameObject.Find("Main Camera").GetComponent<PinchZoom>().enabled = true;
    }
    public void Name()
    {
        nombre = halfonso.text;
    }
    public void Cancel()
    {
        nombre = null;
        halfonso.text = null;
    }
}
