using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBattleApi.Installers
{
    interface IInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
