﻿using UnityEngine;
using System.Collections;
namespace Assets.UltimateIsometricToolkit.Scripts.Core
{
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
            }
        }
        void OnMouseUp()
        {
            timeCounter = 0;
        }
    }
}