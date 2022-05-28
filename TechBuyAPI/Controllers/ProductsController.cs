using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications.QuerySpecifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechBuyAPI.DTOs;
using TechBuyAPI.Errors;
using TechBuyAPI.Utils;

namespace TechBuyAPI.Controllers
{
  public class ProductsController : BaseApiController
  {
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<ProductBrand> _productBrandRepository;
    private readonly IRepository<ProductType> _productTypeRepository;
    private readonly IMapper _mapper;

    public ProductsController(IRepository<Product> productRepository, IRepository<ProductBrand> productBrandRepository,
      IRepository<ProductType> productTypeRepository, IMapper mapper)
    {
      _productRepository = productRepository;
      _productBrandRepository = productBrandRepository;
      _productTypeRepository = productTypeRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<ProductToReturnDto>>> GetProducts(
      [FromQuery] ProductSpecificationParams productParams)
    {
      var specification = new ProductsWithTypesAndBrands(productParams);
      var countSpec = new ProductWithFiltersForCount(productParams);

      var totalItems = await _productRepository.CountAsync(countSpec);

      var products = await _productRepository.ListAsync(specification);

      var data = _mapper.Map<List<ProductToReturnDto>>(products);

      return Ok(new PaginatedResponse<ProductToReturnDto>(productParams.PageIndex, productParams.PageSize, totalItems,
        data));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
      var specification = new ProductsWithTypesAndBrands(id);
      var product = await _productRepository.GetWithSpecification(specification);

      if (product == null)
      {
        return NotFound(new ApiResponse(404));
      }

      return _mapper.Map<ProductToReturnDto>(product);
    }

    [HttpGet("brands")]
    public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
    {
      return Ok(await _productBrandRepository.ListAllAsync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<List<ProductBrand>>> GetProductTypes()
    {
      return Ok(await _productTypeRepository.ListAllAsync());
    }
  }
}