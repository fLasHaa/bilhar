using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApagarCubo : MonoBehaviour
{
    void OnCollisionEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Cubo"))
            Destroy(collision.gameObject);
    }
}
