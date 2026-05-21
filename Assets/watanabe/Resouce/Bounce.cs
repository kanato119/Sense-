using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
	public float force = 10f;				//吹き飛ばす力
	public float stunTime = 0.5f;			//吹き飛んだ後のスタン時間
	private Vector3 hitDir;					//飛ばす方向の

	void OnCollisionEnter(Collision collision)
	{
		foreach (ContactPoint contact in collision.contacts)
		{
			
			if (collision.gameObject.tag == "Player")		//プレイヤータグがついているオブジェクトだけに反応
			{
				hitDir = contact.normal;
				collision.gameObject.GetComponent<CharacterControls>().HitPlayer(-hitDir * force, stunTime);	//ぶつかったら飛ばす
				return;
			}
		}
		
	}
}
