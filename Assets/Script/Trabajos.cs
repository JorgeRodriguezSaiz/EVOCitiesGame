using UnityEngine;
using System.Collections;
using System;

public class Trabajos : MonoBehaviour
{
    [Header("Guardado")]
    public int tipoConstruccion = 0;
    public int numbConstruccion = -1;
    [Header("Construccion")]
    public float exp = 75f;
    public double tiempoConstruccion;
    public GameObject particulas;
    [Header("VariablesTrabajo")]
    public int trabajadoresNecesita = 2;
    public string recurso = null;
    public int cantidadRecursos = 0;
    public double tiempoTrabajo;
    [Header("TiemposInicio")]
    private TimeSpan tiempoRestante;
    private DateTime tiempoActual;
    private DateTime tiempoDesconexion, tiempoFinal;
    [Header("TiemposTrabajo")]
    private TimeSpan tiempoRestanteTrabajo;
    private DateTime tiempoActualTrabajo;
    private DateTime tiempoDesconexionTrabajo, tiempoFinalTrabajo;
    [Header("OpcionesTrabajo")]
    public GameObject interfaz;
    public GameObject primeraOpcion;
    public int tipoTrabajo;
    [Header("CasaPrefab")]
    public GameObject casaPrefab;
    private GameObject[] casitas;
    [HideInInspector]
    public bool funcionar = false, finTrabajo = false, startOn = false;
    public bool trabajando = false;
    public bool comer = false, antecomer = false;

    
    // Use this for initialization
    void Start()
    {
        interfaz = GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().interfaz;
        primeraOpcion = GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().primeraOpcion[tipoTrabajo];
        trabajando = false;
       /* if (numbConstruccion <= ZPlayerPrefs.GetInt("cantidadConstrucciones"))
        {
            gameObject.transform.position = new Vector3(ZPlayerPrefs.GetFloat("posX" + numbConstruccion), ZPlayerPrefs.GetFloat("posY" + numbConstruccion),
                ZPlayerPrefs.GetFloat("posZ" + numbConstruccion));
        }*/
        StartCoroutine(Wait());

    }

