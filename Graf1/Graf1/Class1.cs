using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graf1
{
    class Class1
    {
        static void Main(String[] args)
        {
            var nativewindowSettings = new NativeWindowSettings()
            {
                Size = new OpenTK.Mathematics.Vector2i(1920, 1080),
                Title = "Graf 1"
            };
            using (var Window = new Window3d(GameWindowSettings.Default, nativewindowSettings))
            {
                Window.Run();
            }
        }
    }
}
