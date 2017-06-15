using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
    public class TimeController : MonoBehaviour
    {
        public DateTime tiempoActual;
        public DateTime tiempoDesconexion, tiempoFinal;
        private bool funcionar;

        private void Start()
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
            tiempoFinal = tiempoActual.AddHours(5D);
            tiempoDesconexion = DateTime.Now;
            if (tiempoDesconexion >= tiempoFinal)
            {
                funcionar = true;
            }
        }

        private void Update()
        {
            tiempoDesconexion = DateTime.Now;
            if (tiempoDesconexion >= tiempoFinal)
            {
                funcionar = true;
            }
        }
    }

