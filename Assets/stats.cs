using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class stats : MonoBehaviour {

	public float Score = 0;
	public bool GOver = false;
	public GameObject GOverText;
	public string GOverText_Text;

	// Use this for initialization
	void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () 
	{
		GOverText.GetComponent<Text> ().text = GOverText_Text;
		GOverText.SetActive (GOver);
	}

	public void Quit()
	{
		Application.Quit ();
	}

	public void Restart()
	{
		Score = 0;
		foreach (GameObject GO in GameObject.FindGameObjectsWithTag("Ship"))
			Destroy (GO);
		GOver = false;
		GameObject Ship;
		Ship = Instantiate<GameObject> (Resources.Load<GameObject> ("Player"));
		Ship.transform.position = Ship.GetComponent<PlayerController> ().StartPosition;
	}
}
