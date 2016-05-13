using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float Speed = 1;
	public bool AutoMove = false;
	public Vector3 StartPosition = new Vector3(140.0f,1.0f,0f);
	public Vector3 PortalPosition = new Vector3(200.0f,27.0f,0f);
	public Vector3[] ShipPath;
	int PathCounter = 0;
	GameObject SpeedText;
	GameObject ScoreText;
	GameObject Stats;
	bool InPortal = false;

	// Use this for initialization
	void Start () 
	{
		SpeedText = GameObject.Find ("Speed_L");
		ScoreText = GameObject.Find ("Score_L");
		Stats = GameObject.Find ("Light");
		//StartPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Stats.GetComponent<stats> ().GOver)
			Destroy (this);
		if (!AutoMove) {
			transform.position = new Vector3 (transform.position.x - Speed * Time.deltaTime,
			                                  transform.position.y, transform.position.z);
			System.Array.Resize<Vector3>(ref ShipPath, PathCounter+1);
			ShipPath[PathCounter] = transform.position;
			if (Input.GetAxis ("Vertical") != 0)
				Speed += Input.GetAxis ("Vertical") * 0.05f;
			if (Input.GetAxis ("Horizontal") != 0) 
				transform.position = new Vector3 (transform.position.x, transform.position.y, 
			                                 transform.position.z + Input.GetAxis ("Horizontal") * 0.05f);
			if (Speed < 1)
				Speed = 1;
			if (Speed > 25)
				Speed = 25;
			if(InPortal && (Speed>3))
				Speed = 3;
			Stats.GetComponent<stats>().Score += (Speed*Time.deltaTime);
			SpeedText.GetComponent<Text>().text = "Скорость: " + System.Math.Round(Speed) + " км/с";
			ScoreText.GetComponent<Text>().text = "Ваш счет: " + 
				System.Math.Round(Stats.GetComponent<stats>().Score) + " очков";
		} else 
		{
			transform.position = ShipPath[PathCounter];
		}
		PathCounter++;
	}

	void OnTriggerEnter(Collider other)
	{
		if (!AutoMove) 
		{
			if(!other.gameObject.GetComponent<portal>().AStart)
			{

				//PathCounter = 0;
				//StartPosition = new Vector3 (StartPosition.x - 3, 1, transform.position.z);
				transform.position = PortalPosition;
				GetComponent<Rigidbody>().useGravity = true;
				GetComponent<Rigidbody>().freezeRotation = false;
				InPortal = true;
			}
			else
			{
				GameObject ShadowShip;
				ShadowShip = Instantiate<GameObject> (Resources.Load<GameObject> ("ShadowShip"));
				ShadowShip.transform.position = StartPosition;
				System.Array.Resize<Vector3> (ref ShadowShip.GetComponent<PlayerController> ().ShipPath, 
				                              ShipPath.Length);
				int i = 0;
				foreach (Vector3 pp in ShipPath) {
					ShadowShip.GetComponent<PlayerController> ().ShipPath [i] = pp;
					i++;
				}
				PathCounter = 0;
				InPortal = false;
				GetComponent<Rigidbody>().useGravity = false;
				GetComponent<Rigidbody>().velocity = Vector3.zero;
				GetComponent<Rigidbody>().freezeRotation = true;
				StartPosition = new Vector3 (transform.position.x-3, 1, transform.position.z);
				transform.position = StartPosition;
				transform.rotation = Quaternion.Euler( new Vector3(0,270,0) );
			}
		} else
			PathCounter = 0;
	}

	void OnCollisionEnter (Collision col)
	{
		if (col.gameObject.GetComponent<PlayerController> () != null) 
		{
			Destroy(this);
			SpeedText.GetComponent<Text>().text = "Скорость: 0 км/с";
			Stats.GetComponent<stats> ().GOverText_Text = "Игра окончена! \r\n Нельзя изменить прошлое.";
			Stats.GetComponent<stats>().GOver = true;
		}
	}

}
