using MFilesDeleter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFilesDeleter.Factory
{
  interface IParametersFactory
  {
    ParametersModel Parse(string[] args);
  }

  class ParametersFactory : IParametersFactory
  {
    public ParametersModel Parse(string[] args)
    {
      var p = new ParametersModel();
      var options = new Mono.Options.OptionSet();

      options.Add("h|help", "prints the help", s => p.Action = ActionType.WriteDescription);
      options.Add("v|vault=", "use the specified vault", s => p.Vault = s);
      options.Add("u|user=", "use the specified user", s => p.User = s);
      options.Add("s|server=", "use the specified server", s => p.Server = s);
      options.Add("p|password=", "use the specified password", s => p.Password = s);
      options.Add("destroy-by-class=", "destroy objects of the following classes, use Comma ',' as a separator for multiple classes",
        s => { 
          p.Action = ActionType.DestroyObjectsOfClasses;
          p.DestroyObjectOfClasses = s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        });
      options.Add("destroy-by-objecttype=", "destroy objects of the specified object types, use Comma ',' as a separator for multiple classes",
        s =>
        {
          p.Action = ActionType.DestroyObjectsOfObjectTypes;
          p.DestroyObjectsOfObjectTypes = s.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        });
      options.Add("lc|list-classes", "list all classes and their object counts", s => p.Action = ActionType.ListClassesAndObjects);
      options.Add("lo|list-objecttypes", "list all object types and their object counts", s => p.Action = ActionType.ListObjectTypesAndObjects);

      options.Parse(args);
      p.Options = options;
      return p;
    }
  }
}
