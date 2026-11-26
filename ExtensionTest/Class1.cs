using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using IGExtensionCS;

namespace ExtensionTest
{
    public class IGExtHandle : IExtension, ITab, ICommands, IExtensionContent, ISettingsProvider, IWindow
    {
        IHostContext Host;
        IGExtension ext;
        Assembly asm = Assembly.GetExecutingAssembly();

        public string Id => "ee380a76-716a-41d8-bb33-912710aab027";//created with c# interactive. must be unique
        public string Name => ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyTitleAttribute))).Title;
        public string Version => asm.GetName().Version.ToString();
        public string Author => ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyCompanyAttribute))).Company;
        public string Description => ((AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(asm, typeof(AssemblyDescriptionAttribute))).Description;
        public string MinVersion => "1.2.6";

        public bool SupportsHistory => true;
        public bool ProcessesSettings => true;
        public bool HasShellCommands => true;
        public bool LoadsExtensions => false;
        public bool SupportsFullScreen => false;
        public bool SupportsResize => true;

        public void Initialize(IHostContext host) { ext = new IGExtension(this); Host = host; }
        public void OnLoaded() { }
        public void OnUnloaded() { }

        // ITab
        public string Title => "Maz Fira Tab";
        public string Favicon => "";
        public event Action<string> TitleChanged;
        public event Action<string> FaviconChanged;
        public event Action<int, int> Resize;
        public event Action<bool> Maximize;
        public event Action<bool> Minimize;
        public event Action<int, int> Restore;
        public event Action<bool> Closing;

        public void ChangedTitle(string title) { TitleChanged?.Invoke(title); }
        public void ChangedFavicon(string favicon) { FaviconChanged?.Invoke(favicon); }
        public void OnTabOpened() { ext.CreateTab(); }
        public void OnTabLoaded() { }
        public void OnTabUnloaded() { }
        public void OnTabClosing() { }
        public void OnResize(int width, int height) { ext.Resize(width, height); }
        public void OnMinimize() { }
        public void OnRestore() { }

        // ICommands
        public IEnumerable<ExtensionCommand> GetCommands() { return null; }

        //IExtensionContent
        public IntPtr CreateContentHandle() { return ext.displayElem; }

        //isettingsprovider
        public string Get(string setting, string defaultvalue) { return defaultvalue; }
        public void Set(string setting, string value) { }
        public void Save() { }

        //iwindow
        public void OnWindowOpened() { ext.CreateWindow(); }
        public void OnWindowClosing() { }

        // Extra metadata
        public string Icon => "icons/notes.png";
    }
    public class IGExtension
    {
        IGExtHandle handler;
        public IntPtr displayElem;
        [DllImport("user32.dll")]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        const uint SWP_NOZORDER = 0x0004;
        const uint SWP_NOACTIVATE = 0x0010;
        public IGExtension(IGExtHandle handle)
        {
            handler = handle;
        }

        public void CreateTab()
        {
            UserControl1 tester = new UserControl1(handler);
            displayElem = tester.Handle;
        }

        public void CreateWindow()
        {
            Form1 tester = new Form1(handler);
            tester.Show();
        }

        public void Resize(int width, int height)
        {
            SetWindowPos(
                displayElem,
                IntPtr.Zero,
                0, 0,
                width, height,
                SWP_NOZORDER | SWP_NOACTIVATE
            );
        }
    }
}
