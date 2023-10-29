using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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
                    if (hit.collider.CompareTag("Mavi_Fis") || hit.collider.CompareTag("Sari_Fis") || hit.collider.CompareTag("Kirmizi_Fis"))
                    {
                        hit.collider.GetComponent<SonFis>().SecimPozisyonu(hit.collider.GetComponent<SonFis>().MevcutSoket.GetComponent<Soket>().HareketPozisyonu,
                        hit.collider.GetComponent<SonFis>().MevcutSoket);
                        //Yapacagimiz islerimiz
                        Debug.Log(hit.collider.gameObject.name);
                    }
                }
            }
        }
    }
}
    

