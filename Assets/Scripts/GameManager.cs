using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("ARABA AYARLARI")]
    public GameObject[] arabalar;
    
    public int KacArabaOlsun;
    int aktifIndex=0;
    [Header("PLATFORM AYARLARI")]
    public GameObject platform1;
    public GameObject platform2;
    bool platformDonus;

    public float[] DonusHizlari;
    [Header("CANVAS AYARLARI")]
    public GameObject[] arabaCanvasSprites;
    public Sprite arabaParkGorsel;
    public TextMeshProUGUI[] textler;
    public GameObject[] panellerim;
    [Header("LEVEL AYARLARI")]
    public int ElmasSayisi;
    int kalanaracsayisi;
    public ParticleSystem PatlamaEfekt;
    bool Touchkilit;
    void Start()
    {
        Touchkilit = true;
        platformDonus = true;
        DegerleriKontrolEt();
        kalanaracsayisi = KacArabaOlsun;
        for (int i = 0; i < KacArabaOlsun; i++)
        {
            arabaCanvasSprites[i].SetActive(true);
        }
    }
    public void YeniArabaGetir()
    {
       
        kalanaracsayisi--;
        Debug.Log(kalanaracsayisi);
        if (aktifIndex< KacArabaOlsun)
        {
            arabalar[aktifIndex].SetActive(true);
        }
        else
        {
            kazandin();
        }
        
        arabaCanvasSprites[aktifIndex-1].GetComponent<Image>().sprite = arabaParkGorsel;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                if (Touchkilit)
                {
                    panellerim[0].SetActive(false);
                    panellerim[3].SetActive(true);
                    Touchkilit = false;
                }
                else
                {
                    arabalar[aktifIndex].GetComponent<araba>().ilerle = true;
                    aktifIndex++;
                }
            }
        }
        
        if(platformDonus)
        platform1.transform.Rotate(new Vector3(0, 0, DonusHizlari[0]), Space.Self);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            arabalar[aktifIndex].GetComponent<araba>().ilerle = true;
            aktifIndex++;
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            panellerim[0].SetActive(false);
            panellerim[3].SetActive(true);
        }
    }
    public void kaybettin()
    {
        platformDonus = false;
        textler[6].text = PlayerPrefs.GetInt("elmas").ToString();
        textler[7].text = SceneManager.GetActiveScene().name;
        textler[8].text = (KacArabaOlsun - kalanaracsayisi).ToString();
        textler[9].text = ElmasSayisi.ToString();
        panellerim[3].SetActive(false);
        panellerim[1].SetActive(true);
    }
    void kazandin()
    {
        PlayerPrefs.SetInt("elmas", PlayerPrefs.GetInt("elmas") + ElmasSayisi);
        textler[2].text = PlayerPrefs.GetInt("elmas").ToString();
        textler[3].text = SceneManager.GetActiveScene().name;
        textler[4].text = (KacArabaOlsun - kalanaracsayisi).ToString();
        textler[5].text = ElmasSayisi.ToString();
        panellerim[3].SetActive(false);
        panellerim[2].SetActive(true);
    }
    public void replay()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void nextLevel()
    {
        PlayerPrefs.SetInt("level", SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    void DegerleriKontrolEt()
    {
        if (!PlayerPrefs.HasKey("elmas"))
        {
            PlayerPrefs.SetInt("elmas", 0);
            PlayerPrefs.SetInt("level", 1);
        }
        textler[0].text = PlayerPrefs.GetInt("elmas").ToString();
        textler[1].text = SceneManager.GetActiveScene().name;
    }
    
}
