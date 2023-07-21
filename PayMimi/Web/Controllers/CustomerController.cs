using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using PayMimi.Domain.Services;
using PayMimi.Web.Requests;

namespace PayMimi.Web.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ICustomerService _service;
    private readonly IMapper _mapper;
    private readonly IValidator<CustomerRegistrationIntentRequest> _validator;

    public CustomerController(IMapper mapper, ICustomerService service,
        IValidator<CustomerRegistrationIntentRequest> validator)
    {
        _mapper = mapper;
        _service = service;
        _validator = validator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCustomer(CustomerRegistrationIntentRequest request)
    {
        var result = await _validator.ValidateAsync(request);

        if (!result.IsValid) return BadRequest(result.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));

        var command = _mapper.Map<RegistrationIntentCommand>(request);

        await _service.CreateRegistrationIntent(command);

        return Ok();
    }
}