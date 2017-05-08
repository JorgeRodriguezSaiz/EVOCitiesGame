using UnityEngine;
using System.Collections;

public class Construction : MonoBehaviour {
    public GameObject prefab;
    private Vector3 instantiatePoint;
    private GameObject panelConstruccion;
	// Use this for initialization
	void Start () {
        panelConstruccion = GameObject.Find("PanelConstruir");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnClickBuilding()
    {
        instantiatePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(prefab, instantiatePoint, Quaternion.identity);
        panelConstruccion.SetActive(false);
    }
}
