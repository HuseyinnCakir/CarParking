using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class araba : MonoBehaviour
{
    public bool ilerle;
    public Transform parent;
    public GameObject[] TekerIzleri;
    public GameManager _GameManager;
    public GameObject ParticlePoint;
    
    bool durmaNoktasi=false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ilerle)
        {
            transform.Translate(15f * Time.deltaTime * transform.forward);
        }
        if (!durmaNoktasi)
        {
            transform.Translate(7f * Time.deltaTime * transform.forward);
        }


    }
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.CompareTag("parking"))
        {
            arabaTeknikIslemleri();
            transform.parent = parent;
           
            _GameManager.YeniArabaGetir();
        }
       
        else if (collision.gameObject.CompareTag("araba"))
        {
            _GameManager.PatlamaEfekt.transform.position = ParticlePoint.transform.position;
            _GameManager.PatlamaEfekt.Play();
            arabaTeknikIslemleri();
            _GameManager.kaybettin();
        }
       
    }
    void arabaTeknikIslemleri()
    {
        ilerle = false;
        TekerIzleri[0].SetActive(false);
        TekerIzleri[1].SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DurusNoktasi"))
        {
            durmaNoktasi = true;
            
        }
        else if (other.CompareTag("elmas"))
        {
            other.gameObject.SetActive(false);
            _GameManager.ElmasSayisi++;
        }
        else if (other.CompareTag("OrtaGobek"))
        {
            _GameManager.PatlamaEfekt.transform.position = ParticlePoint.transform.position;
            _GameManager.PatlamaEfekt.Play();
            arabaTeknikIslemleri();
            _GameManager.kaybettin();
        }
        else if (other.CompareTag("OnParking"))
        {
            other.gameObject.GetComponent<OnParking>().ParkingActive();
        }
    }
}
