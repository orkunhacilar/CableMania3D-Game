using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarpismaKontrol : MonoBehaviour
{
    public GameManager _GameManager;
    public int CarpismaIndex;

    private void Update()
    {

        //Bu satırın amacı, belirli bir 3D dünyasında, bir nesnenin pozisyonu ve boyutlarına dayalı olarak,
        //bu nesnenin içine düşen diğer nesneleri algılamak ve bu nesneleri bir dizi içinde saklamaktır.
        //Yani, Physics.OverlapBox fonksiyonu, belirtilen pozisyon ve boyuttaki kutu içinde bulunan diğer nesneleri tespit eder. Bu nesneleri Collider tipindeki bir diziye atar. 

        Collider[] Hitcoll = Physics.OverlapBox(transform.position, transform.localScale / 2, Quaternion.identity);

        for (int i = 0; i < Hitcoll.Length; i++)
        {
            if (Hitcoll[i].CompareTag("KabloParcasi"))
            {
                _GameManager.CarpismayiKontrolEt(CarpismaIndex,false); //kablo ile carpisiyosan false yolla
            }
            else
            {
                _GameManager.CarpismayiKontrolEt(CarpismaIndex, false); //carpismiyosan true yolla
            }

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale / 2);
    }
}
