using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 screenBounds;
    private Camera mainCam;
    private Rigidbody2D rigidBullet;
    private Vector3 posMouse;
    public GameObject explosion;
    private GameObject coinHelper;
    private GameObject coin;
    private coinCount coin_count;
    private CoinAnimation1 animator;
    public static int enemiesDestroyed = 0;
    public Transform target;
    public Camera cam;
    public float speed = 7f;
    
    public GameObject _coinPrefab;


    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        coin_count = new coinCount();
        animator = new CoinAnimation1();
        //_coinPrefab = Resources.Load("coinPrefab.prefab") as GameObject; 
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > screenBounds.y * 1)
        {
            Destroy(this.gameObject);
        }
    }

    public void SetEnemiesDestroyed(int num)
    {
        enemiesDestroyed = num;
    }


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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "rock")
        {
            PlayerStats.rockScore += 5;
            Instantiate(explosion, transform.position, transform.rotation);
            GameManager.instance.playBulletImpact();
            GameManager.instance.isRockShotTutorial = true;
            Destroy(this.gameObject);
            Destroy(other.gameObject);

        }
        // if(other.tag == "Letter") {
        //     EventManager.TriggerEvent ("test", new Dictionary<string, object> { { "amount", other } });
        //     GameManager.instance.playLetterCollect();
        //     Destroy(other.gameObject);
        //     Destroy(this.gameObject);
        // }

        if (other.tag == "enemy")
        {
            PlayerStats.enemyScore += 10;
            Instantiate(explosion, transform.position, transform.rotation);
            enemiesDestroyed += 1;
            if (enemiesDestroyed == 3)
            {
                GameManager.instance.isEnemyShotTutorial = true;
            }
            List<GameObject> gameObjects = FindAllObjectsInScene();
            foreach (var element in gameObjects)
            {
                if (element.name == "Coin")
                {
                    GameObject enemy = other.gameObject;
                    Vector3 positionHelper = enemy.GetComponent<Transform>().localPosition;
                    coin = element;
                    coin.GetComponent<Transform>().position = positionHelper;
                    GameManager.instance.playCoinCollect();
                }
            }
            GameManager.instance.playBulletImpact();
            // other.gameObject.SetActive(false);
            // coin.SetActive(true);
            coin_count.setNumCoins(enemiesDestroyed);
            coinHelper = GameObject.Find("CoinHelper");
            // delayHelper script = coinHelper.GetComponent<delayHelper>();
            // script.callCoroutine(coin);
            
            print("Testing here as well");
            print(target);
            // GameObject posHelper = Instantiate(other.transform.gameObject) as GameObject;
            Destroy(this.gameObject);
            // other.transform.localScale = new Vector3(0.00001f, 0.000001f, 0.000001f);
            Vector3 _initial_posn = new Vector3(other.transform.position.x, other.transform.position.y);
            Destroy(other.gameObject);
            CoinAnimation1 script2 = coinHelper.GetComponent<CoinAnimation1>();
            script2.startCoinMove(_initial_posn,_coinPrefab);
            // Destroy(posHelper);
        }

        if (other.tag == "smart_enemy")
        {
            PlayerStats.enemyScore += 10;
            Instantiate(explosion, transform.position, transform.rotation);
            enemiesDestroyed += 1;
            List<GameObject> gameObjects = FindAllObjectsInScene();
            foreach (var element in gameObjects)
            {
                if (element.name == "Coin")
                {
                    GameObject enemy = other.gameObject;
                    Vector3 positionHelper = enemy.GetComponent<Transform>().localPosition;
                    coin = element;
                    coin.GetComponent<Transform>().position = positionHelper;
                }
            }
            // other.gameObject.SetActive(false);
            coin.SetActive(true);
            coin_count.setNumCoins(enemiesDestroyed);
            coinHelper = GameObject.Find("CoinHelper");
            delayHelper script = coinHelper.GetComponent<delayHelper>();
            script.callCoroutine(coin);
            GameManager.instance.playBulletImpact();
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        if (other.tag == "boss_enemy_level1")
        {
            PlayerStats.enemyScore += 10;
            Instantiate(explosion, transform.position, transform.rotation);
            FindObjectOfType<BossEnemyLevel1>().TakeHit(10);
            GameManager.instance.playBulletImpact();
            Destroy(this.gameObject);
        }

        if (other.tag == "boss_enemy_level2")
        {
            PlayerStats.enemyScore += 15;
            Instantiate(explosion, transform.position, transform.rotation);
            FindObjectOfType<BossEnemyLevel2>().TakeHit(8);
            GameManager.instance.playBulletImpact();
            Destroy(this.gameObject);
        }

        if (other.tag == "boss_enemy_level3")
        {
            PlayerStats.enemyScore += 20;
            Instantiate(explosion, transform.position, transform.rotation);
            FindObjectOfType<BossEnemyLevel3>().TakeHit(6);
            GameManager.instance.playBulletImpact();
            Destroy(this.gameObject);
        }

        if (other.tag == "boss_enemy_level4")
        {
            PlayerStats.enemyScore += 25;
            Instantiate(explosion, transform.position, transform.rotation);
            FindObjectOfType<BossEnemyLevel4>().TakeHit(5);
            GameManager.instance.playBulletImpact();
            Destroy(this.gameObject);
        }
    }
}