using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class Healing : MonoBehaviour
    {
        [SerializeField] private List<GameObject> buddies = new List<GameObject>();
        GameObject healing;
        public ParticleSystem ps;
        private void OnDestroy()
        {
            ps.Play();
            RemoveInvalid();
            if (buddies.Count > 0)
            {
                int i = 0;

                while (i < buddies.Count)
                {
                    healing = buddies[i];
                    HealTarget();
                    i++;
                }
                
            }
        }
        void HealTarget()
        {
            healing.GetComponent<Health>().currentHealth += 4;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy")) buddies.Add(other.gameObject);

        }
        private void OnTriggerExit(Collider other)
        {
            buddies.Remove(other.gameObject);
        }
        private void RemoveInvalid()
        {
            int i = 0;
            while (i < buddies.Count)
            {
                if (buddies[i]) i++;
                else buddies.RemoveAt(i);
            }
        }
    }
}

