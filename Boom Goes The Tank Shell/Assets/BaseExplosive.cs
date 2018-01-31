using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseExplosive : MonoBehaviour {

    [SerializeField] private float range;
    [SerializeField] private float damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "GroundCollider")
        {
            collision.gameObject.GetComponent<Ground>().DestroyAt(collision.contacts[0].point, range, damage);
            Destroy(gameObject);            
        }
    }
}
