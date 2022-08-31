using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Mathematics;
using LearnOpenTK.Common;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Drawing;
using System.Drawing.Imaging;

namespace Graf1
{
    internal class Window3d : GameWindow
    {
        List<Volume> air1 = new List<Volume>();
        List<Volume> air2 = new List<Volume>();
        List<Volume> air3 = new List<Volume>();
        List<Volume> char1 = new List<Volume>();
        List<Volume> char2 = new List<Volume>();
        List<Volume> char3 = new List<Volume>();
        List<Volume> ven1 = new List<Volume>();
        List<Volume> ven2 = new List<Volume>();
        List<Volume> ven3 = new List<Volume>();
        List<Volume> ter = new List<Volume>();
        List<Volume> pikachu = new List<Volume>();

        Asset3d[] asset = new Asset3d[11];
        /*Asset3d[] _object3d = new Asset3d[10];*/
        Asset3d[] tuguAtas = new Asset3d[10];
        Asset3d[] tuguBawah = new Asset3d[10];
        Asset3d[] kaca = new Asset3d[10];
        Asset3d[] Karpet = new Asset3d[14];
        Asset3d camObj = new Asset3d();

        float camx = 0.0f;
        float camy = 0.0f;
        float camz = 0.0f;

        bool camObjMati = true;

        float deg = 0;
        double _time = 0;
        Camera camera;
        float temp_fov = 0.0f;
        bool _firstMove = true;
        Vector2 _lastPos;
        Vector3 _objecPost = new Vector3(0.0f, 0.0f, 0.0f);
        float _rotationSpeed = 1f;
        Asset3d LightObject = new Asset3d();
        Asset3d[] LightObjects = new Asset3d[4];
        ObjVolume lr = new ObjVolume();
        bool senter = true;
        int count = 0;

        Dictionary<String, Material> materials = new Dictionary<string, Material>();
        Dictionary<string, int> textures = new Dictionary<string, int>();

        readonly Vector3[] _cubePositions =
        {
            new Vector3(0.0f, 0.0f, 0.0f),
            new Vector3(2.0f, 5.0f, -15.0f),
            new Vector3(-1.5f, -2.2f, -2.5f),
            new Vector3(-3.8f, -2.0f, -12.3f),
            new Vector3(2.4f, -0.4f, -3.5f),
            new Vector3(-1.7f, 3.0f, -7.5f),
            new Vector3(1.3f, -2.0f, -2.5f),
            new Vector3(1.5f, 2.0f, -2.5f),
            new Vector3(1.5f, 0.2f, -1.5f),
            new Vector3(-1.3f, 1.0f, -1.5f)

        };
        private readonly Vector3[] _pointLightPositionson =
        {
            new Vector3(-4.0f, 2.0f, 2.0f), //kiri depan
            new Vector3(3.5f, 2.0f, -5.0f), //kanan belakang
            new Vector3(3.5f, 2.0f, 2.0f), //kanan depan
            new Vector3(-4.0f, 2.0f, -5.0f), //kiri belakang
            new Vector3(-2.0f, 8.0f, 2.0f), //depan kiri atas
            new Vector3(2.5f, 8.0f, 2.0f), //belakang kanan atas
            new Vector3(0.0f, 1.0f, 0.0f) //tengah
        };
        private readonly Vector3[] _pointLightPositionsoff =
        {
            new Vector3(2.0f, -0.5f, -1.0f), // belakang kanan
            new Vector3(2.0f, -0.5f, 1.0f), // depan kanan
            new Vector3(-2.0f, -0.5f, 1.0f), // depan kiri
            new Vector3(0.0f, 12.6f, 3.0f), // atas tengah
            new Vector3(-2.0f, -0.5f, -1.0f) //belakang kiri
        };

