using System;

namespace Monitoramento.Domain.Interface
{
    public interface IRunner
    {
        IServiceProvider CreateServices(string connectionString);
        void UpdateDatabase(IServiceProvider serviceProvider);
    }
}