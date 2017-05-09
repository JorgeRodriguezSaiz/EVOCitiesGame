using UnityEngine;
using System.Collections;

public class Construction : MonoBehaviour {
    public GameObject prefab;
    public GameObject instancia;
    private Vector3 instantiatePoint;
    public Vector3 mousePos = new Vector3();
    public bool modoConstruccion = false;
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = Camera.main.nearClipPlane;
        if (modoConstruccion)
        {
            instancia.transform.position = Vector3.MoveTowards(instancia.transform.position, mousePos, 100f*Time.deltaTime);
            //modoConstruccion = false;
        }
    }
    public void OnClickBuilding()
    {
        prefab.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
        //instantiatePoint = Camera.main.ScreenToWorldPoint(mousePos);
        instancia = (GameObject)Instantiate(prefab, mousePos, Quaternion.identity);
        modoConstruccion = true;
    }
}
