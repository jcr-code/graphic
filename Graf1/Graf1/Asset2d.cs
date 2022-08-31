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
    internal class Asset2d
    {
        float[] _vertices =
        {

        };
        uint[] _indikasi =
        {

        };
        int _vertexBufferObject;
        int _vertexArrayObject;
        int ebo;
        Shader _shader;
        int index;
        int[] _pascal = { };
       
        public Asset2d(float[] _verticeses, uint[] indikasi)
        {
            _vertices = _verticeses;
            _indikasi = indikasi;
            index = 0;
        }
        public void load(string shadervert, string shaderfrag)
        {
            //Inisialisasi
            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);

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
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            /*GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);

            GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));
            GL.EnableVertexAttribArray(1);*/

            //Jika terdapat data pada indikasi baru inisialisasi Elemetn Buffer Object
            if(_indikasi.Length != 0)
            {
                ebo = GL.GenBuffer(); //ini yang menghandle
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
                GL.BufferData(BufferTarget.ElementArrayBuffer, _indikasi.Length * sizeof(uint), _indikasi, BufferUsageHint.StaticDraw);
            }
            _shader = new Shader(shadervert, shaderfrag);
            _shader.Use();

        }
        public void render(int pilihan)
        {
            _shader.Use();

            GL.BindVertexArray(_vertexArrayObject);
            /*GL.DrawArrays(PrimitiveType.Quads, 0, 3);*/ //Ini kalau 4 titik

            if (pilihan == 0)
            {
                if (_indikasi.Length != 0)
                {
                    GL.DrawElements(PrimitiveType.Triangles, _indikasi.Length, DrawElementsType.UnsignedInt, 0);
                }
                else
                {
                    GL.DrawArrays(PrimitiveType.Triangles, 0, 3);
                }
            }
            else if(pilihan == 1)
            {
                GL.DrawArrays(PrimitiveType.TriangleFan, 0, (_vertices.Length + 1) / 3);
            }
            else if(pilihan == 2)
            {
                GL.DrawArrays(PrimitiveType.LineStrip, 0, index);
            }
            else if (pilihan == 3)
            {
                    
                GL.DrawArrays(PrimitiveType.LineStrip, 0, (_vertices.Length + 1) / 3);
            }
           

            //get uniform from shader
            /*int vertexColorLocation = GL.GetUniformLocation(_shader.Handle, "ourColor");
            GL.Uniform4(vertexColorLocation, 0.3f, 0.1f, 0.2f, 1.0f);*/
        }

        public void createCricle(float center_x, float center_y, float radius) 
        {
            _vertices = new float[1080];
            for(int i = 0; i < 360; i++)
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
        }

        public void updateMousePosition(float _x, float _y, float _z)
        {
            //x
            _vertices[index * 3] = _x;
            //y
            _vertices[index * 3 + 1] = _y;
            //z
            _vertices[index * 3 + 2] = _z;
            index++;

            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
        }

        public List<int> getRow(int rowIndex)
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
                Vector3 p = getP(index, t);
                _vertices_bezier.Add(p.X);
                _vertices_bezier.Add(p.Y);
                _vertices_bezier.Add(p.Z);
            }
            return _vertices_bezier;
    
        }

        public Vector3 getP(int n, float t)
        {
            Vector3 p = new Vector3(0, 0, 0); //0,0 ini start
            float k;
            for (int i = 0; i < n; i++)
            {
                k = (float)Math.Pow((1 - t), n - 1 - i) * (float)Math.Pow(t,i) * _pascal[i];
                p.X += k * _vertices[i * 3];
                p.Y += k * _vertices[i * 3 + 1];
                p.Z += k * _vertices[i * 3 + 2];
            }
            return p;
        }

        public bool getVerticesLength()
        {
            if(_vertices[0] == 0)
            {
                return false;
            }
            if((_vertices.Length + 1) / 3 > 0)
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
        }
    }
}
