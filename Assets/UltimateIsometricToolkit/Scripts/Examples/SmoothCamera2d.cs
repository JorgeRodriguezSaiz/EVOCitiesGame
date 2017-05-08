﻿using UnityEngine;
using Assets.UltimateIsometricToolkit.Scripts.Core;
using System;
public class SmoothCamera2d : MonoBehaviour {

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
    private bool aux;

    void Start()
    {
        target = this.transform;
    }
    // Update is called once per frame
    void Update() {
		if (target) {
            float targetX = target.position.x;
            float targetY = target.position.y;
            /*Vector3 point = Camera.main.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			Vector3 destination = transform.position + delta;*/
            //transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
            /*targetX = Mathf.Clamp(targetX, minXAndY.x, maxXAndY.x);
            targetY = Mathf.Clamp(targetY, minXAndY.y, maxXAndY.y);*/
            if (targetX >= minX && targetX <= maxY && targetY >= minY && targetY <= maxY)
            {
                move();
            }
            if (targetX < minX)
            {
                transform.position = new Vector3(minX, targetY, -10);
            }
            if (targetX > maxX)
            {
                transform.position = new Vector3(maxX, targetY, -10);
            }
            if (targetY < minY)
            {
                transform.position = new Vector3(targetX, minY, -10);
            }
            if (targetX > maxY)
            {
                transform.position = new Vector3(targetX, maxY, -10);
            }
        }
    }
    bool move()
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
        if (mouseposition.x - initialMousePosition.x >= 0)
        {
            aux = true;
        }
        if (mouseposition.x - initialMousePosition.x <= 0)
        {
            aux = false;
        }
        if (mouseposition.y - initialMousePosition.y >= 0)
        {
            aux = true;
        }
        if (mouseposition.y - initialMousePosition.y <= 0)
        {
            aux = false;
        }
        return aux;
    }
}