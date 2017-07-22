using UnityEngine;
using KBEngine;
using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;

public class SceneEntityObject : SceneObject
{
	public SByte state = 0;
	public GameEntityCtrl gameEntity = null;
	public Vector3 destPosition = Vector3.zero;
	public Vector3 destDirection = Vector3.zero;
	UnityEngine.GameObject hud_infosObj = null;
	public Hud_Infos hudinfos = null;
	public KBEngine.Entity kbentity = null;
	public bool isPlayer = false;
	
	public SceneEntityObject()
	{
	}

    ~SceneEntityObject()  // destructor
    {
    	gameEntity.seo = null;
    	gameEntity = null;
		
		if(hudinfos != null)
			hudinfos.seo = null;
    }
    
	public void setName(string name)
	{
		if(hud_infosObj != null)
		{
			hudinfos.setName(name); 
		}
	}
	
	public void updateHPBar(Int32 hp, Int32 hpmax)
	{
		if(hud_infosObj != null)
			hudinfos.updateHPBar(hp, hpmax);
	}
	
	public void create() 
	{
		// 设置默认的模型
		gameObject = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(loader.inst.defaultEntityAsset, position, Quaternion.Euler(eulerAngles));
		gameObject.name = name;
		gameObject.transform.localScale = scale;
		
		if(loader.inst.entityHudInfosAsset != null)
		{
			hud_infosObj = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(loader.inst.entityHudInfosAsset);
			hud_infosObj.name = "hud_infos";
			hudinfos = hud_infosObj.GetComponent<Hud_Infos>();
			hudinfos.seo = this;
			attachHeadInfo();
		}
		else
		{
			Common.WARNING_MSG("SceneEntityObject::Start: not found entityHudInfosAsset!");
		}
	}
	
	public void createPlayer() 
	{
		gameObject = UnityEngine.GameObject.Find("PlayerChar/entity");
		isPlayer = true;
	}
	
	public override void Instantiate()
	{
		asset.Instantiate(this, name, position, eulerAngles, scale);
	}
	
	void attachHeadInfo()
	{
		if(hud_infosObj == null)
			return;
		
        hud_infosObj.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        
		hud_infosObj.transform.parent = gameObject.transform.Find("head_info");
		if(hud_infosObj.transform.parent == null)
		{
			hud_infosObj.transform.parent = gameObject.transform;
			hudinfos.offsetY = 3f;
		}
		
		hud_infosObj.transform.position = new Vector3(hud_infosObj.transform.parent.position.x, 
		hud_infosObj.transform.parent.position.y + hudinfos.offsetY, hud_infosObj.transform.parent.position.z);
	}
	
	public override void onAssetAsyncLoadObjectCB(string name, UnityEngine.Object obj, Asset asset)
	{
		this.name = asset.source;
		if(hud_infosObj != null)
			hud_infosObj.transform.parent = null;

		if(gameObject != null)
		{
			UnityEngine.GameObject.Destroy(gameObject);
			gameObject = null;
		}

		base.onAssetAsyncLoadObjectCB(name, obj, asset);

		gameEntity = gameObject.GetComponent<GameEntityCtrl>();
		gameEntity.seo = this;
		gameEntity.isPlayer = isPlayer;
			
		if(isPlayer == false)
		{
			attachHeadInfo();
			destPosition = position;
			destDirection = eulerAngles;
			Common.calcPositionY(destPosition, out destPosition.y, false);
			gameEntity.gameObject.transform.position = destPosition;
		}
		else
		{
			if(gameObject.GetComponent<RPG_Animation>() == null)
				gameObject.AddComponent<RPG_Animation>();
			
			gameObject.name = "entity";
			
			BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
			if(boxCollider != null)
				UnityEngine.Object.Destroy(boxCollider);
			
			if(gameObject.GetComponent<MeshCollider>() != null)
				gameObject.AddComponent<MeshCollider>();
			
			UnityEngine.GameObject PlayerChar = UnityEngine.GameObject.Find("PlayerChar");			
			if(PlayerChar != null)
				gameObject.transform.parent = PlayerChar.transform;
		}
	}
	
	public void updatePosition(Vector3 destPos)
	{
		destPosition = destPos;
	}
	
