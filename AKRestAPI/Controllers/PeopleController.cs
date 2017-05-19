using Microsoft.AspNetCore.Mvc;
using AKRestAPI.Models;

namespace AKRestAPI.Controllers
{
    [Route("mci/[controller]")]
    public class PeopleController : Controller
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleController(IPeopleRepository peopleRepository)
        {
            _peopleRepository = peopleRepository;
        }

        // GET mci/people
        [HttpGet]
        public OkObjectResult Get()
        {
            return Ok(new { Value = true });
        }

        // GET mci/findByAddress?address=
        [HttpGet("findByAddress")]
        public JsonResult Get(string address)
        {
            if (address == null)
            {
                return Json(new { error = "missing paramter" });
            }
            return Json( _peopleRepository.findByAddress(address));
        }

        // GET mci/findByName?firstName=&lastName=
        [HttpGet("findByName")]
        public JsonResult Get(string firstName, string lastName)
        {
            if (firstName == null || lastName == null)
            {
                return Json(new { error = "missing paramters" });
            }
            return Json(_peopleRepository.findByName(firstName, lastName));
        }

        // GET mci/findByProvider?provider=
        [HttpGet("findByProvider")]
        public JsonResult Get(string firstName, string lastName, string businessName, string speciality, string npi)
        {
            return Json(_peopleRepository.findByProvider(firstName, lastName, businessName, speciality, npi));
        }
    }
}