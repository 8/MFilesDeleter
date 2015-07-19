using MFilesAPI;
using MFilesDeleter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MFilesDeleter.Service
{
  interface IVaultService
  {
    Lazy<Vault> Vault { get; }
  }

  class VaultService : IVaultService
  {
    public Lazy<Vault> Vault { get; private set; }

    public VaultService(IParametersService parametersService)
    {
      this.Vault = new Lazy<Vault>(() => CreateVault(parametersService.Parameters));
    }

    private Vault CreateVault(ParametersModel parameters)
    {
      string user = parameters.User;
      string password = parameters.Password;
      string vault = parameters.Vault;
      string networkAddress = parameters.Server;
      return CreateVault(user, password, vault, networkAddress);
    }

    private Vault CreateVault(string user, string password, string vault, string networkAddress = null)
    {
      /* login to the server and get the vault */
      var serverApp = new MFilesAPI.MFilesServerApplication();

      serverApp.Connect(
        AuthType: string.IsNullOrEmpty(user) ? MFAuthType.MFAuthTypeLoggedOnWindowsUser : MFilesAPI.MFAuthType.MFAuthTypeSpecificMFilesUser,
        UserName: string.IsNullOrEmpty(user) ? Type.Missing : user,
        Password: string.IsNullOrEmpty(password) ? Type.Missing : password,
        NetworkAddress: networkAddress == string.Empty ? null : networkAddress
      );
      var vaultsOnServer = serverApp.GetVaults();

      Vault v = null;
      foreach (MFilesAPI.VaultOnServer vaultOnServer in vaultsOnServer)
      {
        if (vaultOnServer.Name == vault)
        {
          v = vaultOnServer.LogIn();
          break;
        }
      }

      return v;
    }
  }
}
