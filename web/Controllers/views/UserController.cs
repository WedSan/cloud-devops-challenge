using Application.Services;
using Domain.Extensions;
using Domain.Interfaces.Services;
using Infrastructure.Data;
using web.DTO.User;



using Microsoft.AspNetCore.Mvc;
namespace web.Controllers.views
{
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

        // GET: User
        public async Task<IActionResult> Index(int pageNumber = 1, int pageSize = 10)
        {
            var userList = await _userService.GetUsersAsync(pageNumber, pageSize);
            return View(userList);
        }

        // GET: User/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserRequest request)
        {
            if (ModelState.IsValid)
            {
                var userCreated =
                    await _userService.CreateUserAsync(request.Name, request.Email, request.Password,
                        GenderExtension.ToGender(request.Gender) );
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        // GET: User/EditEmail/5
        public async Task<IActionResult> EditEmail(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);  
            if (user == null)
            {
                return NotFound();
            }

            var request = new UpdateEmailRequest
            {
                Email = user.Email 
            };
            
            ViewData["UserId"] = id;

            
            return View(request);
        }


        // POST: User/EditEmail/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditEmail(int id, UpdateEmailRequest request)
        {
            if (ModelState.IsValid)
            {
                await _userEmailUpdater.UpdateEmail(id, request.Email);
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        // GET: User/EditPassword/5
        public IActionResult EditPassword(int id)
        {
            ViewData["UserId"] = id;
            return View(new UpdatePasswordRequest());
        }

        // POST: User/EditPassword/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPassword(int id, UpdatePasswordRequest request)
        {
            if (ModelState.IsValid)
            {
                await _userPasswordUpdater.UpdatePassword(id, request.NewPassword);
                return RedirectToAction(nameof(Index));
            }
                
            return View(request);
        }

        // GET: User/Delete/5
        public IActionResult Delete(int id)
        {
            return View(new { UserId = id });
        }

        // POST: User/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
