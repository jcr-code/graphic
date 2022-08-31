using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graf1
{
    internal class Asset3d
    {
        List<Vector3> _vertices = new List<Vector3>();
        List<uint> _indices = new List<uint>();


        int _vertexBufferObject;
        int _vertexArrayObject;
        int _elementBufferObject;
        Shader _shader;

        /*Matrix4 _view;
        Matrix4 _projection;*/

        public Vector3 _centerPosition = new Vector3(0, 0, 0);
        public List<Vector3> _euler = new List<Vector3>();

        public Asset3d(List<Vector3> vertices, List<uint> indices)
        {
            this._vertices = vertices;
            this._indices = indices;
        }

        public Asset3d()
        {
            _vertices = new List<Vector3>();
            //sumbu X
            _euler.Add(new Vector3(1, 0, 0));
            //sumbu Y
            _euler.Add(new Vector3(0, 1, 0));
            //sumbu Z
            _euler.Add(new Vector3(0, 0, 1));

        }
        public void load(string shadervert, string shaderfrag, float Size_x, float Size_y)
        {
            //Inisialisasi VBO
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Count * Vector3.SizeInBytes, _vertices.ToArray(), BufferUsageHint.StaticDraw);

            //VAO
            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);
            //parameter 1 --> variable _vertices nya itu disimpan di shader index
            //keberapa?
            //parameter 2 --> didalam variable _vertices, ada berapa vertex?
            //paramter 3  --> jenis vertex yang dikirim typenya apa?
            //parameter 4 --> datanya perlu dinormalisasi ndak?
            //parameter 5 --> dalam 1 vertex/1 baris itu mengandung berapa banyak
            //titik?
            //parameter 6 --> data yang mau diolah mulai dari vertex ke berapa
            /*GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);*/

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            //Jika terdapat data pada indikasi baru inisialisasi Elemetn Buffer Object
            if (_indices.Count != 0)
            {
                _elementBufferObject = GL.GenBuffer(); //ini yang menghandle
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, _indices.Count * sizeof(uint), _indices.ToArray(), BufferUsageHint.StaticDraw);
            }
            _shader = new Shader(shadervert, shaderfrag);
            _shader.Use();

            /*_view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Size_x / (float)Size_y, 0.1f, 100.0f);*/

        }

       public void createBox()
        {
            //FACE
            //SEGITIGA BACK 1
            _vertices.Add(new Vector3(-0.5f, -0.5f, 0.5f));
            _vertices.Add(new Vector3(0.5f, -0.5f, 0.5f));
            _vertices.Add(new Vector3(0.5f, 0.5f, 0.5f));
            //SEGITIGA BACK 2
            _vertices.Add(new Vector3(0.5f, 0.5f, 0.5f));
            _vertices.Add(new Vector3(-0.5f, 0.5f, 0.5f));
            _vertices.Add(new Vector3(-0.5f, -0.5f, 0.5f));
            //LEFT FACE
            //SEGITIGA LEFT 1
            _vertices.Add(new Vector3(-0.5f, 0.5f, 0.5f));
            _vertices.Add(new Vector3(-0.5f, 0.5f, -0.5f));
            _vertices.Add(new Vector3(-0.5f, -0.5f, -0.5f));
            //SEGITIGA LEFT 2
            _vertices.Add(new Vector3(-0.5f, -0.5f, -0.5f));
            _vertices.Add(new Vector3(-0.5f, -0.5f, 0.5f));
            _vertices.Add(new Vector3(-0.5f, 0.5f, 0.5f));
            //RIGHT FACE
            //SEGITIGA RIGHT 1
            _vertices.Add(new Vector3(0.5f, 0.5f, 0.5f));
            _vertices.Add(new Vector3(0.5f, 0.5f, -0.5f));
            _vertices.Add(new Vector3(0.5f, -0.5f, -0.5f));
            //SEGITIGA LEFT 2
            _vertices.Add(new Vector3(0.5f, -0.5f, -0.5f));
            _vertices.Add(new Vector3(0.5f, -0.5f, 0.5f));
            _vertices.Add(new Vector3(0.5f, 0.5f, 0.5f));
            //BOTTOM FACE
            //SEGITIGA BOTTOM 1
            _vertices.Add(new Vector3(-0.5f, -0.5f, -0.5f));
            _vertices.Add(new Vector3(0.5f, -0.5f, -0.5f));
            _vertices.Add(new Vector3(0.5f, -0.5f, 0.5f));
            //SEGITIGA BOTTOM 2
            _vertices.Add(new Vector3(0.5f, -0.5f, 0.5f));
            _vertices.Add(new Vector3(-0.5f, -0.5f, 0.5f));
            _vertices.Add(new Vector3(-0.5f, -0.5f, -0.5f));
            //FRONT FACE
            //SEGITIGA BOTTOM 1
            _vertices.Add(new Vector3(-0.5f, 0.5f, -0.5f));
            _vertices.Add(new Vector3(0.5f, 0.5f, -0.5f));
            _vertices.Add(new Vector3(0.5f, 0.5f, 0.5f));
            //SEGITIGA BOTTOM 2
            _vertices.Add(new Vector3(0.5f, 0.5f, 0.5f));
            _vertices.Add(new Vector3(-0.5f, 0.5f, 0.5f));
            _vertices.Add(new Vector3(-0.5f, 0.5f, -0.5f));

        }

        public void createBoxVertices2(float x, float y, float z, float lengthx, float lengthy, float lengthz)
        {
            _centerPosition.X = x;
            _centerPosition.Y = y;
            _centerPosition.Z = z;
            Vector3 temp_vector;

            //FRONT FACE

            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, -1.0f));


            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, -1.0f));

            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, -1.0f));

            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, -1.0f));

            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, -1.0f));

            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, -1.0f));

            //BACK FACE
            //TITIK 5
            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, 1.0f));

            //TITIK 6
            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, 1.0f));
            //TITIK 7
            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, 1.0f));

            //TITIK 6
            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, 1.0f));
            //TITIK 7
            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, 1.0f));

            //TITIK 8
            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 0.0f, 1.0f));

            //LEFT FACE
            //TITIK 1
            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(-1.0f, 0.0f, 0.0f));
            //TITIK 3
            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(-1.0f, 0.0f, 0.0f));
            //TITIK 5
            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(-1.0f, 0.0f, 0.0f));
            //TITIK 3
            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(-1.0f, 0.0f, 0.0f));
            //TITIK 5
            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(-1.0f, 0.0f, 0.0f));
            //TITIK 7
            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(-1.0f, 0.0f, 0.0f));

            //RIGHT FACE
            //TITIK 2
            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(1.0f, 0.0f, 0.0f));
            //TITIK 4
            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(1.0f, 0.0f, 0.0f));
            //TITIK 6
            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(1.0f, 0.0f, 0.0f));
            //TITIK 4
            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(1.0f, 0.0f, 0.0f));
            //TITIK 6
            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(1.0f, 0.0f, 0.0f));
            //TITIK 8
            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(1.0f, 0.0f, 0.0f));

            //BOTTOM FACES
            //TITIK 3
            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, -1.0f, 0.0f));
            //TITIK 4
            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, -1.0f, 0.0f));
            //TITIK 7
            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, -1.0f, 0.0f));
            //TITIK 4
            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, -1.0f, 0.0f));
            //TITIK 7
            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, -1.0f, 0.0f));
            //TITIK 8
            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y - lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, -1.0f, 0.0f));

            //TOP FACES
            //TITIK 1
            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 1.0f, 0.0f));
            //TITIK 2
            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 1.0f, 0.0f));
            //TITIK 5
            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 1.0f, 0.0f));
            //TITIK 2
            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z - lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 1.0f, 0.0f));
            //TITIK 5
            temp_vector.X = x - lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 1.0f, 0.0f));
            //TITIK 6
            temp_vector.X = x + lengthx / 2.0f;
            temp_vector.Y = y + lengthy / 2.0f;
            temp_vector.Z = z + lengthz / 2.0f;
            _vertices.Add(temp_vector);
            _vertices.Add(new Vector3(0.0f, 1.0f, 0.0f));
        }
        public void render(int pilihan, double _time, Matrix4 temp, Matrix4 camera_view, Matrix4 camera_projection, float transx, float transy, float transz, float scale, int rotatex, int rotatey, int rotatez)
        {
            _shader.Use();

            GL.BindVertexArray(_vertexArrayObject);
            /*GL.DrawArrays(PrimitiveType.Quads, 0, 3);*/ //Ini kalau 4 titik

            /*Matrix4 model = Matrix4.Identity * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(_time));*/
            Matrix4 model = Matrix4.Identity;
            model = model * Matrix4.CreateTranslation(transx, transy, transz);
            model = model * Matrix4.CreateScale(scale);
            model = model * Matrix4.CreateRotationX((float)MathHelper.DegreesToRadians(rotatex));
            model = model * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(rotatey));
            model = model * Matrix4.CreateRotationZ((float)MathHelper.DegreesToRadians(rotatez));

            /* model = temp;*/
            _shader.SetMatrix4("model", model);
            _shader.SetMatrix4("view", camera_view);
            _shader.SetMatrix4("projection", camera_projection);

            if (pilihan == 0)
            {
                if (_indices.Count != 0)
                {
                    GL.DrawElements(PrimitiveType.Triangles, _indices.Count, DrawElementsType.UnsignedInt, 0);
                }
                else
                {
                    GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
                }
            }
            else if (pilihan == 1)
            {
                GL.DrawArrays(PrimitiveType.Triangles, 0, (int)(_vertices.Count));
            }
            /*else if (pilihan == 2)
            {
                GL.DrawArrays(PrimitiveType.LineStrip, 0, index);
            }*/
            else if (pilihan == 3)
            {

                GL.DrawArrays(PrimitiveType.LineStrip, 0, (int)(_vertices.Count));
            }


            //get uniform from shader
            /*int vertexColorLocation = GL.GetUniformLocation(_shader.Handle, "ourColor");
            GL.Uniform4(vertexColorLocation, 0.3f, 0.1f, 0.2f, 1.0f);*/
        }
        public void setFragVariable(Vector3 ObjectColor, Vector3 Viewpos)
        {
            _shader.SetVector3("objectColor", ObjectColor);
            _shader.SetVector3("viewPos", Viewpos);
        }
        public void setDirectionalLight(Vector3 direction, Vector3 ambient, Vector3 diffuse, Vector3 specular)
        {
            _shader.SetVector3("dirLight.direction", direction);
            _shader.SetVector3("dirLight.ambient", ambient);
            _shader.SetVector3("dirLight.diffuse", diffuse);
            _shader.SetVector3("dirLight.specular", specular);
        }
        public void setPointLight(Vector3 position, Vector3 ambient, Vector3 diffuse, Vector3 specular, float constant, float linear, float quadratic)
        {
            _shader.SetVector3("pointLight.position", position);
            _shader.SetVector3("pointLight.ambient", ambient);
            _shader.SetVector3("pointLight.diffuse", diffuse);

            _shader.SetVector3("pointLight.specular", specular);
            _shader.SetFloat("pointLight.constant", constant);
            _shader.SetFloat("pointLight.linear", linear);
            _shader.SetFloat("pointLight.quadratic", quadratic);
        }
        public void setSpotLight(Vector3 position, Vector3 direction, Vector3 ambient, Vector3 diffuse, Vector3 specular, float constant, float linear, float quadratic, float cutOff, float outerCutOff)
        {
            _shader.SetVector3("spotLight.position", position);
            _shader.SetVector3("spotLight.direction", direction);
            _shader.SetVector3("spotLight.ambient", ambient);
            _shader.SetVector3("spotLight.diffuse", diffuse);

            _shader.SetVector3("spotLight.specular", specular);
            _shader.SetFloat("spotLight.constant", constant);
            _shader.SetFloat("spotLight.linear", linear);
            _shader.SetFloat("spotLight.quadratic", quadratic);
            _shader.SetFloat("spotLight.cutOff", cutOff); //
            _shader.SetFloat("spotLight.outerCutOff", outerCutOff);
        }
        public void setPointLights(Vector3[] position, Vector3 ambient, Vector3 diffuse, Vector3 specular, float constant, float linear, float quadratic)
        {
            for (int i = 0; i < position.Length; i++)
            {
                _shader.SetVector3($"pointLight[{i}].position", position[i]);
                _shader.SetVector3($"pointLight[{i}].ambient", new Vector3(0.05f, 0.05f, 0.05f));
                _shader.SetVector3($"pointLight[{i}].diffuse", new Vector3(0.8f, 0.8f, 0.8f));
                _shader.SetVector3($"pointLight[{i}].specular", new Vector3(1.0f, 1.0f, 1.0f));
                _shader.SetFloat($"pointLight[{i}].constant", 1.0f);
                _shader.SetFloat($"pointLight[{i}].linear", 0.09f);
                _shader.SetFloat($"pointLight[{i}].quadratic", 0.032f);
            }
        }
        public void createEllipsoid(float _positionX, float _positionY, float _positionZ, float _radius)
        {
            Vector3 temp_vector;
            float _pi = (float)Math.PI;


            for (float v = -_pi / 2; v <= _pi / 2; v += 0.01f)
            {
                for (float u = -_pi; u <= _pi; u += (_pi / 30))
                {
                    temp_vector.X = _positionX + _radius * (float)Math.Cos(v) * (float)Math.Cos(u);
                    temp_vector.Y = _positionY + _radius * (float)Math.Cos(v) * (float)Math.Sin(u);
                    temp_vector.Z = _positionZ + _radius * (float)Math.Sin(v);
                    _vertices.Add(temp_vector);
                }
            }
        }
        public void createHyperboloid(float _positionX, float _positionY, float _positionZ, float _radius)
        {
            Vector3 temp_vector;
            float _pi = (float)Math.PI;


            for (float v = -_pi / 2; v <= _pi / 2; v += 0.01f)
            {
                for (float u = -_pi; u <= _pi; u += (_pi / 30))
                {
                    temp_vector.X = _positionX + _radius * (1 / (float)Math.Cos(v)) * (float)Math.Cos(u);
                    temp_vector.Y = _positionY + _radius * (1 / (float)Math.Cos(v)) * (float)Math.Sin(u);
                    temp_vector.Z = _positionZ + _radius * (float)Math.Tan(v);
                    _vertices.Add(temp_vector);
                }
            }
        }

        public void createBoxVertices(float x, float y, float z)
        {
            //biar lebih fleksibel jangan inisialiasi posisi dan 
            //panjang kotak didalam tapi ditaruh ke parameter
            float _positionX = x;
            float _positionY = y;
            float _positionZ = z;

            float _boxLength = 0.5f;

            //Buat temporary vector
            Vector3 temp_vector;
            //1. Inisialisasi vertex
            // Titik 1
            temp_vector.X = _positionX - _boxLength / 2.0f; // x 
            temp_vector.Y = _positionY + _boxLength / 2.0f; // y
            temp_vector.Z = _positionZ - _boxLength / 2.0f; // z

            _vertices.Add(temp_vector);

            // Titik 2
            temp_vector.X = _positionX + _boxLength / 2.0f; // x
            temp_vector.Y = _positionY + _boxLength / 2.0f; // y
            temp_vector.Z = _positionZ - _boxLength / 2.0f; // z

            _vertices.Add(temp_vector);
            // Titik 3
            temp_vector.X = _positionX - _boxLength / 2.0f; // x
            temp_vector.Y = _positionY - _boxLength / 2.0f; // y
            temp_vector.Z = _positionZ - _boxLength / 2.0f; // z
            _vertices.Add(temp_vector);

            // Titik 4
            temp_vector.X = _positionX + _boxLength / 2.0f; // x
            temp_vector.Y = _positionY - _boxLength / 2.0f; // y
            temp_vector.Z = _positionZ - _boxLength / 2.0f; // z

            _vertices.Add(temp_vector);

            // Titik 5
            temp_vector.X = _positionX - _boxLength / 2.0f; // x
            temp_vector.Y = _positionY + _boxLength / 2.0f; // y
            temp_vector.Z = _positionZ + _boxLength / 2.0f; // z

            _vertices.Add(temp_vector);

            // Titik 6
            temp_vector.X = _positionX + _boxLength / 2.0f; // x
            temp_vector.Y = _positionY + _boxLength / 2.0f; // y
            temp_vector.Z = _positionZ + _boxLength / 2.0f; // z

            _vertices.Add(temp_vector);

            // Titik 7
            temp_vector.X = _positionX - _boxLength / 2.0f; // x
            temp_vector.Y = _positionY - _boxLength / 2.0f; // y
            temp_vector.Z = _positionZ + _boxLength / 2.0f; // z

            _vertices.Add(temp_vector);

            // Titik 8
            temp_vector.X = _positionX + _boxLength / 2.0f; // x
            temp_vector.Y = _positionY - _boxLength / 2.0f; // y
            temp_vector.Z = _positionZ + _boxLength / 2.0f; // z

            _vertices.Add(temp_vector);
            //2. Inisialisasi index vertex
            _indices = new List<uint> {
                // Segitiga Depan 1
                0, 1, 2,
                // Segitiga Depan 2
                1, 2, 3,
                // Segitiga Atas 1
                0, 4, 5,
                // Segitiga Atas 2
                0, 1, 5,
                // Segitiga Kanan 1
                1, 3, 5,
                // Segitiga Kanan 2
                3, 5, 7,
                // Segitiga Kiri 1
                0, 2, 4,
                // Segitiga Kiri 2
                2, 4, 6,
                // Segitiga Belakang 1
                4, 5, 6,
                // Segitiga Belakang 2
                5, 6, 7,
                // Segitiga Bawah 1
                2, 3, 6,
                // Segitiga Bawah 2
                3, 6, 7
            };
        }

        public void createEllipsoid2(float radiusX, float radiusY, float radiusZ, float _x, float _y, float _z, int sectorCount, int stackCount)
        {
            float pi = (float)Math.PI;
            Vector3 temp_vector;
            float sectorStep = 2 * (float)Math.PI / sectorCount;
            float stackStep = (float)Math.PI / stackCount;
            float sectorAngle, StackAngle, x, y, z;

            for (int i = 0; i <= stackCount; ++i)
            {
                StackAngle = pi / 2 - i * stackStep;
                x = radiusX * (float)Math.Cos(StackAngle);
                y = radiusY * (float)Math.Cos(StackAngle);
                z = radiusZ * (float)Math.Sin(StackAngle);

                for (int j = 0; j <= sectorCount; ++j)
                {
                    sectorAngle = j * sectorStep;

                    temp_vector.X = x * (float)Math.Cos(sectorAngle);
                    temp_vector.Y = y * (float)Math.Sin(sectorAngle);
                    temp_vector.Z = z;
                    _vertices.Add(temp_vector);
                }
            }

            uint k1, k2;
            for (int i = 0; i < stackCount; ++i)
            {
                k1 = (uint)(i * (sectorCount + 1));
                k2 = (uint)(k1 + sectorCount + 1);
                for (int j = 0; j < sectorCount; ++j, ++k1, ++k2)
                {
                    if (i != 0)
                    {
                        _indices.Add(k1);
                        _indices.Add(k2);
                        _indices.Add(k1 + 1);
                    }
                    if (i != (stackCount - 1))
                    {
                        _indices.Add(k1 + 1);
                        _indices.Add(k2);
                        _indices.Add(k2 + 1);
                    }
                }
            }
        }

        public void rotate(Vector3 pivot, Vector3 vector, float angle)
        {
            //pivot -> mau rotate di titik mana
            //vector -> mau rotate di sumbu apa? (x,y,z)
            //angle -> rotatenya berapa derajat?

            angle = MathHelper.DegreesToRadians(angle);

            //mulai ngerotasi
            for (int i = 0; i < _vertices.Count; i++)
            {
                _vertices[i] = getRotationResult(pivot, vector, angle, _vertices[i]);
            }
            //rotate the euler direction
            for (int i = 0; i < 3; i++)
            {
                _euler[i] = getRotationResult(pivot, vector, angle, _euler[i], true);

                //NORMALIZE
                //LANGKAH - LANGKAH
                //length = akar(x^2+y^2+z^2)
                float length = (float)Math.Pow(Math.Pow(_euler[i].X, 2.0f) + Math.Pow(_euler[i].Y, 2.0f) + Math.Pow(_euler[i].Z, 2.0f), 0.5f);
                Vector3 temporary = new Vector3(0, 0, 0);
                temporary.X = _euler[i].X / length;
                temporary.Y = _euler[i].Y / length;
                temporary.Z = _euler[i].Z / length;
                _euler[i] = temporary;
            }
            _centerPosition = getRotationResult(pivot, vector, angle, _centerPosition);

            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Count * Vector3.SizeInBytes,
                _vertices.ToArray(), BufferUsageHint.StaticDraw);
        }

        Vector3 getRotationResult(Vector3 pivot, Vector3 vector, float angle, Vector3 point, bool isEuler = false)
        {
            Vector3 temp, newPosition;
            if (isEuler)
            {
                temp = point;
            }
            else
            {
                temp = point - pivot;
            }

            newPosition.X =
                (float)temp.X * (float)(Math.Cos(angle) + Math.Pow(vector.X, 2.0f) * (1.0f - Math.Cos(angle))) +
                (float)temp.Y * (float)(vector.X * vector.Y * (1.0f - Math.Cos(angle)) - vector.Z * Math.Sin(angle)) +
                (float)temp.Z * (float)(vector.X * vector.Z * (1.0f - Math.Cos(angle)) + vector.Y * Math.Sin(angle));
            newPosition.Y =
                (float)temp.X * (float)(vector.X * vector.Y * (1.0f - Math.Cos(angle)) + vector.Z * Math.Sin(angle)) +
                (float)temp.Y * (float)(Math.Cos(angle) + Math.Pow(vector.Y, 2.0f) * (1.0f - Math.Cos(angle))) +
                (float)temp.Z * (float)(vector.Y * vector.Z * (1.0f - Math.Cos(angle)) - vector.X * Math.Sin(angle));
            newPosition.Z =
                (float)temp.X * (float)(vector.X * vector.Z * (1.0f - Math.Cos(angle)) - vector.Y * Math.Sin(angle)) +
                (float)temp.Y * (float)(vector.Y * vector.Z * (1.0f - Math.Cos(angle)) + vector.X * Math.Sin(angle)) +
                (float)temp.Z * (float)(Math.Cos(angle) + Math.Pow(vector.Z, 2.0f) * (1.0f - Math.Cos(angle)));

            if (isEuler)
            {
                temp = newPosition;
            }
            else
            {
                temp = newPosition + pivot;
            }
            return temp;
        }

        public void resetEuler()
        {
            _euler[0] = new Vector3(1, 0, 0);
            _euler[1] = new Vector3(0, 1, 0);
            _euler[2] = new Vector3(0, 0, 1);
        }
        /* public void createCricle(float center_x, float center_y, float radius)
         {
             _vertices = new float[1080];
             for (int i = 0; i < 360; i++)
             {
                 double degInRad = i * Math.PI / 180;
                 //x
                 _vertices[i * 3] = radius * (float)Math.Cos(degInRad) + center_x;
                 //y
                 _vertices[i * 3 + 1] = radius * (float)Math.Sin(degInRad) + center_y;
                 //z
                 _vertices[i * 3 + 2] = 0;
             }
         }

         public void createElips(float center_x, float center_y, float radius_x, float radius_y)
         {
             _vertices = new float[1080];
             for (int i = 0; i < 360; i++)
             {
                 double degInRad = i * Math.PI / 180;
                 //x
                 _vertices[i * 3] = radius_x * (float)Math.Cos(degInRad) + center_x;
                 //y
                 _vertices[i * 3 + 1] = radius_y * (float)Math.Sin(degInRad) + center_y;
                 //z
                 _vertices[i * 3 + 2] = 0;
             }
         }*/

        /* public void updateMousePosition(float _x, float _y, float _z)
         {
             //x
             _vertices[index * 3] = _x;
             //y
             _vertices[index * 3 + 1] = _y;
             //z
             _vertices[index * 3 + 2] = 0;
             index++;

             GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
             GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
         }
 */
        /*public List<int> getRow(int rowIndex)
        {
            List<int> currow = new List<int>();
            //------
            currow.Add(1);
            if (rowIndex == 0)
            {
                return currow;
            }
            //-----
            List<int> prev = getRow(rowIndex - 1);
            for (int i = 1; i < prev.Count; i++)
            {
                int curr = prev[i - 1] + prev[i];
                currow.Add(curr);
            }
            currow.Add(1);
            return currow;
        }

        public List<float> CreateCurveBezier()
        {
            List<float> _vertices_bezier = new List<float>();
            List<int> pascal = getRow(index - 1); //Berdasarkan jumlah titik yang di klik
            _pascal = pascal.ToArray();
            for (float t = 0; t <= 1.0f; t += 0.01f)
            {
                Vector2 p = getP(index, t);
                _vertices_bezier.Add(p.X);
                _vertices_bezier.Add(p.Y);
                _vertices_bezier.Add(0);
            }
            return _vertices_bezier;

        }

        public Vector2 getP(int n, float t)
        {
            Vector2 p = new Vector2(0, 0); //0,0 ini start
            float k;
            for (int i = 0; i < n; i++)
            {
                k = (float)Math.Pow((1 - t), n - 1 - i) * (float)Math.Pow(t, i) * _pascal[i];
                p.X += k * _vertices[i * 3];
                p.Y += k * _vertices[i * 3 + 1];
            }
            return p;
        }

        public bool getVerticesLength()
        {
            if (_vertices[0] == 0)
            {
                return false;
            }
            if ((_vertices.Length + 1) / 3 > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void setVertices(float[] vertices)
        {
            _vertices = vertices;
        }*/
    }
}
