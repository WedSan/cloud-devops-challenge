using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using web.DTO.DataMonitoring;
using web.Mapper;

namespace web.Controllers.views;

public class MonitoringDataController : Controller
{
     private readonly IMonitoringDataService _service;

        public MonitoringDataController(IMonitoringDataService service)
        {
            _service = service;
        }

        // GET: MonitoringData/Index
        public async Task<IActionResult> Index(int pageNumber = 0, int pageSize = 10)
        {
            var monitoringDataList = await _service.GetMonitoringDataAsync(pageNumber, pageSize);
            var response = MonitoringDataMapper.ToDto(monitoringDataList);
            return View(response);
        }

        // GET: MonitoringData/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MonitoringData/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddDataMonitoringRequest request)
        {
            if (ModelState.IsValid)
            {
                IEnumerable<string> dentalProblems = request.DentalProblems
                    .SelectMany(e => e.Split(","))
                    .Select(e => e.Trim());
                
                var monitoringData = await _service.CreateMonitoringDataAsync(request.UserId,
                    MonitoringDataMapper.MapToDentalProblem(dentalProblems));
                return RedirectToAction(nameof(Index));
            }
            return View(request);
        }

        // GET: MonitoringData/Edit/5
        public async Task<IActionResult> Edit(int monitoringDataId)
        {
            var monitoringData = await _service.GetMonitoringDataByIdAsync(monitoringDataId);
            if (monitoringData == null)
            {
                return NotFound();
            }
            ViewData["monitoringDataId"] = monitoringDataId;
            //var response = MonitoringDataMapper.ToDto(monitoringData);
            return View(new UpdateMonitoringDataRequest());
        }

        // POST: MonitoringData/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int monitoringDataId, UpdateMonitoringDataRequest updateRequest)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateMonitoringDataUserAsync(monitoringDataId, updateRequest.UserId);
                return RedirectToAction(nameof(Index));
            }
            return View(updateRequest);
        }

        // GET: MonitoringData/Delete/5
        public async Task<IActionResult> Delete(int monitoringDataId)
        {
            var monitoringData = await _service.GetMonitoringDataByIdAsync(monitoringDataId);
            if (monitoringData == null)
            {
                return NotFound();
            }
            return View(monitoringData);
        }

        // POST: MonitoringData/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDelete(int monitoringDataId)
        {
            await _service.DeleteMonitoringDataAsync(monitoringDataId);
            return RedirectToAction(nameof(Index));
        }
}