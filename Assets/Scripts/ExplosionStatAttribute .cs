using UnityEngine;

public class ExplosionStatAttribute : PropertyAttribute
{
    public float min;
    public float max;
    public float warningThreshold;

    public ExplosionStatAttribute(float min, float max, float warningThreshold)
    {
        this.min = min;
        this.max = max;
        this.warningThreshold = warningThreshold;
    }
}