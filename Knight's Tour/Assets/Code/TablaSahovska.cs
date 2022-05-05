using System;
using System.Collections;
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
    public StartGamePopup StartGamePopup;
    public Button NovaIgraDugme;
    public Button UgasiIgruDugme;
    public Text Rezultat;
    public StanjeIgre StanjeIgre;

    private Polje[,] tabla = new Polje[8, 8];

    private HashSet<Pozicija> pozicijeKojeTrebaObici;
    private Pozicija pozicijaKonja;
    private DateTime vremePocetka;
    private TimeSpan ukupnoVreme = TimeSpan.FromMinutes(3);

    public IEnumerator Start()
    {
        StanjeIgre = StanjeIgre.NijePokrenuta;
        yield return StartGamePopup.CekajDaSeZatvori();
        PokreniIgru();
    }

    private void PokreniIgru()
    {
        NapraviTablu();
        OznaciPoljaKojaTrebaDaSePosete();
        NovaIgraDugme.onClick.AddListener(NapraviNovuIgru);
        UgasiIgruDugme.onClick.AddListener(UgasiIgru);

        pozicijaKonja = new Pozicija(0, 0);
        vremePocetka = DateTime.Now;

        PodesiPreostaloVreme();
        PosetiPolje(DohvatiPoljeKonja());
        StanjeIgre = StanjeIgre.UToku;
    }

    private void UgasiIgru()
    {
        PrikaziKraj(false);
    }

    private void PodesiPreostaloVreme()
    {
        ukupnoVreme = StartGamePopup.TipIgre == TipIgre.SlobodnoKretanje
            ? TimeSpan.FromMinutes(10)
            : TimeSpan.FromMinutes(3);
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
        var tipIgre = StartGamePopup.TipIgre;

        // Generisi pozicije
        pozicijeKojeTrebaObici = tipIgre == TipIgre.SlobodnoKretanje
            ? GenerisiSvePozicije()
            : GenerisiNasumicnePozicije();

        // Obelezi polja
        if (tipIgre == TipIgre.KretanjeSaZadacima)
        {
            foreach (var pozicija in pozicijeKojeTrebaObici)
            {
                var polje = DohvatiPolje(pozicija);
                polje.UpaliX();
            }
        }
    }

    private static HashSet<Pozicija> GenerisiSvePozicije()
    {
        var rezultat = new HashSet<Pozicija>();

        for (var i = 0; i < 8; i++)
        {
            for (var j = 0; j < 8; j++)
            {
                rezultat.Add(new Pozicija(i, j));
            }
        }

        return rezultat;
    }

    private static HashSet<Pozicija> GenerisiNasumicnePozicije()
    {
        var pozicije = new HashSet<Pozicija>();

        var random = new Random((int)DateTime.Now.Ticks);

        var brojElementa = random.Next(3, 10);

        for (int i = 0; i < brojElementa; i++)
        {
            int kol = random.Next(0, 8);
            int vrsta = random.Next(0, 8);

            var novaPozicija = new Pozicija(kol, vrsta);
            pozicije.Add(novaPozicija);
        }

        return pozicije;
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
            pozicijeKojeTrebaObici.Remove(polje.Pozicija);
        }

        ProveriDaLiJeKraj();
    }

    private void UpravljajKlikom()
    {
        if(StanjeIgre != StanjeIgre.UToku)
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



