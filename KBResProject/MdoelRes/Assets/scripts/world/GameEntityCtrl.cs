
using UnityEngine;
using KBEngine;
using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;

public class GameEntityCtrl : MonoBehaviour 
{
	public SceneEntityObject seo = null;
	
	/*
		1: idle (空闲状态)
		2: combatIdle (战斗状态时的空闲状态)
		3: walk(走)
		4: run(跑)
		5: jump(跳)
		6: attack1(攻击动作1)
		7: attack2(攻击动作2)
		8: damage(受击)
		9: dead(死亡)
		10: intonate(吟唱)
		11: spell1(施法术动作1)
		12: spell2(施法术动作2)
		13: spellBuff(施放buff动作)
		14: block(格挡)
	*/
	public string ani_idle = ""; 
	public string ani_combatIdle = ""; 
	public string ani_walk = ""; 
	public string ani_run = "";  
	public string ani_jump = ""; 
	public string ani_attack1 = ""; 
	public string ani_attack2 = ""; 
	public string ani_damage = ""; 
	public string ani_dead = ""; 
	public string ani_intonate = ""; 
	public string ani_spell1 = ""; 
	public string ani_spell2 = ""; 
	public string ani_spellBuff = ""; 
	public string ani_block = ""; 
	
	string current_ani = ""; 
	
	public bool isPlayer = false;
	double lastUpdateTime = 0.0;
	
	public float yoffset = 15.0f;
	
	void Awake ()   
	{
		lastUpdateTime = Time.time;
	}
	
	void Start() 
	{
		if(seo == null)
			return;
		
		if(ani_run == "")
			ani_run = ani_walk;
		
		if(ani_walk == "")
			ani_walk = ani_run;
		
		if(ani_attack2 == "")
			ani_attack2 = ani_attack1;
		
		if(ani_idle != "")
			GetComponent<Animation>()[ani_idle].wrapMode = WrapMode.Loop;
		
		if(ani_combatIdle != "")
			GetComponent<Animation>()[ani_combatIdle].wrapMode = WrapMode.Loop;
		
		if(ani_walk != "")
			GetComponent<Animation>()[ani_walk].wrapMode = WrapMode.Loop;
		
		if(ani_run != "")
			GetComponent<Animation>()[ani_run].wrapMode = WrapMode.Loop;

		if(ani_jump != "")
			GetComponent<Animation>()[ani_jump].wrapMode = WrapMode.Once;
		
		if(ani_attack2 != "")
			GetComponent<Animation>()[ani_attack2].wrapMode = WrapMode.Once;
		
		if(ani_attack1 != "")
			GetComponent<Animation>()[ani_attack1].wrapMode = WrapMode.Once;
		
		if(ani_damage != "")
			GetComponent<Animation>()[ani_damage].wrapMode = WrapMode.Once;
		
		if(ani_dead != "")
			GetComponent<Animation>()[ani_dead].wrapMode = WrapMode.ClampForever;
		
		if(ani_intonate != "")
			GetComponent<Animation>()[ani_intonate].wrapMode = WrapMode.ClampForever;
		
		if(ani_spell1 != "")
			GetComponent<Animation>()[ani_spell1].wrapMode = WrapMode.Once;
		
		if(ani_spell2 != "")
			GetComponent<Animation>()[ani_spell2].wrapMode = WrapMode.Once;
		
		if(ani_spellBuff != "")
			GetComponent<Animation>()[ani_spellBuff].wrapMode = WrapMode.Once;
		
		if(ani_block != "")
			GetComponent<Animation>()[ani_block].wrapMode = WrapMode.Once;
		
		current_ani = ani_idle;
		if(seo.state == 1)
			playAnimation(ani_dead);
		else
		{
			bool fly = ("20002001.unity3d" == seo.name);
			Vector3 pos = seo.position;
			bool ishit = Common.calcPositionY(pos, out pos.y, fly);
			// 为了demo表现， 暂时写死
			if (fly)
			{
				pos.y += yoffset;
			}

			seo.position = pos;
		}
	}
	
	public void playAnimation(string ani)
	{
		if(seo.state == 1)
			ani = ani_dead;
		
		if(ani == "")
			return;
		
		if(GetComponent<Animation>().isPlaying == true)
		{
			if(current_ani != ani && current_ani != ani_jump)
				GetComponent<Animation>().Stop(ani);
			else
				return;
		}

		current_ani = ani;
		GetComponent<Animation>().Play(ani);
	}

