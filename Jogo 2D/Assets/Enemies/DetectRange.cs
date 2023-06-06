using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRange : MonoBehaviour
{
    public List<Collider2D> detectedObj = new List<Collider2D>();
    public Collider2D col;

    public string target = "Player";
   
    //detecta quando o obj entra no range
    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.gameObject.tag == target){
            detectedObj.Add(collider);
        }
    }

    void OnTriggerExit2D(Collider2D collider) {
        detectedObj.Remove(collider);
    }
  
}
