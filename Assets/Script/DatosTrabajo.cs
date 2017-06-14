using UnityEngine;
using System.Collections;

namespace Assets.UltimateIsometricToolkit.Scripts.Core
{
    public class DatosTrabajo : MonoBehaviour {
        public int trabajadoresNecesita;
        public string[] recursos;
        public int tipoTrabajo;
        public int[] cantidadRecursos;
        public float tiempoTrabajo;
        public GameObject trabajo;
        // Use this for initialization
        public void PonerATrabajar()
        {
            trabajo.GetComponent<Trabajos>().Trabajar(tipoTrabajo);
        }
    }
}
