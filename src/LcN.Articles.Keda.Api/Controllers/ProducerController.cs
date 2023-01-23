using LcN.Articles.Keda.Api.Services;
using Microsoft.AspNetCore.Mvc;
using RandomNameGeneratorLibrary;

namespace LcN.Articles.Keda.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProducerController : ControllerBase
{
    private readonly ProducerService _producerService;
    private readonly PlaceNameGenerator _nameGenerator;

    public ProducerController(ProducerService producerService, PlaceNameGenerator nameGenerator)
    {
        _producerService = producerService;
        _nameGenerator = nameGenerator;
    }

    [HttpPost]
    [Route("/start")]
    public ActionResult<ProducerData> StartProducer(int numOfItems = 500)
    {
        return Ok(_producerService.StartProducer(_nameGenerator.GenerateRandomPlaceName(), numOfItems));
    }

    [HttpPost]
    [Route("/many")]
    public ActionResult<ProducerData[]> StartManyProducers(int numOfProducers = 100, int numOfItems = 500)
    {
        var list = new List<ProducerData>();
        for (int i = 0; i < numOfProducers; i++)
        {
            list.Add(_producerService.StartProducer(_nameGenerator.GenerateRandomPlaceName(), numOfItems));
        }

        return Ok(list);
    }

    [HttpGet]
    [Route("/stop")]
    public ActionResult<bool> StopProducer(Guid guid)
    {
        return Ok(_producerService.StopProducer(guid));
    }

    [HttpGet]
    [Route("/all")]
    public ActionResult<ProducerData> GetProducers()
    {
        return Ok(_producerService.GetProducers());
    }
}
