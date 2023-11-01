using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonFis : MonoBehaviour
{
    public GameObject MevcutSoket;
    public string SoketRengi;
    public GameManager _GameManager;

    bool Secildi;
    bool PosDegistir;
    bool SoketOtur;

    GameObject HareketPozisyonu;
    GameObject SoketinKendisi;

    public void SecimPozisyonu(GameObject GidilecekObje, GameObject Soket) //Soketi havaya kaldirma icin deger toplama
    {
        HareketPozisyonu = GidilecekObje; //Takili oldugum soketin hareket poz alip bu scriptte kullanabilmek adina HareketPozisyonuna attim.
        Secildi = true; // Ve secildiyi true yaptim.
    }

    public void PozisyonDegistir(GameObject GidilecekObje, GameObject Soket)
    {
        SoketinKendisi = Soket; // Gidilecek yeni soketide SoketinKendisine atadim.
        HareketPozisyonu = GidilecekObje; // Yeni gidilecek soketin haraket pozisyonunu artik Haraket poz atadim.
        PosDegistir = true; // Pos Degistiri True Yaptim.
    }

    public void SoketeGeriGit(GameObject Soket)
    {
        SoketinKendisi = Soket; // Gidilecek yeni soketide SoketinKendisine atadim. // Burda ayni soket mantiken
       
        SoketOtur = true; // Pos Degistiri True Yaptim.
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Secildi) //True gelirse
        {
            transform.position = Vector3.Lerp(transform.position, HareketPozisyonu.transform.position, .040f); // bu Scriptin Fisini icinde bulundugu soketin haraket pozisyonuna gotur.
            if(Vector3.Distance(transform.position, HareketPozisyonu.transform.position) < .010) //Eger birbirlerine cok yakinlarsa bunu durdur.
            {
                Secildi = false; // yakina gelince artik secildiyi false yapki. O gidis durdursun kendini.
            }
        }

        if (PosDegistir)
        {
            transform.position = Vector3.Lerp(transform.position, HareketPozisyonu.transform.position, .040f); // icinde bulundugum fis yeni soketin yeni haraket noktasina gitsin artik diyorum.
            if (Vector3.Distance(transform.position, HareketPozisyonu.transform.position) < .010) //oraya geldikten sonra
            {
                PosDegistir = false; //bu islem bitmistir posdegistir false
                SoketOtur = true; //sokete otursun artik diyorum.
            }
        }

        if (SoketOtur)
        {
            transform.position = Vector3.Lerp(transform.position, SoketinKendisi.transform.position, .040f); // icinde bulundugum fis soketin kendi pozisyonuna gitsin otursun
            if (Vector3.Distance(transform.position, SoketinKendisi.transform.position) < .010) // mesafe az kalinca yani varinca
            {
                SoketOtur = false; // sokete oturdugu icin bu false
                _GameManager.HareketVar = false; //Hareket bitti burasini false 
                MevcutSoket = SoketinKendisi; //Soketin kendisi yeni soketimdi. Simdi guncelleyelim ve Mevcutsoketimi soketinkendisi yapalim.
                _GameManager.FisleriKontrolEt();
            }
        }
    }
}
