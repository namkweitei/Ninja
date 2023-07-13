using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image imageFill;
    [SerializeField] private float maxHealth;
    [SerializeField] private float minHealth;
    [SerializeField] private float currentHealth;
    public float CurrHealth { get => currentHealth; set => currentHealth = value; }
    private void Update()
    {
        SetHealthBar();
        transform.rotation = Camera.main.transform.rotation;
    }
    public void OnInit(float maxhealth)
    {
        this.maxHealth = maxhealth;
        this.currentHealth = maxHealth;
        imageFill.fillAmount = 1;

    }
    private void SetHealthBar()
    {
        imageFill.fillAmount = currentHealth / maxHealth;
    }
}
