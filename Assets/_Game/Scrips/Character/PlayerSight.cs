using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSight : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Player player;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Enemy"){
            player.SetTarget(other.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Enemy"){
            player.SetTarget(null);
        }
    }
}
