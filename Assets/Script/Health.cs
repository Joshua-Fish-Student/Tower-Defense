using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense{
    public class Health : MonoBehaviour
    {
        LevelManager load;
        Player player;
        Enemy enemy;
        public int currentHealth = 10;
        void TakeDamage(int damageAmount){
            currentHealth -= damageAmount;
            ValueDisplay.OnValueChanged.Invoke(gameObject.name + "Health", currentHealth);
            if(currentHealth <= 0){
                if(!gameObject.CompareTag("Player")){
                    Destroy(gameObject);
                    player.gold += enemy.goldGive;
                    ValueDisplay.OnValueChanged.Invoke("PlayerGold", player.gold);
                } else{
                    load.loadNum = 0;
                    load.LoadIt();
                }
            }
        }
        public static void TryDamage(GameObject target, int damageAmount){
            Health thing = target.GetComponent<Health>();
            if(thing){
                thing.TakeDamage(damageAmount);
            }
        }
        private void Awake(){
            player = FindObjectOfType<Player>();
            if(!gameObject.CompareTag("Player")){
                enemy = GetComponent<Enemy>();
            }
            load = FindObjectOfType<LevelManager>();
        }
    }

}
