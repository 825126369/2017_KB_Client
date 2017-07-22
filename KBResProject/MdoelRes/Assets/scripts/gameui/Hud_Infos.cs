using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Hud_Infos : MonoBehaviour 
{
	UISlider hp_progress = null;
	public float offsetY = 0f;
	public SceneEntityObject seo = null;
	
	// hud text
	public GameObject hudtext_prefab;
	HUDText mText = null;
	
	void Awake ()   
	{
		hp_progress = transform.Find("hp_Bar").gameObject.GetComponent<UISlider>();
		hp_progress.sliderValue = 1f;
	}
	
	~Hud_Infos()
	{
		
	}
	
	void Start () 
    {
    	addHUDText();
	}
	
	void addHUDText()
	{
		GameObject child = NGUITools.AddChild(transform.gameObject, hudtext_prefab);
		mText = child.GetComponentInChildren<HUDText>();
		
		// Make the UI follow the target
		child.AddComponent<UIFollowTarget>().target = transform;
	}
	
    void Update () 
    {
    	if(RPG_Camera.instance == null)
    	{
    		return; 
    	}

        transform.rotation = RPG_Camera.instance.transform.rotation;
    }
	
	public void addDamage(Int32 v)
	{
		mText.Add((-v), Color.red, 0f);
	}

	public void addHP(Int32 v)
	{
		mText.Add(v, Color.green, 0f);
	}

	public void addMP(Int32 v)
	{
		mText.Add(v, Color.blue, 0f);
	}
	
	public void setName(string name)
	{
		UILabel uiname = transform.Find("name").gameObject.GetComponent<UILabel>();
		uiname.text = name;
		set_state(seo.state);
	}
	
	public void updateHPBar(Int32 hp, Int32 hpmax)
	{
		if(hpmax <= 0)
			return;
		
		float pv = (float)hp / (float)hpmax;
		hp_progress.sliderValue = pv;
	}
	
	public void set_state(SByte v)
	{
		Color c = Color.red;
		
		if(seo.state != 3 && seo.kbentity != null)
		{
			if(seo.kbentity.className == "NPC")
				c = Color.green;
			
			if(seo.kbentity.className == "Avatar")
				c = Color.blue;
		}
		
		UILabel uiname = transform.Find("name").gameObject.GetComponent<UILabel>();
		uiname.color = c;
	}
}