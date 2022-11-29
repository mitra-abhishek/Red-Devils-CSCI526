using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoinAnimation1 : MonoBehaviour
{
    // Start is called before the first frame update
    public static Transform target;
    public float speed;
    public static Camera cam;
    // public static GameObject _coinPrefab;
    void Start()
    {
        // if(cam == null)
        // {
        //     cam = Camera.main;
        // }
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

    public void startCoinMove(Vector3 _initial, GameObject _coinPrefab, Transform initalPos)
    {
        print("Testing Here");
        print(_coinPrefab);
        print(initalPos);
        List<GameObject> gameObjects = FindAllObjectsInScene();
            foreach(var element in gameObjects)
            {
                if(element.name == "CoinIcon")
                {
                    GameObject targetObj = element;
                    target = targetObj.GetComponent<Transform>();
                }
                if(element.name == "Main Camera")
                {
                    cam = element.GetComponent<Camera>();
                }
            }
        
        Vector3 targetPos = cam.ScreenToWorldPoint( new Vector3(target.position.x,target.position.y,cam.transform.position.z*-1));
        GameObject initalPosHelper = Instantiate(initalPos.gameObject) as GameObject;
        GameObject _coin = Instantiate(_coinPrefab, initalPosHelper.transform);
        print("Checking here");
        
        print(_initial);
        print(targetPos);
        print(_coin.transform);
       StartCoroutine(MoveCoin(_coin.transform, _initial, targetPos));
    }

    public IEnumerator MoveCoin(Transform obj,Vector3 startPos, Vector3 endPos)
    {
        float time = 0;
        while(time<1){
            time += 1 * Time.deltaTime;
            obj.position = Vector3.Lerp(startPos, endPos, time);

            yield return new WaitForEndOfFrame();
        }
        yield return null;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
