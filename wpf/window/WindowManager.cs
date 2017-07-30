using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace mysamples.wpf.window
{
    /// <summary>
    /// Der WindowManager regelt die Verwaltung und Anzeige
    /// </summary>
    internal class WindowManager
    {
        private static readonly Lazy<WindowManager> INSTANCE_LAZY = new Lazy<WindowManager>();

        private readonly Dictionary<WindowController, Dictionary<int, Window>> windows;

        public WindowManager()
        {
            windows = new Dictionary<WindowController, Dictionary<int, Window>>();
        }

        public static WindowManager Instance => INSTANCE_LAZY.Value;

        public void showWindow(WindowController ctrl)
        {
            Window window = null;
            Dictionary<int, Window> windowDic = null;
            var canCreate = true;
            if (!ctrl.AllowMultipleInstance)
            {
                window = windows.TryGetValue(ctrl, out windowDic) ? windowDic.Values.FirstOrDefault() : null;
                canCreate = window == null;
            }

            var windowCount = windowDic?.Count ?? 0;

            if (canCreate)
            {
                var windowInfo = ctrl.NewWindow(windowCount);
                window = windowInfo.wnd;
                if (window != null)
                {
                    windowDic = windowDic ?? new Dictionary<int, Window>();
                    windowDic[windowInfo.id] = window;
                    windows[ctrl] = windowDic;
                }
            }

            if (window != null)
            {
                ctrl.ShowWindow(window);
            }
        }

        public bool hasWindowOfType<T>()
        {
            return (from e in windows.Keys
                    let type = e.ViewController.ContentView?.GetType()
                    where type == typeof(T)
                    select e).Any();
        }
    }
}