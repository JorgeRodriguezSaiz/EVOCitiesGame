using UnityEngine;
using System.Collections;

public class Construction : MonoBehaviour {
    public GameObject prefab;
    private GameObject instancia;
    private Vector3 instantiatePoint;
    private GameObject panelConstruccion;
    Event e = Event.current;
    public Vector3 mousePos = new Vector3();
    public bool modoConstruccion = false;
    // Use this for initialization
    void Start () {
        panelConstruccion = GameObject.Find("PanelConstruir");
	}
	
	// Update is called once per frame
	void Update () {

        if (modoConstruccion)
        {
            /*mousePos.x = Input.mousePosition.x;
            mousePos.y = Input.mousePosition.y;
            mousePos.z = Camera.main.nearClipPlane;*/
            //instancia.transform.Translate(Camera.main.ScreenToWorldPoint(mousePos));
            instancia.transform.position = Vector3.MoveTowards(transform.position, e.mousePosition, 1*Time.deltaTime);
            //modoConstruccion = false;
        }
    }
    public void OnClickBuilding()
    {
        prefab.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
        instantiatePoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));
        instancia = (GameObject)Instantiate(prefab, instantiatePoint, Quaternion.identity);
        modoConstruccion = true;
    }
}
