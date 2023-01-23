using LcN.Articles.Keda.Api.Services;
using Microsoft.AspNetCore.Mvc;
using RandomNameGeneratorLibrary;

namespace LcN.Articles.Keda.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConsumerController : ControllerBase
{
    private readonly ConsumerService _consumerService;
    private readonly PersonNameGenerator _nameGenerator;

    public ConsumerController(ConsumerService consumerService, PersonNameGenerator nameGenerator)
    {
        _consumerService = consumerService;
        _nameGenerator = nameGenerator;
    }

    [HttpGet]
    [Route("/id")]
    public ActionResult<ConsumerData> GetProducers()
    {
        return Ok(new ConsumerData
        {
            Id = Guid.NewGuid(),
            Name = _nameGenerator.GenerateRandomFirstAndLastName(),
        });
    }
}

public class ConsumerData
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}
