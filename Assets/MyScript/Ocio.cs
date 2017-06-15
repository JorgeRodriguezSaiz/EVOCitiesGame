using UnityEngine;
using System.Collections;
using System;
    public class Ocio : MonoBehaviour
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
        private TimeSpan tiempoRestanteOcio;
        private DateTime tiempoActualOcio;
        private DateTime tiempoDesconexionOcio, tiempoFinalOcio;
        private bool funcionar = false, ociando = false, finOcio = false, startOn = false;
        [Header("Orete")]
        public float vAumentoOro = 0.1f;

        // Use this for initialization
        void Start()
        {
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
            //            if (startOn)
            //          {
            if (!GameObject.Find("Controller").GetComponent<Construction>().modoConstruccion)
            {
                if (tiempoDesconexion >= tiempoFinal)
                {
                    GameObject.Find("Controller").GetComponent<GestionRecursos>().gold +=
                        GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacionTotal
                        * vAumentoOro * Time.deltaTime;

                    if (!funcionar)
                    {
                        funcionar = true;
                        gameObject.transform.GetChild(0).gameObject.SetActive(false);
                        GameObject.Find("God").GetComponent<Exp_controller>().exp += this.exp;
                        //this.enabled = false;
                    }
                    else
                    {
                        if (ociando)
                        {
                            if (tiempoDesconexionOcio >= tiempoFinalOcio)
                            {
                                finOcio = true;
                                if (finOcio)
                                {
                                    finOcio = false;
                                    ZPlayerPrefs.SetInt(recurso, cantidadRecursos);
                                }
                                ociando = false;
                                gameObject.transform.GetChild(0).gameObject.SetActive(false);
                            }
                            else
                            {
                                if (ociando)
                                {
                                    tiempoDesconexionOcio = DateTime.Now;
                                    string aux = string.Format("{0:D2}:{1:D2}:{2:D2}:{3:D2}", tiempoRestanteOcio.Days, tiempoRestanteOcio.Hours,
                                        tiempoRestanteOcio.Minutes, tiempoRestanteOcio.Seconds);
                                    gameObject.GetComponentInChildren<TextMesh>().text = aux;
                                    float tAux = (float)tiempoRestanteOcio.TotalSeconds;
                                    tAux -= 1 * Time.deltaTime;
                                    tiempoRestanteOcio = TimeSpan.FromSeconds(tAux);
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
   
        /*private void OnMouseDown()
        {
            if (tiempoDesconexion >= tiempoFinal)
            {
                if (funcionar)
                {
                    if (!ociando)
                    {
                        Ociar();
                    }
                }
            }
        }
        public void Ociar()
        {
            if (!ociando)
            {
                if (trabajadoresNecesita < 2)
                {
                    ociando = true;
                    gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    tiempoActualOcio = DateTime.Now;
                    ZPlayerPrefs.SetString("TiempoTrabajo " + numbConstruccion, tiempoActualOcio.ToString());
                    tiempoDesconexionOcio = DateTime.Now;
                    tiempoFinalOcio = tiempoActualOcio.AddMinutes(tiempoTrabajo);
                    tiempoRestanteOcio = tiempoFinalOcio - tiempoDesconexionOcio;
                }
            }
        }*/
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
                //tiempoFinal = tiempoActual.AddSeconds(10D);
                tiempoDesconexion = DateTime.Now;
                tiempoRestante = tiempoFinal - tiempoDesconexion;
            }
        }
        IEnumerator Wait()
        {

            yield return new WaitForSecondsRealtime(0.2f);
            //ZPlayerPrefs.DeleteAll();
            if (ZPlayerPrefs.HasKey("Trabajando" + numbConstruccion))
            {
                ociando = bool.Parse(ZPlayerPrefs.GetString("Trabajando " + numbConstruccion));

            }
            else
            {
                ociando = false;
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
                    if (ociando)
                    {
                        if (ZPlayerPrefs.HasKey("TiempoTrabajo " + GetInstanceID()))
                        {
                            tiempoActualOcio = Convert.ToDateTime(ZPlayerPrefs.GetString("TiempoTrabajo " + numbConstruccion));
                        }
                        else
                        {
                            tiempoActualOcio = DateTime.Now;
                            ZPlayerPrefs.SetString("TiempoTrabajo " + numbConstruccion, tiempoActualOcio.ToString());
                            tiempoFinalOcio = tiempoActualOcio.AddMinutes(5D);
                        }
                        //tiempoFinal = tiempoActual.AddMinutes(5D);
                        tiempoDesconexionOcio = DateTime.Now;
                        tiempoRestanteOcio = tiempoFinalOcio - tiempoDesconexionOcio;
                    }
                }
            }
            startOn = true;
        }
    }

