using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float Speed = 5.0f;
    public GameObject cubo;
    private int cubos = 0;
    private int MaxCubos = 10;
    public LayerMask myLayerMask;
    public Text txtCubos;
    public Text txtTimeLeft;
    private float timeLeft = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void LateUpdate()
    {
        if (cubos == 0)
        {
            GerarCubos(MaxCubos);
            ApagarCubos();
        }
        txtCubos.text = cubos.ToString();
    }

    void ApagarCubos()
    {
        GameObject[] osCubos = GameObject.FindGameObjectsWithTag("Cubo");
        for (int i=0; i<osCubos.Length; i++)
        {
            Collider[] cols = Physics.OverlapSphere(osCubos[i].transform.position, 0.1f, myLayerMask);
            if(cols.Length>1)
            {
                Destroy(osCubos[i]);
            }
        }
        Debug.Log(osCubos.Length);
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
        {
            Destroy(other.gameObject);
            cubos--;
        }
    }
}
