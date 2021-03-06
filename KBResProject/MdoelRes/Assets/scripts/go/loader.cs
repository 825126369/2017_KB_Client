using UnityEngine;
using KBEngine;
using System.Collections;
using System;
using System.Xml;
using System.Collections.Generic;

public class loader : MonoBehaviour {
	public static loader inst = null;
	public string currentSceneName = "login";
	
	private SceneObject loadingbarObj = new SceneObject();
	public UnityEngine.Object defaultEntityAsset = null;
	public UnityEngine.Object entityHudInfosAsset = null;
	
	public Dictionary<string, Scene> scenes = new Dictionary<string, Scene>();
	public LoadAssetsPool loadPool = null;
	
	void Awake ()   
	{
		inst = this;
		Common.DEBUG_MSG("go!");

		loadingbarObj.asset = new Asset(); 
		loadingbarObj.asset.source = "loadingbar.unity3d";  
		
		scenes.Add(currentSceneName, new Scene(currentSceneName, this));
	}
	
	// Use this for initialization
	void Start () {
		StartCoroutine(loadInit());
		loadPool = new LoadAssetsPool(this, 5);
		installEvents();
		
		Destroy(UnityEngine.GameObject.Find("baseTerrainRes"));
	}

	void installEvents()
	{
		new KBEEventProc();
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public Scene findScene(string name, bool autocreate)
	{
		Scene scene = null;
		if(!scenes.TryGetValue(currentSceneName, out scene))
		{
			if(autocreate == false)
				return null;
			
			scene = new Scene(currentSceneName, this);
			scenes.Add(currentSceneName, scene);
		}
		
		return scene;
	}
	
	IEnumerator loadInit(){
		Common.DEBUG_MSG("starting loadInit!");
		WWW loadingbarwww = new WWW(Common.safe_url("/StreamingAssets/loadingbar.unity3d")); 
		
		WWW defaultEntityAssetwww = new WWW(Common.safe_url("/StreamingAssets/defaultEntityMesh.unity3d")); 
		WWW entityHudInfosAssetwww = new WWW(Common.safe_url("/StreamingAssets/hud_infos.unity3d")); 
		//WWW terrainDiffuseShaderwww = new WWW(Common.safe_url("/StreamingAssets/shader_Terrain_Diffuse.unity3d")); 
		
		Scene scene = findScene(currentSceneName, true);
    	scene.loadScene(true, false);
    	loadingbar.reset(false);
		
		Common.DEBUG_MSG("loadInit is finished!");
		
		// 下载选人场景
		scene = findScene("selavatar", true);
		scene.loadScene(false, true);
		
		yield return loadingbarwww; 
		
        if (loadingbarwww.error != null)
            Common.ERROR_MSG(loadingbarwww.error);  
		else
		{
			StartCoroutine(_InstantiateLoadingbarObj(loadingbarwww));
		}
		
		yield return defaultEntityAssetwww; 
		StartCoroutine(_InstantiateDefaultEntityAsset(defaultEntityAssetwww));
		
		yield return entityHudInfosAssetwww;
		StartCoroutine(_InstantiateEntityHudInfosAsset(entityHudInfosAssetwww));
		
		//yield return terrainDiffuseShaderwww; 
		//StartCoroutine(_InstantiateTerrainDiffuseShaderObjs(terrainDiffuseShaderwww));
	}
	
	IEnumerator _InstantiateLoadingbarObj(WWW loadingbarwww)
	{
		AssetBundleRequest request = loadingbarwww.assetBundle.LoadAssetAsync("loadingbar", typeof(UnityEngine.GameObject)); 
		yield return request; 
		UnityEngine.GameObject go = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(request.asset);  
		go.name = loadingbarObj.name;
		loadingbarObj.gameObject = go; 
		loadingbarObj.asset.mainAsset = request.asset;
	}
	
	IEnumerator _InstantiateEntityHudInfosAsset(WWW entityHudInfosAssetwww)
	{
		AssetBundleRequest request = entityHudInfosAssetwww.assetBundle.LoadAssetAsync("hud_infos", typeof(UnityEngine.GameObject)); 
		yield return request; 

		entityHudInfosAsset = request.asset;
		Common.DEBUG_MSG("_InstantiateDefaultEntityAsset: " + entityHudInfosAsset);
	}

	IEnumerator _InstantiateDefaultEntityAsset(WWW defaultEntityAssetwww)
	{
		AssetBundleRequest request = defaultEntityAssetwww.assetBundle.LoadAssetAsync("defaultEntityMesh", typeof(UnityEngine.GameObject)); 
		yield return request; 
		//UnityEngine.GameObject go = (UnityEngine.GameObject)UnityEngine.GameObject.Instantiate(request.asset);  
		//go.name = loadingbarObj.name;
		defaultEntityAsset = request.asset;
		Common.DEBUG_MSG("_InstantiateDefaultEntityAsset: " + defaultEntityAsset);
	}
	
	IEnumerator _InstantiateTerrainDiffuseShaderObjs(WWW terrainDiffuseShaderwww)
	{
		terrainDiffuseShaderwww.assetBundle.LoadAllAssets(typeof(Shader));

		WorldManager.default_shader_diffuse = (Shader)UnityEngine.GameObject.Instantiate(terrainDiffuseShaderwww.assetBundle.mainAsset);
		if(WorldManager.currinst != null)
		{
			WorldManager.currinst.onGetDefaultTerrainShaser();
		}
		
		yield break;
	}
	
	public void enterScene(string scenename)
	{
		Scene scene = findScene(currentSceneName, true);
		scene.unload();
		
		currentSceneName = scenename;
		scene = findScene(currentSceneName, true);
		Common.DEBUG_MSG("loader::enterScene: " + scenename + " inst=" + scene);
		scene.loadScene(true, true);
	}
	
	public void autoAttackProxy()
	{
		TargetManger.autoAttack();
	}
}
