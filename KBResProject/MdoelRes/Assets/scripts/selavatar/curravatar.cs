using UnityEngine;
using System.Collections;

public class curravatar : MonoBehaviour {
	
	public byte roleType = 1;
	
	public Texture[] skins;
	
	public Transform weaponHand;
	public Transform currentweapon = null;
	public int weaponsType = 0;
	public int currSkin = 3;
	
	void Awake ()   
	{
	}
	
	// Use this for initialization
	void Start () {
		Common.DEBUG_MSG("curravatar::Awake: roleType=" + roleType + ", weaponsType=" + weaponsType);
		NGUITools.SetActive(this.gameObject, roleType == selavatar_ui.changeAvatarItem);
		
		onFuseChanged();
	}
	
	// Update is called once per frame
	void Update () {
		if(currentweapon == null && wpgroups.obj != null)
		{
			currentweapon = Instantiate(wpgroups.obj.weapons[weaponsType], weaponHand.position, weaponHand.rotation) as Transform;
			currentweapon.localScale = transform.localScale;
		 	currentweapon.parent = weaponHand;
		}
	}

	public void incFuse()
	{
		currSkin += 1;
		if(currSkin >= skins.Length)
			currSkin = skins.Length - 1;
		
		onFuseChanged();
	}
	
	public void decFuse()
	{
		currSkin -= 1;
		if(currSkin < 0)
			currSkin = 0;
		
		onFuseChanged();
	}
	
	void onFuseChanged()
	{
		foreach (Transform child in transform)
		{
		    if(child.gameObject.name == "body")
		    {
				child.gameObject.GetComponent<Renderer>().material.mainTexture = skins[currSkin];
		    	return;
		    }
		}
	}
	
	public void incLianxing()
	{
		onLianxingChanged();
	}
	
	public void decLianxing()
	{
		onLianxingChanged();
	}
	
	void onLianxingChanged()
	{
	}
	
	public void incFaxing()
	{
		onFaxingChanged();
	}
	
	public void decFaxing()
	{
		onFaxingChanged();
	}
	
	void onFaxingChanged()
	{
	}
	
	public void incWenshen()
	{
		onWenshenChanged();
	}
	
	public void decWenshen()
	{
		onWenshenChanged();
	}
	
	void onWenshenChanged()
	{
	}
}
