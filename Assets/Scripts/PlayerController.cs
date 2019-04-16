using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;

[RequireComponent(typeof (Moves))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject canon;
    private Moves moves;
    private AudioSource audioSrc;
    public AudioClip leftShot;
    public AudioClip rightShot;
    public int gun;
    public int rocket;
    private HP hp;

    public TextMeshProUGUI Rockets;
    public TextMeshProUGUI Guns;
    public TextMeshProUGUI Health;
    
    private void Start()
    {
        gun = 20;
        rocket = 5;
        hp = GetComponent<HP>();
        audioSrc = GetComponent<AudioSource>();
        speed = 5f;
        moves = GetComponent<Moves>();
        Rockets.text = "Rockets: " + rocket.ToString();
        Guns.text = "Guns: " + gun.ToString();
        Health.text = "Health: " + hp.hp.ToString();
        StartCoroutine(refreshRocket());
        StartCoroutine(refreshGun());
    }

    private void Update()
    {
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxisRaw("Vertical");
        Vector3 moveZ = transform.forward * zMove;
        Vector3 velocity = moveZ.normalized * speed;
        Vector3 povorot = new Vector3(0f, xMove, 0f) * speed;
        
        moves.Rotate(povorot);
        moves.Move(velocity);

        if (Input.GetKey(KeyCode.LeftShift))
            speed = 10f;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            speed = 5f;
        Shots();
        InvokeRepeating("refreshRocket", 10, 10);
        InvokeRepeating("refreshGun", 5, 5);
        Rockets.text = "Rockets: " + rocket.ToString();
        Guns.text = "Guns: " + gun.ToString();
        Health.text = "Health: " + hp.hp.ToString();
        if (hp.hp <= 0)
        {
            Application.LoadLevel("SampleScene");
        }
    }

    void Shots()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0) && rocket > 0)
        {
            Ray ray = new Ray(canon.transform.position, canon.transform.forward * 1000f);
            RaycastHit hit;
            audioSrc.PlayOneShot(leftShot, 1f);
            Debug.DrawRay(canon.transform.position, canon.transform.forward * 1000f, Color.red);
            rocket--;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "tank")
                {
                    hit.collider.GetComponent<HP>().hp -= 20;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Mouse1) && gun > 0)
        {
            Ray ray = new Ray(canon.transform.position, canon.transform.forward * 100f);
            RaycastHit hit;
            Debug.DrawRay(canon.transform.position, canon.transform.forward * 100f, Color.red);
            audioSrc.PlayOneShot(rightShot, 1f);
            gun--;
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.collider.tag == "tank"){
                    hit.collider.GetComponent<HP>().hp -= 10;
                }
            }
        }
    }

    IEnumerator refreshRocket()
    {
        if (rocket < 5)
            rocket++;
        if (rocket > 5)
            rocket = 5;
        
        yield return new WaitForSeconds(10f);
        StartCoroutine(refreshRocket());
    }

    IEnumerator refreshGun()
    {
        if (gun < 20)
            gun++;
        if (gun > 20)
            gun = 20;
        
        yield return new WaitForSeconds(5f);
        StartCoroutine(refreshGun());
    }
}
