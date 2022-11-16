using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using Unity.Services.Core.Environments;
using Unity.Services.Authentication;
using System;

public struct userAttributes
{
    public int characterLevel;
}

public struct appAttributes
{

}

public class RemoteConfigFetcher : MonoBehaviour
{
    [SerializeField] string environmentName;
    [SerializeField] int characterLevel;
    [SerializeField] bool fecth;
    [SerializeField] float gravity;
    [SerializeField] PhoneGravity phoneGravity;

    async void Awake()
    {
        var options = new InitializationOptions();
        options.SetEnvironmentName(environmentName);
        await UnityServices.InitializeAsync(options);

        Debug.Log("UGS Initialized");
        if (AuthenticationService.Instance.IsSignedIn == false)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        Debug.Log("Player Signed In");
        // Event yang akan memberikan respon apakah fetch sudah selesai dilakukan
        RemoteConfigService.Instance.FetchCompleted += OnFetchConfig;
    }

    private void OnDestroy()
    {
        RemoteConfigService.Instance.FetchCompleted -= OnFetchConfig;
    }

    private void OnFetchConfig(ConfigResponse response)
    {
        Debug.Log(response.requestOrigin);
        Debug.Log(response.body);

        switch (response.requestOrigin)
        {
            case ConfigOrigin.Default:
                Debug.Log("Default");
                break;
            case ConfigOrigin.Cached:
                Debug.Log("Chaced");
                break;
            case ConfigOrigin.Remote:
                Debug.Log("Remote");
                gravity = RemoteConfigService.Instance.appConfig.GetFloat("Gravity");
                phoneGravity.SetGravityMagnitude(gravity);
                break;
        }
    }

    void Update()
    {
        if (fecth)
        {
            fecth = false;

            // fungsi untuk fecth (mengambil nilai terbaru dari server)
            RemoteConfigService.Instance.FetchConfigs(
                new userAttributes() { characterLevel = this.characterLevel },
                new appAttributes()
            );
        }
    }
}
