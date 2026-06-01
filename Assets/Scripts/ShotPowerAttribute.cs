using UnityEngine;

public class ShotPowerAttribute : PropertyAttribute
{
    public float max;

    public ShotPowerAttribute(float max)
    {
        this.max = max ;
    }
}