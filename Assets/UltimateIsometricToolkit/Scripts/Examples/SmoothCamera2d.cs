﻿using UnityEngine;
using Assets.UltimateIsometricToolkit.Scripts.Core;
using System;
public class SmoothCamera2d : MonoBehaviour
{

    //public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform target;
    public float maxX;
    public float maxY;
    public float minX;
    public float minY;
    public float tolerance = 2;
    public float Speed = 10;
    private IsoTransform _isoTransform;
    private Vector3 initialMousePosition;
    Vector3 mouseposition;
    //derecha y arriba true, izquierda y abajo false
    public bool _modoConstruction;
    public float smoothTime = 0.15F;
    public GameObject controller;
    public Vector3 _mousePos = new Vector3();
    public float velocidadCamaraConstruccion = 10f;
    public float distanciaHastaMover = 5f;

    void Start()
    {
        controller = GameObject.Find("Controller");
        target = this.transform;
    }
    // Update is called once per frame
    void Update()
    {
        _modoConstruction = controller.GetComponent<Construction>().modoConstruccion;
        _mousePos = controller.GetComponent<Construction>().mousePos;
        if (target)
        {
            float targetX = target.position.x;
            float targetY = target.position.y;
            if (targetX >= minX && targetX <= maxY && targetY >= minY && targetY <= maxY)
            {
               if (_modoConstruction)
               {
                    if (_mousePos.x > transform.position.x + distanciaHastaMover)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, _mousePos, velocidadCamaraConstruccion * Time.deltaTime);
                    }
                    if (_mousePos.x < transform.position.x - distanciaHastaMover)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, _mousePos, velocidadCamaraConstruccion * Time.deltaTime);
                    }
                    if (_mousePos.y > transform.position.y + distanciaHastaMover)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, _mousePos, velocidadCamaraConstruccion * Time.deltaTime);
                    }
                    if (_mousePos.y < transform.position.y - distanciaHastaMover)
                    {
                        transform.position = Vector3.MoveTowards(transform.position, _mousePos, velocidadCamaraConstruccion * Time.deltaTime);
                    }
                }
               else
               {
                  move();
               }
            }


                if (targetX < minX)
                {
                    transform.position = Vector3.SmoothDamp(transform.position, new Vector3(minX + 0.01f, targetY, -10), ref velocity, smoothTime);
                    
                }
                if (targetX > maxX)
                {
                    transform.position = Vector3.SmoothDamp(transform.position, new Vector3(maxX - 0.01f, targetY, -10), ref velocity, smoothTime);
                }
                if (targetY < minY)
                {
                    transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetX, minY + 0.01f, -10), ref velocity, smoothTime);
                }
                if (targetY > maxY)
                { 
                    transform.position = Vector3.SmoothDamp(transform.position, new Vector3(targetX, maxY - 0.01f, -10), ref velocity, smoothTime);
                }
        }
}
    
    void move()
    {
        if (Input.GetMouseButton(0))
        {//Detecta el mouse
            mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (Input.GetMouseButtonDown(0))
            {
                initialMousePosition = mouseposition;
            }

            //Si hay movimiento entra
            if (initialMousePosition != mouseposition)
            {
                if (Math.Abs(mouseposition.x - initialMousePosition.x) > tolerance && Math.Abs(mouseposition.y - initialMousePosition.y) > tolerance)
                {
                    transform.Translate(new Vector3(-(mouseposition.x - initialMousePosition.x), -(mouseposition.y - initialMousePosition.y), 0) * Time.deltaTime * Speed);

                }
                else
                {
                    if (Math.Abs(mouseposition.x - initialMousePosition.x) > tolerance)
                    {
                        transform.Translate(new Vector3(-(mouseposition.x - initialMousePosition.x), 0, 0) * Time.deltaTime * Speed);
                    }
                    if (Math.Abs(mouseposition.y - initialMousePosition.y) > tolerance)
                    {
                        transform.Translate(new Vector3(0, -(mouseposition.y - initialMousePosition.y), 0) * Time.deltaTime * Speed);
                    }
                }
            }
        }
    }
}