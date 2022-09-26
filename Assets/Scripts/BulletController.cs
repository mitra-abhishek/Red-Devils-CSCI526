using UnityEngine;
using UnityEngine.UI;


public class BulletController : MonoBehaviour
{
    public int bulletsToShoot;

   public void setBullets(int bullets){
       bulletsToShoot=bullets;
   }

   public int getBullets(){
        return bulletsToShoot;  
   }

   public void subtractBullet(){
        bulletsToShoot--;
   }
}