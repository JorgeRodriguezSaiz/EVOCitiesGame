using UnityEngine;
using System.Collections;
using System;

namespace Assets.UltimateIsometricToolkit.Scripts.Core
{
    public class Trabajos : MonoBehaviour
    {
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
        private bool funcionar = false, trabajando = false, finTrabajo = false;

        // Use this for initialization
        void Start()
        {
            //ZPlayerPrefs.DeleteAll();
            if (ZPlayerPrefs.HasKey("Trabajando" + GetInstanceID()))
            {
                trabajando = bool.Parse(ZPlayerPrefs.GetString("Trabajando " + GetInstanceID()));
            }
            else
            {
                trabajando = false;
            }
            if (!funcionar)
            {
                if (ZPlayerPrefs.HasKey("Tiempo " + GetInstanceID()))
                {
                    tiempoActual = Convert.ToDateTime(ZPlayerPrefs.GetString("Tiempo " + GetInstanceID()));
                }
                else
                {
                    tiempoActual = DateTime.Now;
                    ZPlayerPrefs.SetString("Tiempo " + GetInstanceID(), tiempoActual.ToString());
                }
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
                    if (ZPlayerPrefs.HasKey("TiempoTrabajo " + GetInstanceID()))
                    {
                        tiempoActualTrabajo = Convert.ToDateTime(ZPlayerPrefs.GetString("TiempoTrabajo " + GetInstanceID()));
                    }
                    else
                    {
                        tiempoActualTrabajo = DateTime.Now;
                        ZPlayerPrefs.SetString("TiempoTrabajo " + GetInstanceID(), tiempoActualTrabajo.ToString());
                        tiempoFinalTrabajo = tiempoActualTrabajo.AddMinutes(5D);
                    }
                    //tiempoFinal = tiempoActual.AddMinutes(5D);
                    tiempoDesconexionTrabajo = DateTime.Now;
                    tiempoRestanteTrabajo = tiempoFinalTrabajo - tiempoDesconexionTrabajo;
                }
            }

        }

        // Update is called once per frame
        void Update()
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
                            if (tiempoDesconexionTrabajo >= tiempoFinalTrabajo)
                            {
                                finTrabajo = true;
                                if (finTrabajo)
                                {
                                    finTrabajo = false;
                                    ZPlayerPrefs.SetInt(recurso, cantidadRecursos);
                                }
                                trabajando = false;
                                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                            }
                            else
                            {
                                if (trabajando)
                                {
                                    tiempoDesconexionTrabajo = DateTime.Now;
                                    string aux = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}", tiempoRestanteTrabajo.Days, tiempoRestanteTrabajo.Hours,
                                        tiempoRestanteTrabajo.Minutes, tiempoRestanteTrabajo.Seconds);
                                    gameObject.GetComponentInChildren<TextMesh>().text = aux;
                                    float tAux = (float)tiempoRestanteTrabajo.TotalSeconds;
                                    tAux -= 1 * Time.deltaTime;
                                    tiempoRestanteTrabajo = TimeSpan.FromSeconds(tAux);
                                }
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
        private void OnMouseDown()
        {
            if (tiempoDesconexion >= tiempoFinal)
            {
                if (funcionar)
                {
                    if (!trabajando)
                    {
                        Trabajar();
                    }
                }
            }
        }
        public void Trabajar()
        {
            if (!trabajando)
            {
                if (trabajadoresNecesita < 2)
                {
                    trabajando = true;
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    tiempoActualTrabajo = DateTime.Now;
                    ZPlayerPrefs.SetString("TiempoTrabajo " + GetInstanceID(), tiempoActualTrabajo.ToString());
                    tiempoDesconexionTrabajo = DateTime.Now;
                    tiempoFinalTrabajo = tiempoActualTrabajo.AddMinutes(tiempoTrabajo);
                    tiempoRestanteTrabajo = tiempoFinalTrabajo - tiempoDesconexionTrabajo;
                }
            }
        }
    }
}
