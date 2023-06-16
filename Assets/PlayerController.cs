using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float Speed = 5.0f;
    public GameObject cubo;
    private int cubos = 0;
    private int MaxCubos = 100;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (cubos == 0)
            GerarCubos(MaxCubos);
    }

    void GerarCubos(int quantos)
    {
        for(int i=0;i<quantos;i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-9.0f, 9.0f), 4.5f, Random.Range(-9.0f, 9.0f));
            Instantiate(cubo, spawnPos, Quaternion.identity);
        }
        cubos = quantos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");
        Vector3 movimento = new Vector3(moveH, 0.0f, moveV);
        rb.AddForce(movimento * Speed);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cubo"))
            Destroy(other.gameObject);
    }
}
