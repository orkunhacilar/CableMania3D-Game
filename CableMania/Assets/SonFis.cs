using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonFis : MonoBehaviour
{
    public GameObject MevcutSoket;
    [SerializeField] private string SoketRengi;
    [SerializeField] private GameManager _GameManager;

    bool Secildi;
    bool PosDegistir;
    bool SoketOtur;

    GameObject HareketPozisyonu;

    public void SecimPozisyonu(GameObject GidilecekObje, GameObject Soket)
    {
        HareketPozisyonu = GidilecekObje;
        Secildi = true;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Secildi)
        {
            transform.position = Vector3.Lerp(transform.position, HareketPozisyonu.transform.position, .040f); // O haraket pozisyonuna git
            if(Vector3.Distance(transform.position, HareketPozisyonu.transform.position) < .010)
            {
                Secildi = false; // yakina gelince artik secildiyi false yapki. O gidis durdursun kendini.
            }
        }
    }
}
