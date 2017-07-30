using System;

namespace mysamples.api.attribute
{
    /// <summary>
    /// Definiert, dass die Verwendung durch eine Bindung erfolgt und nicht entfernt werden soll.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PassiveUsageByStringAttribute : Attribute
    {
    }
}