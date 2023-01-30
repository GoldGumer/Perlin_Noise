using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perlin_Noise_Manager : MonoBehaviour
{
    [SerializeField] ComputeShader perlin_Noise;
    public RenderTexture render_Texture = null;

    [SerializeField] bool give_Time_To_Perlin = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (render_Texture == null)
        {
            render_Texture = new RenderTexture(source.width, source.height, 24);
            render_Texture.enableRandomWrite = true;
            render_Texture.Create();
        }

        perlin_Noise.SetTexture(0, "Result", render_Texture);
        perlin_Noise.SetFloats("screen", new float[2] { render_Texture.width, render_Texture.height });

        if (give_Time_To_Perlin) perlin_Noise.SetFloat("time", Time.deltaTime);


        uint x, y, z;

        perlin_Noise.GetKernelThreadGroupSizes(0, out x, out y, out z);
        perlin_Noise.Dispatch(0, (int)(render_Texture.width / x), (int)(render_Texture.height / y), (int)z);

        Graphics.Blit(render_Texture, destination);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
