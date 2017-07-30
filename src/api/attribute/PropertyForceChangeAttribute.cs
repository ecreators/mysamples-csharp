using System;

namespace mysamples.api.attribute
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PropertyForceChangeAttribute : Attribute
    { }
}