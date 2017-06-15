using UnityEngine;
using System.Collections;
public class MoveAfterConstruction : MonoBehaviour
{
    public float tiempoParaMover = 1.5f;
    public float timeCounter = 0f;
    public GameObject controller;
    // Use this for initialization
    void Start()
    {
        controller = GameObject.Find("Controller");
    }
    void OnMouseDrag()
    {
        timeCounter += Time.deltaTime;
        if (timeCounter > tiempoParaMover)
        {
            controller.GetComponent<Construction>().instancia = gameObject;
            controller.GetComponent<Construction>().mover = true;
        }
    }
    void OnMouseUp()
    {
        timeCounter = 0;
    }
}
