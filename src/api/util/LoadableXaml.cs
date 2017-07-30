using System;
using System.Diagnostics;
using System.Windows.Markup;
using mysamples.api.wpf.xaml;
using XamlReader = System.Windows.Markup.XamlReader;

namespace mysamples.api.util
{
    /// <summary>
    /// LoadableUI is a UI that defines WPF UI
    /// </summary>
    public class LoadableXaml<T, TReference>
    {
        private readonly Func<(int version, string code, XamlReferenceInitiator<T, TReference> initiatorCode)> checker;
        // benötigt wird
        // 1. eine typ ID, um zu unterscheiden, wo die UI hingehört
        // 2. eine versions nummer um ui unterscheiden, womit die UI kompatible ist
        // 3. den Code

        public LoadableXaml(int typeId, Func<(int version, string code, XamlReferenceInitiator<T, TReference> initiatorCode)> checker)
        {
            TypeID = typeId;
            this.checker = checker;
            Dispose();
        }

        public int TypeID { get; }

        public string Name { get; set; }

        public int Version { get; private set; }

        public string Code { get; private set; }

        public Lazy<ParserContext> Context { get; } = new Lazy<ParserContext>();

        private Lazy<T> lazyView;

        public bool Downgradable { get; set; } = false;

        public bool Updated { get; private set; }

        public XamlReferenceInitiator<T, TReference> Initiator { get; protected set; }

        public T View => lazyViewValue();

        private T lazyViewValue()
        {
            var v = checker();
            doUpdate(v.version, v.code, v.initiatorCode);

            var viewValue = lazyView == null ? default(T) : lazyView.Value;

            if (viewValue == null)
            {
                Dispose();
            }

            Updated = false;

            return viewValue;
        }

        public void Dispose()
        {
            lazyView = new Lazy<T>(makeCode);
        }

        public bool Update()
        {
            // neu abholen
            var v = checker();
            (int oldVersion, int nowVersion) vers = (Version, v.version);

            // update
            doUpdate(v.version, v.code, v.initiatorCode);

            // updated?
            if (vers.oldVersion != vers.nowVersion)
            {
                Initiator = v.initiatorCode;
                Dispose();
                return true;
            }
            return false;
        }

        protected void doUpdate(int version, string code, XamlReferenceInitiator<T, TReference> initiatorCode)
        {
            if (version > Version || Downgradable)
            {
                Version = version;
                Code = code;
                if (initiatorCode != null)
                {
                    Initiator = new GenericTypeLoader<XamlReferenceInitiator<T, TReference>>(initiatorCode).Instance;
                }
                Dispose();
                Updated = true;
            }
        }

        protected T makeCode()
        {
            if (Code == null)
            {
                return default(T);
            }

            // Create a button using a XamlReader
            try
            {
                return (T) XamlReader.Parse(Code, Context.Value);
            }
            catch (Exception e)
            {
                var message = e.ToString();
                Console.WriteLine(message);
                Trace.WriteLine(message);
                Debug.WriteLine(message);
                throw;
            }
        }

        public void Initialize(TReference codeBehind)
        {
            Initiator?.initiate(View, codeBehind);
        }

        public void Dispose(TReference codeBehind)
        {
            Initiator?.dispose(View, codeBehind);
        }
    }
}