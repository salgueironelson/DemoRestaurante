using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DemoRestaurante.Api.Data;
using DemoRestaurante.Api.Entities.DTOs;
using DemoRestaurante.Api.Entities.Models;
using DemoRestaurante.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DemoRestaurante.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatoController : ControllerBase
    {
        private UnitOfWork unitOfWork = new UnitOfWork(new DemoRestauranteContext());

        [HttpGet]

        public IActionResult GetAllPlato()
        {
            try
            {
                var platos = unitOfWork.Platos.Get();
                if (platos != null)
                    return Ok(platos);
                else
                    return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetPlatoDetails(int Id)
        {
            Plato plato = unitOfWork.Platos.GetByID(Id);
            if (plato != null)
                return Ok(plato);
            else
            {
                return NoContent();
            }
        }

        [HttpGet("Restaurante/{id}")]
        public IActionResult GetPlatosRestaurante(int Id)
        {
            var platos = unitOfWork.Platos.Get(p => p.RestauranteId == Id);
            if (platos != null)
                return Ok(platos);
            else
            {
                return NoContent();
            }
        }


        private PlatosList CreateMappedObject(IEnumerable<Plato> platos)
        {
            PlatosList listPlatos = new PlatosList();
            foreach (var item in platos)
            {
                Restaurante restaurante = unitOfWork.Restaurantes.GetByID(item.PlatoId);
                listPlatos.Restaurantes.Add(restaurante);
            }

            return listPlatos;
        }     


        [HttpPost]
        public IActionResult CreatePlato([FromBody] Plato plato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Platos.Insert(plato);
                    unitOfWork.Save();

                    
                    return Created("DemoRestaurante/Create", plato);
                }
            }
            catch (DataException ex)
            {
                return BadRequest(ex);
            }
            return BadRequest(plato);
        }


        [HttpPut("{id}")]
        public IActionResult UpdatePlato([FromRoute] int id, [FromBody] Plato plato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Platos.Update(plato);
                    unitOfWork.Save();
                    return Ok();
                }
                else
                    return BadRequest();
            }
            catch (DataException ex)
            {
                return BadRequest(ex);
            }
        }



        [HttpDelete("{id}")]
        public IActionResult DeletePlato(int Id)
        {

            if (Id != 0)
            {
                unitOfWork.Platos.Delete(Id);
                unitOfWork.Save();
                return Ok("Plato Eliminado");
            }
            else
            {
                return NoContent();
            }
        }
    }
}
