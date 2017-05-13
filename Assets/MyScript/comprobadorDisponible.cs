using UnityEngine;
using System.Collections;
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class comprobadorDisponible : MonoBehaviour {
    public GameObject controlador;
    public GameObject _instancia;
    public bool _disponible;
	// Use this for initialization
	void Start () {
        controlador = GameObject.Find("Controller");
	}
	
	// Update is called once per frame
	void Update () {
	 _instancia = controlador.GetComponent<Construction>().instancia;
     _disponible = controlador.GetComponent<Construction>().disponible;
    }
    void OnTriggerStay2D (Collider2D coll)
    {
        Debug.Log("Entra en el trigger");
        _instancia.GetComponent<SpriteRenderer>().color = Color.red;
        _disponible = false;
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        Debug.Log("Sale del trigger");
        _instancia.GetComponent<SpriteRenderer>().color = Color.white;
        _disponible = true;
    }
}
