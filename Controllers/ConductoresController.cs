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
    public class ConductoresController : ControllerBase
    {
        #region ConduccionDbContext / Constructor

        public readonly ConduccionDbContext _context;
        public ConductoresController(ConduccionDbContext context)
        {
            _context = context;
        }

        #endregion

        #region GET Conductores

        /// <summary>
        /// Este es GET: api/Conductores
        /// </summary>
        /// <returns></returns>
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConductoresDTO>>> Get()
        {
            try
            {
                var conductor = await _context.Conductores.Select(c => new ConductoresDTO
                {
                    //Id = m.Id, //Si lo comento oculta el valor pero no el campo.
                    Identificacion = c.Identificacion,
                    Nombres = c.Nombres,
                    Apellidos = c.Apellidos,
                    Direccion = c.Direccion,
                    Telefono = c.Telefono,
                    EMail = c.EMail,
                    Fecha_Nacimiento = c.Fecha_Nacimiento,
                    Activo = c.Activo,
                    Id_Matricula = c.Id_Matricula
                }).OrderBy(m => m.Identificacion).ToListAsync();

                if (conductor == null)
                {
                    return NotFound();
                }
                else
                {
                    return conductor;
                }
            }
            catch (Exception err)
            {
                throw new Exception("Ha ocurrido un error: " + err.Message);
            }
        }

        #endregion

        #region GET(identificación) Conductores

        /// <summary>
        /// Este es GET(identificación) api/<Conductores>/5 
        /// </summary>
        /// <param name="identificacion"></param>
        /// <returns></returns>

        // 
        [HttpGet("{identificacion}")]
        public async Task<ActionResult<ConductoresDTO>> Get(string identificacion)
        {
            try
            {
                var conductor = await _context.Conductores.Select(c => new ConductoresDTO
                {
                    //Id = m.Id, //Si lo comento oculta el valor pero no el campo.
                    Identificacion = c.Identificacion,
                    Nombres = c.Nombres,
                    Apellidos = c.Apellidos,
                    Direccion = c.Direccion,
                    Telefono = c.Telefono,
                    EMail = c.EMail,
                    Fecha_Nacimiento = c.Fecha_Nacimiento,
                    Activo = c.Activo,
                    Id_Matricula = c.Id_Matricula
                }).FirstOrDefaultAsync(c => c.Identificacion == identificacion);

                if (conductor == null)
                {
                    return NotFound();
                }
                else
                {
                    return conductor;
                }
            }
            catch (Exception err)
            {
                throw new Exception("Ha ocurrido un error: " + err.Message);
            }
        }

        #endregion

        #region POST Conductores

        /// <summary>
        /// Este es POST api/Conductores 
        /// </summary>
        /// <param name="conductores"></param>
        /// <returns></returns>
        
        [HttpPost]
        public async Task<HttpStatusCode> Post(ConductoresDTO conductores)
        {
            try
            {
                var entity = new Conductores()
                {
                    Identificacion = conductores.Identificacion.Trim(),
                    Nombres = conductores.Nombres,
                    Apellidos = conductores.Apellidos,
                    Direccion = conductores.Direccion,
                    Telefono = conductores.Telefono,
                    EMail = conductores.EMail,
                    Fecha_Nacimiento = conductores.Fecha_Nacimiento,
                    Activo = conductores.Activo,
                    Id_Matricula = conductores.Id_Matricula
                };

                //Valida matricula
                if (ValidarMatricula(conductores.Id_Matricula.Trim()) == false)
                {
                    //Valida conductor
                    if (ValidarConductor(conductores.Identificacion.Trim()) == true)
                    {
                        //Valida matricula unica
                        if (ValidarMatriculaUnica(conductores.Id_Matricula.Trim()) == true)
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
                    else
                    {
                        return HttpStatusCode.BadRequest;
                    }
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

        #region PUT Conductores

        /// <summary>
        /// Este es PUT api/Conductores/5 
        /// </summary>
        /// <param name="conductores"></param>
        /// <returns></returns>

        [HttpPut("{identificacion}")]
        public async Task<HttpStatusCode> Put(ConductoresDTO conductores)
        {
            try
            {
                var entity = await _context.Conductores.FirstOrDefaultAsync(c => c.Identificacion == conductores.Identificacion);
                entity.Nombres = conductores.Nombres;
                entity.Apellidos = conductores.Apellidos;
                entity.Direccion = conductores.Direccion;
                entity.Telefono = conductores.Telefono;
                entity.EMail = conductores.EMail;
                entity.Fecha_Nacimiento = conductores.Fecha_Nacimiento;
                entity.Activo = conductores.Activo;
                entity.Id_Matricula = conductores.Id_Matricula;

                //Valida matricula
                if (ValidarMatricula(conductores.Id_Matricula.Trim()) == false)
                {                                        
                    //_context.Entry(entity).State = EntityState.Modified; NOTA: En este caso tuve que comentar esta línea para que puediera actualizar porque si no lo hacia me salia el error de "No se puede actualizar columna Identity ID".
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

        #region DELETE Conductores

        /// <summary>
        /// Este es DELETE: api/Conductores/5 
        /// </summary>
        /// <param name="identificacion"></param>
        /// <returns></returns>

        // 
        [HttpDelete("{identificacion}")]
        public async Task<HttpStatusCode> Delete(string identificacion)
        {
            try
            {
                var entity = new Conductores()
                {
                    Identificacion = identificacion
                };

                _context.Conductores.Attach(entity);
                _context.Conductores.Remove(entity);
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
        /// Valida que el conductor exista.
        /// </summary>
        /// <param name="identificacion"></param>
        /// <returns></returns>

        public bool ValidarConductor(string identificacion)
        {
            Conductores conductor = _context.Conductores.FirstOrDefault(m => m.Identificacion == identificacion.Trim());
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

        #region Validación existencia de la matricula

        /// <summary>
        /// Valida que la matricula exista.
        /// </summary>
        /// <param name="numeroMatricula"></param>
        /// <returns></returns>

        public bool ValidarMatricula(string numeroMatricula)
        {
            Matriculas matricula = _context.Matriculas.FirstOrDefault(m => m.Numero == numeroMatricula.Trim());
            if (matricula == null)
            {
                return true; //No existe
            }
            else
            {
                return false; //Existe
            }
        }

        #endregion

        #region Validación de matricula única en conductores

        /// <summary>
        /// Valida que la matricula sea única en conductores porque la relación Matricula-Conductor es uno a uno.
        /// </summary>
        /// <param name="idMatricula"></param>
        /// <returns></returns>
        public bool ValidarMatriculaUnica(string idMatricula)
        {
            Conductores conductor = _context.Conductores.FirstOrDefault(m => m.Id_Matricula == idMatricula.Trim());
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
