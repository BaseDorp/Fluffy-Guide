﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_Ai : MonoBehaviour
{

    SpriteRenderer catSprite;

    bool caught = false;
    
    [SerializeField]
    float speed;
    [SerializeField]
    float fleeDistance;

    public delegate void CatHerdedEventHandler();

    public static event CatHerdedEventHandler CatHerded;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {       

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("collided");
        if (collision.name == "Hand" && caught == false)
        {
            Debug.Log("running");
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
            Debug.Log("caught");
            caught = true;
            OnCatHerded();
        }
    }

    protected virtual void OnCatHerded()
    {
        if(CatHerded != null)
        {
            CatHerded();
        }
    }
}
