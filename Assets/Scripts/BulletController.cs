using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;



public class BulletController : MonoBehaviour
{
     public int bulletsToShoot;
     public int availableBullets;
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

   public void addBullets(int count) {
        bulletsToShoot += count;
   }

   void Update() {
        bulletsCountText.text = GameManager.instance.bulletController.getBullets().ToString();
        availableBullets=GameManager.instance.bulletController.getBullets();
        if (GameManager.instance.bulletController.getBullets() <= 10 ){
             bulletsCountText.color = Color.red;
        }
   }
}