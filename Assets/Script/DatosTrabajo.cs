using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class DatosTrabajo : MonoBehaviour
{
    [Header("Variables para el trabajo")]
    public int trabajadoresNecesita;
    public string[] recursos;
    public int tipoTrabajo;
    public int[] cantidadRecursos;
    public int recursosAceptados = 0;
    public float tiempoTrabajo;
    public float[] recursoNecesitan;
    public string[] recursoNecesario;
    public GameObject trabajo;
    [Header("Escritura")]
    public Text trabajadores;
    public Text semillas;
    public Text semillas2;
    private void OnEnable()
    {
        if (recursoNecesario.Length == 1)
        {
            semillas.text = ZPlayerPrefs.GetFloat(recursoNecesario[0]).ToString() + "/"+ recursoNecesitan[0];
        }else if (recursoNecesario.Length > 1)
        {
            semillas.text = ZPlayerPrefs.GetFloat(recursoNecesario[0]).ToString() + "/" + recursoNecesitan[0];
            semillas2.text = ZPlayerPrefs.GetFloat(recursoNecesario[1]).ToString() +"/"+ recursoNecesitan[1];
        }
        trabajadores.text = GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion + "/" + trabajadoresNecesita;
        recursosAceptados = 0;
        //Debug.Log(ZPlayerPrefs.GetFloat(recursoNecesario[0]));
        Debug.Log(ZPlayerPrefs.GetFloat("semillasManzanas"));
    }
    // Use this for initialization
    public void PonerATrabajar()
    {
        for (int i = 0; i < recursoNecesitan.Length; i++)
        {
            if (recursoNecesitan[i] <= ZPlayerPrefs.GetFloat(recursoNecesario[i]))
            {
                recursosAceptados++;
            }
        }
        if (trabajadoresNecesita <= GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion && recursoNecesitan.Length == recursosAceptados)
        {
            trabajo.GetComponent<Trabajos>().Trabajar(tipoTrabajo);
            for (int i = 0; i < recursoNecesario.Length; i++)
            {
                ZPlayerPrefs.SetFloat(recursoNecesario[i], ZPlayerPrefs.GetFloat(recursoNecesario[i]) - recursoNecesitan[i]);
            }
            for (int i = 0; i < GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().primeraOpcion.Length; i++)
            {
                GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().primeraOpcion[i].gameObject.SetActive(false);
            }
            GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().interfaz.SetActive(false);
            trabajo.GetComponent<Trabajos>().reescribirVariables();
        }
    }
}

