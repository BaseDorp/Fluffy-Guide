using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_Ai : MonoBehaviour
{

    SpriteRenderer catSprite;
    CircleCollider2D circleCollider;

    bool caught = false;
    float lastFramePos = 0;
    
    [SerializeField]
    float speed;
    [SerializeField]
    float fleeDistance;

    public delegate void CatHerdedEventHandler();

    public static event CatHerdedEventHandler CatHerded;
    
    // Start is called before the first frame update
    void Start()
    {
        this.catSprite = GetComponent<SpriteRenderer>();
        this.circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {       

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Hand" && caught == false)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 0;
            Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

            mousePos.x = mousePos.x - objectPos.x;
            mousePos.y = mousePos.y - objectPos.y;

            Vector3 targetPos = this.transform.position - mousePos;
            targetPos.z = 0;

            
            if (lastFramePos < this.transform.position.x)
            {
                catSprite.flipX = true;
            }
            else if (lastFramePos > this.transform.position.x)
            {
                catSprite.flipX = false;
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            lastFramePos = this.transform.position.y;
        }

        if (collision.tag == "Cage")
        {
            Debug.Log("caught");
            circleCollider.enabled = false;
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
