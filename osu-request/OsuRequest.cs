using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Platform;
using osu_request.Config;
using osu_request.Drawables;
using osu_request.Drawables.Notifications;
using osu_request.Websocket;
using osuTK;

namespace osu_request;

public class OsuRequest : OsuRequestBase
{
    [Cached]
    private readonly NotificationContainer NotificationContainer = new()
    {
        Anchor = Anchor.Centre,
        Origin = Anchor.Centre,
        RelativeSizeAxes = Axes.Both
    };

    [Cached]
    private readonly TabsContainer TabsContainer = new()
    {
        Anchor = Anchor.Centre,
        Origin = Anchor.Centre,
        RelativeSizeAxes = Axes.Both
    };

    private OsuRequestConfig OsuRequestConfig;
    private WebSocketClient WebSocketClient;

    [BackgroundDependencyLoader]
    private void Load(Storage storage)
    {
        CreateConfigsAndManagers(storage);
        CacheDependencies();
        InitialiseChildren();
    }

    protected override void LoadComplete()
    {
        base.LoadComplete();
        TabsContainer.Select(Tabs.Requests);
        StartWebsocket();
    }

    private void StartWebsocket()
    {
        WebSocketClient.OnServerError += () =>
            NotificationContainer.Notify("Server Error", "Critical server error occured. Please report this", OsuRequestColour.Red);
        WebSocketClient.OnBeatmapsetNonExistent += () =>
            NotificationContainer.Notify("Error", "That beatmapset is nonexistent", OsuRequestColour.Red);
        WebSocketClient.OnUserNonexistent += () =>
            NotificationContainer.Notify("Error", "That user is nonexistent", OsuRequestColour.Red);
        WebSocketClient.OnConnect += () =>
            NotificationContainer.Notify("Server connected!", "Authentication has succeeded", OsuRequestColour.Green);
        WebSocketClient.OnDisconnect += () =>
            NotificationContainer.Notify("Server Disconnected", "The server has been disconnected", OsuRequestColour.Red);
        WebSocketClient.OnAuthenticationFail += () =>
        {
            TabsContainer.Select(Tabs.Settings);
            NotificationContainer.Notify("Invalid Credentials", "Please enter valid credentials", OsuRequestColour.Red);
        };
        WebSocketClient.OnNewRequest += _ =>
        {
            NotificationContainer.Notify("New Request!", "A new beatmapset has been requested", OsuRequestColour.Blue);
        };
        WebSocketClient.ConnectAsync();
    }

    private void CreateConfigsAndManagers(Storage storage)
    {
        OsuRequestConfig = new OsuRequestConfig(storage);
        WebSocketClient = new WebSocketClient(OsuRequestConfig);
    }

    private void CacheDependencies()
    {
        Dependencies.CacheAs(OsuRequestConfig);
        Dependencies.CacheAs(WebSocketClient);
    }

    private void InitialiseChildren()
    {
        Child = new DrawSizePreservingFillContainer
        {
            TargetDrawSize = new Vector2(InitialWindowSize.Width, InitialWindowSize.Height),
            Children = new Drawable[]
            {
                TabsContainer,
                NotificationContainer
            }
        };
    }
}