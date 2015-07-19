using MFilesDeleter.Factory;
using MFilesDeleter.Model;
using MFilesDeleter.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFilesDeleter.Service
{
  interface IParametersService
  {
    ParametersModel Parameters { get; }
  }

  class ParametersService : IParametersService
  {
    public ParametersModel Parameters { get; private set; }

    public ParametersService(IParametersFactory parametersFactory,
                             ArgumentsModel arguments)
    {
      this.Parameters = parametersFactory.Parse(arguments.Arguments);
    }
  }
}