        public Window3d(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
            /*asset[0] = new Asset3d();
            asset[0].createBoxVertices2(0, 0, 0, 0.5f);

            //lamp
            asset[1] = new Asset3d();
            asset[1].createBoxVertices2(1.2f, 1.0f, 2.0f, 1.0f);*/


            LightObject.createBoxVertices2(1.2f, 1.0f, 2.0f, 0.5f, 0.5f, 0.5f);

            //Buat object blender
            ObjVolume obj6 = ObjVolume.LoadFromFile("D:/Semester 4/GrafkomProj/Graf1/Graf1/bulbasaur.obj");
            ObjVolume obj1 = ObjVolume.LoadFromFile("D:/Semester 4/GrafkomProj/Graf1/Graf1/ivysaur.obj");
            ObjVolume obj7 = ObjVolume.LoadFromFile("D:/Semester 4/GrafkomProj/Graf1/Graf1/venusaur.obj");

            ObjVolume obj2 = ObjVolume.LoadFromFile("D:/Semester 4/GrafkomProj/Graf1/Graf1/Squirtle.obj");
            ObjVolume obj8 = ObjVolume.LoadFromFile("D:/Semester 4/GrafkomProj/Graf1/Graf1/wartortle.obj");
            ObjVolume obj9 = ObjVolume.LoadFromFile("D:/Semester 4/GrafkomProj/Graf1/Graf1/blastoisefix.obj");

            ObjVolume obj3 = ObjVolume.LoadFromFile("D:/Semester 4/GrafkomProj/Graf1/Graf1/Charmander.obj");
            ObjVolume obj4 = ObjVolume.LoadFromFile("D:/Semester 4/GrafkomProj/Graf1/Graf1/Charmeleon.obj");
            ObjVolume obj5 = ObjVolume.LoadFromFile("D:/Semester 4/GrafkomProj/Graf1/Graf1/Charizad.obj");

            //Pikachu
            ObjVolume obj10 = ObjVolume.LoadFromFile("D:/Semester 4/GrafkomProj/Graf1/Graf1/Pikachu.obj");


            //Texturing 
            /*loadMaterials("D:/Semester 4/GrafkomProj/Graf1/Graf1/blastoisefix.mtl");
            obj1.TextureID = textures[materials["Material__85"].DiffuseMap];
            obj1.TextureID += textures[materials["Material__86"].DiffuseMap];
            obj1.TextureID += textures[materials["Material__87"].DiffuseMap];
            obj1.TextureID += textures[materials["Material__88"].DiffuseMap];*/
            obj6.CalculateNormals();
            ven1.Add(obj6);
            obj1.CalculateNormals();
            ven2.Add(obj1);
            obj7.CalculateNormals();
            ven3.Add(obj7);

            obj2.CalculateNormals();
            air1.Add(obj2);
            obj8.CalculateNormals();
            air2.Add(obj8);
            obj9.CalculateNormals();
            air3.Add(obj9);

            obj3.CalculateNormals();
            char1.Add(obj3);
            obj4.CalculateNormals();
            char2.Add(obj4);
            obj5.CalculateNormals();
            char3.Add(obj5);

            obj10.CalculateNormals();
            pikachu.Add(obj10);

            for (int i = 0; i < asset.Length; i++)
            {
                asset[i] = new Asset3d();
            }

            asset[0].createBoxVertices2(0.0f, -0.5f, -1.8f, 16.0f, 0.4f, 12.0f);
            asset[1].createBoxVertices2(0.0f, -0.5f, -1.8f, 7.0f, 0.4f, 12.0f);
            asset[2].createBoxVertices2(0.0f, -0.5f, -1.8f, 7.0f, 0.4f, 12.0f);
            asset[3].createBoxVertices2(0.0f, -0.5f, -1.8f, 16.0f, 0.4f, 7.0f);
            asset[4].createBoxVertices2(0.0f, -0.5f, -1.8f, 16.0f, 0.4f, 12.0f);
            asset[5].createBoxVertices2(0.0f, -0.5f, -1.8f, 16.0f, 0.4f, 12.0f);
            asset[6].createBoxVertices2(0.0f, -0.5f, -1.8f, 16.0f, 0.4f, 12.0f);
            asset[7].createBoxVertices2(0.0f, -0.5f, -1.8f, 16.0f, 0.4f, 12.0f);
            asset[8].createBoxVertices2(0.0f, -0.5f, -1.8f, 16.0f, 0.4f, 12.0f);
            asset[9].createBoxVertices2(0.0f, -0.5f, -1.8f, 16.0f, 0.4f, 12.0f);
            asset[10].createBoxVertices2(0.0f, 8.2f, -4.8f, 16.0f, 0.4f, 12.0f);

            for (int i = 0; i < tuguBawah.Length; i++)
            {
                tuguBawah[i] = new Asset3d();
            }
            // Tugu evolve ke 2
            tuguBawah[0].createBoxVertices2(-3.4f, 0.0f, -6.8f, 2.0f, 0.6f, 2.0f);
            tuguBawah[1].createBoxVertices2(-0.7f, 0.0f, -6.8f, 2.0f, 0.6f, 2.0f);
            tuguBawah[2].createBoxVertices2(2.0f, 0.0f, -6.8f, 2.0f, 0.6f, 2.0f);

            //Tugu evole ke 1
            tuguBawah[3].createBoxVertices2(-6.0f, 0.0f, 0.8f, 2.0f, 0.6f, 2.0f);
            tuguBawah[4].createBoxVertices2(-6.0f, 0.0f, -2.0f, 2.0f, 0.6f, 2.0f);
            tuguBawah[5].createBoxVertices2(-6.0f, 0.0f, -5.0f, 2.0f, 0.6f, 2.0f);

            //Tugu evole ke 3
            tuguBawah[6].createBoxVertices2(6.0f, 0.0f, 2.0f, 3.5f, 0.6f, 3.0f);
            tuguBawah[7].createBoxVertices2(6.0f, 0.0f, -1.3f, 3.5f, 0.6f, 3.16f);
            tuguBawah[8].createBoxVertices2(6.0f, 0.0f, -4.8f, 3.5f, 0.6f, 3.2f);

            for (int i = 0; i < tuguAtas.Length; i++)
            {
                tuguAtas[i] = new Asset3d();
            }
            // Tugu evolve ke 2
            tuguAtas[0].createBoxVertices2(-3.4f, 0.2f, -6.8f, 1.5f, 0.6f, 1.5f);
            tuguAtas[1].createBoxVertices2(-0.7f, 0.2f, -6.8f, 1.5f, 0.6f, 1.5f);
            tuguAtas[2].createBoxVertices2(2.0f, 0.2f, -6.8f, 1.5f, 0.6f, 1.5f);

            //Tugu evole ke 1
            tuguAtas[3].createBoxVertices2(-6.0f, 0.2f, 0.8f, 1.5f, 0.6f, 1.5f);
            tuguAtas[4].createBoxVertices2(-6.0f, 0.2f, -2.0f, 1.5f, 0.6f, 1.5f);
            tuguAtas[5].createBoxVertices2(-6.0f, 0.2f, -5.0f, 1.5f, 0.6f, 1.5f);

            //Tugu evole ke 3
            tuguAtas[6].createBoxVertices2(6.0f, 0.2f, 2.0f, 3.0f, 0.6f, 2.5f);
            tuguAtas[7].createBoxVertices2(6.0f, 0.2f, -1.3f, 3.0f, 0.6f, 2.5f);
            tuguAtas[8].createBoxVertices2(6.0f, 0.2f, -4.8f, 3.0f, 0.6f, 2.5f);

            for (int i = 0; i < kaca.Length; i++)
            {
                kaca[i] = new Asset3d();
            }

            //Kaca evolve ke 2
            kaca[0].createBoxVertices2(-3.4f, 1.2f, -6.8f, 2.0f, 2.0f, 2.0f);
            kaca[1].createBoxVertices2(-0.7f, 1.2f, -6.8f, 2.0f, 2.0f, 2.0f);
            kaca[2].createBoxVertices2(2.0f, 1.2f, -6.8f, 2.0f, 2.0f, 2.0f);

            //Kaca evole ke 1
            kaca[3].createBoxVertices2(-6.0f, 1.0f, 0.8f, 2.0f, 1.6f, 2.0f);
            kaca[4].createBoxVertices2(-6.0f, 1.0f, -2.0f, 2.0f, 1.6f, 2.0f);
            kaca[5].createBoxVertices2(-6.0f, 1.0f, -5.0f, 2.0f, 1.6f, 2.0f);

            //Kaca evole ke 3
            kaca[6].createBoxVertices2(6.0f, 1.5f, 2.0f, 3.5f, 2.5f, 3.0f);
            kaca[7].createBoxVertices2(6.0f, 1.5f, -1.3f, 3.5f, 2.5f, 3.16f);
            kaca[8].createBoxVertices2(6.0f, 1.5f, -4.8f, 3.5f, 2.5f, 3.2f);

            //Karpet
            for (int i = 0; i < Karpet.Length; i++)
            {
                Karpet[i] = new Asset3d();
            }
            Karpet[0].createBoxVertices2(0f, -0.8f, 1.2f, 6f, 0.025f, 6f);
            Karpet[1].createBoxVertices2(0f, -0.9875f, 4.2f, 6f, 0.4f, 0.025f);
            Karpet[2].createBoxVertices2(0f, -1.09875f, 4.45f, 6f, 0.025f, 0.5f);
            Karpet[3].createBoxVertices2(0f, -1.28625f, 4.7f, 6f, 0.4f, 0.025f);
            Karpet[4].createBoxVertices2(0f, -1.39875f, 4.95f, 6f, 0.025f, 0.5f);
            Karpet[5].createBoxVertices2(0f, -1.58625f, 5.2f, 6f, 0.4f, 0.025f);
            Karpet[6].createBoxVertices2(0f, -1.69875f, 5.45f, 6f, 0.025f, 0.5f);
            Karpet[7].createBoxVertices2(0f, -1.88625f, 5.7f, 6f, 0.4f, 0.025f);
            Karpet[8].createBoxVertices2(0f, -1.99875f, 5.95f, 6f, 0.025f, 0.5f);
            Karpet[9].createBoxVertices2(0f, -2.18625f, 6.2f, 6f, 0.4f, 0.025f);
            Karpet[10].createBoxVertices2(0f, -2.29875f, 6.45f, 6f, 0.025f, 0.5f);
            Karpet[11].createBoxVertices2(0f, -2.48625f, 6.7f, 6f, 0.4f, 0.025f);
            Karpet[12].createBoxVertices2(0f, -2.59875f, 6.95f, 6f, 0.025f, 0.5f);
            Karpet[13].createBoxVertices2(0f, -2.79225f, 7.2f, 6f, 0.4125f, 0.025f);

            camObj.createBoxVertices2(0.0f, 0.0f, 0.0f, 0.5f, 0.5f, 0.5f);
        }
        public Matrix4 generateArbRotationMatrix(Vector3 axis, Vector3 center, float degree)
        {
            var rads = MathHelper.DegreesToRadians(degree);

            var secretFormula = new float[4, 4] {
                { (float)Math.Cos(rads) + (float)Math.Pow(axis.X, 2) * (1 - (float)Math.Cos(rads)), axis.X* axis.Y * (1 - (float)Math.Cos(rads)) - axis.Z * (float)Math.Sin(rads),    axis.X * axis.Z * (1 - (float)Math.Cos(rads)) + axis.Y * (float)Math.Sin(rads),   0 },
                { axis.Y * axis.X * (1 - (float)Math.Cos(rads)) + axis.Z * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Y, 2) * (1 - (float)Math.Cos(rads)), axis.Y * axis.Z * (1 - (float)Math.Cos(rads)) - axis.X * (float)Math.Sin(rads),   0 },
                { axis.Z * axis.X * (1 - (float)Math.Cos(rads)) - axis.Y * (float)Math.Sin(rads),   axis.Z * axis.Y * (1 - (float)Math.Cos(rads)) + axis.X * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Z, 2) * (1 - (float)Math.Cos(rads)), 0 },
                { 0, 0, 0, 1}
            };
            var secretFormulaMatix = new Matrix4
            (
                new Vector4(secretFormula[0, 0], secretFormula[0, 1], secretFormula[0, 2], secretFormula[0, 3]),
                new Vector4(secretFormula[1, 0], secretFormula[1, 1], secretFormula[1, 2], secretFormula[1, 3]),
                new Vector4(secretFormula[2, 0], secretFormula[2, 1], secretFormula[2, 2], secretFormula[2, 3]),
                new Vector4(secretFormula[3, 0], secretFormula[3, 1], secretFormula[3, 2], secretFormula[3, 3])
            );

