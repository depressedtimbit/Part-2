using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSaveManager : MonoBehaviour
{
    // Start is called before the first frame update
    float currentHealth;
    void Start()
    {
        float InitHealth = PlayerPrefs.GetFloat("PlayerHealth", 5);
        Debug.Log(InitHealth.ToString());
        SendMessage("TakeDamage", 99);
        SendMessage("TakeDamage", -InitHealth);
        currentHealth = InitHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, 5);
        PlayerPrefs.SetFloat("PlayerHealth", currentHealth);
        Debug.Log(currentHealth);
        //}
        //if (damage != PlayerPrefs.GetFloat("PlayerHealth", 5))
        //{
    }
}
