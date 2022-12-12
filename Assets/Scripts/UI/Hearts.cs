using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour
{
    [SerializeField] Health health;

    [SerializeField] Image[] hearts;

    void Update()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health.health)
            {
                hearts[i].enabled = true;
            }
            else 
            {
                hearts[i].enabled = false;
            }
        }
    }
}
