using UnityEngine;
using System.Collections;
using System;

public class ControllerTalar : MonoBehaviour {
    [Header("Arboles")]
    public GameObject panelTalar;
    public GameObject arbol;
    public GameObject[] casitas;
    public bool talando = false, funcionar = true;
    public DateTime tiempoActualTalar;
    public DateTime tiempoDesconexionTalar;
    public DateTime tiempoFinalTalar;
    public TimeSpan tiempoRestanteTalar;
    public int numbConstruccion = 3;
    public float tiempoTalar = 0.5f;
    public int trabajadoresNecesita;
    public float maderaTalar = 15;
    [Header("Piedras")]
    public GameObject panelPiedra;
    public float piedraPicar = 15;

    // Use this for initialization
    void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        /*if (talando)
        {
            if (tiempoDesconexionTalar >= tiempoFinalTalar)
            {
                /*GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion += trabajadoresNecesita;

                talando = false;
                Destroy(arbol);
                if (arbol.GetComponent<CombustionEspontaneaArborea>().madera)
                {
                    gameObject.GetComponent<GestionRecursos>().madera += maderaTalar;
                }
                else
                {
                    gameObject.GetComponent<GestionRecursos>().piedra += piedraPicar;
                }
            }
            else
            {
                if (talando)
                {
                    tiempoDesconexionTalar = DateTime.Now;
                    string aux = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}", tiempoRestanteTalar.Days, tiempoRestanteTalar.Hours,
                        tiempoRestanteTalar.Minutes, tiempoRestanteTalar.Seconds);
                    arbol.GetComponentInChildren<TextMesh>().text = aux;
                    float tAux = (float)tiempoRestanteTalar.TotalSeconds;
                    tAux -= 1 * Time.deltaTime;
                    tiempoRestanteTalar = TimeSpan.FromSeconds(tAux);
                    if (!arbol.GetComponent<CombustionEspontaneaArborea>().madera)
                    {
                        if (tAux <= tiempoTalar*60 / 2)
                        {
                            arbol.GetComponent<SpriteRenderer>().enabled = false;
                            arbol.transform.GetChild(1).gameObject.SetActive(true);
                        }
                    }
                    if (arbol.GetComponent<CombustionEspontaneaArborea>().madera)
                    {
                        if (tAux <= (tiempoTalar*60*2) / 3)
                        {
                            arbol.GetComponent<SpriteRenderer>().enabled = false;
                            arbol.transform.GetChild(1).gameObject.SetActive(true);
                        }
                        if (tAux <= tiempoTalar*60 / 3)
                        {
                            arbol.GetComponent<SpriteRenderer>().enabled = false;
                            arbol.transform.GetChild(2).gameObject.SetActive(true);
                        }
                    }
                }
            }
        }*/
    }
    public void OnClickAceptarTalar()
    {
        if (tiempoDesconexionTalar >= tiempoFinalTalar)
        {
            if (GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion > 0)
            {
                casitas = GameObject.FindGameObjectsWithTag("casa");
                for (int casa = 0; casa <= casitas.Length - 1; casa++)
                {
                    if (casitas[casa].GetComponent<ComidaCasa>().isComida)
                    {
                        casitas[casa].GetComponent<ComidaCasa>().minaActual = arbol;
                        GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion -= trabajadoresNecesita;
                        casitas[casa].GetComponent<ComidaCasa>().minaActual = gameObject;
                        arbol.GetComponent<CombustionEspontaneaArborea>().Talar();
                        panelTalar.SetActive(false);
                        panelPiedra.SetActive(false);
                    }
                    else
                    {

                    }
                }
            }
        }
        //arbol.GetComponent<CombustionEspontaneaArborea>().OnClickAceptarTalar();
    }
    public void OnClickCancelarTalar()
    {
        GameObject.Find("ConfirmacionArboles").SetActive(false);
        panelPiedra.SetActive(false);
    }
    /*public void Talar()
    {
        if (!talando)
        {
                talando = true;
                arbol.transform.GetChild(0).gameObject.SetActive(true);
                tiempoActualTalar = DateTime.Now;
                ZPlayerPrefs.SetString("TiempoTalar " + numbConstruccion, tiempoActualTalar.ToString());
                tiempoDesconexionTalar = DateTime.Now;
                tiempoFinalTalar = tiempoActualTalar.AddMinutes(tiempoTalar);
                tiempoRestanteTalar = tiempoFinalTalar - tiempoDesconexionTalar;
            
        }
    }*/
    public void Finish()
    {
        if (tiempoDesconexionTalar >= tiempoFinalTalar)
        {
            GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion += trabajadoresNecesita;

            talando = false;
            Destroy(arbol);
            if (arbol.GetComponent<CombustionEspontaneaArborea>().madera)
            {
                gameObject.GetComponent<GestionRecursos>().madera += maderaTalar;
            }
            else
            {
                gameObject.GetComponent<GestionRecursos>().piedra += piedraPicar;
            }
        }
    }
}
