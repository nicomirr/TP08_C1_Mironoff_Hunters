using UnityEngine;
using System.Collections.Generic;
using Game.Core;

namespace Game.UI
{
    public class UIHealth : MonoBehaviour
    {
        [SerializeField] private List<HealthPointsGroup> _healtPoints;

        public void UpdateHealth(int value)
        {
            foreach (HealthPointsGroup healthPointGroup in _healtPoints)
            {
                List<GameObject> objs = healthPointGroup.HealthPoints;

                foreach (GameObject obj in objs)
                    obj.SetActive(false);
            }

            foreach (HealthPointsGroup healthPointGroup in _healtPoints)
            {
                List<GameObject> objs = healthPointGroup.HealthPoints;

                for (int i = 0; i < value; i++)
                    // Warning: si value supera la cantidad de HealthPoints del grupo, objs[i] tira ArgumentOutOfRangeException. Limitá el for con Mathf.Min(value, objs.Count).
                    objs[i].SetActive(true);
            }
        }
    }

}
