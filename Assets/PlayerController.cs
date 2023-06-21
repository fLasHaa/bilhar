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
    [SerializeField] private float timeLeft = 20.0f;
    public GameObject txtGameOver;
    private bool Playing;
    private int Contar = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Playing = true;
    }

    private void LateUpdate()
    {
        if (cubos == 0)
        {
            GerarCubos(MaxCubos + Contar);
            ApagarCubos();
            Contar = Contar + 5;
            
            //timeLeft = cubos;
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
        //cubos = osCubos.Length;
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
        if (Playing == true)
        {
            float moveH = Input.GetAxis("Horizontal");
            float moveV = Input.GetAxis("Vertical");
            Vector3 movimento = new Vector3(moveH, 0.0f, moveV);
            rb.AddForce(movimento * Speed);
            timeLeft -= Time.deltaTime;
            txtTimeLeft.text = Mathf.Floor(timeLeft).ToString();
            timeLeft = timeLeft + 10.0f;
            if (timeLeft <= 0)
            {
                GameObject[] osCubos = GameObject.FindGameObjectsWithTag("Cubo");
                for (int i = 0; i < osCubos.Length; i++)
                {
                    Destroy(osCubos[i]);
                }
                txtGameOver.SetActive(true);
                txtTimeLeft.text = "0";
                Playing = false;


                //GameOver
            }
        }
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
