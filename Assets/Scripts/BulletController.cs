using UnityEngine;
using UnityEngine.UI;


public class BulletController : MonoBehaviour
{
    public int bulletsToShoot;
    [SerializeField] Text bulletsCountText;

   public void setBullets(int bullets){
       bulletsToShoot=bullets;
   }

   public int getBullets(){
        return bulletsToShoot;  
   }

   public void subtractBullet(){
        bulletsToShoot--;
   }

   void Update() {
        bulletsCountText.text = GameManager.instance.bulletController.getBullets().ToString();
        
        if (GameManager.instance.bulletController.getBullets() <= 10 ){
             bulletsCountText.color = Color.red;
        }
   }
}