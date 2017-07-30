using System;
using System.Text.RegularExpressions;
using System.Windows.Markup;

namespace mysamples.api.wpf.xaml
{
    public class XamlNamespace : ParserContext
    {
        public const string X_DEFAILT_NAMESPACE = "x";

        public XamlNamespace()
        {
            XamlTypeMapper = new XamlTypeMapper(new string[0]);
            XmlnsDictionary.Add(String.Empty, "http://schemas.microsoft.com/winfx/2006/xaml/presentation");
            XmlnsDictionary.Add(X_DEFAILT_NAMESPACE, "http://schemas.microsoft.com/winfx/2006/xaml");
        }

        public void addNamespace<T>(string name)
        {
            const string pattern = @"^\w[\w\d]*$";
            if (!Regex.IsMatch(name, pattern))
            {
                throw new ArgumentException($"Your namespace name is invalid. must match {pattern}. But you did '{name}' wich does not match exactly!");
            }

            var viewType = typeof(T);
            var namespaceReference = viewType.Namespace ?? String.Empty;
            var assemblyName = viewType.Assembly.FullName;
            XamlTypeMapper.AddMappingProcessingInstruction(name, namespaceReference, assemblyName);
            XmlnsDictionary.Add(name, name);
        }
    }
}