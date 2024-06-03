using Application.Models;
using AutoMapper;

namespace Application.Tests.Controllers;

[TestFixture]
public class ProductControllerTests
{
    [SetUp]
    public void SetUp()
    {
        var config = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); });
        _mapper = config.CreateMapper();
    }

    private IMapper _mapper;

    // TODO: Add service mocks and create tests
}