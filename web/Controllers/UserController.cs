using Application.Services;
using Domain.Entities;
using Domain.Extensions;
using Domain.Interfaces.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Runtime.CompilerServices;
using web.DTO.User;
using web.Mapper;

namespace web.Controllers
{
    [ApiController]
    [Route("api/v1/user")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        private readonly IUserEmailUpdater _userEmailUpdater;

        private readonly IUserPasswordUpdater _userPasswordUpdater;

        private readonly AppDbContext _appDbContext;

        public UserController(IUserService userService, IUserEmailUpdater userEmailUpdater, IUserPasswordUpdater userPasswordUpdater, AppDbContext appDbContext)
        {
            _userService = userService;
            _userEmailUpdater = userEmailUpdater;
            _userPasswordUpdater = userPasswordUpdater;
            _appDbContext = appDbContext;
        }

        /// <summary>
        /// Rota para criação de um Usuário
        /// </summary>
        /// <response code="201">Usuário criado</response>
        /// <response code="400">Requisição inválida</response> 
        /// <param name="request">Dados para criação do Usuário. O campo "gender" apenas pode ser "M" ou "F"</param>
        /// <returns>Detalhes do Usuário criado</returns>
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser([FromBody] AddUserRequest request)
        {
            User userCreated = await _userService.CreateUserAsync(request.Name,
                request.Email,
                request.Password,
                GenderExtension.ToGender(request.Gender));

            return CreatedAtAction(nameof(CreateUser), UserMapper.ToDTO(userCreated));
        }

        /// <summary>
        /// Rota para obter todos os Usuários
        /// </summary>
        /// <response code="200">Lista de Usuários</response>
        /// <response code="400">Requisição inválida</response>
        /// <param name="pageNumber">Número da página</param>
        /// <param name="pageSize">Tamanho da página</param>
        /// <returns>Lista de Usuários</returns>
        [HttpGet]
        public async Task<ActionResult<User>> GetAllUsers(int pageNumber = 1, int pageSize = 10)
        {
            IEnumerable<User> userList = await _userService.GetUsersAsync(pageNumber, pageSize);

            return Ok(UserMapper.ToDTO(userList));
        }

        /// <summary>
        /// Rota para atualizar o email de um Usuário
        /// </summary>
        /// <response code="204">Email atualizado</response>
        /// <response code="400">Requisição inválida</response>
        /// <param name="request">Novo email</param>
        /// <param name="userId">ID do Usuário</param>
        /// <returns>Sem conteúdo</returns>
        [HttpPatch("/{userId}/email")]
        public async Task<ActionResult> UpdateEmail([FromBody] UpdateEmailRequest request, int userId)
        {
            await _userEmailUpdater.UpdateEmail(userId, request.Email);
            return NoContent();
        }

        /// <summary>
        /// Rota para atualizar a senha de um Usuário
        /// </summary>
        /// <response code="204">Senha atualizada</response>
        /// <response code="400">Requisição inválida</response>
        /// <param name="request">Nova senha</param>
        /// <param name="userId">ID do Usuário</param>
        /// <returns>Sem conteúdo</returns>
        [HttpPatch("/{userId}/password")]
        public async Task<ActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request, int userId)
        {
            await _userPasswordUpdater.UpdatePassword(userId, request.NewPassword);
            return NoContent();
        }

        /// <summary>
        /// Rota para deletar um Usuário
        /// </summary>
        /// <response code="204">Usuário deletado</response>
        /// <response code="400">Requisição inválida</response>
        /// <param name="userId">ID do Usuário</param>
        /// <returns>Sem conteúdo</returns>
        [HttpDelete("/{userId}")]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            await _userService.DeleteUserAsync(userId);
            return NoContent();
        }
    }
}
