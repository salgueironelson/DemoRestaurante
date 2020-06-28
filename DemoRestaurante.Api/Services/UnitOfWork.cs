using DemoRestaurante.Api.Data;
using DemoRestaurante.Api.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoRestaurante.Api.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private DemoRestauranteContext _dbContext;
        private BaseRepository<Restaurante> _restaurante;
        private BaseRepository<Plato> _plato;

        public UnitOfWork(DemoRestauranteContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<Restaurante> Restaurantes
        {
            get
            {
                return _restaurante ??
                                    (_restaurante = new BaseRepository<Restaurante>(_dbContext));
            }
        }

        public IRepository<Plato> Platos
        {
            get
            {
                return _plato ??
                                    (_plato = new BaseRepository<Plato>(_dbContext));

            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }

    }
}
