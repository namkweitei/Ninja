using UnityEngine;

public class Fruits : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player")
        {
            other.GetComponent<Player>().AddFruit();
            Destroy(gameObject);
        }
    }
}
