using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

namespace Assets.UltimateIsometricToolkit.Scripts.Core
{
    public class Construction : MonoBehaviour
    {
        public GameObject prefab;
        public GameObject instancia;
        public GameObject botonAceptar;
        public GameObject botonCancelar;
        public GameObject panelJugar;
        public GameObject cuadrado;
        public float speed = 100f;
        public Vector3 sizeCasilla;
        public Vector3 mousePos = new Vector3();
        public Vector3 mousePosCanvas = new Vector3();
        private Vector3 targetPosition;
        public Vector3 auxAceptar = new Vector3(5,5,0);
        public Vector3 auxCancelar = new Vector3(5, 5, 0);
        public bool modoConstruccion = false;
        public bool disponible = true;
        public float tiempoParaMover = 1.5f;
        private float timeCounter = 0f;
        // Use this for initialization
        void Start()
        {
            disponible = true;
        }

        // Update is called once per frame
        void Update()
        {
            sizeCasilla = cuadrado.GetComponent<SpriteRenderer>().bounds.size;
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = Camera.main.nearClipPlane;
            if (instancia != null)
            {
                
                
                    modoConstruccion = true;
                    botonAceptar.SetActive(true);
                    botonCancelar.SetActive(true);
                    panelJugar.SetActive(false);
                
            }
            if (modoConstruccion)
            {
                disponible = instancia.GetComponent<comprobadorDisponible>()._disponible;
                if (instancia.transform.position.y > mousePos.y && Math.Abs(instancia.transform.position.y - mousePos.y) > 0.25 * sizeCasilla.y)
                {
                    instancia.transform.position = new Vector3(instancia.transform.position.x, instancia.transform.position.y - (float)0.25 * sizeCasilla.y, instancia.transform.position.z);
                    botonAceptar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxAceptar.x, instancia.transform.position.y + auxAceptar.y, instancia.transform.position.z));
                    botonCancelar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxCancelar.x, instancia.transform.position.y + auxCancelar.y, instancia.transform.position.z));
                }
                if (instancia.transform.position.y < mousePos.y && Math.Abs(instancia.transform.position.y - mousePos.y) > 0.25 * sizeCasilla.y)
                {
                    instancia.transform.position = new Vector3(instancia.transform.position.x, (float)0.25 * sizeCasilla.y + instancia.transform.position.y, instancia.transform.position.z);
                    botonAceptar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxAceptar.x, instancia.transform.position.y + auxAceptar.y, instancia.transform.position.z));
                    botonCancelar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxCancelar.x, instancia.transform.position.y + auxCancelar.y, instancia.transform.position.z));
                }
                if (instancia.transform.position.x > mousePos.x && Math.Abs(instancia.transform.position.x - mousePos.x) > 0.25 * sizeCasilla.x)
                {
                    instancia.transform.position = new Vector3(instancia.transform.position.x - (float)0.25 * sizeCasilla.x, instancia.transform.position.y, instancia.transform.position.z);
                    botonAceptar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxAceptar.x, instancia.transform.position.y + auxAceptar.y, instancia.transform.position.z));
                    botonCancelar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxCancelar.x, instancia.transform.position.y + auxCancelar.y, instancia.transform.position.z));
                }
                if (instancia.transform.position.x < mousePos.x && Math.Abs(instancia.transform.position.x - mousePos.x) > 0.25 * sizeCasilla.x)
                {
                    instancia.transform.position = new Vector3((float)0.25 * sizeCasilla.x + instancia.transform.position.x, instancia.transform.position.y, instancia.transform.position.z);
                    botonAceptar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxAceptar.x, instancia.transform.position.y + auxAceptar.y, instancia.transform.position.z));
                    botonCancelar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxCancelar.x, instancia.transform.position.y + auxCancelar.y, instancia.transform.position.z));
                }
                /*if(instancia.transform.position.y != mousePos.y && Math.Abs(instancia.transform.position.y - mousePos.y) > 0.25*sizeCasilla.y)
                {
                    instancia.transform.position = Vector3.MoveTowards(instancia.transform.position, mousePos, speed * Time.deltaTime);
                    botonAceptar.transform.position = Vector3.MoveTowards(botonAceptar.transform.position, Input.mousePosition + auxAceptar, speed*100000000 * Time.deltaTime);
                    botonCancelar.transform.position = Vector3.MoveTowards(botonCancelar.transform.position, Input.mousePosition + auxCancelar, speed * 100000000 * Time.deltaTime);
                }
                if (instancia.transform.position.x != mousePos.x && Math.Abs(instancia.transform.position.x - mousePos.x) > 0.25*sizeCasilla.x)
                {
                    instancia.transform.position = Vector3.MoveTowards(instancia.transform.position, mousePos, speed * Time.deltaTime);
                    botonAceptar.transform.position = Vector3.MoveTowards(botonAceptar.transform.position, Input.mousePosition + auxAceptar, speed*100000 * Time.deltaTime);
                    botonCancelar.transform.position = Vector3.MoveTowards(botonCancelar.transform.position, Input.mousePosition + auxCancelar, speed * 100000000 * Time.deltaTime);
                }*/
            }
        }
        public void OnClickBuilding(GameObject prefabPasar)
        {
            prefab = prefabPasar;
            
            if (GameObject.Find("Controller").GetComponent<GestionRecursos>().madera >= prefab.GetComponent<Recursos>().maderaNecesaria 
                && GameObject.Find("Controller").GetComponent<GestionRecursos>().piedra >= prefab.GetComponent<Recursos>().piedraNecesaria
                && GameObject.Find("Controller").GetComponent<GestionRecursos>().gold >= prefab.GetComponent<Recursos>().goldNecesaria
                && GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion >= prefab.GetComponent<Recursos>().trabajadoresNecesita)
            {
                GameObject.Find("Controller").GetComponent<GestionRecursos>().madera -= prefab.GetComponent<Recursos>().maderaNecesaria;
                ZPlayerPrefs.SetFloat("madera", GameObject.Find("Controller").GetComponent<GestionRecursos>().madera);
                GameObject.Find("Controller").GetComponent<GestionRecursos>().piedra -= prefab.GetComponent<Recursos>().piedraNecesaria;
                ZPlayerPrefs.SetFloat("piedra", GameObject.Find("Controller").GetComponent<GestionRecursos>().piedra);
                GameObject.Find("Controller").GetComponent<GestionRecursos>().gold -= prefab.GetComponent<Recursos>().goldNecesaria;
                ZPlayerPrefs.SetFloat("gold", GameObject.Find("Controller").GetComponent<GestionRecursos>().gold);
                GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion -= prefab.GetComponent<Recursos>().trabajadoresNecesita;
                ZPlayerPrefs.SetFloat("poblacion", GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion);
                instancia = (GameObject)Instantiate(prefab, mousePos, Quaternion.identity);
                instancia.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
               botonAceptar.transform.position = Vector3.MoveTowards(botonAceptar.transform.position, Input.mousePosition + auxAceptar, 
                    speed * 100000000 * Time.deltaTime);
                botonCancelar.transform.position = Vector3.MoveTowards(botonCancelar.transform.position, Input.mousePosition + auxCancelar, 
                    speed * 100000000 * Time.deltaTime);
                modoConstruccion = true;
                botonAceptar.SetActive(true);
                botonCancelar.SetActive(true);
            }
        }
        public void OnClickAceptar()
        {
            if (disponible)
            {
                if (instancia.tag == "trabajo")
                {
                    instancia.GetComponent<Trabajos>().IniciarConstruccion();
                    instancia.GetComponent<Trabajos>().numbConstruccion = ZPlayerPrefs.GetInt("cantidadConstrucciones") + 1;
                }
                else if (instancia.tag == "casa")
                {
                    instancia.GetComponent<Casa>().IniciarConstruccion();
                    instancia.GetComponent<Casa>().numbConstruccion = ZPlayerPrefs.GetInt("cantidadConstrucciones") + 1;
                }
                else if (instancia.tag == "ocio")
                {
                    instancia.GetComponent<Ocio>().IniciarConstruccion();
                    instancia.GetComponent<Ocio>().numbConstruccion = ZPlayerPrefs.GetInt("cantidadConstrucciones") + 1;
                }

                modoConstruccion = false;
                instancia.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
                instancia = null;
            }
            else
            {
                Destroy(instancia);
                modoConstruccion = false;
            }
        }
        public void OnClickCancelar()
        {
            Destroy(instancia);
            modoConstruccion = false;
        }
    }
}
