using System;
using OpenTK.Graphics.OpenGL4;

namespace RedCarpet.Gfx
{
    public class SmShaderProgram
    {
        private int programId;

        public SmShaderProgram(string vertexShaderSrc, string fragmentShaderSrc)
        {
            // Compile shaders
            int vertexShader = compileShader(ShaderType.VertexShader, vertexShaderSrc);
            int fragmentShader = compileShader(ShaderType.FragmentShader, fragmentShaderSrc);

            // Generate a shader program
            programId = GL.CreateProgram();
            GL.AttachShader(programId, vertexShader);
            GL.AttachShader(programId, fragmentShader);
            GL.LinkProgram(programId);

            int success;
            GL.GetProgram(programId, GetProgramParameterName.LinkStatus, out success);
            if (success != 1)
            {
                string error = GL.GetProgramInfoLog(programId);
                throw new Exception("shader program link error:" + error);
            }

            // Destroy shader objects
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        public void Use()
        {
            GL.UseProgram(programId);
        }

        public int GetUniformLocation(string name)
        {
            return GL.GetUniformLocation(programId, name);
        }

        public void Destroy()
        {
            GL.DeleteProgram(programId);
        }

        private int compileShader(ShaderType type, string source)
        {
            int shader = GL.CreateShader(type);
            GL.ShaderSource(shader, source);
            GL.CompileShader(shader);

            int success;
            GL.GetShader(shader, ShaderParameter.CompileStatus, out success);
            Console.WriteLine("compileShader success = " + success);
            if (success != 1)
            {
                string error = GL.GetShaderInfoLog(shader);
                throw new Exception("shader compile error (type - " + type + "): " + error);
            }

            return shader;
        }

    }
}
