using Android.App;
using Android.Views;

namespace Perspex.Android.Platform.Specific
{
    public interface IAndroidActivity
    {
        Activity Activity { get; }

        IAndroidView ContentView { get; set; }
    }
}