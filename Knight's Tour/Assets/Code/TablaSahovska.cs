using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = System.Random;


public class TablaSahovska : MonoBehaviour
{
    public Polje PoljePrefarb;
    public GameObject Konj;
    public Vector3 KrajnjaDestinacija;
    public float Brzina = 1;
    public bool KrajIgre;
    public Canvas KrajIgrePopup;
    public Button NovaIgraDugme;
    public Text Rezultat;

    private Polje[,] tabla = new Polje[8, 8];

    [SerializeField]
    private List<Pozicija> pozicijeKojeTrebaObici;
    private Pozicija pozicijaKonja;
    private DateTime vremePocetka;
    private TimeSpan ukupnoVreme = TimeSpan.FromMinutes(3);

    void Start()
    {
        NapraviTablu();
        OznaciPoljaKojaTrebaDaSePosete();
        NovaIgraDugme.onClick.AddListener(NapraviNovuIgru);

        pozicijaKonja = new Pozicija(0, 0);
        vremePocetka = DateTime.Now;

        PosetiPolje(DohvatiPoljeKonja());
    }

    public TimeSpan ProtekloVreme()
    {
        return DateTime.Now - vremePocetka;
    }

    private void NapraviTablu() 
    {

        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                var polje = Instantiate(PoljePrefarb);
                polje.Pozicija = new Pozicija(i, j);
                polje.transform.position = new Vector3(i, 0, j);
                tabla[i, j] = polje;
            }
        }

    }

    private void OznaciPoljaKojaTrebaDaSePosete()
    {
        pozicijeKojeTrebaObici = new List<Pozicija>();
        pozicijeKojeTrebaObici.Clear();

        var random = new Random((int)DateTime.Now.Ticks);

        var brojElementa = random.Next(5, 15);

        for (int i = 0; i < brojElementa; i++)
        {
            int kol = random.Next(0, 8);
            int vrsta = random.Next(0, 8);

            var novaPozicija = new Pozicija(kol, vrsta);
            int provera = 0;
            for (int j = 0; j < pozicijeKojeTrebaObici.Count; j++)
            {
                if (pozicijeKojeTrebaObici[j].Kolona == novaPozicija.Kolona && pozicijeKojeTrebaObici[j].Red == novaPozicija.Red)
                {
                    provera = 1;
                    Debug.Log("Random x2 " + novaPozicija);
                }

            }
            if (provera == 0)
            {
                pozicijeKojeTrebaObici.Add(novaPozicija);
                var polje = DohvatiPolje(novaPozicija);
                polje.UpaliX();
                Debug.Log("Oznaceno: " + novaPozicija);
                provera = 0;
            }

        }
    }


    public Polje DohvatiPolje(Pozicija pozicija)
    {
        return tabla[pozicija.Kolona, pozicija.Red];
    }

    void Update()
    {
        UpravljajKlikom();

        var trenutna = Konj.transform.position;
        var put = Brzina * Time.deltaTime;
        var nova = Vector3.MoveTowards(trenutna, KrajnjaDestinacija, put);
        Konj.transform.position = nova;

    }

    private void PosetiPolje(Polje polje)
    {
        pozicijaKonja = polje.Pozicija;
        polje.ObeleziPoljeKaoPoseceno();

        if (polje.PoljeJeObelezeno)
        {
            polje.SkiniX();
            IzbaciElementIzListe(polje.Pozicija);
        }

        ProveriDaLiJeKraj();
    }

    private void UpravljajKlikom()
    {
        if (KrajIgre)
            return;

        if (!Input.GetMouseButtonDown(0))
            return;

        var ray = Camera.main!.ScreenPointToRay(Input.mousePosition);

        if (!Physics.Raycast(ray, out var raycastHit, 100f))
            return;

        if (raycastHit.transform == null)
            return;

        var poljeKomponenta = raycastHit.transform.GetComponent<Polje>();

        if (!PravilaKretanja.DaLiKonjMozeDaSkoci(pozicijaKonja, poljeKomponenta.Pozicija))
        {
            Debug.Log("Nedostupno");
            return;
        }

        KrajnjaDestinacija = poljeKomponenta.transform.position;
        PosetiPolje(poljeKomponenta);
    }

    private void IzbaciElementIzListe(Pozicija pozicijaKojuTrebaIzbaciti)
    {
        for (var index = 0; index < pozicijeKojeTrebaObici.Count; index++)
        {
            var pozicija = pozicijeKojeTrebaObici[index];

            if (pozicija.Kolona == pozicijaKojuTrebaIzbaciti.Kolona && pozicija.Red == pozicijaKojuTrebaIzbaciti.Red)
            {
                pozicijeKojeTrebaObici.RemoveAt(index);
                return;
            }
        }
    }

    private static Pozicija DohvatiPolje(Vector3 pozicija)
    {
        var kolona = Mathf.RoundToInt(pozicija.x);
        var vrsta = Mathf.RoundToInt(pozicija.z);
        return new Pozicija(kolona, vrsta);
    }

    private Polje DohvatiPoljeKonja()
    {
        return DohvatiPolje(pozicijaKonja);
    }

    private void ProveriDaLiJeKraj()
    {
        if (pozicijeKojeTrebaObici.Count == 0)
        {
            PrikaziKraj(true);
            return;
        }

        if (DohvatiPreostaloVreme() == TimeSpan.Zero)
        {
            PrikaziKraj(false);
            return;
        }

        var dostupnaPolja = PravilaKretanja.DohvatiSvaPoljaNaKojaMozeDaSeSkoci(pozicijaKonja);

        foreach (var pozicija in dostupnaPolja)
        {
            var polje = tabla[pozicija.Kolona, pozicija.Red];
            if (!polje.Poseceno)
                return;
        }

        // Nema ne-posecenih polja
        PrikaziKraj(false);
    }

    public void PrikaziKraj(bool pobeda)
    {
        KrajIgre = true;

        if (pobeda)
        {
            Rezultat.text = "Cestitamo!";
        }
        else
        {
            Rezultat.text = "Probaj ponovo!";
        }

        KrajIgrePopup.gameObject.SetActive(true);
    }

    private void NapraviNovuIgru()
    {
        SceneManager.LoadScene("MainScene");
    }

    public TimeSpan DohvatiPreostaloVreme()
    {
        var preostaloVreme = ukupnoVreme - ProtekloVreme();

        return preostaloVreme > TimeSpan.Zero
            ? preostaloVreme
            : TimeSpan.Zero;
    }
}



