using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace TowerDefense{
    public class Health : MonoBehaviour
    {
        LevelManager load;
        Player player;
        Enemy enemy;
        public int currentHealth = 10;
        bool hasntLost = true;
        void TakeDamage(int damageAmount){
            currentHealth -= damageAmount;
            ValueDisplay.OnValueChanged.Invoke(gameObject.name + "Health", currentHealth);
            if (currentHealth <= 0)
            {
                if (!gameObject.CompareTag("Player"))
                {
                    if (gameObject.GetComponent<Healing>()) gameObject.GetComponent<Healing>().PlayEffect();
                    enemy.speed = 0;
                    enemy.animator.SetTrigger("Death");
                    if (!gameObject.GetComponent<Enemy>().isWizard)
                    {
                        if (((int)Random.Range(1, 3)) == 1)
                        {
                            player.gold += enemy.enemyType.goldGive;
                        }
                        else
                        {
                            player.gold += (int)Mathf.Floor(enemy.enemyType.goldGive / 2);
                        }

                        ValueDisplay.OnValueChanged.Invoke("PlayerGold", player.gold);
                    }

                }
                else if (hasntLost)
                {
                    player.gameOver.Invoke();
                    hasntLost = false;
                    load.waveLength = 99999999f;
                }
            }
            else
            {
                if (enemy) enemy.animator.SetTrigger("Damage");
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
