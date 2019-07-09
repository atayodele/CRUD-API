using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CrudApi.ViewModel;
using CrudApi.Models;

namespace CrudApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CustomerController(
            ICustomerService customerService,
            IMapper mapper)
        {
            _customerService = customerService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomer(id);
            if (customer == null)
                return BadRequest($"Customer with {id} is not found");
            var returnCustomer = _mapper.Map<ListCustomerVM>(customer);
            return Ok(returnCustomer);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetCustomers();
            var returnCustomer = _mapper.Map<IEnumerable<ListCustomerVM>>(customers);
            return Ok(returnCustomer);
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] AddCustomerVM customerVM)
        {
            if (!string.IsNullOrEmpty(customerVM.Email))
                customerVM.Email = customerVM.Email.ToLower();
            if (await _customerService.CustomerExists(customerVM.Email))
                ModelState.AddModelError("Email", "Email is already taken");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var Create = _mapper.Map<Customer>(customerVM);
            _customerService.Add(Create);
            if (await _customerService.SaveAll())
                return Ok("Customer Created Successfully");
            return BadRequest("Failed to create customer");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerVM customerVm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var customerFromRepo = await _customerService.GetCustomer(id);
            if (customerFromRepo == null)
                return NotFound($"Could not find customer with an ID of {id}");
            _mapper.Map(customerVm, customerFromRepo);
            if (await _customerService.SaveAll())
            {
                return Ok("Customer Profile updated successfully");
            }
            return BadRequest($"Updating customer {customerVm.Fname} failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var customer= await _customerService.GetCustomer(id);
            if (customer == null)
                return NotFound($"Could not find customer with an ID of {id}");
            _customerService.Delete(customer);
            if (await _customerService.SaveAll())
                return Ok("Customer Deleted Successfully");
            return BadRequest($"Failed to delete customer with Id {id}");
        }
    }
}