using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using web.DTO.ReportDentalProblem; 
using web.Mapper; 

namespace web.Controllers
{
    [Controller]
    [Route("api/v1/report-dental-problems")]
    public class ReportDentalProblemController : Controller
    {
        private readonly IReportDentalProblemService _service;

        public ReportDentalProblemController(IReportDentalProblemService service)
        {
            _service = service;
        }

        /// <summary>
        /// Rota para criação de um Relato de Problema Dentário
        /// </summary>
        /// <response code="201">Relato de Problema Dentario criado</response>
        /// <response code="400">Requisição inválida</response>
        /// <remarks>
        ///    A requisição deve conter os seguintes dados:
        ///    - **MonitoringDataId**: Identificador único dos dados de monitoramento aos quais o relato será associado.
        ///    - **Problem**: Descrição detalhada do problema dentário relatado.
        /// </remarks>
        /// <param name="request">Dados para criação do Relato de problema dentário</param>
        /// <returns>Detalhes do Relato do Problema Dentário criado</returns>
        [HttpPost]
        public async Task<ActionResult<ReportDentalProblemResponse>> CreateReportDentalProblem([FromBody] AddReportDentalProblemRequest request)
        {
            ReportDentalProblem reportDentalProblem = await _service.CreateReportDentalProblemAsync(request.MonitoringDataId, request.Problem);
            ReportDentalProblemResponse response = ReportDentalProblemMapper.ToDto(reportDentalProblem);
            return CreatedAtAction(nameof(CreateReportDentalProblem), response);
        }

        /// <summary>
        /// Rota para obter todos os Relatos de Problemas Dentários
        /// </summary>
        /// <response code="200">Lista de Relatos de Problemas Dentários</response>
        /// <response code="400">Requisição inválida</response>
        /// <param name="pageNumber">Número da página</param>
        /// <param name="pageSize">Tamanho da página</param>
        /// <returns>Lista de Relatos de Problemas Dentários</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReportDentalProblemResponse>>> GetAllReportDentalProblems(int pageNumber = 0, int pageSize = 10)
        {
            IEnumerable<ReportDentalProblem> reportDentalProblemList = await _service.GetReportDentalProblemAsync(pageNumber, pageSize);
            IEnumerable<ReportDentalProblemResponse> response = ReportDentalProblemMapper.ToDto(reportDentalProblemList);
            return Ok(response);
        }

        /// <summary>
        /// Rota para obter um Relato de Problema Dentário por ID
        /// </summary>
        /// <response code="200">Detalhes do Relato de Problema Dentário</response>
        /// <response code="404">Relato de Problema Dentário não encontrado</response>
        /// <param name="reportDentalProblemId">ID do Relato de Problema Dentário</param>
        /// <returns>Detalhes do Relato de Problema Dentário</returns>
        [HttpGet("{reportDentalProblemId}")]
        public async Task<ActionResult<ReportDentalProblemResponse>> GetReportDentalProblemById(int reportDentalProblemId)
        {
            ReportDentalProblem reportDentalProblem = await _service.GetReportDentalProblemByIdAsync(reportDentalProblemId);
            ReportDentalProblemResponse response = ReportDentalProblemMapper.ToDto(reportDentalProblem);
            return Ok(response);
        }

        /// <summary>
        /// Rota para atualizar um Relato de Problema Dentário
        /// </summary>
        /// <response code="200">Relato de Problema Dentário atualizado</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="404">Relato de Problema Dentário não encontrado</response>
        /// <param name="reportDentalProblemId">ID do Relato de Problema Dentário</param>
        /// <remarks>
        ///    A requisição deve conter os seguintes dados:
        ///    - **reportDentalProblemId**: Identificador único do Relato de Problema Dentário a ser atualizado.
        ///    - **Problem**: Nova descrição do problema dentário relatado.
        /// </remarks>
        /// <param name="updateRequest">Dados para atualização do Relato de Problema Dentário</param>
        /// <returns>Detalhes do Relato de Problema Dentário atualizado</returns>
        [HttpPatch("{reportDentalProblemId}")]
        public async Task<ActionResult<ReportDentalProblemResponse>> UpdateReportDentalProblem(int reportDentalProblemId, [FromBody] UpdateReportDentalProblemRequest updateRequest)
        {
            ReportDentalProblem reportDentalProblemUpdated = await _service.UpdateReportDentalProblemAsync(reportDentalProblemId, updateRequest.Problem);
            ReportDentalProblemResponse response = ReportDentalProblemMapper.ToDto(reportDentalProblemUpdated);
            return Ok(response);
        }

        /// <summary>
        /// Rota para deletar um Relato de Problema Dentário
        /// </summary>
        /// <response code="204">Relato de Problema Dentário deletado</response>
        /// <response code="404">Relato de Problema Dentário não encontrado</response>
        /// <param name="reportDentalProblemId">ID do Relato de Problema Dentário</param>
        /// <returns>Sem conteúdo</returns>
        [HttpDelete("{reportDentalProblemId}")]
        public async Task<ActionResult> DeleteReportDentalProblem(int reportDentalProblemId)
        {
            await _service.DeleteReportDentalProblemaAsync(reportDentalProblemId);
            return NoContent();
        }
    }
}
