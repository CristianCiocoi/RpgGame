using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float moveSpeed = 5f;//analogy to serialize field
    public Rigidbody2D theRB;  //getting the Rigidbody2D component the other way 
    //is the known way GetComponent<Rigidbody2D>().

    public Animator myAnimator;

    public static PlayerController instance;  //we are creaing a reference to the current script
    //static means that we can only be one version of this for every single obj in the whole world that has this PlayerController script
    public string areaTransitionName;


    private Vector3 botomLeftLimit;
    private Vector3 topRightLimit;


    public bool canMove = true;

    void Start()
    {
        Singleton();

    }

    private void Singleton()
    {
        //a singleton version
        if (instance == null)
        {
            //knowing that instance can only be one we can do this:
            instance = this; //the instance value has to be assigned to this script
                             //that means that when the games starts the very first thing that will happen is we load the scene and the instance value will be load to this player
        }
        else
        {
            if(instance != this)
            {
                Destroy(gameObject);
            }

        }
        DontDestroyOnLoad(gameObject);//when we load into a new scene dont destroy whatever is in the brackets
    }

    void Update()
    {
        if (canMove)
        {
            theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;


        }
        else
        {
            theRB.velocity = Vector2.zero;
        }

        myAnimator.SetFloat("moveX", theRB.velocity.x);
        myAnimator.SetFloat("moveY", theRB.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            if (canMove)
            {
                myAnimator.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
                myAnimator.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
            }

        }
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, botomLeftLimit.x, topRightLimit.x), Mathf.Clamp(transform.position.y, botomLeftLimit.y, topRightLimit.y), transform.position.z);

    }



    public void SetBounds(Vector3 botomLeftLimit, Vector3 topRightLimit)
    {
        this.botomLeftLimit = botomLeftLimit + new Vector3(1f,1f,0f);
        this.topRightLimit = topRightLimit - new Vector3(1f, 1f, 0f);
    }
}
