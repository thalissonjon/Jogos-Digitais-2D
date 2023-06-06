using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{

    public float slimeDamage = 1;
    public float knockbackForce = 500f;

    public DetectRange detectRange;
    Rigidbody2D rb;

    SpriteRenderer spriteRenderer;

    public float speed = 300f;

    void Start(){
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate(){
        
        if(detectRange.detectedObj.Count > 0){
            Collider2D detectedObj0 = detectRange.detectedObj[0];
            // move na direçao do player
            Vector2 direction = (detectedObj0.transform.position - transform.position).normalized;

            rb.AddForce(direction * speed * Time.deltaTime);

            if(direction.x > 0){
                spriteRenderer.flipX = false;
            }
            else if (direction.x < 0){
                spriteRenderer.flipX = true;
            }

        }
    }

    void OnCollisionEnter2D(Collision2D col){
        Collider2D collider = col.collider;
        iDamageable damageable = collider.GetComponent<iDamageable>();

        if(damageable != null){
            Vector2 direction = (Vector2) (collider.transform.position - transform.position).normalized;
            Vector2 knockback = direction * knockbackForce;

            // collider.SendMessage("Hitted", swordDamage, knockback); // Método antigo
            damageable.Hitted(slimeDamage, knockback);
        }
    }

}
