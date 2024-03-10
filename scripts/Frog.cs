using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : Enemy
{
    public GameObject leftCap;
    public GameObject rightCap;

    [SerializeField] private float jumpLenght = 10f;
    [SerializeField] private float jumpHeight = 15f;
    [SerializeField] private LayerMask ground;
    private Collider2D coll;
    

    private bool facingLeft = true;
    

    protected override void Start()
    {
        base.Start();
        coll = GetComponent<Collider2D>();
        
    }
    private void Update() 
    {
        //tranzitie sarit-cadere
        if(anim.GetBool("jumping"))
        {
            if(rb.velocity.y < .1)
            {
                anim.SetBool("falling", true);
                anim.SetBool("jumping", false);
            }
        }
        //tranzitie cadere-static
        if(coll.IsTouchingLayers(ground) && anim.GetBool("falling"))
        {
                anim.SetBool("falling", false);
        }
    }
    private void Move()
    {
        if(facingLeft)
        {
            //limit stanga verif
            if(transform.position.x > leftCap.gameObject.transform.position.x)
            {
                //se intoarce in directia corecta
                if(transform.localScale.x != 1)
                {
                    transform.localScale = new Vector3(1,1);
                }
                //testez daca broasca e pe pamant, atunci sare
                if(coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLenght, jumpHeight);
                    anim.SetBool("jumping", true);
                }
            }
            else
            {
                facingLeft = false;
            }
                //daca nu, se indreapta catre dreapta
        }
        else
        {
            //limit dreapta verif
            if (transform.position.x < rightCap.gameObject.transform.position.x)
            {
                //se intoarce in directia corecta
                if (transform.localScale.x != -1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                //testez daca broasca e pe pamant, atunci sare
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLenght, jumpHeight);
                    anim.SetBool("jumping", true);
                }
            }
            else
            {
                facingLeft = true;
            }
            
        }
    }
    private void popping()
    {

    }
}
