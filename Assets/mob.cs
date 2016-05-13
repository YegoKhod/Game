using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class mob : MonoBehaviour {

	GameObject SpeedText;
	GameObject ScoreText;
	GameObject Stats;

	// Use this for initialization
	void Start () 
	{
		SpeedText = GameObject.Find ("Speed_L");
		ScoreText = GameObject.Find ("Score_L");
		Stats = GameObject.Find ("Light");
	}

	void OnCollisionEnter (Collision col)
	{
		Destroy (col.gameObject);
		SpeedText.GetComponent<Text>().text = "Скорость: 0 км/с";
		Stats.GetComponent<stats> ().GOverText_Text = "Игра окончена! \r\n Вы врезались в препятствие.";
		Stats.GetComponent<stats>().GOver = true;
	}
}
