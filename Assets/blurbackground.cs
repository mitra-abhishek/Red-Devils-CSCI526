using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class blurbackground : MonoBehaviour
{
     private SpriteRenderer m_SpriteRenderer;
     private Sprite m_Sprite;
     private Texture2D originalImage;
     

     public float blurTime = 3.0f;
     
     public float blurDiff = 1.0f;
     
     
     private int stepValue = 256;
     private Texture2D texture;
     
    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        //Graphics.CopyTexture(m_SpriteRenderer.sprite.texture,originalImage);
        //m_Sprite = m_SpriteRenderer.sprite;
        //texture = new Texture2D(1920,1080);
        StartCoroutine(blurLoop());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void blurImage()
    {
        Debug.Log("Blur Image "+ texture);

        //Graphics.CopyTexture(originalImage,texture);
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        GetComponent<SpriteRenderer>().sprite = sprite;
        for (int y = 0; y < texture.height; y = y + stepValue)
        {
            for (int x = 0; x < texture.width; x = x + stepValue) //Goes through each 2 x 2 block of pixels
            {
                Color pixelColour = texture.GetPixel(x,y);
                Debug.Log("Color change");
                for (int dy = 0; dy < stepValue && y+dy < texture.height;  dy++)
                    for (int dx = 0; dx < stepValue && x+dx < texture.width;  dx++)
                        texture.SetPixel(x+dx, y+dy, pixelColour);
            }
        }
        Debug.Log("New Texture:" + texture);
        texture.Apply();
    }
    
    IEnumerator blurLoop(){
        while(true){
            yield return new WaitForSeconds(blurTime);
            //blurImage();
            //var blurImage = Task.Run(() => { this.blurImage(); });
            Debug.Log("Blur Log");
            Debug.Log("Blur Log :" + Shader.GetGlobalFloat("_Blur"));
            blurDiff += 0.05f;
            GetComponent<SpriteRenderer>().material.SetFloat("_Blur",blurDiff);
            stepValue = Mathf.FloorToInt(stepValue / 2.0f);
            yield return new WaitForFixedUpdate();
        }
    }
    
}
