using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(PP_ColorDepthRenderer), PostProcessEvent.AfterStack, "Custom/ColorDepth")]
public sealed class PP_ColorDepth : PostProcessEffectSettings
{
    [Range(1,16)]
    public IntParameter colorBitDepth = new IntParameter { value = 16 };
}

public sealed class PP_ColorDepthRenderer : PostProcessEffectRenderer<PP_ColorDepth>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/SH_ColorDepth"));
        sheet.properties.SetFloat("_Depth", Mathf.Pow(2,settings.colorBitDepth));
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}

