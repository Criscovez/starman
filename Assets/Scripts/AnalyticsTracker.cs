using System.Collections.Generic;
using Unity.Services.Analytics;

public static class AnalyticsTracker
{
    public static void TrackScreenSeen(string viewId, string buttonId)
    {
        AnalyticsService.Instance.CustomData("pantallaAccedida", new Dictionary<string, object>()
        {
            { "Id_scene", viewId },
            { "Id_button", buttonId },
        });
    }
}