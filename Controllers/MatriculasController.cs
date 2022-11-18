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
    public class MatriculasController : ControllerBase
    {
        #region ConduccionDbContext / Constructor

        private readonly ConduccionDbContext _context;

        public MatriculasController(ConduccionDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Get Matriculas

        /// <summary>
        /// Este es el GET: api/Matriculas
        /// </summary>

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatriculasDTO>>> Get()
        {
            try
            {
                var matri = await _context.Matriculas.Select(m => new MatriculasDTO
                {
                    //Id = m.Id, //Si lo comento oculta el valor pero no el campo.
                    Numero = m.Numero,
                    FechaExpedicion = m.FechaExpedicion,
                    ValidoHasta = m.ValidoHasta,
                    Activo = m.Activo
                }).OrderBy(m => m.Numero).ToListAsync();

                if (matri == null)
                {
                    return NotFound();
                }
                else 
                {
                    return matri;
                }
            }
            catch (Exception err)
            {
                throw new Exception("Ha ocurrido un error: " + err.Message);
            }
        }

        #endregion

        #region Get(Numero) Matriculas

        /// <summary>
        /// Este es Get con número: api/Matriculas/5
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>

        [HttpGet("{numero}")]
        public async Task<ActionResult<MatriculasDTO>> Get(string numero)
        {
            try
            {
                var matri = await _context.Matriculas.Select(m => new MatriculasDTO
                {
                    //Id = m.Id, //Si lo comento oculta el valor pero no el campo.
                    Numero = m.Numero,
                    FechaExpedicion = m.FechaExpedicion,
                    ValidoHasta = m.ValidoHasta,
                    Activo = m.Activo
                }).FirstOrDefaultAsync(m => m.Numero == numero);

                if (matri == null)
                {
                    return NotFound();
                }
                else
                {
                    return matri;
                }
            }
            catch (Exception err)
            {
                throw new Exception("Ha ocurrido un error: " + err.Message);
            }
        }

        #endregion

        #region Post Matriculas

        /// <summary>
        /// Este es el Post: api/Matriculas
        /// </summary>
        /// <param name="matriculas"></param>
        /// <returns></returns>
        
        [HttpPost]
        public async Task<HttpStatusCode> Post(MatriculasDTO matriculas)
        {
            try
            {
                var entity = new Matriculas()
                {
                    Numero = matriculas.Numero.Trim(),
                    FechaExpedicion = matriculas.FechaExpedicion,
                    ValidoHasta = matriculas.ValidoHasta,
                    Activo = matriculas.Activo
                };

                _context.Add(entity);
                await _context.SaveChangesAsync();
                return HttpStatusCode.Created;

            }
            catch (Exception err)
            {
                throw new Exception("Ha ocurrido un error: " + err.Message);
            }
        }

        #endregion

        #region Put Matriculas

        /// <summary>
        /// Este es PUT: api/Matriculas/5 
        /// </summary>
        /// <param name="matriculas"></param>
        /// <returns></returns>

        [HttpPut("{numero}")]
        public async Task<HttpStatusCode> Put(Matriculas matriculas)
        {
            try
            {
                var entity = await _context.Matriculas.FirstOrDefaultAsync(m => m.Numero == matriculas.Numero);
                entity.FechaExpedicion = matriculas.FechaExpedicion;
                entity.ValidoHasta = matriculas.ValidoHasta;
                entity.Activo = matriculas.Activo;

                //_context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return HttpStatusCode.NoContent;

            }
            catch (Exception err)
            {
                throw new Exception("Ha ocurrido un error: " + err.Message);
            }
        }

        #endregion

        #region Delete Matriculas

        /// <summary>
        /// Este es DELETE: api/Matriculas/5
        /// </summary>
        /// <param name="numero"></param>
        /// <returns></returns>

        [HttpDelete("{numero}")]
        public async Task<HttpStatusCode> Delete(string numero)
        {
            try
            {
                var entity = new Matriculas()
                {
                    Numero = numero
                };

                _context.Matriculas.Attach(entity);
                _context.Matriculas.Remove(entity);
                await _context.SaveChangesAsync();

                return HttpStatusCode.OK;

            }
            catch (Exception err)
            {
                throw new Exception("Ha ocurrido un error: " + err.Message);
            }
        }

        #endregion
    }
}