	public void stopPlayAnimation(string ani)
	{
		if(ani == "")
			ani = current_ani;
		
		if(ani == "jump")
			ani = ani_jump;
		
		if(GetComponent<Animation>().isPlaying == true && current_ani == ani)
			return;
		
		current_ani = "";
		GetComponent<Animation>().Stop(ani);
	}
			
	public void playJumpAnimation()
	{
		playAnimation(ani_jump);
	}
	
	public void playDeadAnimation()
	{
		playAnimation(ani_dead);
	}
	
	public void playAttackAnimation()
	{
		if(ani_jump == current_ani ||
			ani_attack1 == current_ani || 
			ani_attack2 == current_ani ||
			ani_damage == current_ani ||
			ani_intonate == current_ani ||
			ani_spell1 == current_ani ||
			ani_spell2 == current_ani ||
			ani_block == current_ani)
		{
			if(GetComponent<Animation>().isPlaying == true)
				return;
		}
			
		string ani;
		if(UnityEngine.Random.Range(0, 2) == 0)
			ani = ani_attack1;
		else
			ani = ani_attack2;
		
		stopPlayAnimation("");
		playAnimation(ani);
	}

	public void playDamageAnimation()
	{
		stopPlayAnimation("");
		playAnimation(ani_damage);
	}
	
	public void playIdleAnimation()
	{
		if(ani_attack1 == current_ani || 
			ani_attack2 == current_ani ||
			ani_damage == current_ani ||
			ani_intonate == current_ani ||
			ani_spell1 == current_ani ||
			ani_spell2 == current_ani ||
			ani_block == current_ani ||
			ani_jump == current_ani)
		{
			if(GetComponent<Animation>().isPlaying == true)
				return;
		}
		
		if(ani_idle == current_ani || 
			ani_combatIdle == current_ani)
		{
			if(GetComponent<Animation>().isPlaying == true)
				return;
		}
			
		string ani;
		if(seo.state == 0)
			ani = ani_idle;
		else
			ani = ani_combatIdle;
		
		playAnimation(ani);
	}
	
	public void playWalkAnimation()
	{
		playAnimation(ani_walk);
	}

	public void playRunAnimation()
	{
		playAnimation(ani_run);
	}
	
	public void set_moveSpeed(float v)
	{
		v /= 10f;
		if(ani_walk != "")
			GetComponent<Animation>()[ani_walk].speed = v;
		
		if(ani_run != "")
			GetComponent<Animation>()[ani_run].speed = v;
	}
	
	void Update () 
	{
		if(seo == null)
		{
			gameObject.GetComponent<Renderer>().enabled = false;
			return;
		}
		
		if(isPlayer == true)
			return;

		float thisDeltaTime = (float)(Time.time - lastUpdateTime);
		if(Vector3.Distance(seo.eulerAngles, seo.destDirection) > 0.0004f)
		{
			seo.rotation = Quaternion.Slerp(seo.rotation, Quaternion.Euler(seo.destDirection), 8f * thisDeltaTime);
		}

		float dist = Vector3.Distance(new Vector3(seo.destPosition.x, 0f, seo.destPosition.z), 
			new Vector3(seo.position.x, 0f, seo.position.z));
		
		bool fly = ("20002001.unity3d" == seo.name);

		if(dist > 0.5f)
		{
			float deltaSpeed = (seo.speed * thisDeltaTime);
			
			if(seo.speed <= 3)
				playAnimation(ani_walk);
			else
				playAnimation(ani_run);
			
			Vector3 pos = seo.position;

			Vector3 movement = seo.destPosition - pos;
			movement.y = 0f;
			movement.Normalize();
			
			movement *= deltaSpeed;
			
			if(dist > deltaSpeed || movement.magnitude > deltaSpeed)
				pos += movement;
			else
				pos = seo.destPosition;

			bool ishit = Common.calcPositionY(pos, out pos.y, fly);
			if(!ishit)
			{
				return;
			}

			// 为了demo表现， 暂时写死
			if (fly)
			{
				if(ishit)
					pos.y += yoffset;
			}

			seo.position = pos;
		}
		else
		{
			playIdleAnimation();
		}

		// 为了demo表现， 暂时写死
		if (fly && ani_dead == current_ani && yoffset > 0.0f)
		{
			Vector3 pos = seo.position;
			bool ishit = Common.calcPositionY(pos, out pos.y, fly);
			if(ishit)
			{
				pos.y += yoffset;
				yoffset -= 0.3f;
			}
			
			seo.position = pos;
		}

		lastUpdateTime = Time.time;
	}
}

