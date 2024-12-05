using examen2_backend.Application.Interfaces;
using examen2_backend.Application.Managers;
using examen2_backend.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace examen2_backend.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChangeController : ControllerBase
    {
        private readonly IChangeManagement _changeManagement;
        private readonly IChangeRepository _changeRepository;

        public ChangeController(IChangeManagement changeManagement,
                                IChangeRepository changeRepository)
        {
            _changeManagement = changeManagement;
            _changeRepository = changeRepository;
        }

        [HttpGet]
        public IActionResult GetCurrentChange()
        {
            return Ok(_changeRepository.changeMoney);
        }

        [HttpPost]
        public IActionResult GiveChange(GiveChangeRequestModel request)
        {
            List<MoneyModel> result = _changeManagement.GiveChange(request.requestedMoney, request.updatedChange);
            return result != null ? Ok(result) : BadRequest();
        }
    }
}
