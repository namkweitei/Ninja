using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour
{
    [SerializeField] protected int speed = 1;
    [SerializeField] protected Vector3 direction = Vector3.right;
    [SerializeField] protected float timeDestroy = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(this.direction * this.speed * Time.deltaTime);
        timeDestroy -= Time.deltaTime;
        if (timeDestroy < 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground")  || collision.CompareTag("Enemy"))
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<Enemy>().Hit(10);
            }
            Destroy(gameObject);
        }
    }

}
