using UnityEngine;
using System.Collections;

public class Construction : MonoBehaviour {
    public GameObject prefab;
    public GameObject instancia;
    public GameObject botonAceptar;
    public Vector3 mousePos = new Vector3();
    public Vector3 mousePosCanvas = new Vector3();
    public bool modoConstruccion = false;
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.x = Mathf.Round(mousePos.x);
        mousePos.y = Mathf.Round(mousePos.y);
        mousePos.z = Camera.main.nearClipPlane;
        mousePosCanvas = Input.mousePosition;
        mousePosCanvas.z = Camera.main.nearClipPlane;
        if (modoConstruccion)
        {
            instancia.transform.position = Vector3.MoveTowards(instancia.transform.position, mousePos, 100f*Time.deltaTime);
            botonAceptar.transform.position = Vector3.MoveTowards(botonAceptar.transform.position, Input.mousePosition, 10000f * Time.deltaTime);
        }
    }
    public void OnClickBuilding()
    {
        instancia = (GameObject)Instantiate(prefab, mousePos, Quaternion.identity);
        instancia.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
        modoConstruccion = true;
    }
    public void OnClickAceptar()
    {
        modoConstruccion = false;
        instancia.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }
    public void OnClickCancelar()
    {
        Destroy(instancia);
        modoConstruccion = false;
    }
}
