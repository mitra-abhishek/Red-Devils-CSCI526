using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class InstructionFader : MonoBehaviour
{
    private SpriteRenderer mouse_kb;
    // Start is called before the first frame update
    void Start()
    {
        
        //await Task.Delay(2000);
        StartCoroutine(FadeImage());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    IEnumerator FadeImage()
    {
        mouse_kb = this.GetComponent<SpriteRenderer>();
        for (float i = 1; i >= 0; i -= Time.deltaTime/5)
            {
                // set color with i as alpha
                mouse_kb.material.color = new Color(1, 1, 1, i);
                yield return null;
            }
        Destroy(gameObject);
    }
}