            return secretFormulaMatix;
        }
        protected override void OnLoad()
        {

            base.OnLoad();
            //Background color
            GL.Enable(EnableCap.DepthTest);
            GL.ClearColor(0.0f, 0.5f, 0.5f, 1.0f);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            camera = new Camera(new Vector3(0, 0, 1), Size.X / Size.Y);
            temp_fov = camera.Fov;
            Console.WriteLine("List tombol yang dapat digunakan : ");
            Console.WriteLine("Arrow key -> atau D = Ke samping kanan ");
            Console.WriteLine("Arrow key <- atau A = Ke samping kiri");
            Console.WriteLine("Arrow key ^  atau W = Ke atas");
            Console.WriteLine("Arrow key v  atau S = Ke belakang");
            Console.WriteLine("Tombol K            = Rotasi camera sumbu x ke atas");
            Console.WriteLine("Tombol N            = Rotasi camera sumbu y ke kiri");
            Console.WriteLine("Tombol M            = Rotasi camera sumbu x ke bawah");
            Console.WriteLine("Tombol ,            = Rotasi camera sumbu y ke kanan");
            Console.WriteLine("Tombol Space        = Ke atas");
            Console.WriteLine("Tombol Lctrl        = Ke bawah");
            Console.WriteLine("Tombol V            = Field of view ke posisi awal");
            Console.WriteLine("Mouse Wheel         = Zoom field of view camera");
            Console.WriteLine("Tombol O            = Untuk mengganti Lightning");
            Console.WriteLine("Tombol F            = Untuk mengganti Senter atau pointLight");
            Console.WriteLine("Tombol T            = Third person representation");

            /*for (int i = 0; i < asset.Length; i++)
            {
                asset[i].load("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y);
            }
            asset[0].load("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y);
            asset[1].load("../../../Shader/shader.vert", "../../../Shader/shader.frag", Size.X, Size.Y);*/
            /*lr.load("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, objects);*/


            foreach (Volume v in ven1)
            {
                v.loader("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, v);
                v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            }
            foreach (Volume v in ven2)
            {
                v.loader("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, v);
                v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            }
            foreach (Volume v in ven3)
            {
                v.loader("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, v);
                v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            }
            foreach (Volume v in air1)
            {
                v.loader("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, v);
                v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            }
            foreach (Volume v in air2)
            {
                v.loader("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, v);
                v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            }
            foreach (Volume v in air3)
            {
                v.loader("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, v);
                v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            }
            foreach (Volume v in char1)
            {
                v.loader("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, v);
                v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            }
            foreach (Volume v in char2)
            {
                v.loader("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, v);
                v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            }
            foreach (Volume v in char3)
            {
                v.loader("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, v);
                v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            }
            foreach (Volume v in ter)
            {
                v.loader("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, v);
                v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            }
            foreach (Volume v in pikachu)
            {
                v.loader2("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, 0.0f, 6.2f, 0.0f, v);
                v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            }
            for (int i = 0; i < asset.Length - 1; i++)
            {
                asset[i].load("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y);
                asset[i].setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            }
            //Atap
            asset[10].load("../../../Shader/transparent.vert", "../../../Shader/transparent.frag", Size.X, Size.Y);
            asset[10].setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);

            for (int i = 0; i < tuguBawah.Length; i++)
            {
                tuguBawah[i].load("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y);
                tuguBawah[i].setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            }
            for (int i = 0; i < tuguAtas.Length; i++)
            {
                tuguAtas[i].load("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y);
                tuguAtas[i].setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            }
            for (int i = 0; i < kaca.Length; i++)
            {
                kaca[i].load("../../../Shader/transparent.vert", "../../../Shader/transparent.frag", Size.X, Size.Y);
                kaca[i].setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            }
            for (int i = 0; i < Karpet.Length; i++)
            {
                Karpet[i].load("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y);
                Karpet[i].setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            }
            camObj.load("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y);
            camObj.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            /*lr.setPointLights(_pointLightPositions, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);*/
            /*LightObject.load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);*/
            /* for (int i = 0; i < 10; i++)
             {
                 _object3d[i] = new Asset3d();
                 _object3d[i].createBoxVertices2(_cubePositions[i].X, _cubePositions[i].Y, _cubePositions[i].Z, 0.5f);
                 _object3d[i].load(Constants.path + "objectnew.vert", Constants.path + "objectnew.frag", Size.X, Size.Y);
                 _object3d[i].setPointLights(_pointLightPositions, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);

             }*/
            /*for (int i = 0; i < 4; i++)
            {
                LightObjects[i] = new Asset3d();
                LightObjects[i].createBoxVertices2(_pointLightPositionson[i].X, _pointLightPositionson[i].Y, _pointLightPositionson[i].Z, 0.5f, 0.5f, 0.5f);
                LightObjects[i].load(Constants.path + "shader.vert", Constants.path + "shader.frag", Size.X, Size.Y);
            }*/
            CursorGrabbed = true;
        }
        int loadImage(Bitmap image)
        {
            int texID = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, texID);
            BitmapData data = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0,
                (OpenTK.Graphics.OpenGL4.PixelFormat)OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            image.UnlockBits(data);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return texID;
        }

        int loadImage(string filename)
        {
            try
            {
                Bitmap file = new Bitmap(filename);
                return loadImage(file);
            }
            catch (FileNotFoundException e)
            {
                return -1;
            }
        }

        private void loadMaterials(String filename)
        {
            foreach (var mat in Material.LoadFromFile(filename))
            {
                if (!materials.ContainsKey(mat.Key))
                {
                    materials.Add(mat.Key, mat.Value);
                }
            }

            // Load textures
            foreach (Material mat in materials.Values)
            {
                if (File.Exists(mat.AmbientMap) && !textures.ContainsKey(mat.AmbientMap))
                {
                    textures.Add(mat.AmbientMap, loadImage(mat.AmbientMap));
                }

                if (File.Exists(mat.DiffuseMap) && !textures.ContainsKey(mat.DiffuseMap))
                {
                    textures.Add(mat.DiffuseMap, loadImage(mat.DiffuseMap));
                }

                if (File.Exists(mat.SpecularMap) && !textures.ContainsKey(mat.SpecularMap))
                {
                    textures.Add(mat.SpecularMap, loadImage(mat.SpecularMap));
                }

                if (File.Exists(mat.NormalMap) && !textures.ContainsKey(mat.NormalMap))
                {
                    textures.Add(mat.NormalMap, loadImage(mat.NormalMap));
                }

                if (File.Exists(mat.OpacityMap) && !textures.ContainsKey(mat.OpacityMap))
                {
                    textures.Add(mat.OpacityMap, loadImage(mat.OpacityMap));
                }
            }
        }
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            base.OnRenderFrame(args);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            Matrix4 temp = Matrix4.Identity;
            _time += 7.0 * args.Time;
            deg += MathHelper.DegreesToRadians(20f);

            /*for (int i = 0; i < asset.Length; i++)
            {
                asset[i].setFragVariable(new Vector3(1.0f, 0.5f, 0.31f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f));
                asset[i].render(0, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix());
            }
            asset[0].setFragVariable(new Vector3(1.0f, 0.5f, 0.31f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.2f, 1.0f, 2.0f), camera.Position);
            asset[0].render(1, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix());
            //asset[1].render(1, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix());*/
            foreach (Volume v in ven1)
            {
                v.renderer(2, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), v, -2.8f, 0.0f, -20.0f, 0.3f, 0, 90, 0);
                v.setFragVariable(new Vector3(0.0f, 0.8f, 0.3f), camera.Position);
                v.setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                        1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                if(senter == true)
                {
                    v.setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                }
            }
            foreach (Volume v in ven2)
            {
                v.renderer(2, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), v, -3.4f, 0.0f, -6.8f, 1.0f, 0, 0, 0);
                v.setFragVariable(new Vector3(0.0f, 0.8f, 0.3f), camera.Position);
                v.setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                        1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                if (senter == true)
                {
                    v.setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                }
            }
            foreach (Volume v in ven3)
            {
                v.renderer(2, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), v, -1.6f, 0.0f, -2.02f, 3.0f, 0, -90, 0);
                v.setFragVariable(new Vector3(0.0f, 0.8f, 0.3f), camera.Position);
                v.setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                        1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                if (senter == true)
                {
                    v.setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                }
            }
            foreach (Volume v in air1)
            {
                v.renderer(2, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), v, 2.8f, 0.15f, -3.5f, 1.7f, 0, 90, 0);
                v.setFragVariable(new Vector3(0.0f, 0.3f, 0.8f), camera.Position);
                v.setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                        1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                if (senter == true)
                {
                    v.setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                }
            }
            foreach (Volume v in air2)
            {
                v.renderer(2, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), v, 2.3f, 0.3f, -6.8f, 1.0f, 0, 0, 0);
                v.setFragVariable(new Vector3(0.0f, 0.3f, 0.8f), camera.Position);
                v.setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                        1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                if (senter == true)
                {
                    v.setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                }
            }
            foreach (Volume v in air3)
            {
                v.renderer(2, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), v, 2.0f, 0.0f, -5.9f, 1.0f, 0, -90, 0);
                v.setFragVariable(new Vector3(0.0f, 0.3f, 0.8f), camera.Position);
                v.setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                        1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                if (senter == true)
                {
                    v.setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                }
            }
            foreach (Volume v in char1)
            {
                v.renderer(2, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), v, 1.9f, 0.0f, -5.8f, 1.0f, 0, 90, 0);
                v.setFragVariable(new Vector3(0.8f, 0.2f, 0.2f), camera.Position);
                v.setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                        1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                if (senter == true)
                {
                    v.setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                }
            }
            foreach (Volume v in char2)
            {
                v.renderer(2, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), v, -0.7f, 0.0f, -6.3f, 1.0f, 0, 0, 0);
                v.setFragVariable(new Vector3(0.8f, 0.2f, 0.2f), camera.Position);
                v.setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                        1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                if (senter == true)
                {
                    v.setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                }
            }
            foreach (Volume v in char3)
            {
                v.renderer(2, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), v, -1.3f, 0.0f, -5.5f, 1.0f, 0, -90, 0);
                v.setFragVariable(new Vector3(0.8f, 0.2f, 0.2f), camera.Position);
                v.setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                        1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                if (senter == true)
                {
                    v.setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                }
            }
            foreach (Volume v in pikachu)
            {
                v.renderer2(2, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), v, 0.0f, 0.0f, 0.0f, 1.0f, 0, 0, 0, 0.1f);
                v.setFragVariable(new Vector3(0.964f, 0.811f, 0.341f), camera.Position);
                v.setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                        1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                if (senter == true)
                {
                    v.setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                }
            }
            foreach (Volume v in ter)
            {
                v.renderer(2, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), v, 0.0f, -3.0f, -2.8f, 1.0f, 0, 0, 0);
                v.setFragVariable(new Vector3(0.4f, 0.4f, 0.4f), camera.Position);
                v.setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                        1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                if (senter == true)
                {
                    v.setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                }
            }

            for (int i = 0; i < tuguBawah.Length; i++)
            {
                tuguBawah[i].render(1, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0.0f, -0.5f, 0.0f, 1.0f, 0, 0, 0);
                tuguBawah[i].setFragVariable(new Vector3(0.831f, 0.686f, 0.215f), camera.Position);
                //  asset[i].setDirectionalLight(new Vector3(-0.2f, -1.0f, -0.3f), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
                // asset[i].setPointLight(LightObject._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                tuguBawah[i].setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                if (senter == true)
                {
                    tuguBawah[i].setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                }
            }

            for (int i = 0; i < tuguAtas.Length; i++)
            {
                tuguAtas[i].render(1, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0.0f, -0.5f, 0.0f, 1.0f, 0, 0, 0);
                tuguAtas[i].setFragVariable(new Vector3(0.0431f, 0.0431f, 0.2705f), camera.Position);
                //  asset[i].setDirectionalLight(new Vector3(-0.2f, -1.0f, -0.3f), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
                // asset[i].setPointLight(LightObject._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                tuguAtas[i].setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                if (senter == true)
                {
                    tuguAtas[i].setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                }
            }

            for (int i = 0; i < kaca.Length; i++)
            {
                kaca[i].render(1, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0.0f, -0.5f, 0.0f, 1.0f, 0, 0, 0);
                kaca[i].setFragVariable(new Vector3(0.647f, 0.8705f, 0.9803f), camera.Position);
                //  asset[i].setDirectionalLight(new Vector3(-0.2f, -1.0f, -0.3f), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
                // asset[i].setPointLight(LightObject._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                kaca[i].setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                if (senter == true)
                {
                    kaca[i].setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                }
            }
            for (int i = 0; i < Karpet.Length; i++)
            {                                                               
                Karpet[i].render(1, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0.0f, 0f, 0f, 1.0f, 0, 0, 0);
                Karpet[i].setFragVariable(new Vector3(1.0f, 0.0f, .0f), camera.Position);
                //[i].setDirectionalLight(new Vector3(-0.2f, -1.0f, -0.3f), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
                // asset[i].setPointLight(LightObject._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                Karpet[i].setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                if (senter == true)
                {
                    Karpet[i].setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                }
            }
            //alas
            asset[0].render(1, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0.0f, -0.5f, 0.0f, 1.0f, 0, 0, 0);
            asset[0].setFragVariable(new Vector3(1.0f, 1.0f, 1.0f), camera.Position);
            //  asset[i].setDirectionalLight(new Vector3(-0.2f, -1.0f, -0.3f), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
            // asset[i].setPointLight(LightObject._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            asset[0].setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            if (senter == true)
            {
                asset[0].setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            }

            //kanan
            asset[1].render(1, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 2.6f, -7.3f, 0.0f, 1.0f, 0, 0, 90);
            asset[1].setFragVariable(new Vector3(1.0f, 1.0f, 1.0f), camera.Position);
            //  asset[i].setDirectionalLight(new Vector3(-0.2f, -1.0f, -0.3f), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
            // asset[i].setPointLight(LightObject._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            asset[1].setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            if (senter == true)
            {
                asset[1].setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            }

            //kiri
            asset[2].render(1, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 2.6f, 8.3f, 0.0f, 1.0f, 0, 0, 90);
            asset[2].setFragVariable(new Vector3(1.0f, 1.0f, 1.0f), camera.Position);
            //  asset[i].setDirectionalLight(new Vector3(-0.2f, -1.0f, -0.3f), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
            // asset[i].setPointLight(LightObject._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            asset[2].setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            if (senter == true)
            {
                asset[2].setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            }

            //belakang
            asset[3].render(1, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0.0f, -7.3f, -0.75f, 1.0f, 90, 0, 0);
            asset[3].setFragVariable(new Vector3(1.0f, 1.0f, 1.0f), camera.Position);
            //  asset[i].setDirectionalLight(new Vector3(-0.2f, -1.0f, -0.3f), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
            // asset[i].setPointLight(LightObject._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            asset[3].setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            if (senter == true)
            {
                asset[3].setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            }

            //Tangga
            asset[4].render(1, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0.0f, -0.8f, 0.5f, 1.0f, 0, 0, 0);
            asset[4].setFragVariable(new Vector3(1.0f, 1.0f, 1.0f), camera.Position);
            //  asset[i].setDirectionalLight(new Vector3(-0.2f, -1.0f, -0.3f), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
            // asset[i].setPointLight(LightObject._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            asset[4].setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            if (senter == true)
            {
                asset[4].setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            }

            asset[5].render(1, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0.0f, -1.1f, 1.0f, 1.0f, 0, 0, 0);
            asset[5].setFragVariable(new Vector3(1.0f, 1.0f, 1.0f), camera.Position);
            //  asset[i].setDirectionalLight(new Vector3(-0.2f, -1.0f, -0.3f), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
            // asset[i].setPointLight(LightObject._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            asset[5].setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            if (senter == true)
            {
                asset[5].setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            }
            asset[6].render(1, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0.0f, -1.4f, 1.5f, 1.0f, 0, 0, 0);
            asset[6].setFragVariable(new Vector3(1.0f, 1.0f, 1.0f), camera.Position);
            //  asset[i].setDirectionalLight(new Vector3(-0.2f, -1.0f, -0.3f), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
            // asset[i].setPointLight(LightObject._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            asset[6].setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            if (senter == true)
            {
                asset[6].setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            }

            asset[7].render(1, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0.0f, -1.7f, 2.0f, 1.0f, 0, 0, 0);
            asset[7].setFragVariable(new Vector3(1.0f, 1.0f, 1.0f), camera.Position);
            //  asset[i].setDirectionalLight(new Vector3(-0.2f, -1.0f, -0.3f), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
            // asset[i].setPointLight(LightObject._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            asset[7].setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            if (senter == true)
            {
                asset[7].setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            }

            asset[8].render(1, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0.0f, -2.0f, 2.5f, 1.0f, 0, 0, 0);
            asset[8].setFragVariable(new Vector3(1.0f, 1.0f, 1.0f), camera.Position);
            //  asset[i].setDirectionalLight(new Vector3(-0.2f, -1.0f, -0.3f), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
            // asset[i].setPointLight(LightObject._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            asset[8].setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            if (senter == true)
            {
                asset[8].setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            }

            asset[9].render(1, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0.0f, -2.3f, 3.0f, 1.0f, 0, 0, 0);
            asset[9].setFragVariable(new Vector3(1.0f, 1.0f, 1.0f), camera.Position);
            //  asset[i].setDirectionalLight(new Vector3(-0.2f, -1.0f, -0.3f), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
            // asset[i].setPointLight(LightObject._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            asset[9].setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            if (senter == true)
            {
                asset[9].setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            }

            //Atap
            asset[10].render(1, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0.0f, -2.3f, 3.0f, 1.0f, 0, 0, 0);
            asset[10].setFragVariable(new Vector3(1.0f, 1.0f, 1.0f), camera.Position);
            //  asset[i].setDirectionalLight(new Vector3(-0.2f, -1.0f, -0.3f), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
            // asset[i].setPointLight(LightObject._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
            asset[10].setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            if (senter == true)
            {
                asset[10].setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            }
            if (camObjMati == false)
            {
                camObj.render(1, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), camx, camy, camz, 1.0f, 0, 0, 0);
                camObj.setFragVariable(new Vector3(0.7f, 0.0f, 1.0f), camera.Position);
                //  asset[i].setDirectionalLight(new Vector3(-0.2f, -1.0f, -0.3f), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
                // asset[i].setPointLight(LightObject._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                camObj.setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                if (senter == true)
                {
                    camObj.setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                }
            }
            /*LightObject.render(1, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0.0f, 0.0f, 0.0f, 1.0f, 0, 0, 0);*/
            /*for (int i = 0; i < 10; i++)
            {
                _object3d[i].render(1, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix());
                _object3d[i].setFragVariable(new Vector3(1.0f, 0.5f, 0.31f), camera.Position);
                //  _object3d[i].setDirectionalLight(new Vector3(-0.2f, -1.0f, -0.3f), new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.4f, 0.4f, 0.4f), new Vector3(0.5f, 0.5f, 0.5f));
                //_object3d[i].setPointLight(LightObject._centerPosition, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                _object3d[i].setSpotLight(new Vector3(0, 5, 0), new Vector3(0, -1, 0), new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
                _object3d[i].setSpotLight(camera.Position, camera.Front, new Vector3(0.0f, 0.0f, 0.0f), new Vector3(1.0f, 1.0f, 1.0f), new Vector3(1.0f, 1.0f, 1.0f),
                    1.0f, 0.09f, 0.032f, MathF.Cos(MathHelper.DegreesToRadians(12.5f)), MathF.Cos(MathHelper.DegreesToRadians(12.5f)));
            }*/
            /* for (int i = 0; i < 4; i++)
             {
                 LightObjects[i].render(1, _time, temp, camera.GetViewMatrix(), camera.GetProjectionMatrix(), 0.0f, 0.0f, 0.0f, 1.0f, 0, 0, 0);
             }*/
            SwapBuffers();
        }

        protected override void OnMouseWheel(MouseWheelEventArgs e)
        {
            base.OnMouseWheel(e);
            camera.Fov = camera.Fov - e.OffsetY;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0, 0, Size.X, Size.Y);
            camera.AspectRatio = Size.X / (float)Size.Y;
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {

            base.OnUpdateFrame(args);

            /*objects[0].Scale = new Vector3(0.9999f, 0.9999f, 0.9999f);*/
            
            var input = KeyboardState; //Var ini menyimpan status keyboard ketika window dinyalakan
            var mouse_input = MouseState; //Var ini menyimpan kondisi mouse
            float cameraSpeed = 1.5f;

            if (input.IsKeyDown(Keys.Down) || input.IsKeyDown(Keys.S))
            {
                if (camera.Position.X > 4.0f)
                {
                    camera.Position = new Vector3(3.9f, camera.Position.Y, camera.Position.Z);
                }
                else if (camera.Position.X < -4.7f)
                {
                    camera.Position = new Vector3(-4.6f, camera.Position.Y, camera.Position.Z);
                }
                else if (camera.Position.Y < -0.7f)
                {
                    camera.Position = new Vector3(camera.Position.X, -0.6f, camera.Position.Z);
                }
                else if (camera.Position.Y > 5.6f)
                {
                    camera.Position = new Vector3(camera.Position.X, 5.5f, camera.Position.Z);
                }
                else if (camera.Position.Z < -5.7f)
                {
                    camera.Position = new Vector3(camera.Position.X, camera.Position.Y, -5.6f);
                }
                else
                {
                    camera.Position -= camera.Front * cameraSpeed * (float)args.Time;
                    camz += cameraSpeed * (float)args.Time;
                }
            }
            if (input.IsKeyDown(Keys.Up) || input.IsKeyDown(Keys.W))
            {
                if (camera.Position.X > 4.0f)
                {
                    camera.Position = new Vector3(3.9f, camera.Position.Y, camera.Position.Z);
                }
                else if (camera.Position.X < -4.7f)
                {
                    camera.Position = new Vector3(-4.6f, camera.Position.Y, camera.Position.Z);
                }
                else if (camera.Position.Y < -0.7f)
                {
                    camera.Position = new Vector3(camera.Position.X, -0.6f, camera.Position.Z);
                }
                else if (camera.Position.Y > 5.6f)
                {
                    camera.Position = new Vector3(camera.Position.X, 5.5f, camera.Position.Z);
                }
                else if (camera.Position.Z < -5.7f)
                {
                    camera.Position = new Vector3(camera.Position.X, camera.Position.Y, -5.6f);
                }
                else
                {
                    camera.Position += camera.Front * cameraSpeed * (float)args.Time;
                    camz -= cameraSpeed * (float)args.Time;
                }
            }
            if (input.IsKeyDown(Keys.Left) || input.IsKeyDown(Keys.A))
            {
                if (camera.Position.X > 4.0f)
                {
                    camera.Position = new Vector3(3.9f, camera.Position.Y, camera.Position.Z);
                }
                else if (camera.Position.X < -4.7f)
                {
                    camera.Position = new Vector3(-4.6f, camera.Position.Y, camera.Position.Z);
                }
                else if (camera.Position.Y < -0.7f)
                {
                    camera.Position = new Vector3(camera.Position.X, -0.6f, camera.Position.Z);
                }
                else if (camera.Position.Y > 5.6f)
                {
                    camera.Position = new Vector3(camera.Position.X, 5.5f, camera.Position.Z);
                }
                else if (camera.Position.Z < -5.7f)
                {
                    camera.Position = new Vector3(camera.Position.X, camera.Position.Y, -5.6f);
                }
                else
                {
                    camera.Position -= camera.Right * cameraSpeed * (float)args.Time;
                    camx -= cameraSpeed * (float)args.Time;
                }
            }
            if (input.IsKeyDown(Keys.Right) || input.IsKeyDown(Keys.D))
            {
                if (camera.Position.X > 4.0f)
                {
                    camera.Position = new Vector3(3.9f, camera.Position.Y, camera.Position.Z);
                }
                else if (camera.Position.X < -4.7f)
                {
                    camera.Position = new Vector3(-4.6f, camera.Position.Y, camera.Position.Z);
                }
                else if (camera.Position.Y < -0.7f)
                {
                    camera.Position = new Vector3(camera.Position.X, -0.6f, camera.Position.Z);
                }
                else if (camera.Position.Y > 5.6f)
                {
                    camera.Position = new Vector3(camera.Position.X, 5.5f, camera.Position.Z);
                }
                else if (camera.Position.Z < -5.7f)
                {
                    camera.Position = new Vector3(camera.Position.X, camera.Position.Y, -5.6f);
                }
                else
                {
                    camera.Position += camera.Right * cameraSpeed * (float)args.Time;
                    camx += cameraSpeed * (float)args.Time;
                }
            }

            var mouse = MouseState;
            var sensitivity = 0.2f;

            if (_firstMove)
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }
            else
            {
                var deltaX = mouse.X - _lastPos.X;  
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);
                camera.Yaw += deltaX * sensitivity;   //kekanan atau kekiri
                camera.Pitch -= deltaY * sensitivity; //keatas kebawah ada lagi Roll (ke sumbu X)
                camy -= MathHelper.DegreesToRadians(deltaY * sensitivity);
                camx += MathHelper.DegreesToRadians(deltaX * sensitivity);
                /*camz = camera.Position.Z ;*/
            }

            if (KeyboardState.IsKeyDown(Keys.N))
            {
                var axis = new Vector3(0, 1, 0);
                camera.Position -= _objecPost;
                camera.Yaw += _rotationSpeed;
                camera.Position = Vector3.Transform(camera.Position,
                    generateArbRotationMatrix(axis, _objecPost, _rotationSpeed).ExtractRotation());
                camera.Position += _objecPost;

                camera._front = -Vector3.Normalize(camera.Position - _objecPost);
            }
            if (KeyboardState.IsKeyDown(Keys.Comma))
            {
                var axis = new Vector3(0, 1, 0);
                camera.Position -= _objecPost;
                camera.Yaw -= _rotationSpeed;
                camera.Position = Vector3.Transform(camera.Position,
                    generateArbRotationMatrix(axis, _objecPost, -_rotationSpeed).ExtractRotation());
                camera.Position += _objecPost;

                camera._front = -Vector3.Normalize(camera.Position - _objecPost);
            }
            if (KeyboardState.IsKeyDown(Keys.K))
            {
                var axis = new Vector3(1, 0, 0);
                camera.Position -= _objecPost;
                camera.Pitch -= _rotationSpeed;
                camera.Position = Vector3.Transform(camera.Position,
                    generateArbRotationMatrix(axis, _objecPost, _rotationSpeed).ExtractRotation());
                camera.Position += _objecPost;
                camera._front = -Vector3.Normalize(camera.Position - _objecPost);
            }
            if (KeyboardState.IsKeyDown(Keys.M))
            {
                var axis = new Vector3(1, 0, 0);
                camera.Position -= _objecPost;
                camera.Pitch += _rotationSpeed;
                camera.Position = Vector3.Transform(camera.Position,
                    generateArbRotationMatrix(axis, _objecPost, -_rotationSpeed).ExtractRotation());
                camera.Position += _objecPost;
                camera._front = -Vector3.Normalize(camera.Position - _objecPost);
            }
            if (input.IsKeyDown(Keys.V))
            {
                camera.Fov = temp_fov;
            }
            if (input.IsKeyDown(Keys.T))
            {
                if(camObjMati == false)
                {
                    camObjMati = true;
                }
                else
                {
                    camObjMati = false;
                }
            }
            // Naik (Spasi)
            if (KeyboardState.IsKeyDown(Keys.Space))
            {
                if (camera.Position.X > 4.0f)
                {
                    camera.Position = new Vector3(3.9f, camera.Position.Y, camera.Position.Z);
                }
                else if (camera.Position.X < -4.7f)
                {
                    camera.Position = new Vector3(-4.6f, camera.Position.Y, camera.Position.Z);
                }
                else if (camera.Position.Y < -0.7f)
                {
                    camera.Position = new Vector3(camera.Position.X, -0.6f, camera.Position.Z);
                }
                else if (camera.Position.Y > 5.6f)
                {
                    camera.Position = new Vector3(camera.Position.X, 5.5f, camera.Position.Z);
                }
                else if (camera.Position.Z < -5.7f)
                {
                    camera.Position = new Vector3(camera.Position.X, camera.Position.Y, -5.6f);
                }
                else
                {
                    camera.Position += camera.Up * cameraSpeed * (float)args.Time;
                    camy += cameraSpeed * (float)(args.Time);
                }
                
            }
            // Turun (Ctrl)
            if (KeyboardState.IsKeyDown(Keys.LeftControl))
            {
                if (camera.Position.X > 4.0f)
                {
                    camera.Position = new Vector3(3.9f, camera.Position.Y, camera.Position.Z);
                }
                else if (camera.Position.X < -4.7f)
                {
                    camera.Position = new Vector3(-4.6f, camera.Position.Y, camera.Position.Z);
                }
                else if (camera.Position.Y < -0.7f)
                {
                    camera.Position = new Vector3(camera.Position.X, -0.6f, camera.Position.Z);
                }
                else if (camera.Position.Y > 5.6f)
                {
                    camera.Position = new Vector3(camera.Position.X, 5.5f, camera.Position.Z);
                }
                else if (camera.Position.Z < -5.7f)
                {
                    camera.Position = new Vector3(camera.Position.X, camera.Position.Y, -5.6f);
                }
                else
                {
                    camera.Position -= camera.Up * cameraSpeed * (float)args.Time;
                    camy -= cameraSpeed * (float)args.Time;
                }
            }
            if (input.IsKeyDown(Keys.F))
            {
                if (senter == true)
                {
                    senter = false;
                }
                else
                {
                    senter = true;
                }
            }
            if (input.IsKeyDown(Keys.O))
            {
                count++;
                if (count % 2 == 0)
                {
                    foreach (Volume v in ven1)
                    {
                        v.loader("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, v);
                        v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    foreach (Volume v in ven2)
                    {
                        v.loader("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, v);
                        v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    foreach (Volume v in ven3)
                    {
                        v.loader("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, v);
                        v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    foreach (Volume v in air1)
                    {
                        v.loader("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, v);
                        v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    foreach (Volume v in air2)
                    {
                        v.loader("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, v);
                        v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    foreach (Volume v in air3)
                    {
                        v.loader("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, v);
                        v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    foreach (Volume v in char1)
                    {
                        v.loader("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, v);
                        v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    foreach (Volume v in char2)
                    {
                        v.loader("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, v);
                        v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    foreach (Volume v in char3)
                    {
                        v.loader("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, v);
                        v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    foreach (Volume v in ter)
                    {
                        v.loader("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, v);
                        v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    foreach (Volume v in pikachu)
                    {
                        v.loader2("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y, 0.0f, 0.0f, 0.0f, v);
                        v.setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    for (int i = 0; i < asset.Length - 1; i++)
                    {
                        asset[i].load("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y);
                        asset[i].setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }

                    asset[10].load("../../../Shader/transparent.vert", "../../../Shader/transparent.frag", Size.X, Size.Y);
                    asset[10].setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);

                    for (int i = 0; i < tuguBawah.Length; i++)
                    {
                        tuguBawah[i].load("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y);
                        tuguBawah[i].setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    for (int i = 0; i < tuguAtas.Length; i++)
                    {
                        tuguAtas[i].load("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y);
                        tuguAtas[i].setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    for (int i = 0; i < kaca.Length; i++)
                    {
                        kaca[i].load("../../../Shader/transparent.vert", "../../../Shader/transparent.frag", Size.X, Size.Y);
                        kaca[i].setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    for (int i = 0; i < Karpet.Length; i++)
                    {
                        Karpet[i].load("../../../Shader/objectnew.vert", "../../../Shader/objectnew.frag", Size.X, Size.Y);
                        Karpet[i].setPointLights(_pointLightPositionson, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                }
                else
                {
                    foreach (Volume v in ven1)
                    {
                        v.loader("../../../Shader/objectnew_light.vert", "../../../Shader/objectnew_light.frag", Size.X, Size.Y, v);
                        v.setPointLights(_pointLightPositionsoff, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    foreach (Volume v in ven2)
                    {
                        v.loader("../../../Shader/objectnew_light.vert", "../../../Shader/objectnew_light.frag", Size.X, Size.Y, v);
                        v.setPointLights(_pointLightPositionsoff, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    foreach (Volume v in ven3)
                    {
                        v.loader("../../../Shader/objectnew_light.vert", "../../../Shader/objectnew_light.frag", Size.X, Size.Y, v);
                        v.setPointLights(_pointLightPositionsoff, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    foreach (Volume v in air1)
                    {
                        v.loader("../../../Shader/objectnew_light.vert", "../../../Shader/objectnew_light.frag", Size.X, Size.Y, v);
                        v.setPointLights(_pointLightPositionsoff, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    foreach (Volume v in air2)
                    {
                        v.loader("../../../Shader/objectnew_light.vert", "../../../Shader/objectnew_light.frag", Size.X, Size.Y, v);
                        v.setPointLights(_pointLightPositionsoff, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    foreach (Volume v in air3)
                    {
                        v.loader("../../../Shader/objectnew_light.vert", "../../../Shader/objectnew_light.frag", Size.X, Size.Y, v);
                        v.setPointLights(_pointLightPositionsoff, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    foreach (Volume v in char1)
                    {
                        v.loader("../../../Shader/objectnew_light.vert", "../../../Shader/objectnew_light.frag", Size.X, Size.Y, v);
                        v.setPointLights(_pointLightPositionsoff, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    foreach (Volume v in char2)
                    {
                        v.loader("../../../Shader/objectnew_light.vert", "../../../Shader/objectnew_light.frag", Size.X, Size.Y, v);
                        v.setPointLights(_pointLightPositionsoff, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    foreach (Volume v in char3)
                    {
                        v.loader("../../../Shader/objectnew_light.vert", "../../../Shader/objectnew_light.frag", Size.X, Size.Y, v);
                        v.setPointLights(_pointLightPositionsoff, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    foreach (Volume v in ter)
                    {
                        v.loader("../../../Shader/objectnew_light.vert", "../../../Shader/objectnew_light.frag", Size.X, Size.Y, v);
                        v.setPointLights(_pointLightPositionsoff, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    foreach (Volume v in pikachu)
                    {
                        v.loader2("../../../Shader/objectnew_light.vert", "../../../Shader/objectnew_light.frag", Size.X, Size.Y, 0.0f, 0.0f, 0.0f, v);
                        v.setPointLights(_pointLightPositionsoff, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    for (int i = 0; i < asset.Length - 1; i++)
                    {
                        asset[i].load("../../../Shader/objectnew_light.vert", "../../../Shader/objectnew_light.frag", Size.X, Size.Y);
                        asset[i].setPointLights(_pointLightPositionsoff, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    asset[10].load("../../../Shader/transparentoff.vert", "../../../Shader/transparentoff.frag", Size.X, Size.Y);
                    asset[10].setPointLights(_pointLightPositionsoff, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);

                    for (int i = 0; i < tuguBawah.Length; i++)
                    {
                        tuguBawah[i].load("../../../Shader/objectnew_light.vert", "../../../Shader/objectnew_light.frag", Size.X, Size.Y);
                        tuguBawah[i].setPointLights(_pointLightPositionsoff, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    for (int i = 0; i < tuguAtas.Length; i++)
                    {
                        tuguAtas[i].load("../../../Shader/objectnew_light.vert", "../../../Shader/objectnew_light.frag", Size.X, Size.Y);
                        tuguAtas[i].setPointLights(_pointLightPositionsoff, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    for (int i = 0; i < kaca.Length; i++)
                    {
                        kaca[i].load("../../../Shader/transparentoff.vert", "../../../Shader/transparentoff.frag", Size.X, Size.Y);
                        kaca[i].setPointLights(_pointLightPositionsoff, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                    for (int i = 0; i < Karpet.Length; i++)
                    {
                        Karpet[i].load("../../../Shader/objectnew_light.vert", "../../../Shader/objectnew_light.frag", Size.X, Size.Y);
                        Karpet[i].setPointLights(_pointLightPositionsoff, new Vector3(0.05f, 0.05f, 0.05f), new Vector3(0.8f, 0.8f, 0.8f), new Vector3(1.0f, 1.0f, 1.0f), 1.0f, 0.09f, 0.032f);
                    }
                }
            }
        }
    }
}
