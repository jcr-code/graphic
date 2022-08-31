using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace Graf1
{
    static class Constants
    {
        public const string path = "../../../Shader/";
    }
    internal class Window: GameWindow
    {
        Camera _camera;
        Asset2d[] _object = new Asset2d[4]; //1 karena hanya ingin menggambar 1;

        /*float[] _vertices =
        {
            //x     //y      //z
            -0.5f, -0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            0.0f, 0.5f, 0.0f
        };*/

        /*float[] _vertices =
        {   
            //Position              //Colors
            //x     //y      //z
            -0.5f, -0.5f, 0.0f, 1.0f, 0.0f, 0.0f, //-> vertex 1 merah
            0.5f, -0.5f, 0.0f, 0.0f, 1.0f, 0.0f, //-> vertex 2 hijau
            0.0f, 0.5f, 0.0f, 0.0f, 0.0f, 1.0f //-> vertex 3 biru
        };*/

        /* float[] _vertices =
         {
              //x     //y     //z
              0.5f, 0.5f, 0.0f, //top right
              0.5f, -0.5f, 0.0f, //bottom right
              -0.5f, -0.5f, 0.0f, //bottom left
              -0.5f, 0.5f, 0.0f //top left
          };*/

        /*uint[] indikasi =
        {
             0,1,2,
             2,3,0
         };*/

        public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {

        }

        protected override void OnLoad()
        {
            base.OnLoad();
            //Background color
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);

            _object[0] = new Asset2d(
                new float[]
                {
                    -0.75f, 0.0f, 0.0f,
                    -0.25f, 0.0f, 0.0f,
                    -0.5f, 0.5f, 0.0f,
                },
                new uint[]
                {

                }
            );
            _object[1] = new Asset2d(
                new float[]
                {

                },
                new uint[]
                {

                }
            );
            _object[2] = new Asset2d(
                new float[1080],
                new uint[]
                {

                }
            );
            _object[3] = new Asset2d(
                new float[]
                {

                },
                new uint[]
                {

                }
            );
            _object[0].load(Constants.path + "shader.vert", Constants.path + "shader.frag");
            _object[1].createElips(0.0f, 0.5f, 0.25f, 0.5f);
            _object[1].load(Constants.path + "shader.vert", Constants.path + "shader.frag");
            _object[2].load(Constants.path + "shader.vert", Constants.path + "shader.frag");
            _object[3].load(Constants.path + "shader.vert", Constants.path + "shader.frag");
            /*GL.GetInteger(GetPName.MaxVertexAttribs, out int maxAttributeCount);
            Console.WriteLine($"Maximum number of " + $"vertex atrribute supported: { maxAttributeCount}");*/
        }

        //Frame bakal jalan terus-terus tiap frame satu frame datang ke layar ini yang render
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            _object[0].render(0);
            _object[1].render(1);
            if (_object[2].getVerticesLength())
            {
                List<float> _verticesTemp = _object[2].CreateCurveBezier();
                _object[3].setVertices(_verticesTemp.ToArray());
                _object[3].load(Constants.path + "shader.vert", Constants.path + "shader.frag");
                _object[3].render(3);
            }
            _object[2].render(2);
            SwapBuffers();
        }

        //ini tiap perubahan frame
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0,0,Size.X,Size.Y);
        }

        //Jalan berdasarkan FPS setting
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);

            var input = KeyboardState; //Var ini menyimpan status keyboard ketika window dinyalakan
            var mouse_input = MouseState; //Var ini menyimpan kondisi mouse
            if (input.IsKeyDown(Keys.Escape)) 
            {
                Close();
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
            if(e.Button == MouseButton.Left)
            {
                float _x = (MousePosition.X - Size.X/2)/(Size.X/2);
                float _y = -(MousePosition.Y - Size.Y/2)/(Size.Y/2);
                float _z = 0;
                Console.WriteLine("x = " + _x + "y = " + _y + "z = " + _z);
                _object[2].updateMousePosition(_x, _y, _z);
            }
        }
    }
}
