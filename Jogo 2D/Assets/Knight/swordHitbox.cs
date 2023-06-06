using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordHitbox : MonoBehaviour
{   
    public float swordDamage = 1f;
    public float knockbackForce = 300f;
    public Collider2D swordCollider;

    public Vector3 faceRight = new Vector3(0.65f, 0, 0);
    public Vector3 faceLeft = new Vector3(-0.25f, 0, 0);
    // public Vector3 rotationY = new Quaternion(0, 0, 90); // Vetor de rotacao (up, down)
    public Vector3 faceUp = new Vector3(0, 0.65f, 0);
    public Vector3 faceDown = new Vector3(0, -0.25f, 0);

    void Start(){
        if(swordCollider = null){
            Debug.LogWarning("Sword Collider nao foi setado");
        }
    }

    void OnTriggerEnter2D(Collider2D collider){

        iDamageable damagableObject = (iDamageable) collider.GetComponent<iDamageable>(); // filtrando em inimigos

        if(damagableObject != null){
            //calcular dist entre player e slime
            Vector3 parentPosition = transform.parent.position; 
            Vector2 direction = (Vector2) (collider.gameObject.transform.position - parentPosition).normalized;
            Vector2 knockback = direction * knockbackForce;

            // collider.SendMessage("Hitted", swordDamage, knockback); // Método antigo
            damagableObject.Hitted(swordDamage, knockback);
        } 
    }

    void isFacingUp(bool isFacingUp){
        if(isFacingUp){
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            gameObject.transform.localPosition = faceUp;
        }
        else{
            gameObject.transform.localRotation = Quaternion.Euler(0, 0, 90);
            gameObject.transform.localPosition = faceDown;
        }
    }

    void isFacingRight(bool isFacingRight){
        if(isFacingRight){
            gameObject.transform.localPosition = faceRight;
        } else{
            gameObject.transform.localPosition = faceLeft;
        }
    }
}
