using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatText : MonoBehaviour
{
    [SerializeField] Text hp;
    
    public void OnInit(float hp)
    {
        this.hp.text ="-"+hp.ToString();
        Invoke(nameof(Despawn), 1f);
    }
    public void Despawn()
    {
        Destroy(gameObject);
    }
}
