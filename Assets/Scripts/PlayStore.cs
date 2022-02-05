using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class PlayStore : MonoBehaviour
{
    public static PlayGamesPlatform platform;

    [SerializeField]
    private GameObject messsage_panel;

    [SerializeField]
    private Text log_err;

    private string play_store_id = "4598629";
    private string apple_store_id = "4598628";

    [SerializeField]
    private bool is_test_app;

    // Start is called before the first frame update
    void Start()
    {
        if (platform == null)
        {
            PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;

            platform = PlayGamesPlatform.Activate();

            ADS();
        }

        Social.Active.localUser.Authenticate((bool success, string err) =>
        {
            if (success)
            {
                print("Tudo ok!");
            }
            else if (!success)
            {
                log_err.text = "error: " + err;
            }
        });
    }

    public void ADS()
    {
        Advertisement.Initialize(play_store_id, is_test_app);
    }

    public void Reward_Video()
    {
        if (Advertisement.IsReady("Rewarded_Android"))    
        {
            Advertisement.Show("Rewarded_Android");
        }
    }
}
