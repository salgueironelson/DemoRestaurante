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

        [HttpGet("{idRestaurante}")]
        public IActionResult GetAllPlatoByRestaurante(int idRestaurante)
        {
            if (idRestaurante != 0)
            {
                var user = unitOfWork.Restaurantes.Get(x => x.RestauranteId == idRestaurante);
                if (user != null)
                {
                    var platos = unitOfWork.Platos.Get(x => x.RestauranteId == idRestaurante);
                    if (platos != null)
                    {
                        var result = CreateMappedObject(platos);

                        var serializedlist = JsonConvert.SerializeObject(result, Formatting.Indented,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Serialize
                            });


                        return Ok(serializedlist);
                    }
                    else
                        return NoContent();
                }
                else
                {
                    return BadRequest("Restaurante no existe");
                }

            }
            else
            {
                return BadRequest("Restaurante no existe");
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

        [HttpGet("{id}/{platoid, }")]
        public IActionResult GetPlatoDetails(int Id, bool isHamburguesa)
        {
            if (isHamburguesa)
            {

                var plato = unitOfWork.Platos.Get(p => p.PlatoId == Id);
                if (plato != null)
                    return Ok(plato);
                else
                {
                    return NoContent();
                }
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPost("{idRestaurante}")]
        public IActionResult CreatePlato([FromBody] Restaurante restaurante, int idRestaurante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Restaurantes.Insert(restaurante);
                    unitOfWork.Save();

                    Plato plato = new Plato();
                    plato.RestauranteId = idRestaurante;
                    plato.PlatoId = restaurante.RestauranteId;
                    unitOfWork.Platos.Insert(plato);
                    unitOfWork.Save();
                    return Created("DemoRestaurante/CreateContact", restaurante);
                }
            }
            catch (DataException ex)
            {
                return BadRequest(ex);
            }
            return BadRequest(restaurante);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlato([FromRoute] int id, [FromBody] Plato plato)
        {
            Plato PlatoSearch = unitOfWork.Platos.GetByID(id);
            if (PlatoSearch != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        unitOfWork.Platos.Update(plato);
                        unitOfWork.Save();
                        return Ok();
                    }
                }
                catch (DataException ex)
                {
                    return BadRequest(ex);
                }
            }
            else
            {
                return NotFound("El Plato que intenta actualizar no existe");
            }
            return BadRequest();
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
