using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_Ai : MonoBehaviour
{

    SpriteRenderer catSprite;
    CircleCollider2D circleCollider;
    AudioSource audioSource;
    Transform child;

    bool flipped = false;
    public bool caught = false;
    float lastFramePos = 0;
    
    [SerializeField]
    float speed;
    [SerializeField]
    float fleeDistance;
    [SerializeField]
    float bounceSpeed;
    [SerializeField]
    float bounceHeight;

    public delegate void CatHerdedEventHandler();

    public static event CatHerdedEventHandler CatHerded;
    
    // Start is called before the first frame update
    void Start()
    {
        this.catSprite = GetComponentInChildren<SpriteRenderer>();
        this.circleCollider = GetComponent<CircleCollider2D>();
        audioSource = GetComponent<AudioSource>();
        this.child = this.gameObject.transform.GetChild(0);
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

            
            if (mousePos.x < this.transform.position.x && flipped == false)
            {
                //this.gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                flipped = true;
            }
            else if (mousePos.x > this.transform.position.x && flipped)
            {
                //facing left
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                flipped = false;
            }

            transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            //child.transform.localPosition = new Vector3(transform.position.x, (Mathf.Sin(Time.time * bounceSpeed) * bounceHeight) + transform.position.y, 0);
            lastFramePos = this.transform.position.x;
        }

        if (collision.tag == "Cage")
        {
            Debug.Log("caught");
            circleCollider.enabled = false;
            caught = true;
            OnCatHerded();
            audioSource.Play();
        }
    }

    protected virtual void OnCatHerded()
    {
        if(CatHerded != null)
        {
            CatHerded();
        }
    }

    private void OnEnable()
    {
        caught = false;
    }
}
