using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using web.DTO.DentalAnalysis;
using web.Mapper;

namespace web.Controllers
{
    [Controller]
    [Route("api/v1/dental-analysis")]
    public class DentalAnalysisController : Controller
    {
        private readonly IDentalAnalysisService _service;

        public DentalAnalysisController(IDentalAnalysisService service)
        {
            _service = service;
        }

        /// <summary>
        ///    Rota para criação de uma análise dentária
        /// </summary>
        /// <remarks>
        ///    A requisição deve conter os seguintes dados:
        ///    - **UserId**: Identificador único do usuário (paciente) para o qual a análise será criada.
        ///    - **AnalysisDate**: Data da análise dentária.
        ///    - **ProbabilityProblem**: Valor da probabilidade do dente estar com problemas com base na análise.
        ///    - **MonitoringDataIdList**: Lista de IDs dos dados de monitoramento associados à análise.
        /// </remarks>
        /// <response code="201">Analise Dentaria criada</response>
        /// <response code="400">Requisição inválida</response>
        /// <param name="request">Adicionar valor da probabilidade do dente estar com problemas com base na análise</param>
        /// <returns>Detalhes da análise dentária criada</returns>
        [HttpPost]
        public async Task<ActionResult> CreateDentalAnalysis([FromBody] AddDentalAnalysisRequest request)
        {
            DentalAnalysis dentalAnalysisCreated = await _service.CreateDentalAnalysisAsync(request.UserId,
                                        request.AnalysisDate,
                                        request.ProbabilityProblem,
                                        request.MonitoringDataIdList);

            DentalAnalysisResponse response = DentalAnalysisMapper.ToDTO(dentalAnalysisCreated);
            return CreatedAtAction(nameof(CreateDentalAnalysis), response);
        }

        /// <summary>
        ///    Rota para atualizar uma análise dentária
        /// </summary>
        /// <remarks>
        ///    A requisição deve conter os seguintes dados:
        ///    - **newProbabilityProblem**: Nova probabilidade de ser algum problema dentário.
        /// </remarks>
        /// <response code="200">Análise dentária atualizada</response>
        /// <response code="400">Requisição inválida</response>
        /// <response code="404">Análise dentária não encontrada</response>
        /// <param name="dentalAnalysisId">ID da análise dentária</param>
        /// <param name="request">Dados para atualização da análise dentária. Com ID da análise do problema dentário e a nova probabilidade de ser algum problema</param>
        /// <returns>Detalhes da análise dentária atualizada</returns>
        [HttpPatch("{dentalAnalysisId}")]
        public async Task<ActionResult> UpdateDentalAnalysis(int dentalAnalysisId, [FromBody] UpdateDentalAnalysis request)
        {
            DentalAnalysis dentalAnalysisUpdated = await _service.UpdateDentalAnalysisUserAsync(dentalAnalysisId, request.newProbabilityProblem);

            DentalAnalysisResponse response = DentalAnalysisMapper.ToDTO(dentalAnalysisUpdated);
            return Ok(response);
        }

        /// <summary>
        ///    Rota para obter todas as análises dentárias
        /// </summary>
        /// <response code="200">Lista de análises dentárias</response>
        /// <response code="400">Requisição inválida</response>
        /// <param name="pageNumber">Número da página</param>
        /// <param name="pageSize">Tamanho da página</param>
        /// <returns>Lista de análises dentárias</returns>
        [HttpGet]
        public async Task<ActionResult> GetAllDentalAnalysis(int pageNumber = 1, int pageSize = 10)
        {
            IEnumerable<DentalAnalysis> dentalAnalysisCreated = await _service.GetDentalAnalysisAsync(pageNumber, pageSize);

            List<DentalAnalysisResponse> response = DentalAnalysisMapper.ToDTO(dentalAnalysisCreated);
            return Ok(response);
        }

        /// <summary>
        ///    Rota para obter uma análise dentária por ID
        /// </summary>
        /// <response code="200">Detalhes da análise dentária</response>
        /// <response code="404">Análise dentária não encontrada</response>
        /// <param name="dentalAnalysisId">ID da análise dentária</param>
        /// <returns>Detalhes da análise dentária</returns>
        [HttpGet("{dentalAnalysisId}")]
        public async Task<ActionResult> GetAllDentalAnalysisById(int dentalAnalysisId)
        {
            DentalAnalysis dentalAnalysisFounded = await _service.GetDentalAnalysisByIdAsync(dentalAnalysisId);

            DentalAnalysisResponse response = DentalAnalysisMapper.ToDTO(dentalAnalysisFounded);
            return Ok(response);
        }

        /// <summary>
        ///    Rota para deletar uma análise dentária
        /// </summary>
        /// <response code="204">Análise dentária deletada</response>
        /// <response code="404">Análise dentária não encontrada</response>
        /// <param name="dentalAnalysisId">ID da análise dentária</param>
        /// <returns></returns>
        [HttpDelete("{dentalAnalysisId}")]
        public async Task<ActionResult> DeleteDentalAnalysis(int dentalAnalysisId)
        {
            await _service.DeleteDentalAnalysisAsync(dentalAnalysisId);

            return NoContent();
        }
    }
}
