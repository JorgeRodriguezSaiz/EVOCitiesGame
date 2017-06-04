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
        public Vector3 mousePos = new Vector3();
        public Vector3 mousePosCanvas = new Vector3();
        public bool modoConstruccion = false;
        public bool disponible = true;
        public float madera = 0;
        public float piedra = 0;
        public float poblacion = 1f;
        public float poblacionTotal = 10f;
        public Text textoPoblacion;
        public Text textoPoblacionTotal;
        // Use this for initialization
        void Start()
        {
            disponible = true;
        }

        // Update is called once per frame
        void Update()
        {
            textoPoblacion.text = Convert.ToString(poblacion) + "/";
            textoPoblacionTotal.text = Convert.ToString(poblacionTotal);
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.x = Mathf.Round(mousePos.x);
            mousePos.y = Mathf.Round(mousePos.y);
            mousePos.z = Camera.main.nearClipPlane;
            if (modoConstruccion)
            {
                disponible = instancia.GetComponent<comprobadorDisponible>()._disponible;
                instancia.transform.position = Vector3.MoveTowards(instancia.transform.position, mousePos, 100f * Time.deltaTime);
                botonAceptar.transform.position = Vector3.MoveTowards(botonAceptar.transform.position, Input.mousePosition, 10000f * Time.deltaTime);
            }

        }
        public void OnClickBuilding(GameObject prefabPasar)
        {
            prefab = prefabPasar;
            //if (madera >= prefab.GetComponent<Recursos>().maderaNecesaria && piedra >= prefab.GetComponent<Recursos>().piedraNecesaria)
            //{
            //madera -= prefab.GetComponent<Recursos>().maderaNecesaria;
            // piedra -= prefab.GetComponent<Recursos>().piedraNecesaria;
            instancia = (GameObject)Instantiate(prefab, mousePos, Quaternion.identity);
            instancia.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
            modoConstruccion = true;
            botonAceptar.SetActive(true);
            //}
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
