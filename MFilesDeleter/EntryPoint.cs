using MFilesDeleter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFilesDeleter
{
  class EntryPoint
  {
    static int Main(string[] args)
    {
      TinyIoC.TinyIoCContainer container = new TinyIoC.TinyIoCContainer();
      container.Register<ArgumentsModel>(new ArgumentsModel(args));
      container.RegisterInterfaceImplementations("MFilesDeleter.Service", TinyIoCExtensions.RegisterOptions.AsSingleton, TinyIoCExtensions.RegisterTypes.AsInterfaceTypes);
      container.RegisterInterfaceImplementations("MFilesDeleter.Factory", TinyIoCExtensions.RegisterOptions.AsSingleton, TinyIoCExtensions.RegisterTypes.AsInterfaceTypes);

      try
      {
        var program = container.Resolve<Program>();
        return program.Run();
      }
      catch (Exception ex)
      {
        Console.WriteLine(string.Format("The following error occurred:{0}{1}", ex, Environment.NewLine));
        return 1;
      }
    }
  }
}
