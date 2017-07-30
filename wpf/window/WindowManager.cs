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
                registerWindowClosedEvent(window);
                ctrl.ShowWindow(window);
            }
        }

        private void registerWindowClosedEvent(Window window)
        {
            window.Closed += onWindowClosed;
        }

        private void unregisterWindowClosedEvent(Window window)
        {
            window.Closed -= onWindowClosed;
        }

        private List<(bool found, WindowController controller, int windowId, Window window)> FindWindows(Predicate<(WindowController controller, int windowId, Window window)> filter)
        {
            return
                (from kvp1 in windows
                    let x = (from kvp2 in kvp1.Value
                        let data = (controller: kvp1.Key, windowId: kvp2.Key, window: kvp2.Value)
                        let found = filter?.Invoke(data) ?? true
                        let r = (found: found, controller: data.controller, windowId: kvp2.Key, window: data.window)
                        where r.found
                        select r).ToList()
                    select x)
                .SelectMany(inner => inner)
                .ToList();
        }

        public void closeAll()
        {
            FindWindows(d => true).ForEach(d => d.window.Close());
        }

        private void onWindowClosed(object sender, EventArgs args)
        {
            removeWindow((Window) sender);
        }

        private void removeWindow(Window wnd)
        {
            var wnds = FindWindows(d => d.window.GetHashCode() == wnd.GetHashCode());
            foreach (var result in wnds)
            {
                var cache = windows[result.controller];
                cache.Remove(result.windowId);
                unregisterWindowClosedEvent(result.window);
                if (!cache.Any())
                {
                    windows.Remove(result.controller);
                }
            }
        }

        public bool hasWindowOfType<T>() => FindWindows(d => d.window is T).Any();
    }
}