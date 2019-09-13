using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

	public Animator anim;
	public bool damage, attack;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	void Update()
	{

		if (Input.GetKeyDown("space") && anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && damage == false && !anim.GetBool("PressKey"))
		{

			attack = true;
			anim.SetBool("AttackKey", true);
		}

		else if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
		{
			attack = false;
			anim.SetBool("AttackKey", false);
		}


		if (damage)
		{
			anim.SetBool("DamageKey", true);

		}
		else
		{
			anim.SetBool("DamageKey", false);
		}

	}
}
