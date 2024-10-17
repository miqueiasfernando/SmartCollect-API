using Fiap.Api.Coletas.Services;
using Microsoft.AspNetCore.Mvc;
using Fiap.Api.Coletas.ViewModel;
using Fiap.Api.Coletas.Models;
using AutoMapper;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Authorization;
using Fiap.Api.Coletas.Data.Contexts;
using Fiap.Api.Coletas.Exception;

namespace Fiap.Api.Coletas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ColetasController : ControllerBase
    {
        private readonly IColetasService _service;
        private readonly IMapper _mapper;

        public ColetasController(IColetasService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        //---------------------------- LISTAR COLETAS AGENDADADAS PAGINADO---------------------------------------//
        [HttpGet]
        [Authorize(Roles = "operador, analista, gerente")]
        public ActionResult<ColetasPaginacaoReferenciaViewModel> Get([FromQuery] int referencia = 0, [FromQuery] int tamanho = 10)
        {
            var Coletas = _service.ListarColetasReferencia(referencia, tamanho);
            var viewModelList = _mapper.Map<IEnumerable<ColetasViewModel>>(Coletas);

            if (!viewModelList.Any())
            {
                return NoContent();
            }

            var viewModel = new ColetasPaginacaoReferenciaViewModel
            {
                Coletas = viewModelList,
                PageSize = tamanho,
                Ref = referencia,
                NextRef = viewModelList.Last().Id
            };

            return Ok(viewModel);
        }


        //---------------------------- COLETAS POR ID---------------------------------------//
        [HttpGet("{id}")]
        [Authorize(Roles = "analista, gerente")]
        public ActionResult<ColetasViewModel> Get([FromRoute] long id)
        {
            try
            {
                var coleta = _service.ObtercoletaPorId(id);
                if (coleta == null)
                    return NotFound();

                var viewModel = _mapper.Map<ColetasViewModel>(coleta);
                return Ok(viewModel);
            }
            catch (NotFoundException ex)
            {
                // Exceção de recurso não encontrado (404)
                return NotFound($"coleta não encontrado com ID {id}");
            }
            catch (ApplicationException ex)
            {
                // Outras exceções genéricas (500)
                return StatusCode(500, $"Erro interno ao processar a solicitação: {ex.Message}");
            }

        }


        //---------------------------- CRIAR NOVO AGENDAMENTO DE COLETA ---------------------------------------//
        [HttpPost]
        [Authorize(Roles = "analista, gerente")]
        public ActionResult Post([FromBody] ColetasViewModel viewModel)
        {
            var coleta = _mapper.Map<ColetasModel>(viewModel);
            _service.Criar(coleta);
            return CreatedAtAction(nameof(Get), new { id = coleta.Id }, coleta);
        }


        //---------------------------- ATUALIZAR AGENDAMENTO ---------------------------------------//
        [HttpPut("{id}")]
        [Authorize(Roles = "gerente")]
        public ActionResult Put(long id, [FromBody] ColetasViewModel viewModel)
        {
            var coletaExiste = _service.ObtercoletaPorId(id);
            if (viewModel == null)
            {
                return NotFound();
            }
            _mapper.Map(viewModel, coletaExiste);
            _service.Atualizar(coletaExiste);
            return NoContent();
        }


        //---------------------------- EXCLUIR AGENDAMENTO ---------------------------------------//
        [HttpDelete("{id}")]
        [Authorize(Roles = "gerente")]
        public ActionResult Delete(long id)
        {
            _service.Excluir(id);
            return NoContent();
        }
    }
}
