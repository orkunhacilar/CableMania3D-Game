using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject SeciliObje; 
    GameObject SeciliSoket;
    public bool HareketVar;

    [Header("----Level Ayarlari")]
    public GameObject[] CarpismaKontrolObjeleri;
    public GameObject[] Fisler;
    public int HedefSoketSayisi;
    public bool[] CarpismaDurumlari;
    int TamamlanmaSayisi;


    // Start is called before the first frame update
    void Start()
    {

    }

    public void FisleriKontrolEt()
    {
        foreach (var item in Fisler)
        {
            if (item.GetComponent<SonFis>().MevcutSoket.name == item.GetComponent<SonFis>().SoketRengi)
            {
                TamamlanmaSayisi++;
            }
        }

        if(TamamlanmaSayisi == HedefSoketSayisi)
        {
            Debug.Log("Tüm soketler yerinde");
            foreach (var item in CarpismaKontrolObjeleri)
            {
                item.SetActive(true);
            }

        }
        else
        {
            Debug.Log("Eslesme Tamamlanmadi !");
        }
        TamamlanmaSayisi = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100)) //Kameramdan mouseumun oldugu noktaya ısın yollayıp hit ile alıyorum
            {
                if (hit.collider != null)
                {

                    // ## SON FIS         //FISI ICINDE BULUNDUGUM SOKETIN HARAKET POZISYONUNA TASIYAN YER.
                    if(SeciliObje==null && !HareketVar)
                    {

                        if (hit.collider.CompareTag("Mavi_Fis") || hit.collider.CompareTag("Sari_Fis") || hit.collider.CompareTag("Kirmizi_Fis"))
                        {

                            SonFis _SonFis = hit.collider.GetComponent<SonFis>(); //Tıkladıgım son fişin içinde bulunan scripti aldım.
                            _SonFis.SecimPozisyonu(_SonFis.MevcutSoket.GetComponent<Soket>().HareketPozisyonu, _SonFis.MevcutSoket); //tikladigim fisin icinde bulunan mevcut soketin hareket pozisyonunu ve bizzat kendisini bu fonksyon ile yolladim.

                            //Yapacagimiz islerimiz
                             Debug.Log(hit.collider.gameObject.name);

                            SeciliObje = hit.collider.gameObject; // SON TIKLADIGIM FISI SECILI OBJEYE ATTIM.
                            SeciliSoket = _SonFis.MevcutSoket;  // ILK ISLEM ALAN SOKETIMIDE SECILI SOKETE ATTIM.
                            HareketVar = true; // HARAKET VARI TRUE YAPTIM.
                        }
                    }
                    // ## SON FIS

                    // ## SOKET
                    if (hit.collider.CompareTag("Soket")){

                        if (SeciliObje != null && !hit.collider.GetComponent<Soket>().Doluluk && SeciliSoket != hit.collider.gameObject) // Farkli bir sokete gidiyorsam
                        {
                            SeciliSoket.GetComponent<Soket>().Doluluk = false; // Baska yere gidiyosam kendi soketimi bosalt
                            Soket _Soket = hit.collider.GetComponent<Soket>(); //yeni soketin scriptini _Soket olarak al
                            SeciliObje.GetComponent<SonFis>().PozisyonDegistir(_Soket.HareketPozisyonu, hit.collider.gameObject); //yeni pozisyonlari yolla
                            _Soket.Doluluk = true; //yeni soketin doldugunu soyle

                            //Tekrar secilebilmesi adina
                            SeciliObje = null;
                            SeciliSoket = null;
                          
                        }else if (SeciliSoket == hit.collider.gameObject) // ayni sokete geri koymaya karar verdiysem
                        {
                            SeciliObje.GetComponent<SonFis>().SoketeGeriGit(hit.collider.gameObject);

                            //Tekrar secilebilmesi adina
                            SeciliObje = null;
                            SeciliSoket = null;
                            HareketVar = true;
                        }
                        
                    }
                    // ## SOKET

                }
            }
        }
    }

    public void CarpismayiKontrolEt(int CarpismaIndex, bool durum)
    {
        CarpismaDurumlari[CarpismaIndex] = durum;

        if (CarpismaDurumlari[0] && CarpismaDurumlari[1])
        {
            Debug.Log("KAZANDIN");
        }
        else
        {
            Debug.Log("CARPMA VAR");
        }
    }
}
    

