using UnityEngine;
using System.Collections.Generic;
using Game.Core;

public class UIHealth : MonoBehaviour
{
    [SerializeField] private List<HealthPointsGroup> _healtPoints;

    public void UpdateHealth(int value)
    {
        foreach (HealthPointsGroup healthPointGroup in _healtPoints)
        {
            List<GameObject> objs = healthPointGroup.HealthPoints;    
            
            foreach(GameObject obj in objs) 
                obj.SetActive(false);            
        }

        foreach (HealthPointsGroup healthPointGroup in _healtPoints)
        {
            List<GameObject> objs = healthPointGroup.HealthPoints;

            for(int i = 0; i < value; i++)
                objs[i].SetActive(true);
        }
    }        
}
