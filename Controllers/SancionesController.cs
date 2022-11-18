using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PracticaDTOFluentVaidation.Controllers
{
    #region Referencias

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using PracticaDTOFluentVaidation.DAL.DbContext;
    using PracticaDTOFluentVaidation.DAL.Entities;
    using PracticaDTOFluentVaidation.DTO;

    #endregion

    [Route("api/[controller]")]
    [ApiController]
    public class SancionesController : ControllerBase
    {
        #region ConduccionDbContext / Constructor

        public readonly ConduccionDbContext _context;

        public SancionesController(ConduccionDbContext context)
        {
            _context = context;
        }

        #endregion

        #region GET Sanciones

        /// <summary>
        /// Este es GET: api/Sanciones
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SancionesDTO>>> Get()
        {
            try
            {
                var sanciones = await _context.Sanciones.Select(s => new SancionesDTO
                {
                    FechaActual = s.FechaActual,
                    Sancion = s.Sancion,
                    Observacion = s.Observacion,
                    Valor = s.Valor,
                    ConductorId = s.ConductorId,
                    Nombre = s.Conductor.Nombres + " " + s.Conductor.Apellidos
                }).ToListAsync();

                if (sanciones == null)
                {
                    return NotFound();
                }
                else
                {
                    return sanciones;
                }
            }
            catch (Exception err)
            {
                throw new Exception("Ha ocurrido un error: " + err.Message);
            }
        }

        #endregion

        #region Get(Id) Sanciones

        /// <summary>
        /// Este es el GET con Id: api/Sanciones/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // 
        [HttpGet("{id}")]
        public async Task<ActionResult<SancionesDTO>> Get(int id)
        {
            try
            {
                var sanciones = await _context.Sanciones.Select(s => new SancionesDTO
                {
                    Id = s.Id,
                    FechaActual = s.FechaActual,
                    Sancion = s.Sancion,
                    Observacion = s.Observacion,
                    Valor = s.Valor,
                    ConductorId = s.ConductorId,
                    Nombre = s.Conductor.Nombres + " " + s.Conductor.Apellidos
                }).FirstOrDefaultAsync(s => s.Id == id);

                if (sanciones == null)
                {
                    return NotFound();
                }
                else
                {
                    return sanciones;
                }
            }
            catch (Exception err)
            {
                throw new Exception("Ha ocurrido un error: " + err.Message);
            }
        }

        #endregion

        #region POST Sanciones

        /// <summary>
        /// Este es POST: api/Sanciones
        /// </summary>
        /// <param name="sanciones"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<HttpStatusCode> Post(SancionesDTO sanciones)
        {
            try
            {
                var entity = new Sanciones()
                {
                    Sancion = sanciones.Sancion,
                    Observacion = sanciones.Observacion,
                    Valor = sanciones.Valor,
                    ConductorId = sanciones.ConductorId                    
                };
                
                    //Valida conductor
                    if (ValidarConductor(sanciones.ConductorId.Trim()) == false)
                    {
                        _context.Add(entity);
                        await _context.SaveChangesAsync();
                        return HttpStatusCode.Created;
                    }
                    else
                    {
                        return HttpStatusCode.BadRequest;
                    }

            }
            catch (Exception err)
            {
                throw new Exception("Ha ocurrido un error: " + err.Message);
            }
        }

        #endregion

        #region PUT Sanciones

        /// <summary>
        /// Este es el PUT: api/Sanciones/5
        /// </summary>
        /// <param name="sanciones"></param>
        /// <returns></returns>
        
        [HttpPut("{id}")]
        public async Task<HttpStatusCode> Put(SancionesDTO sanciones)
        {
            try
            {
                var entity = await _context.Sanciones.FirstOrDefaultAsync(c => c.Id == sanciones.Id);
                entity.Sancion = sanciones.Sancion;
                entity.Observacion = sanciones.Observacion;
                entity.Valor = sanciones.Valor;
                entity.ConductorId = sanciones.ConductorId;

                //Valida matricula
                if (ValidarConductor(sanciones.ConductorId.Trim()) == false)
                {
                    _context.Entry(entity).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    return HttpStatusCode.NoContent;
                }
                else
                {
                    return HttpStatusCode.BadRequest;
                }
            }
            catch (Exception err)
            {
                throw new Exception("Ha ocurrido un error: " + err.Message);
            }
        }

        #endregion

        #region DELETE Sanciones

        /// <summary>
        /// Este es DELETE: api/Sanciones/5 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       
        [HttpDelete("{id}")]
        public async Task<HttpStatusCode> Delete(int id)
        {
            try
            {
                var entity = new Sanciones()
                {
                    Id = id
                };

                _context.Sanciones.Attach(entity);
                _context.Sanciones.Remove(entity);
                await _context.SaveChangesAsync();

                return HttpStatusCode.OK;

            }
            catch (Exception err)
            {
                throw new Exception("Ha ocurrido un error: " + err.Message);
            }
        }

        #endregion

        #region Validación existencia del conductor

        /// <summary>
        /// Valida que el conductor exista
        /// </summary>
        /// <param name="conductorId"></param>
        /// <returns></returns>
        public bool ValidarConductor(string conductorId)
        {
            Conductores conductor = _context.Conductores.FirstOrDefault(m => m.Identificacion == conductorId.Trim());
            if (conductor == null)
            {
                return true; //No existe
            }
            else
            {
                return false; //Existe
            }
        }

        #endregion
    }
}
