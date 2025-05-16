using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense{
    public class Enemy : MonoBehaviour
    {
        public Enemy_SO enemyType;
        public Path path;
        public int index = 0;
        public Animator animator;
        public float speed = 1f;
        int damage = 1;
        int maxGoldGive = 1;
        public bool isWizard;
        public bool end = false;
        

        void Start(){
            path = FindObjectOfType<Path>();
            StartCoroutine(FollowPath());
            speed = enemyType.speed;
            damage = enemyType.damage;
            maxGoldGive = enemyType.goldGive;
        }
        IEnumerator FollowPath(){
            Vector3 target;
            while(path.TryGetPoint(index, out target)){
                Vector3 start = transform.position;

                float maxDistance = Mathf.Min(speed * Time.deltaTime, (target - start).magnitude);
                transform.position = Vector3.MoveTowards(start, target, maxDistance);

                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(target - start), 0.05f);

                if (transform.position == target) index++;
                yield return null;
            }
            Player player = FindObjectOfType<Player>();
            Instantiate(player.ps, transform.position, transform.rotation);
            end = true;
            Health.TryDamage(player.gameObject,damage);
            Destroy(gameObject);
        }
        void DestroySelf()
        {
            Destroy(gameObject);
        }
        
    }

}
