using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DamageableCharacter : MonoBehaviour, iDamageable
{

    Animator anime;
    Rigidbody2D rb;

    public GameObject healthText;

    public float Health{
        set{
            if(value < _health) {
                anime.SetTrigger("Hit");
                RectTransform textTransform = Instantiate(healthText).GetComponent<RectTransform>();
                textTransform.transform.position = Camera.main.WorldToScreenPoint(new Vector3 (transform.position.x + 1.2f, transform.position.y, 0));
                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);
            }

            _health = value;

            if(_health<=0){
                anime.SetBool("isAlive", false);
            }
        }
        get{
            return _health;
        }
    }
    public float _health = 3;
    
    public void Start(){
        anime = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        // anime.SetBool("isAlive", true);
    }

    public void Hitted(float damage, Vector2 knockback){
        Health -= damage;

        rb.AddForce(knockback);
        // Debug.Log("Forca: " + knockback);
    }

    public void Hitted(float damage){
        Health -= damage;
    }

    public void DestroyEnemy(){
        Destroy(gameObject);
    }
}
