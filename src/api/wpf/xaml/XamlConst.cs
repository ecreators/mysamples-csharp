using System.IO;

namespace mysamples.api.wpf.xaml
{
    public class XamlConst
    {
        public static string readResourceToString(string @namespace)
        {
            var reference = $"{@namespace}";
            string result;
            using (var stream = typeof(XamlConst).Assembly.GetManifestResourceStream(reference))
                // ReSharper disable once AssignNullToNotNullAttribute
            using (var sr = new StreamReader(stream))
            {
                result = sr.ReadToEnd();
            }
            return result;
        }
    }
}