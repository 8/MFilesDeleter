using MFilesDeleter.Model;
using MFilesDeleter.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFilesDeleter
{
  class Program
  {
    private readonly IParametersService ParametersService;
    private readonly IDestroyObjectService DestroyObjectService;
    private readonly IClassesService ClassesService;
    private readonly IObjectTypeService ObjectTypeService;
    private readonly IFindObjectsService FindObjectsService;

    public Program(IParametersService parametersService,
                   IDestroyObjectService destroyObjectService,
                   IClassesService classesService,
                   IObjectTypeService objectTypeService,
                   IFindObjectsService findObjectsService)
    {
      this.ParametersService = parametersService;
      this.DestroyObjectService = destroyObjectService;
      this.ClassesService = classesService;
      this.ObjectTypeService = objectTypeService;
      this.FindObjectsService = findObjectsService;
    }

    #region Methods

    internal int Run()
    {
      var p = this.ParametersService.Parameters;

      int ret;
      try { ret = Run(p); }
      catch (Exception ex)
      {
        Log(ex.ToString());
        ret = 1;
      }
      return ret;
    }

    private int Run(ParametersModel p)
    {
      switch (p.Action)
      {
        case ActionType.WriteDescription:
          WriteDescription(p);
          break;

        case ActionType.DestroyObjectsOfClasses:
          DestroyObjectsByClass(p);
          break;

        case ActionType.DestroyObjectsOfObjectTypes:
          DestroyObjectsByObjectType(p);
          break;

        case ActionType.ListClassesAndObjects:
          ListClassesAndObjects(p);
          break;

        case ActionType.ListObjectTypesAndObjects:
          ListObjectTypesAndObjects(p);
          break;
      }

      return 0;
    }

    private void WriteDescription(ParametersModel p)
    {
      var writer = GetLogWriter();
      writer.WriteLine("MFilesImporter written by martin kramer <martin.kramer@lostindetails.com>");
      p.Options.WriteOptionDescriptions(writer);
    }

    private void DestroyObjectsByClass(ParametersModel p)
    {
      foreach (string className in p.DestroyObjectOfClasses)
      {
        Log(string.Format("destroying objects of class: '{0}'", className));
        int count = this.DestroyObjectService.DestroyObjectsByClass(className);
        Log(string.Format("destroyed {0} objects", count));
      }
    }

    private void DestroyObjectsByObjectType(ParametersModel p)
    {
      foreach (string objectType in p.DestroyObjectsOfObjectTypes)
      {
        Log(string.Format("destroying objects of object type: '{0}'", objectType));
        int count = this.DestroyObjectService.DestroyObjectsByObjectType(objectType);
        Log(string.Format("destroyed {0} objects", count));
      }
    }

    private void ListClassesAndObjects(ParametersModel p)
    {
      var classes = this.ClassesService.GetClasses();

      foreach (string className in classes)
      {
        var searchResults = this.FindObjectsService.FindObjectsByClass(className);
        Log(string.Format("class '{0}' has {1} objects", className, searchResults.Count));
      }
    }

    private void ListObjectTypesAndObjects(ParametersModel p)
    {
      var objectTypes = this.ObjectTypeService.GetObjectTypes();

      foreach (string objectType in objectTypes)
      {
        var searchResults = this.FindObjectsService.FindObjectsByObjectType(objectType);
        Log(string.Format("object type: '{0}' has {1} objects", objectType, searchResults.Count));
      }
    }

    private void Log(string text)
    {
      GetLogWriter().WriteLine(text);
    }

    private TextWriter GetLogWriter()
    {
      return Console.Out;
    }

    #endregion
  }
}
