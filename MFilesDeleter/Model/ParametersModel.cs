using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFilesDeleter.Model
{
  enum ActionType
  {
    WriteDescription,
    ListClassesAndObjects,
    ListObjectTypesAndObjects,
    DestroyObjectsOfClasses,
    DestroyObjectsOfObjectTypes
  }

  class ParametersModel
  {
    public string BaseFolder { get; set; }
    public Mono.Options.OptionSet Options { get; set; }
    public ActionType Action { get; set; }

    public string Vault { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public string Server { get; set; }
    public string[] DestroyObjectOfClasses { get; set; }
    public string[] DestroyObjectsOfObjectTypes { get; set; }

    public ParametersModel()
    {
      this.BaseFolder = this.Vault = this.Server = string.Empty;
      this.Action = ActionType.WriteDescription;
      this.DestroyObjectOfClasses = new string[0];
      this.DestroyObjectsOfObjectTypes = new string[0];
    }
  }
}
