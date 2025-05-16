using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense{
    [RequireComponent(typeof(Animator))]
    public class Tower : MonoBehaviour
    {
        [SerializeField] private List<GameObject> enemiesInRange = new List<GameObject>();
        public Tower_SO towerType;
        private bool firing = false;
        public GameObject enemyTarget;
        Animator animator;
        Player player;
        LevelManager levelManager;
        Grid grid;
        public bool isGoldTower;
        public int giveUntilDestroy = 5;

        private void Start(){
            animator = GetComponent<Animator>();
            player = FindObjectOfType<Player>();
            levelManager = FindObjectOfType<LevelManager>();
            grid = FindObjectOfType<Grid>();
        }

        public void DamageTarget(){
            if (!enemyTarget) return;
            Health.TryDamage(enemyTarget, towerType.damage);
        }

        private void RemoveDestoryedEnemies(){
            int i = 0;
            while(i < enemiesInRange.Count){
                if(enemiesInRange[i]) i++;
                else enemiesInRange.RemoveAt(i);
            }
        }

        IEnumerator DamageEnemyTarget(){
            firing = true;

            while(enemiesInRange.Count > 0){
                RemoveDestoryedEnemies();
                if(enemiesInRange.Count > 0){
                    enemyTarget = enemiesInRange[0];
                    animator.SetTrigger("Fire");
                }
                
                

                yield return new WaitForSeconds(towerType.fireRate);
            }
            firing = false;
        }
        void Fire()
        {
            if(towerType.name == "FireWizardTower")
                {
                    GetComponentInChildren<ParticleSystem>().Play();
                }
        }

        private void OnTriggerEnter(Collider other){
            if(other.gameObject.CompareTag("Enemy")) enemiesInRange.Add(other.gameObject);

            if(!firing && !isGoldTower) StartCoroutine(DamageEnemyTarget());
        }
        private void OnTriggerExit(Collider other){
            enemiesInRange.Remove(other.gameObject);
        }

        IEnumerator GivePlayerGold()
        {
            firing = true;
            yield return new WaitForSeconds(towerType.fireRate);
            player.gold += towerType.damage;
            ValueDisplay.OnValueChanged.Invoke("PlayerGold", player.gold);
            giveUntilDestroy -= 1;
            firing = false;
        }
        private void Update()
        {
            if (isGoldTower && !firing) StartCoroutine(GivePlayerGold());
            if (giveUntilDestroy == 0 && isGoldTower && !levelManager.isTitleScreen)
            {
                grid.gameObjects.Remove(Grid.WorldToGrid(gameObject.transform.position));
                Destroy(gameObject);
            }
        }
    }

}
