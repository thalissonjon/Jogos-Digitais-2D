using UnityEngine;

public interface iDamageable {

    public float Health {set; get;}

    public void Hitted(float damage, Vector2 knockback);
    public void Hitted(float damage); //sem knockback

    public void DestroyEnemy();
}