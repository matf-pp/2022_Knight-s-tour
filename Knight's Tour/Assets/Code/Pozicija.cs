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
    public override int GetHashCode()
    {
        return Kolona * 8 + Red;
    }

    protected bool Equals(Pozicija other)
    {
        return Kolona == other.Kolona
               && Red == other.Red;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Pozicija)obj);
    }
}