	public void set_moveSpeed(float v)
	{
		speed = v;
		if(gameEntity != null)
		{
			gameEntity.set_moveSpeed(v);
		}
	}
	
	public void addHP(Int32 v)
	{
		if(hud_infosObj != null)
		{
			if(v > 0)
				hudinfos.addHP(v);
			else
				hudinfos.addDamage(v);
		}
	}
	
	public void addMP(Int32 v)
	{
		if(hud_infosObj != null)
			hudinfos.addHP(v);
	}
	
	public void set_state(SByte v)
	{
		state = v;
		if(hudinfos != null)
		{
			hudinfos.set_state(v);
		}
		
		if(gameEntity != null)
		{
			switch(state)
			{
				case 0:
					gameEntity.playIdleAnimation();
					break;
				case 1:
					gameEntity.playDeadAnimation();
					break;
				case 2:
					gameEntity.playIdleAnimation();
					break;
				case 3:
					gameEntity.playIdleAnimation();
					break;
				default:
					gameEntity.playIdleAnimation();
					break;
			};
		}
	}
	
	public void attack(Int32 skillID, Int32 damageType, SceneEntityObject receiver)
	{
		if(gameEntity != null)
		{
			gameEntity.playAttackAnimation();
		}
		
		if(receiver == null || receiver.gameEntity == null)
			return;

		if(particles.inst != null)
		{
			Vector3 v = position;
			UnityEngine.GameObject pobj = null;
			
			switch(skillID)
			{
				case 7000101:
					pobj = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(particles.inst.allpartis[37], gameEntity.gameObject.transform.Find("attackPoint").transform.position, rotation);
					SM_moveToEntity sm = pobj.GetComponent<SM_moveToEntity>();
					sm.moveSpeed = 10.0f;
					sm.target = receiver.gameEntity.gameObject;
					break;
				default:
					break;
			};
			
			if(pobj)
				pobj.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
		}
	}
	
	public void recvDamage(Int32 skillID, Int32 damageType, Int32 damage)
	{
		if(gameEntity != null)
		{
			gameEntity.playDamageAnimation();
		}

		if(hud_infosObj != null)
			hudinfos.addDamage(damage);

		if(particles.inst != null)
		{
			Vector3 v = position;
			UnityEngine.GameObject pobj = null;
			v.y += 1f;
			
			switch(skillID)
			{
				case 1:
					break;
				case 1000101:
					pobj = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(particles.inst.allpartis[47], v, rotation);
					break;
				case 2000101:
					v.y += 1.5f;
					pobj = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(particles.inst.allpartis[21], v, rotation);
					break;
				case 3000101:
					pobj = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(particles.inst.allpartis[32], v, rotation);
					break;
				case 4000101:
					pobj = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(particles.inst.allpartis[45], v, rotation);
					break;
				case 5000101:
					v.y -= 1f;
					pobj = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(particles.inst.allpartis[14], v, rotation);
					break;
				case 6000101:
					v.y += 0.5f;
					pobj = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(particles.inst.allpartis[9], v, rotation);
					pobj.transform.parent = gameObject.transform;
					break;
				case 7000101:
					//pobj = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(particles.inst.allpartis[37], v, rotation);
					//pobj.transform.parent = gameObject.transform;
					break;
				default:
					break;
			};
			
			if(pobj)
				pobj.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
		}
	}
	
	public void playAnimation(string ani)
	{
		if(gameEntity != null)
		{
			gameEntity.playAnimation(ani);
		}
	}
	
	public void stopPlayAnimation(string ani)
	{
		if(gameEntity != null)
		{
			gameEntity.stopPlayAnimation(ani);
		}
	}
	
	public void playJumpAnimation()
	{
		if(gameEntity != null)
		{
			gameEntity.playJumpAnimation();
		}
	}
	
	public void playIdleAnimation()
	{
		if(gameEntity != null)
		{
			gameEntity.playIdleAnimation();
		}
	}
	
	public void playWalkAnimation()
	{
		if(gameEntity != null)
		{
			gameEntity.playWalkAnimation();
		}
	}

	public void playRunAnimation()
	{
		if(gameEntity != null)
		{
			gameEntity.playRunAnimation();
		}
	}
	
	public void playAttackAnimation()
	{
		if(gameEntity != null)
		{
			gameEntity.playAttackAnimation();
		}
	}
}

