using UnityEngine;
using System.Collections;
using System;

public class Trabajos : MonoBehaviour
{
    [Header("Guardado")]
    public int tipoConstruccion = 0;
    public int numbConstruccion = 0;
    [Header("Construccion")]
    public float exp = 75f;
    public double tiempoConstruccion;
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
    private bool funcionar = false, finTrabajo = false, startOn = false;
    public bool trabajando = false;
    [Header("OpcionesTrabajo")]
    public GameObject interfaz;
    public GameObject primeraOpcion;
    public int tipoTrabajo;
    [Header("CasaPrefab")]
    public GameObject casaPrefab;
    private GameObject[] casitas;




    // Use this for initialization
    void Start()
    {
        interfaz = GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().interfaz;
        primeraOpcion = GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().primeraOpcion[tipoTrabajo];
        trabajando = false;
        if (numbConstruccion <= ZPlayerPrefs.GetInt("cantidadConstrucciones"))
        {
            gameObject.transform.position = new Vector3(ZPlayerPrefs.GetFloat("posX" + numbConstruccion), ZPlayerPrefs.GetFloat("posY" + numbConstruccion),
                ZPlayerPrefs.GetFloat("posZ" + numbConstruccion));
        }
        StartCoroutine(Wait());

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion > trabajadoresNecesita)
        {

            if (startOn)
            {
                if (!GameObject.Find("Controller").GetComponent<Construction>().modoConstruccion)
                {
                    if (tiempoDesconexion >= tiempoFinal)
                    {
                        if (!funcionar)
                        {
                            funcionar = true;
                            gameObject.transform.GetChild(0).gameObject.SetActive(false);
                            GameObject.Find("God").GetComponent<Exp_controller>().exp += this.exp;
                            //this.enabled = false;
                        }
                        else
                        {
                            if (trabajando)
                            {
                                if (tiempoDesconexionTrabajo < tiempoFinalTrabajo)
                                {
                                    if (trabajando)
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
                                }
                                else
                                {
                                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                                    string aux = string.Format("00:00:00:00");
                                    gameObject.GetComponentInChildren<TextMesh>().text = aux;
                                }
                            }
                        }
                    }
                    else if (!funcionar)
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
    }
    private void OnMouseDown()
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
                    if (tiempoDesconexionTrabajo >= tiempoFinalTrabajo)
                    {
                        finTrabajo = true;
                        if (finTrabajo)
                        {
                            primeraOpcion = GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().
                                primeraOpcion[ZPlayerPrefs.GetInt("tipoTrabajo" + numbConstruccion)];
                            GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion += trabajadoresNecesita;
                            ZPlayerPrefs.SetFloat("poblacion", GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion);
                            finTrabajo = false;
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
                    }
                }
            }
        }
    }
    public void Trabajar(int trabajo)
    {
        primeraOpcion = GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().primeraOpcion[trabajo];
        if (primeraOpcion.GetComponent<DatosTrabajo>().trabajadoresNecesita < GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion)
        {
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
        }
    }
    public void IniciarConstruccion()
    {
        StopAllCoroutines();
        funcionar = false;
        gameObject.transform.GetChild(0).gameObject.SetActive(true); ;
        ZPlayerPrefs.SetInt("cantidadConstrucciones", ZPlayerPrefs.GetInt("cantidadConstrucciones") + 1);
        ZPlayerPrefs.SetFloat("posX" + ZPlayerPrefs.GetInt("cantidadConstrucciones"), gameObject.transform.position.x);
        ZPlayerPrefs.SetFloat("posY" + ZPlayerPrefs.GetInt("cantidadConstrucciones"), gameObject.transform.position.y);
        ZPlayerPrefs.SetFloat("posZ" + ZPlayerPrefs.GetInt("cantidadConstrucciones"), gameObject.transform.position.z);
        ZPlayerPrefs.SetInt("tipoConstruccion" + ZPlayerPrefs.GetInt("cantidadConstrucciones"), tipoConstruccion);
        numbConstruccion = ZPlayerPrefs.GetInt("cantidadConstrucciones");
        if (!funcionar)
        {
            tiempoActual = DateTime.Now;
            ZPlayerPrefs.SetString("Tiempo " + numbConstruccion, tiempoActual.ToString());
            tiempoFinal = tiempoActual.AddMinutes(tiempoConstruccion);
            tiempoDesconexion = DateTime.Now;
            tiempoRestante = tiempoFinal - tiempoDesconexion;
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
                    //Trabajar();
                }
            }
        }
    }
    IEnumerator Wait()
    {

        yield return new WaitForSecondsRealtime(0.2f);
        //ZPlayerPrefs.DeleteAll();
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
                numbConstruccion = ZPlayerPrefs.GetInt("cantidadConstrucciones");
                tiempoFinal = tiempoActual.AddMinutes(tiempoConstruccion);
                //tiempoFinal = tiempoActual.AddSeconds(10D);
                tiempoDesconexion = DateTime.Now;
                tiempoRestante = tiempoFinal - tiempoDesconexion;
            }
            if (tiempoDesconexion >= tiempoFinal)
            {
                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                funcionar = true;
                //this.enabled = false;
                if (trabajando)
                {
                    if (ZPlayerPrefs.HasKey("TiempoTrabajo " + numbConstruccion))
                    {
                        tiempoActualTrabajo = Convert.ToDateTime(ZPlayerPrefs.GetString("TiempoTrabajo " + numbConstruccion));
                        tiempoFinalTrabajo = tiempoActualTrabajo.AddMinutes(GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().
                            primeraOpcion[ZPlayerPrefs.GetInt("tipoTrabajo" + numbConstruccion)].GetComponent<DatosTrabajo>().tiempoTrabajo);
                    }
                    //tiempoFinal = tiempoActual.AddMinutes(5D);
                    tiempoDesconexionTrabajo = DateTime.Now;
                    tiempoRestanteTrabajo = tiempoFinalTrabajo - tiempoDesconexionTrabajo;
                }
            }
        }
        startOn = true;
    }
}

