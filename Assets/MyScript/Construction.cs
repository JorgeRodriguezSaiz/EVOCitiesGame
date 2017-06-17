using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Construction : MonoBehaviour
{
    public GameObject prefab;
    public GameObject instancia;
    public GameObject botonAceptar;
    public GameObject botonCancelar;
    public GameObject botonAceptarMover;
    public GameObject botonCancelarMover;
    public GameObject panelJugar;
    public GameObject cuadrado;
    public float speed = 100f;
    public Vector3 sizeCasilla;
    public Vector3 mousePos = new Vector3();
    public Vector3 mousePosCanvas = new Vector3();
    public Vector3 auxAceptar = new Vector3(5, 5, 0);
    public Vector3 auxCancelar = new Vector3(5, 5, 0);
    public bool modoConstruccion = false;
    public bool disponible = true;
    public float tiempoParaMover = 1.5f;
    public bool mover = false;
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
            if (mover)
            {
                modoConstruccion = true;
                panelJugar.SetActive(false);
                botonAceptarMover.SetActive(true);
                botonCancelarMover.SetActive(true);
            }
            if (!mover)
            {
                modoConstruccion = true;
                botonAceptar.SetActive(true);
                botonCancelar.SetActive(true);
                panelJugar.SetActive(false);
            }
            
        }
        if (modoConstruccion)
        {
            disponible = instancia.GetComponent<comprobadorDisponible>()._disponible;
            if (instancia.transform.position.y > mousePos.y && Math.Abs(instancia.transform.position.y - mousePos.y) > 0.25 * sizeCasilla.y)
            {
                instancia.transform.position = new Vector3(instancia.transform.position.x, instancia.transform.position.y - (float)0.25 * sizeCasilla.y, instancia.transform.position.z);
                botonAceptar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxAceptar.x, instancia.transform.position.y + auxAceptar.y, instancia.transform.position.z));
                botonCancelar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxCancelar.x, instancia.transform.position.y + auxCancelar.y, instancia.transform.position.z));
                botonAceptarMover.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxAceptar.x, instancia.transform.position.y + auxAceptar.y, instancia.transform.position.z));
                botonCancelarMover.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxCancelar.x, instancia.transform.position.y + auxCancelar.y, instancia.transform.position.z));
            }
            if (instancia.transform.position.y < mousePos.y && Math.Abs(instancia.transform.position.y - mousePos.y) > 0.25 * sizeCasilla.y)
            {
                instancia.transform.position = new Vector3(instancia.transform.position.x, (float)0.25 * sizeCasilla.y + instancia.transform.position.y, instancia.transform.position.z);
                botonAceptar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxAceptar.x, instancia.transform.position.y + auxAceptar.y, instancia.transform.position.z));
                botonCancelar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxCancelar.x, instancia.transform.position.y + auxCancelar.y, instancia.transform.position.z));
                botonAceptarMover.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxAceptar.x, instancia.transform.position.y + auxAceptar.y, instancia.transform.position.z));
                botonCancelarMover.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxCancelar.x, instancia.transform.position.y + auxCancelar.y, instancia.transform.position.z));
            }
            if (instancia.transform.position.x > mousePos.x && Math.Abs(instancia.transform.position.x - mousePos.x) > 0.25 * sizeCasilla.x)
            {
                instancia.transform.position = new Vector3(instancia.transform.position.x - (float)0.25 * sizeCasilla.x, instancia.transform.position.y, instancia.transform.position.z);
                botonAceptar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxAceptar.x, instancia.transform.position.y + auxAceptar.y, instancia.transform.position.z));
                botonCancelar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxCancelar.x, instancia.transform.position.y + auxCancelar.y, instancia.transform.position.z));
                botonAceptarMover.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxAceptar.x, instancia.transform.position.y + auxAceptar.y, instancia.transform.position.z));
                botonCancelarMover.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxCancelar.x, instancia.transform.position.y + auxCancelar.y, instancia.transform.position.z));
            }
            if (instancia.transform.position.x < mousePos.x && Math.Abs(instancia.transform.position.x - mousePos.x) > 0.25 * sizeCasilla.x)
            {
                instancia.transform.position = new Vector3((float)0.25 * sizeCasilla.x + instancia.transform.position.x, instancia.transform.position.y, instancia.transform.position.z);
                botonAceptar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxAceptar.x, instancia.transform.position.y + auxAceptar.y, instancia.transform.position.z));
                botonCancelar.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxCancelar.x, instancia.transform.position.y + auxCancelar.y, instancia.transform.position.z));
                botonAceptarMover.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxAceptar.x, instancia.transform.position.y + auxAceptar.y, instancia.transform.position.z));
                botonCancelarMover.transform.position = Camera.main.WorldToScreenPoint(new Vector3(instancia.transform.position.x + auxCancelar.x, instancia.transform.position.y + auxCancelar.y, instancia.transform.position.z));
            }
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
                instancia.GetComponent<Trabajos>().numbConstruccion = ZPlayerPrefs.GetInt("cantidadConstrucciones") + 1;
                instancia.GetComponent<Trabajos>().IniciarConstruccion();
               
            }
            else if (instancia.tag == "casa")
            {
                instancia.GetComponent<Casa>().numbConstruccion = ZPlayerPrefs.GetInt("cantidadConstrucciones") + 1;
                instancia.GetComponent<Casa>().IniciarConstruccion();
                
            }
            else if (instancia.tag == "ocio")
            {
                instancia.GetComponent<Ocio>().numbConstruccion = ZPlayerPrefs.GetInt("cantidadConstrucciones") + 1;
                instancia.GetComponent<Ocio>().IniciarConstruccion();
               
            }
            else if (instancia.tag == "decoraciones")
            {
                instancia.GetComponent<Deacoraciones>().numbConstruccion = ZPlayerPrefs.GetInt("cantidadConstrucciones") + 1;
                instancia.GetComponent<Deacoraciones>().IniciarConstruccion();

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
    public void OnClickAceptarMover()
    {
        modoConstruccion = false;
        mover = false;
        if (disponible)
        {
            instancia.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            if (instancia.tag == "trabajo")
            {
                ZPlayerPrefs.SetFloat("posX" + instancia.GetComponent<Trabajos>().numbConstruccion, instancia.transform.position.x);
                ZPlayerPrefs.SetFloat("posY" + instancia.GetComponent<Trabajos>().numbConstruccion, instancia.transform.position.y);
                ZPlayerPrefs.SetFloat("posZ" + instancia.GetComponent<Trabajos>().numbConstruccion, instancia.transform.position.z);
            }
            else if (instancia.tag == "casa")
            {
                ZPlayerPrefs.SetFloat("posX" + instancia.GetComponent<Casa>().numbConstruccion, instancia.transform.position.x);
                ZPlayerPrefs.SetFloat("posY" + instancia.GetComponent<Casa>().numbConstruccion, instancia.transform.position.y);
                ZPlayerPrefs.SetFloat("posZ" + instancia.GetComponent<Casa>().numbConstruccion, instancia.transform.position.z);
            }
            else if (instancia.tag == "ocio")
            {
                ZPlayerPrefs.SetFloat("posX" + instancia.GetComponent<Ocio>().numbConstruccion, instancia.transform.position.x);
                ZPlayerPrefs.SetFloat("posY" + instancia.GetComponent<Ocio>().numbConstruccion, instancia.transform.position.y);
                ZPlayerPrefs.SetFloat("posZ" + instancia.GetComponent<Ocio>().numbConstruccion, instancia.transform.position.z);
            }
            else if (instancia.tag == "deacoraciones")
            {
                ZPlayerPrefs.SetFloat("posX" + instancia.GetComponent<Deacoraciones>().numbConstruccion, instancia.transform.position.x);
                ZPlayerPrefs.SetFloat("posY" + instancia.GetComponent<Deacoraciones>().numbConstruccion, instancia.transform.position.y);
                ZPlayerPrefs.SetFloat("posZ" + instancia.GetComponent<Deacoraciones>().numbConstruccion, instancia.transform.position.z);
            }
            instancia = null;
        }
        else
        {
            if (instancia.tag == "trabajo")
            {
                instancia.transform.position = new Vector3(ZPlayerPrefs.GetFloat("posX" + instancia.GetComponent<Trabajos>().numbConstruccion),
                ZPlayerPrefs.GetFloat("posY" + instancia.GetComponent<Trabajos>().numbConstruccion), ZPlayerPrefs.GetFloat("posZ" + instancia.GetComponent<Trabajos>().numbConstruccion));
            }
            else if (instancia.tag == "casa")
            {
                instancia.transform.position = new Vector3(ZPlayerPrefs.GetFloat("posX" + instancia.GetComponent<Casa>().numbConstruccion),
                ZPlayerPrefs.GetFloat("posY" + instancia.GetComponent<Casa>().numbConstruccion), ZPlayerPrefs.GetFloat("posZ" + instancia.GetComponent<Casa>().numbConstruccion));
            }
            else if (instancia.tag == "ocio")
            {
                instancia.transform.position = new Vector3(ZPlayerPrefs.GetFloat("posX" + instancia.GetComponent<Ocio>().numbConstruccion),
                ZPlayerPrefs.GetFloat("posY" + instancia.GetComponent<Ocio>().numbConstruccion), ZPlayerPrefs.GetFloat("posZ" + instancia.GetComponent<Ocio>().numbConstruccion));
            }
            else if (instancia.tag == "deacoraciones")
            {
                instancia.transform.position = new Vector3(ZPlayerPrefs.GetFloat("posX" + instancia.GetComponent<Deacoraciones>().numbConstruccion),
                ZPlayerPrefs.GetFloat("posY" + instancia.GetComponent<Deacoraciones>().numbConstruccion), ZPlayerPrefs.GetFloat("posZ" + instancia.GetComponent<Deacoraciones>().numbConstruccion));
            }
        }
    }
    public void OnClickCancelarMover()
    {
        mover = false;
        modoConstruccion = false;
        if (instancia.tag == "trabajo")
        {
            instancia.transform.position = new Vector3(ZPlayerPrefs.GetFloat("posX" + instancia.GetComponent<Trabajos>().numbConstruccion),
            ZPlayerPrefs.GetFloat("posY" + instancia.GetComponent<Trabajos>().numbConstruccion), ZPlayerPrefs.GetFloat("posZ" + instancia.GetComponent<Trabajos>().numbConstruccion));
        }
        else if (instancia.tag == "casa")
        {
            instancia.transform.position = new Vector3(ZPlayerPrefs.GetFloat("posX" + instancia.GetComponent<Casa>().numbConstruccion),
            ZPlayerPrefs.GetFloat("posY" + instancia.GetComponent<Casa>().numbConstruccion), ZPlayerPrefs.GetFloat("posZ" + instancia.GetComponent<Casa>().numbConstruccion));
        }
        else if (instancia.tag == "ocio")
        {
            instancia.transform.position = new Vector3(ZPlayerPrefs.GetFloat("posX" + instancia.GetComponent<Ocio>().numbConstruccion),
            ZPlayerPrefs.GetFloat("posY" + instancia.GetComponent<Ocio>().numbConstruccion), ZPlayerPrefs.GetFloat("posZ" + instancia.GetComponent<Ocio>().numbConstruccion));
        }
        else if (instancia.tag == "deacoraciones")
        {
            instancia.transform.position = new Vector3(ZPlayerPrefs.GetFloat("posX" + instancia.GetComponent<Deacoraciones>().numbConstruccion),
            ZPlayerPrefs.GetFloat("posY" + instancia.GetComponent<Deacoraciones>().numbConstruccion), ZPlayerPrefs.GetFloat("posZ" + instancia.GetComponent<Deacoraciones>().numbConstruccion));
        }
        instancia = null;
    }
}

