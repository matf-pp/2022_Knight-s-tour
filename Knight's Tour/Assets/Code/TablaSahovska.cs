using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TablaSahovska : MonoBehaviour
{
    public GameObject BeloPoljePrefarb;
    public GameObject CrnoPoljePrefarb;
    
    public GameObject Konj;

    public Vector3 KrajnjaDestinacija;
    public float Brzina = 1;

    void Start()
    {
        NapraviTablu(); 
    }

    private void NapraviTablu() 
    {

        for (int i = 0; i < 8; i++) 
        { 
            for (int j = 0; j < 8; j++)
            {
                var prefarb = (i + j) % 2 == 0 ? BeloPoljePrefarb : CrnoPoljePrefarb;
                var polje = Instantiate(prefarb);
                polje.transform.position = new Vector3(i, 0, j);
            }
        }

    }

    void Update()
    {
        UpravljajKlikom();

        var trenutna = Konj.transform.position;
        var put = Brzina * Time.deltaTime;
        var nova = Vector3.MoveTowards(trenutna, KrajnjaDestinacija, put);
        Konj.transform.position = nova;

    }

    private void UpravljajKlikom()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        var ray = Camera.main!.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out var raycastHit, 100f))
            return;

        if (raycastHit.transform != null)
        {
            var pozicija = raycastHit.transform.position;
            var kolona = Mathf.RoundToInt(pozicija.x);
            var vrsta = Mathf.RoundToInt(pozicija.z);
            KrajnjaDestinacija = pozicija;
            Debug.Log($"Pogodak! Pozicija: {kolona}, {vrsta}", raycastHit.transform);
        }
    }
}
