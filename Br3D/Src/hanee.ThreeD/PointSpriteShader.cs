using devDept.Graphics;
using OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.ThreeD
{
    class PointSpriteShader : GLShader
    {
        private static string vertexCode = @"void main()
    {
        gl_Position = ftransform();
    }";

        private static string fragmentCode = @"
    uniform sampler2D tex;

    void main()
    {
        vec4 finalColor = texture(tex, gl_PointCoord);

        if (finalColor.w == 0)
            discard;
        gl_FragColor = finalColor;
    }
    ";

        public PointSpriteShader() : base(vertexCode, fragmentCode)
        {
        }

        public override void SetParameters(object shaderParams)
        {
            ShaderParametersBase shaderParameters = (ShaderParametersBase)shaderParams;

            Enable(shaderParameters.renderContext);

            gl.Uniform1i(GetUniformLocation("tex"), 0);

            Disable(shaderParameters.renderContext);
        }
    }
}
