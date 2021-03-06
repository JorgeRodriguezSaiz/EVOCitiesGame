﻿using UnityEngine;
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
    public GameObject particulas;
    [HideInInspector]
    public GameObject[] casitas;
    public bool comer = false, antecomer = false;
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
            /*if (tiempoDesconexion >= tiempoFinal)
            {
                /*if (!ZPlayerPrefs.HasKey("terminadoConstruir" + numbConstruccion))
                {
                    ZPlayerPrefs.SetInt("terminadoConstruir" + numbConstruccion,0);
                    funcionar = true;
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    GameObject.Find("God").GetComponent<Exp_controller>().exp += this.exp;
                    GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacionTotal += poblacionCasa;
                    GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion += poblacionCasa;
                    ZPlayerPrefs.SetFloat("poblacionTotal", GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacionTotal);
                    ZPlayerPrefs.SetFloat("poblacion", GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion);
                    Debug.Log(PlayerPrefs.HasKey("terminadoConstruir" + numbConstruccion));
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    //this.enabled = false;
                }


            }*/
            if (!funcionar)
            {

                if (tiempoDesconexion < tiempoFinal)
                {
                    string aux = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}", tiempoRestante.Days, tiempoRestante.Hours, tiempoRestante.Minutes, tiempoRestante.Seconds);
                    gameObject.GetComponentInChildren<TextMesh>().text = aux;
                    float tAux = (float)tiempoRestante.TotalSeconds;
                    tAux -= 1 * Time.deltaTime;
                    tiempoRestante = TimeSpan.FromSeconds(tAux);
                }
                else
                {
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    gameObject.transform.GetChild(5).gameObject.SetActive(true);
                }
            }
        }
    }
    private void OnMouseUp()
    {
        if (tiempoDesconexion >= tiempoFinal)
        {
            if (!GameObject.Find("Controller").GetComponent<Construction>().modoConstruccion)
            {
                if (!ZPlayerPrefs.HasKey("terminadoConstruir" + numbConstruccion))
                {
                    ZPlayerPrefs.SetInt("terminadoConstruir" + numbConstruccion, 0);
                    funcionar = true;
                    particulas.SetActive(true);
                    gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    GameObject.Find("God").GetComponent<Exp_controller>().exp += this.exp;
                    GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacionTotal += poblacionCasa;
                    GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion += poblacionCasa;
                    ZPlayerPrefs.SetFloat("poblacionTotal", GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacionTotal);
                    ZPlayerPrefs.SetFloat("poblacion", GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion);
                    GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion += gameObject.GetComponent<Recursos>().trabajadoresNecesita;
                    ZPlayerPrefs.SetFloat("poblacion", GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion);
                    gameObject.GetComponent<SpriteRenderer>().enabled = true;
                    gameObject.transform.GetChild(1).gameObject.SetActive(false);
                    antecomer = true;
                    comer = true;
                    gameObject.transform.GetChild(5).gameObject.SetActive(false);
                    gameObject.transform.GetChild(6).gameObject.SetActive(true);
                    //this.enabled = false;
                }
            }
        }
    }
    public void IniciarConstruccion()
    {
        casitas = GameObject.FindGameObjectsWithTag("casa");
        for (int casa = 0; casa <= casitas.Length - 1; casa++)
        {
            if (casitas[casa].GetComponent<ComidaCasa>().isComida)
            {
                StopAllCoroutines();
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                funcionar = false;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                ZPlayerPrefs.SetInt("cantidadConstrucciones", numbConstruccion);
                ZPlayerPrefs.SetFloat("posX" + ZPlayerPrefs.GetInt("cantidadConstrucciones"), gameObject.transform.position.x);
                ZPlayerPrefs.SetFloat("posY" + ZPlayerPrefs.GetInt("cantidadConstrucciones"), gameObject.transform.position.y);
                ZPlayerPrefs.SetFloat("posZ" + ZPlayerPrefs.GetInt("cantidadConstrucciones"), gameObject.transform.position.z);
                ZPlayerPrefs.SetInt("tipoConstruccion" + numbConstruccion, tipoConstruccion);
                ZPlayerPrefs.SetFloat("poblacion", GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion - gameObject.GetComponent<Recursos>().trabajadoresNecesita);
                //GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion -= gameObject.GetComponent<Recursos>().trabajadoresNecesita;
                tiempoActual = DateTime.Now;
                ZPlayerPrefs.SetString("Tiempo" + numbConstruccion, tiempoActual.ToString());
                tiempoFinal = tiempoActual.AddMinutes(tiempoConstruccion);
                //tiempoFinal = tiempoActual.AddSeconds(20D);
                tiempoDesconexion = DateTime.Now;
                tiempoRestante = tiempoFinal - tiempoDesconexion;
                startOn = true;
                break;
            }
        }
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
                    //numbConstruccion = ZPlayerPrefs.GetInt("cantidadConstrucciones");
                    tiempoFinal = tiempoActual.AddMinutes(tiempoConstruccion);
                    //tiempoFinal = tiempoActual.AddSeconds(10D);
                    tiempoDesconexion = DateTime.Now;
                    tiempoRestante = tiempoFinal - tiempoDesconexion;
                }
                if (tiempoDesconexion >= tiempoFinal)
                {
                    if (ZPlayerPrefs.HasKey("terminadoConstruir" + numbConstruccion))
                        this.enabled = false;
                    else if (!ZPlayerPrefs.HasKey("terminadoConstruir" + numbConstruccion))
                    {
                        gameObject.transform.GetChild(0).gameObject.SetActive(true);
                        gameObject.GetComponent<SpriteRenderer>().enabled = false;
                        gameObject.transform.GetChild(1).gameObject.SetActive(true);
                        string aux = string.Format("00:00:00:00");
                        gameObject.GetComponentInChildren<TextMesh>().text = aux;
                        /* funcionar = true;
                         gameObject.transform.GetChild(0).gameObject.SetActive(false);
                         GameObject.Find("God").GetComponent<Exp_controller>().exp += this.exp;
                         GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacionTotal += poblacionCasa;
                         GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion += poblacionCasa;
                         ZPlayerPrefs.SetFloat("poblacionTotal", GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacionTotal);
                         ZPlayerPrefs.SetFloat("poblacion", GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion);
                         ZPlayerPrefs.SetInt("terminadoConstruir" + numbConstruccion, 0);
                         Debug.Log(PlayerPrefs.HasKey("terminadoConstruir" + numbConstruccion));
                         this.enabled = false;*/
                    }
                }
            }
            startOn = true;
        }
    }
}

