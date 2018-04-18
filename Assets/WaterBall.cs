using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBall : MonoBehaviour
{
	[SerializeField]
	Vector2 firstBumpMapSpeed;

	[SerializeField]
	Vector2 secondBumpMapSpeed;

	Material m;

	// Use this for initialization
	void Start () {
		m = GetComponent<Renderer>().material;
	}
	
	// Update is called once per frame
	void Update ()
	{
		var main = m.GetTextureOffset("_MainTex");
		var second = m.GetTextureOffset("_DetailAlbedoMap");
		m.SetTextureOffset("_MainTex", main + firstBumpMapSpeed * Time.deltaTime);
		m.SetTextureOffset("_DetailAlbedoMap", second + secondBumpMapSpeed * Time.deltaTime);
	}
}
