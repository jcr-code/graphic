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
    public abstract class Volume
    {
        public Vector3 Position = Vector3.Zero;
        public Vector3 Rotation = Vector3.Zero;
        public Vector3 Scale = Vector3.One;

        public virtual int VertCount { get; set; }
        public virtual int IndiceCount { get; set; }
        public virtual int ColorDataCount { get; set; }
        public virtual int NormalCount { get { return Normals.Length; } }
        public virtual int TextureCoordsCount { get; set; }

        public Matrix4 ModelMatrix = Matrix4.Identity;
        public Matrix4 ViewProjectionMatrix = Matrix4.Identity;
        public Matrix4 ModelViewProjectionMatrix = Matrix4.Identity;

        Vector3[] Normals = new Vector3[0];

        /* public Material Material = new Material();*/

        public abstract Vector3[] GetVerts();
        public abstract int[] GetIndices(int offset = 0);
        public abstract Vector3[] GetColorData();
        public abstract void CalculateModelMatrix();

        public virtual Vector3[] GetNormals()
        {
            return Normals;
        }

        public void CalculateNormals()
        {
            Vector3[] normals = new Vector3[VertCount];
            Vector3[] verts = GetVerts();
            int[] inds = GetIndices();

            // Compute normals for each face
            for (int i = 0; i < IndiceCount; i += 3)
            {
                Vector3 v1 = verts[inds[i]];
                Vector3 v2 = verts[inds[i + 1]];
                Vector3 v3 = verts[inds[i + 2]];

                // The normal is the cross-product of two sides of the triangle
                normals[inds[i]] += Vector3.Cross(v2 - v1, v3 - v1);
                normals[inds[i + 1]] += Vector3.Cross(v2 - v1, v3 - v1);
                normals[inds[i + 2]] += Vector3.Cross(v2 - v1, v3 - v1);
            }

            for (int i = 0; i < NormalCount; i++)
            {
                normals[i] = normals[i].Normalized();
            }

            Normals = normals;
        }

        public Vector3[] CalculateNormals2()
        {
            Vector3[] normals = new Vector3[VertCount];
            Vector3[] verts = GetVerts();
            int[] inds = GetIndices();

            // Compute normals for each face
            for (int i = 0; i < IndiceCount; i += 3)
            {
                Vector3 v1 = verts[inds[i]];
                Vector3 v2 = verts[inds[i + 1]];
                Vector3 v3 = verts[inds[i + 2]];

                // The normal is the cross-product of two sides of the triangle
                normals[inds[i]] += Vector3.Cross(v2 - v1, v3 - v1);
                normals[inds[i + 1]] += Vector3.Cross(v2 - v1, v3 - v1);
                normals[inds[i + 2]] += Vector3.Cross(v2 - v1, v3 - v1);
            }

            for (int i = 0; i < NormalCount; i++)
            {
                normals[i] = normals[i].Normalized();
            }

            return normals;
        }

        public bool IsTextured = false;
        public int TextureID;
        public abstract Vector2[] GetTextureCoords();

        int _vertexBufferObject;
        int _vertexArrayObject;
        int _normalBufferObject;
        int _elementBufferObject;
        Shader _shader;

        Matrix4 _view;
        Matrix4 _projection;
        Matrix4 _model;

        public Vector3 _centerPosition = new Vector3(0, 0, 0);
        public List<Vector3> _euler = new List<Vector3>();

        public Material Material = new Material();

        public Volume()
        {
            Console.WriteLine("OKE");
        }
        public void loader(string shadervert, string shaderfrag, float Size_x, float Size_y, Volume objects)
        {
            Vector3[] verts = objects.GetVerts();
            for (int i = 0; i < verts.Length; i++)
            {
                verts[i] = verts[i] * 0.5f;
            }
            Vector3[] normal = objects.CalculateNormals2();
            for (int i = 0; i < normal.Length; i++)
            {
                normal[i] = normal[i] * 0.5f;
            }
            int[] indices = objects.GetIndices();

            //Inisialisasi VBO
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, verts.Length * Vector3.SizeInBytes, verts, BufferUsageHint.StaticDraw);

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

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //Inisialisasi VBO Normal
            _normalBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _normalBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, normal.Length * Vector3.SizeInBytes, normal, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(1);

            //Jika terdapat data pada indikasi baru inisialisasi Elemetn Buffer Object
            if (indices.Length != 0)
            {
                _elementBufferObject = GL.GenBuffer(); //ini yang menghandle
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);
            }
            _shader = new Shader(shadervert, shaderfrag);

            //Texture 
            /*var texCoordLocation = _shader.GetAttribLocation()*/
            _shader.Use();
            /*_view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Size_x / (float)Size_y, 0.1f, 100.0f);*/

        }
        public void loader2(string shadervert, string shaderfrag, float Size_x, float Size_y, float transx, float transy, float transz, Volume objects)
        {
            ModelMatrix = ModelMatrix * Matrix4.CreateTranslation(transx, transy, transz);
            Vector3[] verts = objects.GetVerts();
            for (int i = 0; i < verts.Length; i++)
            {
                verts[i] = verts[i] * 0.5f;
            }
            Vector3[] normal = objects.CalculateNormals2();
            for (int i = 0; i < normal.Length; i++)
            {
                normal[i] = normal[i] * 0.5f;
            }
            int[] indices = objects.GetIndices();

            //Inisialisasi VBO
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, verts.Length * Vector3.SizeInBytes, verts, BufferUsageHint.StaticDraw);

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

            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            //Inisialisasi VBO Normal
            _normalBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _normalBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, normal.Length * Vector3.SizeInBytes, normal, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(1);

            //Jika terdapat data pada indikasi baru inisialisasi Elemetn Buffer Object
            if (indices.Length != 0)
            {
                _elementBufferObject = GL.GenBuffer(); //ini yang menghandle
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, _elementBufferObject);
                GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(int), indices, BufferUsageHint.StaticDraw);
            }
            _shader = new Shader(shadervert, shaderfrag);

            //Texture 
            /*var texCoordLocation = _shader.GetAttribLocation()*/
            _shader.Use();
            /*_view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
            _projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Size_x / (float)Size_y, 0.1f, 100.0f);*/

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
        public void renderer(int pilihan, double _time, Matrix4 temp, Matrix4 camera_view, Matrix4 camera_projection, Volume objects, float transx, float transy, float transz, float scale, int rotatex, int rotatey, int rotatez)
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

            int indiceat = 0;

            if (pilihan == 0)
            {
                /*if (_indices.Count != 0)
                {

                    GL.DrawElements(PrimitiveType.Triangles, _indices.Count, DrawElementsType.UnsignedInt, 0);
                }
                else
                {
                    GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
                }*/
            }
            /*else if (pilihan == 1)
            {
                GL.DrawArrays(PrimitiveType.Triangles, 0, (int)(_vertices.Count));
            }*/
            /*else if (pilihan == 2)
            {
                GL.DrawArrays(PrimitiveType.LineStrip, 0, index);
            }*/
            else if (pilihan == 2)
            { 
                GL.DrawElements(PrimitiveType.Triangles, objects.IndiceCount, DrawElementsType.UnsignedInt, indiceat * sizeof(uint));
                indiceat += objects.IndiceCount;
            }
            /*else if (pilihan == 3)
            {
                GL.DrawArrays(PrimitiveType.LineStrip, 0, (int)(_vertices.Count));
            }*/


            //get uniform from shader
            /*int vertexColorLocation = GL.GetUniformLocation(_shader.Handle, "ourColor");
            GL.Uniform4(vertexColorLocation, 0.3f, 0.1f, 0.2f, 1.0f);*/
        }
        public void renderer2(int pilihan, double _time, Matrix4 temp, Matrix4 camera_view, Matrix4 camera_projection, Volume objects, float transx, float transy, float transz, float scale, int rotatex, int rotatey, int rotatez, double time)
        {
            _shader.Use();

            GL.BindVertexArray(_vertexArrayObject);
            /*GL.DrawArrays(PrimitiveType.Quads, 0, 3);*/ //Ini kalau 4 titik

            /*Matrix4 model = Matrix4.Identity * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(_time));*/

            ModelMatrix = ModelMatrix * Matrix4.CreateRotationY((float)MathHelper.DegreesToRadians(time));

            /* model = temp;*/
            _shader.SetMatrix4("model", ModelMatrix);
            _shader.SetMatrix4("view", camera_view);
            _shader.SetMatrix4("projection", camera_projection);

            int indiceat = 0;

            if (pilihan == 0)
            {
                /*if (_indices.Count != 0)
                {

                    GL.DrawElements(PrimitiveType.Triangles, _indices.Count, DrawElementsType.UnsignedInt, 0);
                }
                else
                {
                    GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
                }*/
            }
            /*else if (pilihan == 1)
            {
                GL.DrawArrays(PrimitiveType.Triangles, 0, (int)(_vertices.Count));
            }*/
            /*else if (pilihan == 2)
            {
                GL.DrawArrays(PrimitiveType.LineStrip, 0, index);
            }*/
            else if (pilihan == 2)
            {
                GL.DrawElements(PrimitiveType.Triangles, objects.IndiceCount, DrawElementsType.UnsignedInt, indiceat * sizeof(uint));
                indiceat += objects.IndiceCount;
            }
            /*else if (pilihan == 3)
            {
                GL.DrawArrays(PrimitiveType.LineStrip, 0, (int)(_vertices.Count));
            }*/


            //get uniform from shader
            /*int vertexColorLocation = GL.GetUniformLocation(_shader.Handle, "ourColor");
            GL.Uniform4(vertexColorLocation, 0.3f, 0.1f, 0.2f, 1.0f);*/
        }
    }
}
