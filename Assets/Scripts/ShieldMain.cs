using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag=="rock"){
            Destroy(other.gameObject);
        }
        if(other.tag=="enemy_bullet"){
            Destroy(other.gameObject);
        }
        if(other.tag=="smart_enemy_bullet"){
            Destroy(other.gameObject);
        }
        if(other.tag=="enemy"){
            Destroy(other.gameObject);
        }
        if(other.tag=="smart_enemy"){
            Destroy(other.gameObject);
        }
    }
}
