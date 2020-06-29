using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using DemoRestaurante.Api.Data;
using DemoRestaurante.Api.Entities.Models;
using DemoRestaurante.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoRestaurante.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestauranteController : ControllerBase
    {
        private DemoRestauranteContext dbContext = new DemoRestauranteContext();
        private UnitOfWork unitOfWork = new UnitOfWork(new DemoRestauranteContext());
        [HttpGet]
        public IActionResult GetAllRestaurante()
        {
            try
            {
                var restaurantes = unitOfWork.Restaurantes.Get();
                if (restaurantes != null)
                    return Ok(restaurantes);
                else
                    return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetRestauranteDetails(int Id)
        {
            Restaurante restaurante = unitOfWork.Restaurantes.GetByID(Id);
            if (restaurante != null)
                return Ok(restaurante);
            else
            {
                return NoContent();
            }
        }
        [HttpPost]
        public IActionResult Create([FromBody] Restaurante restaurante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Restaurantes.Insert(restaurante);
                    unitOfWork.Save();
                    return Created("DemoRestaurante/Create", restaurante);
                }
            }
            catch (DataException ex)
            {
                return BadRequest(ex);
            }
            return BadRequest(restaurante);
        }

        // PUT api/values/5

        [HttpPut]
        public IActionResult UpdateRestaurante([FromBody] Restaurante restaurante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    unitOfWork.Restaurantes.Update(restaurante);
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


        [HttpDelete]
        public IActionResult DeleteRestaurante([FromHeader] int Id)
        {

            if (Id != 0)
            {
                unitOfWork.Restaurantes.Delete(Id);
                unitOfWork.Save();
                return Ok("Restaurante Eliminado");
            }
            else
            {
                return NoContent();
            }
        }
    }
}
