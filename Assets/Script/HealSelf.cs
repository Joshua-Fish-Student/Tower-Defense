using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class HealSelf : MonoBehaviour
    {
        Health health;
        public ParticleSystem ps;
        // Start is called before the first frame update
        void Start()
        {
            health = GetComponent<Health>();
            StartCoroutine(Heal());
        }

        IEnumerator Heal()
        {
            yield return new WaitForSeconds(5);
            health.currentHealth += 5;
            ps.Play();
        }
    }
}
