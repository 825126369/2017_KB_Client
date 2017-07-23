using UnityEngine;
using System.Collections;
using xk_System.AssetPackage;

public class SelectRoleScenePrepareTask : Singleton<SelectRoleScenePrepareTask>
{
    public LoadProgressInfo mTask = new LoadProgressInfo();
    public IEnumerator Prepare()
    {
        yield return LoadMap();
        yield return LoadPlayer();

        ObjectRoot.Instance.scene_root.root.SetActive(true);
        mTask.progress = 100;
        yield return 0;
    }


    IEnumerator LoadMap()
    {
        AssetInfo mAssetInfo = ResourceABsFolder.Instance.getAsseetInfo("3d_prefab_map", "map_2");
        yield return AssetBundleManager.Instance.AsyncLoadAsset(mAssetInfo);
        GameObject mapObj = AssetBundleManager.Instance.LoadAsset(mAssetInfo) as GameObject;
        mapObj.transform.SetParent(ObjectRoot.Instance.scene_root.MapParent);
        mapObj.transform.localScale = Vector3.one;
        mapObj.transform.localPosition = Vector3.zero;
        mapObj.SetActive(true);
        mTask.progress += 50;
    }

    IEnumerator LoadPlayer()
    {
        /* AssetInfo mAssetInfo = ResourceABsFolder.Instance.getAsseetInfo("3d_prefab_map", "map_1");
         yield return AssetBundleManager.Instance.AsyncLoadAsset(mAssetInfo);
         GameObject mapObj = AssetBundleManager.Instance.LoadAsset(mAssetInfo) as GameObject;
         mapObj.transform.SetParent(ObjectRoot.Instance.scene_root.root.transform);
         mapObj.transform.localScale = Vector3.one;
         mapObj.transform.localPosition = Vector3.zero;

         mTask.progress += 50;*/

        yield return 0;
    }
}
