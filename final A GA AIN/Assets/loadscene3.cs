using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadscene3 : MonoBehaviour
{
	void OnTriggerEnter2D(Collider2D other)
	{

		SceneManager.LoadScene("battle scene 2");
	}

}
