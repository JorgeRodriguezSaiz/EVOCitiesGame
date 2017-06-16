using UnityEngine;
using System.Collections;
using System;
public class Casa : MonoBehaviour
{
    [Header("Experiencia")]
    public float exp = 75f;
    public float poblacionCasa = 2;
    public int tipoConstruccion = 0;
    public int numbConstruccion = 0;
    public TimeSpan tiempoRestante;
    public DateTime tiempoActual;
    public DateTime tiempoDesconexion, tiempoFinal;
    public bool funcionar = false, startOn = false;
    public double tiempoConstruccion;
    // Use this for initialization
    void Start()
    {
        //ZPlayerPrefs.DeleteAll();
        StartCoroutine(Wait());

    }

    // Update is called once per frame
    void Update()
    {
        if (startOn)
        {
            tiempoDesconexion = DateTime.Now;
            if (!GameObject.Find("Controller").GetComponent<Construction>().modoConstruccion)
            {
                if (tiempoDesconexion >= tiempoFinal)
                {
                    if (!PlayerPrefs.HasKey("terminadoConstruir" + numbConstruccion))
                    {
                        ZPlayerPrefs.SetInt("terminadoConstruir" + numbConstruccion, 0);
                        funcionar = true;
                        gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        GameObject.Find("God").GetComponent<Exp_controller>().exp += this.exp;
                        GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacionTotal += poblacionCasa;
                        GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion += poblacionCasa;
                        ZPlayerPrefs.SetFloat("poblacionTotal", GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacionTotal);
                        ZPlayerPrefs.SetFloat("poblacion", GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion);
                        this.enabled = false;
                    }
                    

                }
                if (!funcionar)
                {
                    string aux = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}", tiempoRestante.Days, tiempoRestante.Hours, tiempoRestante.Minutes, tiempoRestante.Seconds);
                    gameObject.GetComponentInChildren<TextMesh>().text = aux;
                    float tAux = (float)tiempoRestante.TotalSeconds;
                    tAux -= 1 * Time.deltaTime;
                    tiempoRestante = TimeSpan.FromSeconds(tAux);
                }
            }
        }
    }
    public void IniciarConstruccion()
    {
        StopAllCoroutines();
        funcionar = false;

        gameObject.transform.GetChild(0).gameObject.SetActive(true);
        ZPlayerPrefs.SetInt("cantidadConstrucciones", ZPlayerPrefs.GetInt("cantidadConstrucciones", -1) + 1);
        ZPlayerPrefs.SetFloat("posX" + ZPlayerPrefs.GetInt("cantidadConstrucciones"), gameObject.transform.position.x);
        ZPlayerPrefs.SetFloat("posY" + ZPlayerPrefs.GetInt("cantidadConstrucciones"), gameObject.transform.position.y);
        ZPlayerPrefs.SetFloat("posZ" + ZPlayerPrefs.GetInt("cantidadConstrucciones"), gameObject.transform.position.z);
        ZPlayerPrefs.SetInt("tipoConstruccion" + ZPlayerPrefs.GetInt("cantidadConstrucciones"), tipoConstruccion);
        tiempoActual = DateTime.Now;
        ZPlayerPrefs.SetString("Tiempo" + numbConstruccion, tiempoActual.ToString());
        tiempoFinal = tiempoActual.AddMinutes(tiempoConstruccion);
        //tiempoFinal = tiempoActual.AddSeconds(20D);
        tiempoDesconexion = DateTime.Now;
        tiempoRestante = tiempoFinal - tiempoDesconexion;
        startOn = true;
    }
    IEnumerator Wait()
    {

        yield return new WaitForSecondsRealtime(0.2f);
        //ZPlayerPrefs.DeleteAll();
        if (!GameObject.Find("Controller").GetComponent<Construction>().modoConstruccion)
        {
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
                    if(PlayerPrefs.HasKey("terminadoConstruir" + numbConstruccion))
                        this.enabled = false;
                    if (!ZPlayerPrefs.HasKey("terminadoConstruir" + numbConstruccion))
                    {
                        gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        funcionar = true;
                        gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        GameObject.Find("God").GetComponent<Exp_controller>().exp += this.exp;
                        GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacionTotal += poblacionCasa;
                        GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion += poblacionCasa;
                        ZPlayerPrefs.SetFloat("poblacionTotal", GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacionTotal);
                        ZPlayerPrefs.SetFloat("poblacion", GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion);
                    }
                    this.enabled = false;
                    ZPlayerPrefs.SetInt("terminadoConstruir" + numbConstruccion, 0);

                }
            }
            startOn = true;
        }
    }
}

