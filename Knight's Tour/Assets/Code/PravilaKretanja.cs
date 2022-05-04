using System;
using UnityEngine;
using System.Collections.Generic;

public class PravilaKretanja
{
	public static bool DaLiKonjMozeDaSkoci(Pozicija pocetno, Pozicija krajnje)
	{
		var diffKolona = Mathf.Abs(pocetno.Kolona - krajnje.Kolona);
		var diffRed = Mathf.Abs(pocetno.Red - krajnje.Red);

		return diffKolona == 1 && diffRed == 2
			   || diffKolona == 2 && diffRed == 1;
	}

	public static List<Pozicija> DohvatiSvaPoljaNaKojaMozeDaSeSkoci(Pozicija trenutnaPozicija)
	{
		var rezultat = new List<Pozicija>();

		var kolona = trenutnaPozicija.Kolona;
		var red = trenutnaPozicija.Red;

		var novaKolona = kolona - 2;
		var noviRed = red - 1;

		if (Polje.Postoji(novaKolona, noviRed))
			rezultat.Add(new Pozicija(novaKolona, noviRed));

		novaKolona = kolona - 2;
		noviRed = red + 1;

		if (Polje.Postoji(novaKolona, noviRed))
			rezultat.Add(new Pozicija(novaKolona, noviRed));

		novaKolona = kolona + 2;
		noviRed = red - 1;

		if (Polje.Postoji(novaKolona, noviRed))
			rezultat.Add(new Pozicija(novaKolona, noviRed));

		novaKolona = kolona + 2;
		noviRed = red + 1;

		if (Polje.Postoji(novaKolona, noviRed))
			rezultat.Add(new Pozicija(novaKolona, noviRed));

		novaKolona = kolona + 1;
		noviRed = red + 2;

		if (Polje.Postoji(novaKolona, noviRed))
			rezultat.Add(new Pozicija(novaKolona, noviRed));

		novaKolona = kolona + 1;
		noviRed = red - 2;

		if (Polje.Postoji(novaKolona, noviRed))
			rezultat.Add(new Pozicija(novaKolona, noviRed));

		novaKolona = kolona - 1;
		noviRed = red + 2;

		if (Polje.Postoji(novaKolona, noviRed))
			rezultat.Add(new Pozicija(novaKolona, noviRed));

		novaKolona = kolona - 1;
		noviRed = red - 2;

		if (Polje.Postoji(novaKolona, noviRed))
			rezultat.Add(new Pozicija(novaKolona, noviRed));

		return rezultat;
	}
}
