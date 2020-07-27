using System;
using CommandLine;
using Urho3DNet;

namespace rbfxSample
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<ApplicationOptions>(args)
                .WithParsed<ApplicationOptions>(o =>
                {
                    using (var context = new Context())
                    {
                        using (var application = new DemoApplication(context, o))
                        {
                            application.Run();
                        }
                    }
                });
        }
    }
}
