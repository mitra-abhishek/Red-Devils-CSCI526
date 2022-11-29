using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemyLevel4 : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    public GameObject BossEnemy_bulletPrefab;
    public float enemy_bulletSpeed = 7f;
    private float angle = 0;
    private float enemy_bullet_ReAppearTime=1f;
    public BossEnemyHealthBehavior healthbar;
    public float points;
    public float maxPoints=50;
    private GameObject coin;
    private coinCount coin_count;
    private GameObject coinHelper;

    public static List<GameObject> FindAllObjectsInScene()
     {
         UnityEngine.SceneManagement.Scene activeScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
 
         GameObject[] rootObjects = activeScene.GetRootGameObjects();
 
         GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
 
         List<GameObject> objectsInScene = new List<GameObject>();
 
         for (int i = 0; i < rootObjects.Length; i++)
         {
             objectsInScene.Add(rootObjects[i]);
         }
 
         for (int i = 0; i < allObjects.Length; i++)
         {
             if (allObjects[i].transform.root)
             {
                 for (int i2 = 0; i2 < rootObjects.Length; i2++)
                 {
                     if (allObjects[i].transform.root == rootObjects[i2].transform && allObjects[i] != rootObjects[i2])
                     {
                         objectsInScene.Add(allObjects[i]);
                         break;
                     }
                 }
             }
         }
         return objectsInScene;
     }
    // Start is called before the first frame update
    void Start()
    {
        rb=this.GetComponent<Rigidbody2D>();
        rb.velocity=new Vector2(GameManager.instance.RockSpeed, 0);
        screenBounds=Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(enemy_bulletLoop());
        maxPoints=50;
        points=50;
        healthbar.SetHealth(points, maxPoints);
        healthbar.SetHealth(points, maxPoints);
        coin_count = new coinCount();
    }

    public void TakeHit(float damage)
    {
        points-=damage;
        healthbar.SetHealth(points, 50);
        //Debug.log(points);
        if(points<=0){
            GameManager.instance.gameWon = true;
            Destroy(gameObject);
            Bullets.enemiesDestroyed += 2;
            List<GameObject> gameObjects = FindAllObjectsInScene();
            foreach(var element in gameObjects)
            {
                if(element.name == "Coin")
                {
                    GameObject enemy = this.gameObject;
                    Vector3 positionHelper = enemy.GetComponent<Transform>().localPosition;
                    coin = element;
                    coin.GetComponent<Transform>().position = positionHelper;
                }
            }
            // other.gameObject.SetActive(false);
            coin.SetActive(true);
            coin_count.setNumCoins(Bullets.enemiesDestroyed);
            coinHelper = GameObject.Find("CoinHelper");
            delayHelper script = coinHelper.GetComponent<delayHelper>();
            script.callCoroutine(coin);
        }
    }

    public void shoot(){
        GameObject bullet=Instantiate(BossEnemy_bulletPrefab) as GameObject;
        bullet.transform.position=GetComponent<Rigidbody2D>().position;
        Vector2 bulletDirection = new Vector2(Mathf.Sin(Mathf.Deg2Rad * -angle), Mathf.Cos(Mathf.Deg2Rad * -angle));
        bullet.GetComponent<Rigidbody2D>().velocity = -bulletDirection * enemy_bulletSpeed;
        bullet.transform.eulerAngles = new Vector3(0, 0, angle);
    }

    IEnumerator enemy_bulletLoop(){
        while(true){
            yield return new WaitForSeconds(enemy_bullet_ReAppearTime);
            shoot();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x>screenBounds.x){
            rb.velocity=new Vector2(-GameManager.instance.RockSpeed, 0);
        }
        else if(transform.position.x<-1*screenBounds.x){
            rb.velocity=new Vector2(GameManager.instance.RockSpeed, 0);
        }
    }
}
