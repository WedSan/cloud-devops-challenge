using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using web.DTO.DataMonitoring;
using web.Mapper;

namespace web.Controllers
{
    [Controller]
    [Route("api/v1/monitoring-data")]
    public class MonitoringDataApiController : Controller
    {
        private IMonitoringDataService _service;

        public MonitoringDataApiController(IMonitoringDataService service)
        {
            _service = service;
        }

        /// <summary>
        /// Rota para criação de um Dado de Monitoramento
        /// </summary>
        /// <response code="201">Dado de Monitoramento criado</response>
        /// <response code="400">Requisição inválida</response>
        /// <remarks>
        ///    A requisição deve conter os seguintes dados:
        ///    - **UserId**: Identificador único do usuário (paciente) ao qual o dado de monitoramento será associado.
        ///    - **DentalProblems**: Lista de problemas dentários identificados durante o monitoramento.
        /// </remarks>
        /// <param name="request">Dados para criação do Dado Monitoramento</param>
        /// <returns>Detalhes do Dado Monitoramento criado</returns>
        [HttpPost]
        public async Task<ActionResult<MonitoringDataResponse>> CreateMonitoringData([FromBody] AddDataMonitoringRequest request)
        {
            MonitoringData monitoringData = await _service.CreateMonitoringDataAsync(request.UserId, MonitoringDataMapper.MapToDentalProblem(request.DentalProblems));
            MonitoringDataResponse response = MonitoringDataMapper.ToDto(monitoringData);
            return CreatedAtAction(nameof(CreateMonitoringData), response);
        }

        /// <summary>
        /// Rota para obter todos os Dados de Monitoramento
        /// </summary>
        /// <response code="200">Lista de Dados de Monitoramento</response>
        /// <param name="pageNumber">Número da página</param>
        /// <param name="pageSize">Tamanho da página</param>
        /// <returns>Lista de Dados de Monitoramento</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MonitoringDataResponse>>> GetAllMonitoringData(int pageNumber = 0, int pageSize = 10)
        {
            IEnumerable<MonitoringData> monitoringDataList = await _service.GetMonitoringDataAsync(pageNumber, pageSize);
            IEnumerable<MonitoringDataResponse> response = MonitoringDataMapper.ToDto(monitoringDataList);
            return Ok(response);
        }

        /// <summary>
        /// Rota para obter um Dado de Monitoramento pelo ID
        /// </summary>
        /// <response code="200">Dado de Monitoramento encontrado</response>
        /// <response code="404">Dado de Monitoramento não encontrado</response>
        /// <param name="monitoringDataId">ID do Dado de Monitoramento</param>
        /// <returns>Detalhes do Dado de Monitoramento</returns>
        [HttpGet("{monitoringDataId}")]
        public async Task<ActionResult<MonitoringDataResponse>> GetMonitoringDataById(int monitoringDataId)
        {
            MonitoringData monitoringData = await _service.GetMonitoringDataByIdAsync(monitoringDataId);
            if (monitoringData == null)
            {
                return NotFound();
            }
            MonitoringDataResponse response = MonitoringDataMapper.ToDto(monitoringData);
            return Ok(response);
        }

        /// <summary>
        /// Rota para atualizar um Dado de Monitoramento
        /// </summary>
        /// <response code="200">Dado de Monitoramento atualizado</response>
        /// <response code="404">Dado de Monitoramento não encontrado</response>
        /// <param name="monitoringDataId">ID do Dado de Monitoramento</param>
        /// <remarks>
        ///    A requisição deve conter os seguintes dados:
        ///    - **monitoringDataId**: Identificador único do Dado de Monitoramento a ser atualizado.
        ///    - **UserId**: Novo identificador do usuário (paciente) associado ao Dado de Monitoramento.
        /// </remarks>
        /// <param name="updateRequest">Dados para atualização do Dado de Monitoramento</param>
        /// <returns>Detalhes do Dado de Monitoramento atualizado</returns>
        [HttpPatch("{monitoringDataId}")]
        public async Task<ActionResult<MonitoringDataResponse>> UpdateMonitoringData(int monitoringDataId, [FromBody] UpdateMonitoringDataRequest updateRequest)
        {
            MonitoringData monitoringDataUpdated = await _service.UpdateMonitoringDataUserAsync(monitoringDataId, updateRequest.UserId);
            if (monitoringDataUpdated == null)
            {
                return NotFound();
            }
            MonitoringDataResponse response = MonitoringDataMapper.ToDto(monitoringDataUpdated);
            return Ok(response);
        }

        /// <summary>
        /// Rota para deletar um Dado de Monitoramento
        /// </summary>
        /// <response code="204">Dado de Monitoramento deletado</response>
        /// <response code="404">Dado de Monitoramento não encontrado</response>
        /// <param name="monitoringDataId">ID do Dado de Monitoramento</param>
        /// <returns>Sem conteúdo</returns>
        [HttpDelete("{monitoringDataId}")]
        public async Task<ActionResult> DeleteMonitoringData(int monitoringDataId)
        {
            await _service.DeleteMonitoringDataAsync(monitoringDataId);
            return NoContent();
        }
    }
}
