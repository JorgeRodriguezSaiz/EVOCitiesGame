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
    public float tiempoTrabajo;
    public float recursoNecesitan = 0;
    public string recursoNecesario = "gold";
    public GameObject trabajo;
    [Header("Escritura")]
    public Text trabajadores;
    public Text semillas;
    private void OnEnable()
    {
        semillas.text = recursoNecesitan.ToString();
        trabajadores.text = GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion + "/" + trabajadoresNecesita;
    }
    // Use this for initialization
    public void PonerATrabajar()
    {
        if (trabajadoresNecesita <= GameObject.Find("Controller").GetComponent<GestionRecursos>().poblacion && recursoNecesitan <= ZPlayerPrefs.GetFloat(recursoNecesario))
        {
            trabajo.GetComponent<Trabajos>().Trabajar(tipoTrabajo);
            ZPlayerPrefs.SetFloat(recursoNecesario, ZPlayerPrefs.GetFloat(recursoNecesario) - recursoNecesitan);
            for (int i = 0; i < GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().primeraOpcion.Length; i++)
            {
                GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().primeraOpcion[i].gameObject.SetActive(false);
            }
            GameObject.Find("Controller").GetComponent<InterfazTrabajoIn>().interfaz.SetActive(false);
        }
    }
}

