using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    //Method for generating a noise map which returns a grid of values
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight,int seed, float scale,int octaves, float persistence, float lacunarity,Vector2 offset) { 
    float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for(int i=0; i< octaves; i++)
        {
            float offsetX = prng.Next(-100000,100000) + offset.x;
            float offsetY = prng.Next(-100000,100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        


        //clamp it to avoid a division by 0 error
        if (scale <= 0) {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight=float.MaxValue;

        //zooming into the center
        float halfWidth = mapWidth / 2f;
        float halfHeight = mapHeight / 2f;

        //loop through the noisemap
        for (int y = 0; y < mapHeight; y++) { 
            for(int x=0; x < mapWidth; x++){

                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                //loop through all the octaves
                for(int i=0; i< octaves; i++){
                    //sampling height values. divide by scale so as not to get the same value every time
                    float sampleX = (x-halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y-halfHeight) / scale * frequency + octaveOffsets[i].y;

                    //generate perlin value
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY)*2-1;
                    //aply it to the noise map
                    noiseHeight+=perlinValue*amplitude;

                    amplitude*=persistence;
                    frequency*=lacunarity;
                }

                if (noiseHeight > maxNoiseHeight){
                    maxNoiseHeight = noiseHeight;
                }
                else if(noiseHeight<minNoiseHeight){
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x,y]=noiseHeight;
                
            }
        }
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++){
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }
                return noiseMap;
    }
}
