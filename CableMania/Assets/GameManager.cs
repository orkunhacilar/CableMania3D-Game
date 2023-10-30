using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject SeciliObje; 
    GameObject SeciliSoket;
    public bool HareketVar;


    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100)) //Kameramdan mouseumun oldugu noktaya ısın yollayıp hit ile alıyorum
            {
                if (hit.collider != null)
                {

                    // ## SON FIS
                    if(SeciliObje!=null && !HareketVar)
                    {

                        if (hit.collider.CompareTag("Mavi_Fis") || hit.collider.CompareTag("Sari_Fis") || hit.collider.CompareTag("Kirmizi_Fis"))
                        {

                            SonFis _SonFis = hit.collider.GetComponent<SonFis>();
                            _SonFis.SecimPozisyonu(_SonFis.MevcutSoket.GetComponent<Soket>().HareketPozisyonu, _SonFis.MevcutSoket);

                            //Yapacagimiz islerimiz
                            // Debug.Log(hit.collider.gameObject.name);

                            SeciliObje = hit.collider.gameObject;
                            SeciliSoket = _SonFis.MevcutSoket;
                            HareketVar = true;
                        }
                    }
                    // ## SON FIS

                    // ## SOKET
                    if (hit.collider.CompareTag("Soket")){
                        if (SeciliObje != null && !hit.collider.GetComponent<Soket>().Doluluk && SeciliSoket != hit.collider.gameObject) // Farkli bir sokete gidiyorsam
                        {
                            SeciliSoket.GetComponent<Soket>().Doluluk = false;
                            Soket _Soket = hit.collider.GetComponent<Soket>();
                            SeciliObje.GetComponent<SonFis>().PozisyonDegistir(_Soket.HareketPozisyonu, hit.collider.gameObject);
                            _Soket.Doluluk = true;


                            SeciliObje = null;
                            SeciliSoket = null;
                          
                        }
                        
                    }
                    // ## SOKET

                }
            }
        }
    }
}
    

