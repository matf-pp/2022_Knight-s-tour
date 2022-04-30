using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TablaSahovska : MonoBehaviour
{
    public GameObject BeloPoljePrefarb;
    public GameObject CrnoPoljePrefarb;
    // Start is called before the first frame update
    void Start()
    {
        NapraviTablu(); 
    }

    private void NapraviTablu() 
    {
        // instanciramo i pozicioniramo sva polja - odredjene boje

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


    // Update is called once per frame
    void Update()
    {
        
    }
}
