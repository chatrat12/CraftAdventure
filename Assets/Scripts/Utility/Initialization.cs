using System.Linq;
using UnityAsync;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Initialization
{
    public static void Initialize(Vector3 position)
    {
        var player = InitializePlayerScene(position);
        InitializeCameraRig(player);
        InitializeUI(player);
    }

    private static Player InitializePlayerScene(Vector3 position)
    {
        var playerScene = SceneManager.CreateScene("PlayerScene");

        var avatar = GameObject.Instantiate(Resources.Load<PlayerAvatar>("Player/P_Player"));
        SceneManager.MoveGameObjectToScene(avatar.gameObject, playerScene);
        avatar.transform.position = position;

        return avatar.Character as Player;
    }

    private static void InitializeCameraRig(Player player)
    {
        var cameraRig = GameObject.Instantiate(Resources.Load<ThirdPersonCamera>("Player/P_CameraRig_ThirdPerson"));
        SceneManager.MoveGameObjectToScene(cameraRig.gameObject, player.Avatar.gameObject.scene);
        cameraRig.transform.localPosition = player.Avatar.transform.position;
        cameraRig.Target = player.Avatar.transform;
        cameraRig.Init();
        player.CameraRig = cameraRig;
    }

    private async static void InitializeUI(Player player)
    {
        SceneManager.LoadScene("UI", LoadSceneMode.Additive);
        await Await.NextUpdate();
        var t = SceneManager.GetSceneByName("UI").GetRootGameObjects();
        var playerUI = SceneManager.GetSceneByName("UI").GetRootGameObjects()
                                                        .Select(g => g.GetComponentInChildren<PlayerUI>())
                                                        .First(m => m != null);
        player.Menu = playerUI.Menus;
        playerUI.Player = player;
        player.Menu.Hide();
    }
}