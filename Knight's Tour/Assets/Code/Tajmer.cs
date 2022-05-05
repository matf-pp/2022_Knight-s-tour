using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Tajmer : MonoBehaviour
{
    public TablaSahovska Tabla;
    public Text PreostaloVremeLabela;

    public IEnumerator Start()
    {
        yield return new WaitUntil(() => Tabla.StanjeIgre == StanjeIgre.UToku);

        while (true)
        {
            yield return new WaitForSeconds(1);
            var preostaloVreme = Tabla.DohvatiPreostaloVreme();
            PreostaloVremeLabela.text = preostaloVreme.ToString("mm\\:ss");

            if (preostaloVreme == TimeSpan.Zero) break;
        }

        Tabla.PrikaziKraj(false);
    }
}
