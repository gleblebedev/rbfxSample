using System.Diagnostics;
#if !__IOS__
using CommandLine;
#endif 

namespace rbfxSample
{
    public class ApplicationOptions
    {
        public ApplicationOptions()
        {
            Windowed = Debugger.IsAttached;
        }

#if !__IOS__
        [Option('w', "windowed", Required = false, HelpText = "Windowed mode")] 
#endif 
        public bool Windowed { get; set; }

#if !__IOS__
        [Option("width", Required = false, HelpText = "Window width")]
#endif 
        public int? Width { get; set; }

#if !__IOS__
        [Option("height", Required = false, HelpText = "Window height")]
#endif 
        public int? Height { get; set; }

#if !__IOS__
        [Option('h', "highdpi", Required = false, HelpText = "High DPI mode")]
#endif 
        public bool HighDpi { get; set; } = false;

#if !__IOS__
        [Option("renderpath", Required = false, HelpText = "Render path")]
#endif 
        public string RenderPath { get; set; } = "RenderPaths/ForwardHWDepth.xml";
    }
}