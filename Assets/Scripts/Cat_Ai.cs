﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_Ai : MonoBehaviour
{

    SpriteRenderer catSprite;

    bool caught = false;
    
    [SerializeField]
    float speed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        //if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        //{
        //    // Place of mouse
        //    Debug.Log(hit.point);
        //}

        Flee();

    }

    void Flee()
    {
        


        //// should be the opposite direction of the mouse
        //Vector3 targetPos = this.transform.position - mousePos;
        //targetPos.z = 0;
        //transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("asfa");
        if (collision.name == "Hand" && caught == false)
        {
            Debug.Log("cojkaos");
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;
            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;

            Vector3 targetPos = this.transform.position - mousePos;
            targetPos.z = 0;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        }

        if (collision.tag == "Cage")
        {
            caught = true;
        }
    }
}