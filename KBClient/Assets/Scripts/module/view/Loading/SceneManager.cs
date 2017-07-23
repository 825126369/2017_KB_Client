using UnityEngine;
using System.Collections;
using xk_System.View;
using UnityEngine.SceneManagement;
using xk_System.View.Modules;
using System.Collections.Generic;

public static class SceneInfo
{
    public const string Scene_1 = "init";
    public const string Scene_SelectRole = "selectRole";
    public const string Scene_2 = "main";
    public const string Scene_3 = "fight";
}

public class SceneSystem : Singleton<SceneSystem>
{
    private string currentSceneName = "";
    public void GoToScene(string sceneName)
    {
        currentSceneName = sceneName;
        LoadSceneResource();
    }

    private void LoadSceneResource()
    {
        WindowManager.Instance.CleanManager();
        WindowManager.Instance.ShowView<SceneLoadingView>();
        TaskProgressBar mTask = SceneSystemLoadingModel.Instance.GetPrepareTask(currentSceneName);
        EnterFrame.Instance.add(StartTask,mTask);
    }

    private void StartTask(object data)
    {
        TaskProgressBar mTask = data as TaskProgressBar;
        float jindu = mTask.getProgress();
        if(jindu>=1f)
        {
            EnterFrame.Instance.remove(StartTask);
            WindowManager.Instance.HideView<SceneLoadingView>();
            FinishTask();
        }
    }

    private void FinishTask()
    {
        if (currentSceneName == SceneInfo.Scene_SelectRole)
        {
            WindowManager.Instance.ShowView<RoleSelectView>();
        } else
        {
            WindowManager.Instance.ShowView<MainView>();
        }
    }
}

public class SceneSystemLoadingModel:Singleton<SceneSystemLoadingModel>
{
    TaskProgressBar mTask=null;
    public TaskProgressBar GetPrepareTask(string sceneName)
    {
        switch (sceneName)
        {
            case SceneInfo.Scene_1:
                break;
            case SceneInfo.Scene_SelectRole:
                PrepareScene_SelectRole_Task();
                break;
            case SceneInfo.Scene_2:
                PrepareScene_Main_Task();
                break;
            case SceneInfo.Scene_3:
                break;
        }
        return mTask;
    }

    public void PrepareScene_SelectRole_Task()
    {
        Queue<SubTaskProgress> mSubTaskList = new Queue<SubTaskProgress>();
        SubTaskProgress mSubTask = new SubTaskProgress();
        mSubTask.SubMaxProgress = 100;
        mSubTask.mSubTask = SelectRoleScenePrepareTask.Instance.mTask;
        mSubTaskList.Enqueue(mSubTask);
        mTask = new TaskProgressBar(mSubTaskList);
        UpdateManager.Instance.xStartCoroutine(SelectRoleScenePrepareTask.Instance.Prepare());

    }

    public void PrepareScene_Main_Task()
    {
        Queue<SubTaskProgress> mSubTaskList = new Queue<SubTaskProgress>();
        SubTaskProgress mSubTask = new SubTaskProgress();
        mSubTask.SubMaxProgress = 100;
        mSubTask.mSubTask = MainScenePrepareTask.Instance.mTask;
        mSubTaskList.Enqueue(mSubTask);
        mTask = new TaskProgressBar(mSubTaskList);
        UpdateManager.Instance.xStartCoroutine(MainScenePrepareTask.Instance.Prepare());

    }
}
