using UnityEngine;
using KBEngine;
using System; 
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class game_ui_autopos : MonoBehaviour 
{
	public Transform minimapCamera = null;
	public Transform minimapPoint = null;
	public UILabel minimap_pos = null;
	
	public static UILabel hp_label = null;
	public static UILabel mp_label = null;
	public static UILabel target_hp_label = null;
	public static UILabel target_mp_label = null;
	
	public static UISlider hp_ProgressBar = null;
	public static UISlider target_hp_ProgressBar = null;
	public static UISlider mp_ProgressBar = null;
	public static UISlider target_mp_ProgressBar = null;
	
	public static UILabel name_label = null;
	public static UILabel target_name_label = null;
	public static UILabel level_label = null;
	public static UILabel target_level_label = null;
	
	public static UnityEngine.GameObject target_bar = null;
	public static UnityEngine.GameObject ui_relive = null;
	
	void Awake ()     
	{
	}
	
	// Use this for initialization
	void Start () {
		hp_label = UnityEngine.GameObject.Find("hp").GetComponent<UILabel>();
		mp_label = UnityEngine.GameObject.Find("mp").GetComponent<UILabel>();
		target_hp_label = UnityEngine.GameObject.Find("target_hp").GetComponent<UILabel>();
		target_mp_label = UnityEngine.GameObject.Find("target_mp").GetComponent<UILabel>();
		
		hp_ProgressBar = UnityEngine.GameObject.Find("hp_ProgressBar").GetComponent<UISlider>();
		target_hp_ProgressBar = UnityEngine.GameObject.Find("target_hp_ProgressBar").GetComponent<UISlider>();
		mp_ProgressBar = UnityEngine.GameObject.Find("mp_ProgressBar").GetComponent<UISlider>();
		target_mp_ProgressBar = UnityEngine.GameObject.Find("target_mp_ProgressBar").GetComponent<UISlider>();
		
		name_label = UnityEngine.GameObject.Find("ename").GetComponent<UILabel>();
		level_label = UnityEngine.GameObject.Find("elevel").GetComponent<UILabel>();
		target_name_label = UnityEngine.GameObject.Find("target_ename").GetComponent<UILabel>();
		target_level_label = UnityEngine.GameObject.Find("target_elevel").GetComponent<UILabel>();
		
		target_bar = UnityEngine.GameObject.Find("target_power_bar");
		hideTargetBar();
		
		ui_relive = UnityEngine.GameObject.Find("relive");
		UnityEngine.GameObject gobackrelive = UnityEngine.GameObject.Find("gobackrelive");
		UIEventListener.Get(gobackrelive).onClick = on_gobackreliveClick;   
		UnityEngine.GameObject localrelive = UnityEngine.GameObject.Find("localrelive");
		UIEventListener.Get(localrelive).onClick = on_localreliveClick;  
		hideRelivePanel();
		
		KBEngine.Entity player = KBEngineApp.app.player();
		if(player != null && KBEEventProc.inst != null)
		{
			KBEEventProc.inst.set_HP(player, player.getDefinedProperty("HP"));
			KBEEventProc.inst.set_HP_Max(player, player.getDefinedProperty("HP_Max"));
			KBEEventProc.inst.set_MP(player, player.getDefinedProperty("MP"));
			KBEEventProc.inst.set_MP_Max(player, player.getDefinedProperty("MP_Max"));
			KBEEventProc.inst.set_level(player, player.getDefinedProperty("level"));
			KBEEventProc.inst.set_name(player, player.getDefinedProperty("name"));
			
			if((SByte)player.getDefinedProperty("state") == 1)
				showRelivePanel();
		}
	}
	
	void OnDestroy()
	{
		KBEngine.Event.deregisterOut(this);
		
		reset();
	}
	
	void installEvents()
	{
	}
	
	void on_gobackreliveClick(UnityEngine.GameObject item)
	{
		Common.DEBUG_MSG("on_gobackreliveClick: " + item.name);
		KBEngine.Entity player = KBEngineApp.app.player();
		if(player != null && player.className == "Avatar")
		{
			((KBEngine.Avatar)player).relive(0);
			hideRelivePanel();
		}
	}

	void on_localreliveClick(UnityEngine.GameObject item)
	{
		Common.DEBUG_MSG("on_localreliveClick: " + item.name);
		KBEngine.Entity player = KBEngineApp.app.player();
		if(player != null && player.className == "Avatar")
		{
			((KBEngine.Avatar)player).relive(1);
			hideRelivePanel();
		}
	}
	
	public static void showRelivePanel()
	{
		if(ui_relive != null)
			NGUITools.SetActive(ui_relive, true);
	}
	
	public static void hideRelivePanel()
	{
		if(ui_relive != null)
			NGUITools.SetActive(ui_relive, false);
	}
	
	void OnGUI()
	{
		UnityEngine.GameObject minimap = UnityEngine.GameObject.Find("minimap");
		Vector3 pos = minimap.transform.localPosition;
		pos.x = (Screen.width / 2 - 78.0f);
		pos.y = (Screen.height / 2 - 10.0f);
		minimap.transform.localPosition = pos;
		
		UnityEngine.GameObject my_power_bar = UnityEngine.GameObject.Find("my_power_bar");
		pos = my_power_bar.transform.localPosition;
		pos.x = -(Screen.width / 2 - 270f);
		pos.y = (Screen.height / 2 - 20.0f);
		my_power_bar.transform.localPosition = pos;
		
		UnityEngine.GameObject exp_ProgressBar = UnityEngine.GameObject.Find("exp_ProgressBar");
		pos = exp_ProgressBar.transform.localPosition;
		pos.x = -480f;
		pos.y = -(Screen.height / 2) + 1f;
		exp_ProgressBar.transform.localPosition = pos;

		UnityEngine.GameObject target_power_bar = UnityEngine.GameObject.Find("target_power_bar");
		if(target_power_bar != null)
		{
			pos = target_power_bar.transform.localPosition;
			pos.x = -(Screen.width / 2 - 200);
			pos.y = (Screen.height / 2) - 80f;
			target_power_bar.transform.localPosition = pos;
		}
		
		UnityEngine.GameObject sys_itemslots1 = UnityEngine.GameObject.Find("sys_itemslots1");
		pos = sys_itemslots1.transform.localPosition;
		pos.x = -100f;
		pos.y = -(Screen.height / 2) + 20f;
		sys_itemslots1.transform.localPosition = pos;

		UnityEngine.GameObject sysitems_down_btn = UnityEngine.GameObject.Find("sysitems_down_btn");
		pos = sysitems_down_btn.transform.localPosition;
		pos.x = -130f;
		pos.y = -(Screen.height / 2) + 10f;
		sysitems_down_btn.transform.localPosition = pos;

		UnityEngine.GameObject sysitems_up_btn = UnityEngine.GameObject.Find("sysitems_up_btn");
		pos = sysitems_up_btn.transform.localPosition;
		pos.x = -130f;
		pos.y = -(Screen.height / 2) + 60f;
		sysitems_up_btn.transform.localPosition = pos;
		
		UnityEngine.GameObject sys_itemslotsF = UnityEngine.GameObject.Find("sys_itemslotsF");
		pos = sys_itemslotsF.transform.localPosition;
		pos.x = -100f;
		pos.y = -(Screen.height / 2) + 50f;
		sys_itemslotsF.transform.localPosition = pos;
		
		UnityEngine.GameObject sysitems_index_label = UnityEngine.GameObject.Find("sysitems_index_label");
		pos = sys_itemslotsF.transform.localPosition;
		pos.x = -133f;
		pos.y = -(Screen.height / 2) + 35f;
		sysitems_index_label.transform.localPosition = pos;
			
		UnityEngine.GameObject sys_itemdbuff = UnityEngine.GameObject.Find("sys_itemdbuff");
		pos = sys_itemdbuff.transform.localPosition;
		pos.x = 50f;
		pos.y = (Screen.height / 2) - 15f;
		sys_itemdbuff.transform.localPosition = pos;
		
		UnityEngine.GameObject chat_ui = UnityEngine.GameObject.Find("chat");
		pos = chat_ui.transform.localPosition;
		pos.x = -(Screen.width / 2 - 180);
		pos.y = -(Screen.height / 2) + 150f;
		chat_ui.transform.localPosition = pos;
		
		UnityEngine.GameObject sysctrls = UnityEngine.GameObject.Find("sysctrls");
		pos = sysctrls.transform.localPosition;
		pos.x = 180f;
		pos.y = -(Screen.height / 2) + 20f;
		sysctrls.transform.localPosition = pos;
	}
	
	public static void showTargetBar(SceneEntityObject seo)
	{
		if(seo == null || seo.kbentity == null || target_bar == null)
			return;

		NGUITools.SetActive(target_bar, true);
		
		KBEngine.Entity entity = seo.kbentity;
		string name = (string)entity.getDefinedProperty("name");
		object level = entity.getDefinedProperty("level");
		object hp = entity.getDefinedProperty("HP");
		object hpmax = entity.getDefinedProperty("HP_Max");
		object mp = entity.getDefinedProperty("MP");
		object mpmax = entity.getDefinedProperty("MP_Max");
			
		target_name_label.text = name;
		if(level != null)
			target_level_label.text = "lv:" + level;
		else
			target_level_label.text = "";
		
		updateTargetBar_HP(hp, hpmax);
		updateTargetBar_MP(mp, mpmax);
	}

	public static void updatePower_Progress(UISlider progress, UILabel label, object v, object v_max)
	{
		if(progress == null || v == null || v_max == null)
			return;
		
		if((Int32)v_max <= 0)
			return;
		
		float pv = (float)(Int32)v / (float)(Int32)v_max;
		progress.sliderValue = pv;
		
		label.text = v + "/" + v_max;
	}
	
	public static void updateTargetBar_HP(object hp, object hpmax)
	{
		game_ui_autopos.updatePower_Progress(game_ui_autopos.target_hp_ProgressBar, game_ui_autopos.target_hp_label, hp, hpmax);
	}

	public static void updateTargetBar_MP(object mp, object mpmax)
	{
		game_ui_autopos.updatePower_Progress(game_ui_autopos.target_mp_ProgressBar, game_ui_autopos.target_mp_label, mp, mpmax);
	}
	
	public static void hideTargetBar()
	{
		NGUITools.SetActive(target_bar, false);
	}
	
	public static void updatePlayerBar_HP(object hp, object hpmax)
	{
		game_ui_autopos.updatePower_Progress(game_ui_autopos.hp_ProgressBar, game_ui_autopos.hp_label, hp, hpmax);
	}

	public static void updatePlayerBar_MP(object mp, object mpmax)
	{
		game_ui_autopos.updatePower_Progress(game_ui_autopos.mp_ProgressBar, game_ui_autopos.mp_label, mp, mpmax);
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	void reset()
	{
		if(UnityEngine.GameObject.Find("minimap") == null)
			return;
		
		UnityEngine.GameObject minimapCameraObj = UnityEngine.GameObject.Find("minimapCamera");
		if(minimapCameraObj != null)
		{
			minimapCamera = minimapCameraObj.transform;
			minimapPoint = UnityEngine.GameObject.Find("minimap_point").transform;
			minimap_pos = UnityEngine.GameObject.Find("minimap_pos").GetComponent<UILabel>();
			
			Camera c = minimapCameraObj.GetComponent<Camera>();
			Material materialTex = UnityEngine.GameObject.Find("minimap_texture").GetComponent<UITexture>().material;
			c.targetTexture = materialTex.GetTexture("_MainTex") as RenderTexture;
			c.orthographic = true;
			c.orthographicSize = 128;
			c.orthographic = true;
			
			UnityEngine.GameObject minimap_frame = UnityEngine.GameObject.Find("minimap_frame");
			minimap_frame.GetComponent<UITexture>().mainTexture = materialTex.GetTexture("_Mask");
			Vector3 scale = UnityEngine.GameObject.Find("minimap_texture").transform.localScale;
			
			minimap_frame.transform.localScale = scale;
		}
	}
	
	void FixedUpdate()
	{
		if(RPG_Animation.instance != null)
		{
			if(minimapCamera != null)
			{
				Vector3 playerpos = RPG_Animation.instance.transform.position;
				playerpos.y += 256f;
				
				minimapCamera.position = playerpos;
				minimap_pos.text = ((int)playerpos.x) + ":" + ((int)playerpos.z);
				minimapPoint.rotation = Quaternion.Euler(0, 0, -RPG_Animation.instance.transform.rotation.eulerAngles.y);
			}
			else
			{
				reset();
			}
		}
	}
}
