using System;

[Serializable]
 public class Pozicija
 {
	public int Kolona;
	public int Red;

	public Pozicija() { }

	public Pozicija(int kolona, int red)
	{
		Kolona = kolona;
		Red = red;
	}

	public override string ToString()
	{
		return $"({Kolona}, {Red})";
	}
}


