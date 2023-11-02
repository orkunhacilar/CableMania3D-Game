using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    GameObject SeciliObje; 
    GameObject SeciliSoket;
    public bool HareketVar;

    [Header("----Level Ayarlari")]
    [SerializeField] private GameObject[] CarpismaKontrolObjeleri;
    [SerializeField] private GameObject[] Fisler;
    [SerializeField] private int  HedefSoketSayisi;
    [SerializeField] private List<bool> CarpismaDurumlari;

    int TamamlanmaSayisi;
    int CarpmaKontrolSayisi;
    SonFis _SonFis;

    [Header("----Diger Objeler")]
    [SerializeField] private GameObject[] Isiklar;

    [Header("----UI OBJELERI")]
    [SerializeField] private GameObject KontrolPaneli;
    [SerializeField] private TextMeshProUGUI KontrolText;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < HedefSoketSayisi-1; i++)
        {
            CarpismaDurumlari.Add(false);
        }
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
            } //objeleri ortaya cikar biraz zaman gecsin salinimlar bitince kontrol et kesisim var mi
            StartCoroutine(CarpismaVarmi());
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

                            _SonFis = hit.collider.GetComponent<SonFis>(); //Tıkladıgım son fişin içinde bulunan scripti aldım.

                            _SonFis.HareketEt(
                            "Secim",
                            _SonFis.MevcutSoket,
                            _SonFis.MevcutSoket.GetComponent<Soket>().HareketPozisyonu
                            ); //tikladigim fisin icinde bulunan mevcut soketin hareket pozisyonunu ve bizzat kendisini bu fonksyon ile yolladim.

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

                            _SonFis.HareketEt(
                             "PozisyonDegis",
                             hit.collider.gameObject,
                             _Soket.HareketPozisyonu); //yeni pozisyonlari yolla

                            _Soket.Doluluk = true; //yeni soketin doldugunu soyle

                            //Tekrar secilebilmesi adina
                            SeciliObje = null;
                            SeciliSoket = null;
                          
                        }else if (SeciliSoket == hit.collider.gameObject) // ayni sokete geri koymaya karar verdiysem
                        {
                            _SonFis.HareketEt(
                              "SoketeOtur",
                              hit.collider.gameObject);

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
          //  Debug.Log("KAZANDIN");
        }
        else
        {
           // Debug.Log("CARPMA VAR");
        }
    }

    IEnumerator CarpismaVarmi()
    {
        Isiklar[0].SetActive(false);
        Isiklar[1].SetActive(true);

        KontrolPaneli.SetActive(true);
        KontrolText.text = "Being Checked .... ";

       

        yield return new WaitForSeconds(4f);

        foreach (var item in CarpismaDurumlari)
        {
            if (item)
                CarpmaKontrolSayisi++;
        }
        if (CarpmaKontrolSayisi == CarpismaDurumlari.Count)
        {
            Isiklar[1].SetActive(false);
            Isiklar[2].SetActive(true);
            KontrolText.text = "WIN !!!";
            //Kazandin Paneli Cikacak.
        }

        else
        {
            Isiklar[1].SetActive(false);
            Isiklar[0].SetActive(true);
           
            KontrolText.text = "There is a collision in the cables ";
            Invoke("PaneliKapat", 2f);

            foreach (var item in CarpismaKontrolObjeleri)
            {
                item.SetActive(false);
            }

        }
        CarpmaKontrolSayisi = 0;
    }

    void PaneliKapat()
    {
        KontrolPaneli.SetActive(false);
    }
}
    