    // Update is called once per frame
    void Update()
    {

        if (startOn)
        {
            if (tiempoDesconexion >= tiempoFinal)
            {
                if (!funcionar)
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    gameObject.transform.GetChild(3).gameObject.SetActive(true);
                    /* if (!ZPlayerPrefs.HasKey("terminadoConstruir" + numbConstruccion))
                     {
                         funcionar = true;
                         gameObject.transform.GetChild(0).gameObject.SetActive(false);
                         GameObject.Find("God").GetComponent<Exp_controller>().exp += this.exp;
                         ZPlayerPrefs.SetInt("terminadoConstruir" + numbConstruccion, 0);
                         GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion += trabajadoresNecesita;
                         ZPlayerPrefs.SetFloat("poblacion", GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion);
                         gameObject.GetComponent<SpriteRenderer>().enabled = true;
                         gameObject.transform.GetChild(1).gameObject.SetActive(false);
                         //this.enabled = false;
                     }*/
                }
                else
                {
                    if (trabajando)
                    {
                        if (tiempoDesconexionTrabajo < tiempoFinalTrabajo)
                        {

                            gameObject.transform.GetChild(0).gameObject.SetActive(true);
                            tiempoDesconexionTrabajo = DateTime.Now;
                            string aux = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}", tiempoRestanteTrabajo.Days, tiempoRestanteTrabajo.Hours,
                                tiempoRestanteTrabajo.Minutes, tiempoRestanteTrabajo.Seconds);
                            gameObject.GetComponentInChildren<TextMesh>().text = aux;
                            float tAux = (float)tiempoRestanteTrabajo.TotalSeconds;
                            tAux -= 1 * Time.deltaTime;
                            tiempoRestanteTrabajo = TimeSpan.FromSeconds(tAux);
                        }
                        else
                        {
                            gameObject.transform.GetChild(0).gameObject.SetActive(false);
                            gameObject.transform.GetChild(4).gameObject.SetActive(true);
                        }
                    }
                }

            }
            else if (!funcionar)
            {
                if (tiempoDesconexion < tiempoFinal)
                {
                    tiempoDesconexion = DateTime.Now;
                    string aux = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}", tiempoRestante.Days, tiempoRestante.Hours,
                        tiempoRestante.Minutes, tiempoRestante.Seconds);
                    gameObject.GetComponentInChildren<TextMesh>().text = aux;
                    float tAux = (float)tiempoRestante.TotalSeconds;
                    tAux -= 1 * Time.deltaTime;
                    tiempoRestante = TimeSpan.FromSeconds(tAux);
                }
            }
        }


    }
    private void OnMouseUp()
    {
        if (!GameObject.Find("Controller").GetComponent<Construction>().modoConstruccion)
        {
            if (tiempoDesconexion >= tiempoFinal)
            {
                if (funcionar)
                {
                    if (!trabajando)
                    {
                        interfaz.SetActive(true);
                        primeraOpcion.SetActive(true);
                        for (int i = 0; i < GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().primeraOpcion.Length; i++)
                        {
                            GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().primeraOpcion[i].GetComponent<DatosTrabajo>().trabajo = gameObject;
                        }
                        GameObject.Find("Main Camera").GetComponent<Transform>().position = new Vector3(transform.position.x + 4, transform.position.y,
                             GameObject.Find("Main Camera").GetComponent<Transform>().position.z);
                        GameObject.Find("Main Camera").GetComponent<SmoothCamera2d>().enabled = false;
                        GameObject.Find("Main Camera").GetComponent<PinchZoom>().enabled = false;
                    }
                    else
                    {
                        if (tiempoDesconexionTrabajo >= tiempoFinalTrabajo && trabajando)
                        {
                            gameObject.transform.GetChild(4).gameObject.SetActive(false);
                            gameObject.transform.GetChild(5).gameObject.SetActive(true);
                            particulas.SetActive(true);

                            finTrabajo = true;
                            if (finTrabajo)
                            {
                                primeraOpcion = GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().
                                    primeraOpcion[ZPlayerPrefs.GetInt("tipoTrabajo" + numbConstruccion)];
                                GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion += primeraOpcion.GetComponent<DatosTrabajo>().trabajadoresNecesita;
                                ZPlayerPrefs.SetFloat("poblacion", GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion);
                                finTrabajo = false;
                                GameObject.Find("God").GetComponent<Exp_controller>().exp += primeraOpcion.GetComponent<DatosTrabajo>().exp;
                                comer = true;
                                for (int i = 0; i < primeraOpcion.GetComponent<DatosTrabajo>().recursos.Length; i++)
                                {
                                    ZPlayerPrefs.SetFloat(primeraOpcion.GetComponent<DatosTrabajo>().recursos[i],
                                       ZPlayerPrefs.GetFloat(primeraOpcion.GetComponent<DatosTrabajo>().recursos[i]) +
                                       primeraOpcion.GetComponent<DatosTrabajo>().cantidadRecursos[i]);
                                }
                            }
                            trabajando = false;
                            ZPlayerPrefs.SetString("Trabajando " + numbConstruccion, trabajando.ToString());
                            gameObject.transform.GetChild(0).gameObject.SetActive(false);
                            reescribirVariables();
                        }
                    }
                }
                else
                {
                    if (!ZPlayerPrefs.HasKey("terminadoConstruir" + numbConstruccion))
                    {
                        funcionar = true;
                        particulas.SetActive(true);
                        gameObject.transform.GetChild(3).gameObject.SetActive(false);
                        gameObject.transform.GetChild(5).gameObject.SetActive(true);
                        GameObject.Find("God").GetComponent<Exp_controller>().exp += this.exp;
                        ZPlayerPrefs.SetInt("terminadoConstruir" + numbConstruccion, 0);
                        GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion += trabajadoresNecesita;
                        ZPlayerPrefs.SetFloat("poblacion", GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion);
                        gameObject.GetComponent<SpriteRenderer>().enabled = true;
                        gameObject.transform.GetChild(1).gameObject.SetActive(false);
                        antecomer = true;
                        comer = true;
                        //this.enabled = false;
                    }
                }
            }
        }
    }
    public void Trabajar(int trabajo)
    {
        primeraOpcion = GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().primeraOpcion[trabajo];
        if (primeraOpcion.GetComponent<DatosTrabajo>().trabajadoresNecesita <= GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion)
        {
            casitas = GameObject.FindGameObjectsWithTag("casa");
            for (int casa = 0; casa <= casitas.Length - 1; casa++)
            {
                if (casitas[casa].GetComponent<ComidaCasa>().isComida)
                {
                    antecomer = true;
                    casitas[casa].GetComponent<ComidaCasa>().minaActual = gameObject;
                    trabajando = true;
                    ZPlayerPrefs.SetString("Trabajando " + numbConstruccion, trabajando.ToString());
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    tiempoActualTrabajo = DateTime.Now;
                    ZPlayerPrefs.SetString("TiempoTrabajo " + numbConstruccion, tiempoActualTrabajo.ToString());
                    ZPlayerPrefs.SetInt("tipoTrabajo" + numbConstruccion, trabajo);
                    ZPlayerPrefs.SetFloat("poblacion", GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion -
                        primeraOpcion.GetComponent<DatosTrabajo>().trabajadoresNecesita);
                    GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion -= primeraOpcion.GetComponent<DatosTrabajo>().trabajadoresNecesita;
                    tiempoDesconexionTrabajo = DateTime.Now;
                    tiempoFinalTrabajo = tiempoActualTrabajo.AddMinutes(primeraOpcion.GetComponent<DatosTrabajo>().tiempoTrabajo);
                    tiempoRestanteTrabajo = tiempoFinalTrabajo - tiempoDesconexionTrabajo;
                    primeraOpcion = GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().primeraOpcion[tipoTrabajo];
                    GameObject.Find("Main Camera").GetComponent<SmoothCamera2d>().enabled = true;
                    GameObject.Find("Main Camera").GetComponent<PinchZoom>().enabled = true;
                    break;
                }
            }
        }
    }
    public void reescribirVariables()
    {
        if (ZPlayerPrefs.GetFloat("madera") != GameObject.Find("Controller").GetComponent<GestionRecursos>().madera)
        {
            GameObject.Find("Controller").GetComponent<GestionRecursos>().madera = ZPlayerPrefs.GetFloat("madera");
        }
        if (ZPlayerPrefs.GetFloat("manzanas") != GameObject.Find("Controller").GetComponent<GestionRecursos>().manzanas)
        {
            GameObject.Find("Controller").GetComponent<GestionRecursos>().manzanas = ZPlayerPrefs.GetFloat("manzanas");
        }
        if (ZPlayerPrefs.GetFloat("piedra") != GameObject.Find("Controller").GetComponent<GestionRecursos>().piedra)
        {
            GameObject.Find("Controller").GetComponent<GestionRecursos>().piedra = ZPlayerPrefs.GetFloat("piedra");
        }
        if (ZPlayerPrefs.GetFloat("comida") != GameObject.Find("Controller").GetComponent<GestionRecursos>().comida)
        {
            GameObject.Find("Controller").GetComponent<GestionRecursos>().comida = ZPlayerPrefs.GetFloat("comida");
        }
        if (ZPlayerPrefs.GetFloat("semillasManzanas") != GameObject.Find("Controller").GetComponent<GestionRecursos>().semillasManzanas)
        {
            GameObject.Find("Controller").GetComponent<GestionRecursos>().semillasManzanas = ZPlayerPrefs.GetFloat("semillasManzanas");
        }
        if (ZPlayerPrefs.GetFloat("semillasFrambuesa") != GameObject.Find("Controller").GetComponent<GestionRecursos>().semillasFrambuesa)
        {
            GameObject.Find("Controller").GetComponent<GestionRecursos>().semillasFrambuesa = ZPlayerPrefs.GetFloat("semillasFrambuesa");
        }
        if (ZPlayerPrefs.GetFloat("frambuesas") != GameObject.Find("Controller").GetComponent<GestionRecursos>().frambuesas)
        {
            GameObject.Find("Controller").GetComponent<GestionRecursos>().frambuesas = ZPlayerPrefs.GetFloat("frambuesas");
        }
        if (ZPlayerPrefs.GetFloat("variscita") != GameObject.Find("Controller").GetComponent<GestionRecursos>().variscita)
        {
            GameObject.Find("Controller").GetComponent<GestionRecursos>().variscita = ZPlayerPrefs.GetFloat("variscita");
        }
        if (ZPlayerPrefs.GetFloat("carbon") != GameObject.Find("Controller").GetComponent<GestionRecursos>().carbon)
        {
            GameObject.Find("Controller").GetComponent<GestionRecursos>().carbon = ZPlayerPrefs.GetFloat("carbon");
        }
        if (ZPlayerPrefs.GetFloat("arcilla") != GameObject.Find("Controller").GetComponent<GestionRecursos>().arcilla)
        {
            GameObject.Find("Controller").GetComponent<GestionRecursos>().arcilla = ZPlayerPrefs.GetFloat("arcilla");
        }
        if (ZPlayerPrefs.GetFloat("jarronBasico") != GameObject.Find("Controller").GetComponent<GestionRecursos>().jarronBasico)
        {
            GameObject.Find("Controller").GetComponent<GestionRecursos>().jarronBasico = ZPlayerPrefs.GetFloat("jarronBasico");
        }
        if (ZPlayerPrefs.GetFloat("jarronDecorado") != GameObject.Find("Controller").GetComponent<GestionRecursos>().jarronDecorado)
        {
            GameObject.Find("Controller").GetComponent<GestionRecursos>().jarronDecorado = ZPlayerPrefs.GetFloat("jarronDecorado");
        }
        if (ZPlayerPrefs.GetFloat("diamantes") != GameObject.Find("Controller").GetComponent<GestionRecursos>().diamantes)
        {
            GameObject.Find("Controller").GetComponent<GestionRecursos>().diamantes = ZPlayerPrefs.GetFloat("diamantes");
        }
    }
    public void IniciarConstruccion()
    {
        casitas = GameObject.FindGameObjectsWithTag("casa");
        for (int casa = 0; casa <= casitas.Length - 1; casa++)
        {
            if (casitas[casa].GetComponent<ComidaCasa>().isComida)
            {
                casitas[casa].GetComponent<ComidaCasa>().minaActual = gameObject;
                StopAllCoroutines();
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                funcionar = false;
                startOn = true;
                gameObject.transform.GetChild(0).gameObject.SetActive(true); ;
                ZPlayerPrefs.SetInt("cantidadConstrucciones", numbConstruccion);
                ZPlayerPrefs.SetFloat("posX" + ZPlayerPrefs.GetInt("cantidadConstrucciones"), gameObject.transform.position.x);
                ZPlayerPrefs.SetFloat("posY" + ZPlayerPrefs.GetInt("cantidadConstrucciones"), gameObject.transform.position.y);
                ZPlayerPrefs.SetFloat("posZ" + ZPlayerPrefs.GetInt("cantidadConstrucciones"), gameObject.transform.position.z);
                ZPlayerPrefs.SetInt("tipoConstruccion" + numbConstruccion, tipoConstruccion);
                //numbConstruccion = ZPlayerPrefs.GetInt("cantidadConstrucciones");
                tiempoActual = DateTime.Now;
                ZPlayerPrefs.SetString("Tiempo " + numbConstruccion, tiempoActual.ToString());
                tiempoFinal = tiempoActual.AddMinutes(tiempoConstruccion);
                tiempoDesconexion = DateTime.Now;
                tiempoRestante = tiempoFinal - tiempoDesconexion;
                break;
            }
        }
    }
    public void EmpezarConstruir()
    {
        if (!trabajando && GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion > 0)
        {
            casitas = GameObject.FindGameObjectsWithTag("casa");
            for (int casa = 0; casa <= casitas.Length - 1; casa++)
            {
                if (casitas[casa].GetComponent<ComidaCasa>().isComida)
                {
                    GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion -= trabajadoresNecesita;
                    ZPlayerPrefs.SetFloat("poblacion", GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion);
                    casitas[casa].GetComponent<ComidaCasa>().minaActual = gameObject;
                }
            }
        }
    }
    IEnumerator Wait()
    {

        yield return new WaitForSecondsRealtime(0.2f);
        //ZPlayerPrefs.DeleteAll();
        if (!GameObject.Find("Controller").GetComponent<Construction>().modoConstruccion)
        {
            if (ZPlayerPrefs.HasKey("Trabajando " + numbConstruccion))
            {
                trabajando = bool.Parse(ZPlayerPrefs.GetString("Trabajando " + numbConstruccion));

            }
            else
            {
                trabajando = false;
            }
            if (!funcionar)
            {
                if (ZPlayerPrefs.HasKey("Tiempo " + numbConstruccion))
                {
                    tiempoActual = Convert.ToDateTime(ZPlayerPrefs.GetString("Tiempo " + numbConstruccion));
                    //numbConstruccion = ZPlayerPrefs.GetInt("cantidadConstrucciones");
                    tiempoFinal = tiempoActual.AddMinutes(tiempoConstruccion);
                    //tiempoFinal = tiempoActual.AddSeconds(10D);
                    tiempoDesconexion = DateTime.Now;
                    tiempoRestante = tiempoFinal - tiempoDesconexion;
                }
                if (tiempoDesconexion >= tiempoFinal)
                {
                    /* if (!ZPlayerPrefs.HasKey("terminadoConstruir" + numbConstruccion))
                     {
                         GameObject.Find("God").GetComponent<Exp_controller>().exp += this.exp;
                         ZPlayerPrefs.SetInt("terminadoConstruir" + numbConstruccion, 0);
                         GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion += trabajadoresNecesita;
                         ZPlayerPrefs.SetFloat("poblacion", GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion);
                     }*/
                    if (ZPlayerPrefs.HasKey("terminadoConstruir" + numbConstruccion))
                    {
                        gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        funcionar = true;
                    }
                    else if (!ZPlayerPrefs.HasKey("terminadoConstruir" + numbConstruccion))
                    {
                        gameObject.transform.GetChild(0).gameObject.SetActive(true);
                        gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        gameObject.transform.GetChild(1).gameObject.SetActive(true);
                        string aux = string.Format("00:00:00:00");
                        gameObject.GetComponentInChildren<TextMesh>().text = aux;
                    }
                    //this.enabled = false;
                    if (trabajando)
                    {
                        tiempoActualTrabajo = Convert.ToDateTime(ZPlayerPrefs.GetString("TiempoTrabajo " + numbConstruccion));
                        tiempoFinalTrabajo = tiempoActualTrabajo.AddMinutes(GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().
                        primeraOpcion[ZPlayerPrefs.GetInt("tipoTrabajo" + numbConstruccion)].GetComponent<DatosTrabajo>().tiempoTrabajo);
                        //tiempoFinal = tiempoActual.AddMinutes(5D);
                        tiempoDesconexionTrabajo = DateTime.Now;
                        tiempoRestanteTrabajo = tiempoFinalTrabajo - tiempoDesconexionTrabajo;
                    }
                }
            }
            startOn = true;
        }
    }
}

