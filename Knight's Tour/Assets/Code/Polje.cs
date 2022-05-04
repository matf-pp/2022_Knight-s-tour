using UnityEngine;

public class Polje : MonoBehaviour
{
    public Pozicija Pozicija;
    public bool Poseceno;
	public GameObject XZnak;
	public bool PoljeJeObelezeno;

	public void ObeleziPoljeKaoPoseceno()
	{
		Poseceno = true;
		GetComponent<MeshRenderer>().enabled = true;
		GetComponent<BoxCollider>().enabled = false;
	}

	public static bool Postoji(int kolona, int red)
	{
		return kolona >= 0
			   && red >= 0
			   && kolona < 8
			   && red < 8;
	}

	public void UpaliX()
	{
		PoljeJeObelezeno = true;
		XZnak.SetActive(true);
	}

	public void SkiniX()
	{
		PoljeJeObelezeno = false;
		XZnak.SetActive(false);
	}
}
