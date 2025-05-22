using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using web.DTO.DentalHistory;
using web.DTO.DentalHistory.web.DTO.DentalHistory;
using web.Mapper;

namespace web.Controllers
{
    [Controller]
    [Route("api/v1/dental-history")]
    public class DentalHistoryController : Controller
    {
        private readonly IDentalHistoryEntityService _service;

        public DentalHistoryController(IDentalHistoryEntityService service)
        {
            _service = service;
        }

        /// <summary>
        ///    Rota para criação de um histórico dental
        /// </summary>
        /// <response code="201">Historico Dental criado</response>
        /// <response code="400">Requisição inválida</response>
        /// <remarks>
        ///    A requisição deve conter os seguintes dados:
        ///    - **UserId**: Identificador único do usuário (paciente) para o qual o histórico será criado.
        ///    - **Procedures**: Lista de procedimentos realizados (exemplo: limpeza, extração, restauração).
        ///    - **ConsultationDate**: Data da consulta odontológica associada ao histórico.
        ///    - **ToothCondition**: Descrição geral da condição dos dentes no momento da consulta.
        /// </remarks>
        /// <param name="request">Dados para criação do histórico dental</param>
        /// <returns>Detalhes do histórico dental criado</returns>
        [HttpPost]
        public async Task<ActionResult> CreateDentalHistory([FromBody] AddDentalHistoryRequest request)
        {
            DentalHistory dentalHistoryCreated = await _service.CreateDentalHistoryAsync(
                request.UserId,
                request.Procedures,
                request.ConsultationDate,
                request.ToothCondition
            );

            DentalHistoryResponse response = DentalHistoryMapper.ToDTO(dentalHistoryCreated);
            return CreatedAtAction(nameof(CreateDentalHistory), response);
        }

        /// <summary>
        ///    Rota para obter todos os históricos dentais
        /// </summary>
        /// <response code="200">Lista de históricos dentais</response>
        /// <response code="400">Requisição inválida</response>
        /// <param name="pageNumber">Número da página</param>
        /// <param name="pageSize">Tamanho da página</param>
        /// <returns>Lista de históricos dentais</returns>
        [HttpGet]
        public async Task<ActionResult> GetAllDentalHistories(int pageNumber = 1, int pageSize = 10)
        {
            IEnumerable<DentalHistory> dentalHistories = await _service.GetDentalHistoriesAsync(pageNumber, pageSize);

            List<DentalHistoryResponse> response = DentalHistoryMapper.ToDTO(dentalHistories);
            return Ok(response);
        }

        /// <summary>
        ///    Rota para obter um histórico dental por ID
        /// </summary>
        /// <response code="200">Histórico dental encontrado</response>
        /// <response code="404">Histórico dental não encontrado</response>
        /// <param name="dentalHistoryId">ID do histórico dental</param>
        /// <returns>Detalhes do histórico dental</returns>
        [HttpGet("{dentalHistoryId}")]
        public async Task<ActionResult> GetDentalHistoryById(int dentalHistoryId)
        {
            DentalHistory dentalHistoryFounded = await _service.GetDentalHistoryByIdAsync(dentalHistoryId);

            if (dentalHistoryFounded == null)
            {
                return NotFound();
            }

            DentalHistoryResponse response = DentalHistoryMapper.ToDTO(dentalHistoryFounded);
            return Ok(response);
        }

        /// <summary>
        ///    Rota para atualizar um histórico dental
        /// </summary>
        /// <response code="200">Histórico dental atualizado</response>
        /// <response code="400">Requisição inválida</response>
        /// <remarks>
        ///    A requisição deve conter os seguintes dados:
        ///    - **NewProcedures**: Lista de novos procedimentos realizados (exemplo: limpeza, extração, restauração).
        /// </remarks>
        /// <param name="dentalHistoryId">ID do histórico dental</param>
        /// <param name="request">Dados para atualização do histórico dental</param>
        /// <returns>Detalhes do histórico dental atualizado</returns>
        [HttpPatch("{dentalHistoryId}")]
        public async Task<ActionResult> UpdateDentalHistory(int dentalHistoryId, [FromBody] UpdateDentalHistoryRequest request)
        {
            DentalHistory dentalHistoryUpdated = await _service.UpdateDentalHistoryUserAsync(dentalHistoryId, request.NewProcedures);

            DentalHistoryResponse response = DentalHistoryMapper.ToDTO(dentalHistoryUpdated);
            return Ok(response);
        }

        /// <summary>
        ///    Rota para deletar um histórico dental
        /// </summary>
        /// <response code="204">Histórico dental deletado</response>
        /// <response code="404">Histórico dental não encontrado</response>
        /// <param name="dentalHistoryId">ID do histórico dental</param>
        [HttpDelete("{dentalHistoryId}")]
        public async Task<ActionResult> DeleteDentalHistory(int dentalHistoryId)
        {
            await _service.DeleteDentalHistoryAsync(dentalHistoryId);
            return NoContent();
        }
    }
}