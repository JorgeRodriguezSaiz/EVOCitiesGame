using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class CombustionEspontaneaArborea : MonoBehaviour {
    public bool madera = true;
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
    public int numeroArbol = 0;
    public float tiempoTalar = 0.5f;
    public int exp;
    public int trabajadoresNecesita;
    public float maderaTalar = 15;
    [Header("Piedras")]
    public GameObject panelPiedra;
    public float piedraPicar = 15;
    public float recurso = 0;
    // Use this for initialization
    void Start () {
        StartCoroutine(Wait());
	}
	
	// Update is called once per frame
	void Update () {
        if (talando)
        {
            if (tiempoDesconexionTalar >= tiempoFinalTalar)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.transform.GetChild(4).gameObject.SetActive(true);
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
                }*/
            }
            else
            {
                if (talando)
                {
                    tiempoDesconexionTalar = DateTime.Now;
                    string aux = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}", tiempoRestanteTalar.Days, tiempoRestanteTalar.Hours,
                        tiempoRestanteTalar.Minutes, tiempoRestanteTalar.Seconds);
                    gameObject.GetComponentInChildren<TextMesh>().text = aux;

                    float tAux = (float)tiempoRestanteTalar.TotalSeconds;
                    tAux -= 1 * Time.deltaTime;
                    tiempoRestanteTalar = TimeSpan.FromSeconds(tAux);
                    if (!gameObject.GetComponent<CombustionEspontaneaArborea>().madera)
                    {
                        if (tAux <= tiempoTalar * 60 / 2)
                        {
                            gameObject.GetComponent<SpriteRenderer>().enabled = false;
                            gameObject.transform.GetChild(1).gameObject.SetActive(true);
                        }
                    }
                    if (gameObject.GetComponent<CombustionEspontaneaArborea>().madera)
                    {
                        if (tAux <= (tiempoTalar * 60 * 2) / 3)
                        {
                            gameObject.GetComponent<SpriteRenderer>().enabled = false;
                            gameObject.transform.GetChild(1).gameObject.SetActive(true);
                        }
                        if (tAux <= tiempoTalar * 60 / 3)
                        {
                            gameObject.GetComponent<SpriteRenderer>().enabled = false;
                            gameObject.transform.GetChild(2).gameObject.SetActive(true);
                        }
                    }
                }
            }
        }

    }
    public void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (talando)
            {
                GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion += trabajadoresNecesita;
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                gameObject.transform.GetChild(3).gameObject.SetActive(true);
                gameObject.transform.GetChild(5).gameObject.SetActive(true);
                talando = false;
                GameObject.Find("God").GetComponent<Exp_controller>().exp += exp;
                if (madera)
                {
                    GameObject.Find("Controller").GetComponent<GestionRecursos>().madera += maderaTalar;
                }
                else
                {
                    GameObject.Find("Controller").GetComponent<GestionRecursos>().piedra += piedraPicar;
                }
                ZPlayerPrefs.SetInt("talado" + numeroArbol, 0);
                Destroy(gameObject);
            }
        
        else
        {
            if (gameObject.tag == "arbol")
            {
                madera = true;
                GameObject.Find("Controller").GetComponent<ControllerTalar>().panelTalar.SetActive(true);
                tiempoTalar = GameObject.Find("Controller").GetComponent<ControllerTalar>().tiempoTalar;
                GameObject.Find("Controller").GetComponent<ControllerTalar>().trabajadoresNecesita = trabajadoresNecesita;
                recurso = GameObject.Find("Controller").GetComponent<ControllerTalar>().maderaTalar;
            }
            if (gameObject.tag == "piedra")
            {
                madera = false;
                GameObject.Find("Controller").GetComponent<ControllerTalar>().panelPiedra.SetActive(true);
                tiempoTalar = GameObject.Find("Controller").GetComponent<ControllerTalar>().tiempoTalar;
                trabajadoresNecesita = GameObject.Find("Controller").GetComponent<ControllerTalar>().trabajadoresNecesita;
                recurso = GameObject.Find("Controller").GetComponent<ControllerTalar>().piedraPicar;
            }
            GameObject.Find("Controller").GetComponent<ControllerTalar>().arbol = gameObject;
        }
        }
    }
    /*public void OnClickAceptarTalar()
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
                        GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion -= trabajadoresNecesita;
                        casitas[casa].GetComponent<ComidaCasa>().minaActual = gameObject;
                        Talar();
                        panelTalar.SetActive(false);
                        panelPiedra.SetActive(false);
                    }
                    else
                    {

                    }
                }
            }
        }
    }*/
    public void Talar()
    {
        if (!talando)
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            tiempoActualTalar = DateTime.Now;
            ZPlayerPrefs.SetString("TiempoTalar " + numeroArbol, tiempoActualTalar.ToString());
            tiempoDesconexionTalar = DateTime.Now;
            tiempoFinalTalar = tiempoActualTalar.AddMinutes(tiempoTalar);
            tiempoRestanteTalar = tiempoFinalTalar - tiempoDesconexionTalar;
            talando = true;
            ZPlayerPrefs.SetString("talando" + numeroArbol, talando.ToString());
        }
    }
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        if(ZPlayerPrefs.HasKey("talado" + numeroArbol))
        {
            Destroy(gameObject);
        }
        else
        {
            if(ZPlayerPrefs.HasKey("talando" + numeroArbol))
            {
                //talando = bool.Parse(ZPlayerPrefs.GetString("talando" + numeroArbol));
                if (bool.Parse(ZPlayerPrefs.GetString("talando" + numeroArbol)))
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    tiempoActualTalar = Convert.ToDateTime(ZPlayerPrefs.GetString("TiempoTalar " + numeroArbol));
                    tiempoDesconexionTalar = DateTime.Now;
                    tiempoFinalTalar = tiempoActualTalar.AddMinutes(tiempoTalar);
                    tiempoRestanteTalar = tiempoFinalTalar - tiempoDesconexionTalar;
                    talando = true;
                }
            }
        }
    }
}
