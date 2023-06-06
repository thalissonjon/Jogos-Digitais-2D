using System.Collections;
using System.Collections.Generic;
using UnityEngine;  
using System;

public class Player : MonoBehaviour
{
    public DamageableCharacter flashRed; 
    
    private float atualHealth;

    bool IsMoving{
        set{
            isMoving = value;
            anime.SetBool("isMoving", isMoving);
        }
    }

    public Animator anime;
    public SpriteRenderer sprite;

    [SerializeField] public float speed;
    [SerializeField] float maxSpeed = 320f;
    
    private float attackCd = .5f;
    private float attackTime = .5f; //setar valor pra resetar o tempo depois de atacar
    private bool isAttacking;

    public GameObject swordHitbox;

    public Collider2D swordCollider;
    
    bool canMove = true;
    bool isMoving = false;


    public float idleFriction = 0.9f;


    // var rigidbody

    public Rigidbody2D rb;

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        swordCollider = swordHitbox.GetComponent<Collider2D>();
        atualHealth = flashRed.Health;
        // spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update(){
        // Atacar
        if(isAttacking)
        {
            attackCd -= Time.deltaTime;
            if(attackCd <= 0)
            {
                isAttacking = false;
                anime.SetBool("isAttacking", false);   
            }
        }
        if(Input.GetKeyDown(KeyCode.Mouse0) && isAttacking == false)
        {   
            attackCd = attackTime;
            anime.SetBool("isAttacking", true);
            isAttacking = true;
        }

        // Checar posicao do mouse
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        var mouseDiffX = mousePosition.x - transform.position.x;
        var mouseDiffY = mousePosition.y - transform.position.y;

        anime.SetFloat("mousePosX", mouseDiffX);
        anime.SetFloat("mousePosY", mouseDiffY);

        if(Math.Abs(mouseDiffX) > Math.Abs(mouseDiffY)){
            if(mouseDiffX > 0){
                // spriteRenderer.flipX = false;
                gameObject.BroadcastMessage("isFacingRight", true);
                // Debug.Log("Direita");
            } else if (mouseDiffX < 0){
                // spriteRenderer.flipX = true;
                gameObject.BroadcastMessage("isFacingRight", false);
                // Debug.Log("Esquerda");
            }
        }
        else{
            if(mouseDiffY > 0){
                gameObject.BroadcastMessage("isFacingUp", true);
            }
            else if (mouseDiffY < 0){
                gameObject.BroadcastMessage("isFacingUp", false);
            }
        }
        damageCheck();
    
    }
    void FixedUpdate()
    {
        // Movimentacao do Player
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0f);

        anime.SetFloat("Speed", move.magnitude); // Retornar um float do vector3
        anime.SetFloat("Horizontal", move.x);
        anime.SetFloat("Vertical",move.y);

        if(canMove == true && move != Vector3.zero){
             rb.AddForce(move * speed * Time.deltaTime);
             IsMoving = true;

             if(rb.velocity.magnitude > maxSpeed){
                float limitedSpeed = Mathf.Lerp(rb.velocity.magnitude, maxSpeed, idleFriction);
                rb.velocity = rb.velocity.normalized * limitedSpeed;
             }
        }
        else{
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, idleFriction);
            // rb.velocity = Vector2.zero;
            IsMoving = false;
            // Debug.Log("Entrou");
        }
        //transform.position = transform.position + move * speed * Time.deltaTime;
        // rb.velocity = new Vector2(move.x * speed, move.y * speed);     
    }


    public IEnumerator FlashRed(){
        sprite.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.material.color = Color.white;
    }

    public void damageCheck(){
        if(atualHealth > flashRed.Health){
            atualHealth = flashRed.Health;
            StartCoroutine(FlashRed());
        }
    }
    
}
