using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using web.DTO.DentalProcedure;
using web.Mapper;

namespace web.Controllers
{
    [ApiController]
    [Route("api/v1/dental-procedure")]
    public class DentalProcedureController : Controller
    {
        private readonly IDentalProcedureService _service;

        public DentalProcedureController(IDentalProcedureService service)
        {
            _service = service;
        }

        /// <summary>
        ///    Rota para criação de um procedimento dental
        /// </summary>
        /// <response code="201">Procedimento Dental criado</response>
        /// <response code="400">Requisição inválida</response>
        /// <remarks>
        ///    A requisição deve conter os seguintes dados:
        ///    - **MonitoringDataId**: Identificador único dos dados de monitoramento associados ao procedimento dental.
        ///    - **Problem**: Descrição do problema ou condição que motivou a realização do procedimento.
        /// </remarks>
        /// <param name="request">Dados para criação do procedimento dental</param>
        /// <returns>Detalhes do procedimento dental criado</returns>
        [HttpPost]
        public async Task<ActionResult<DentalProcedureResponse>> CreateDentalProcedure([FromBody] AddDentalProcedureRequest request)
        {
            var dentalProcedure = await _service.CreateDentalProcedureAsync(request.MonitoringDataId, request.Problem);
            var response = DentalProcedureMapper.ToDto(dentalProcedure);
            return CreatedAtAction(nameof(CreateDentalProcedure), response);
        }

        /// <summary>
        ///    Rota para obter todos os procedimentos dentais
        /// </summary>
        /// <response code="200">Lista de procedimentos dentais</response>
        /// <param name="pageNumber">Número da página</param>
        /// <param name="pageSize">Tamanho da página</param>
        /// <returns>Lista de procedimentos dentais</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DentalProcedureResponse>>> GetAllDentalProcedures(int pageNumber = 0, int pageSize = 10)
        {
            IEnumerable<DentalProcedure> dentalProcedures = await _service.GetDentalProcedureAsync(pageNumber, pageSize);
            IEnumerable<DentalProcedureResponse> response = DentalProcedureMapper.ToDto(dentalProcedures);
            return Ok(response);
        }

        /// <summary>
        ///    Rota para obter um procedimento dental pelo ID
        /// </summary>
        /// <response code="200">Detalhes do procedimento dental</response>
        /// <response code="404">Procedimento dental não encontrado</response>
        /// <param name="dentalProcedureId">ID do procedimento dental</param>
        /// <returns>Detalhes do procedimento dental</returns>
        [HttpGet("{dentalProcedureId}")]
        public async Task<ActionResult<DentalProcedureResponse>> GetDentalProcedureById(int dentalProcedureId)
        {
            var dentalProcedure = await _service.GetDentalProcedureByIdAsync(dentalProcedureId);
            var response = DentalProcedureMapper.ToDto(dentalProcedure);
            return Ok(response);
        }

        /// <summary>
        ///    Rota para atualizar um procedimento dental
        /// </summary>
        /// <response code="200">Procedimento dental atualizado</response>
        /// <response code="404">Procedimento dental não encontrado</response>
        /// <param name="dentalProcedureId">ID do procedimento dental</param>
        /// <remarks>
        ///    A requisição deve conter os seguintes dados:
        ///    - **dentalProcedureId**: Identificador único do procedimento dental a ser atualizado.
        ///    - **Problem**: Descrição do problema ou condição relacionada ao procedimento dental.
        /// </remarks>
        /// <param name="updateRequest">Dados para atualização do procedimento dental</param>
        /// <returns>Detalhes do procedimento dental atualizado</returns>
        [HttpPatch("{dentalProcedureId}")]
        public async Task<ActionResult<DentalProcedureResponse>> UpdateDentalProcedure(int dentalProcedureId, [FromBody] UpdateDentalProcedureRequest updateRequest)
        {
            var updatedDentalProcedure = await _service.UpdateDentalProcedureAsync(dentalProcedureId, updateRequest.Problem);
            var response = DentalProcedureMapper.ToDto(updatedDentalProcedure);
            return Ok(response);
        }

        /// <summary>
        ///    Rota para deletar um procedimento dental
        /// </summary>
        /// <response code="204">Procedimento dental deletado</response>
        /// <response code="404">Procedimento dental não encontrado</response>
        /// <param name="dentalProcedureId">ID do procedimento dental</param>
        /// <returns>Sem conteúdo</returns>
        [HttpDelete("{dentalProcedureId}")]
        public async Task<ActionResult> DeleteDentalProcedure(int dentalProcedureId)
        {
            await _service.DeleteDentalProcedureAsync(dentalProcedureId);
            return NoContent();
        }
    }
}
