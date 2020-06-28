using DemoRestaurante.Api.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoRestaurante.Api.Services
{
    public interface IUnitOfWork
    {
        IRepository<Restaurante> Restaurantes { get; }
        IRepository<Plato> Platos { get; }
        void Save();
    }
}
