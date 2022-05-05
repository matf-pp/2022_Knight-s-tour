using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartGamePopup : MonoBehaviour
{
    public Button SlobodnoKretanjeDugme;
    public Button KretanjeSaZadacimaDugme;
    public Button IzlazIzAplikacijeDugme;

    public TipIgre TipIgre { get; private set; } = TipIgre.NijeIzabran;

    public void Start()
    {
        SlobodnoKretanjeDugme.onClick.AddListener(KliknutoNaSlobodnoKretanje);
        KretanjeSaZadacimaDugme.onClick.AddListener(KiknutoNaKretanjeSaZadacima);
        IzlazIzAplikacijeDugme.onClick.AddListener(IzlazIzAplikacije);
    }

    private void IzlazIzAplikacije()
    {
        Application.Quit();
    }

    private void KiknutoNaKretanjeSaZadacima()
    {
        TipIgre = TipIgre.KretanjeSaZadacima;
    }

    private void KliknutoNaSlobodnoKretanje()
    {
        TipIgre = TipIgre.SlobodnoKretanje;
    }

    public IEnumerator CekajDaSeZatvori()
    {
        yield return new WaitUntil(() => TipIgre != TipIgre.NijeIzabran);
        gameObject.SetActive(false);
    }
}
