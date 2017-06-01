using UnityEngine;
using System.Collections;
namespace Assets.UltimateIsometricToolkit.Scripts.Core
{
    [RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]

    public class comprobadorDisponible : MonoBehaviour {
        public GameObject controlador;
        public bool _disponible = true;
        // Use this for initialization
        void Start() {
            controlador = GameObject.Find("Controller");
            _disponible = true;
        }

        // Update is called once per frame
        void Update() {
            _disponible = controlador.GetComponent<Construction>().disponible;
        }
        void OnTriggerEnter2D(Collider2D coll)
        {
            Debug.Log("Entra en el trigger");
            //_instancia.GetComponent<SpriteRenderer>().color = Color.red;
            controlador.GetComponent<Construction>().instancia.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0f, .5f);
            //_disponible = false;
        }
        void OnTriggerExit2D(Collider2D coll)
        {
            Debug.Log("Sale del trigger");
            //_instancia.GetComponent<SpriteRenderer>().color = Color.white;
            controlador.GetComponent<Construction>().instancia.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
            _disponible = true;
        }
    }
}
