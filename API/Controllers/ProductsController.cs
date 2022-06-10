using API.Dtos;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.1")]
[ApiVersion("1.0")]
[Authorize(Roles = "Administrator")]

public class ProductsController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<ProductDto>>> Get11()
    {
        var products = await _unitOfWork.Products
                                    .GetAllAsync();
        return _mapper.Map<List<ProductDto>>(products);
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Pager<ProductListDto>>> Get([FromQuery] Params productParams)
    {
        var result = await _unitOfWork.Products
                                    .GetAllAsync(productParams.PageIndex, productParams.PageSize,
                                    productParams.Search);

        var productListDto = _mapper.Map<List<ProductListDto>>(result.records);

        Response.Headers.Add("X-InlineCount", result.totalRecords.ToString());

        return new Pager<ProductListDto>(productListDto, result.totalRecords, 
            productParams.PageIndex, productParams.PageSize, productParams.Search);

    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDto>> Get(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null)
            return NotFound();

        return _mapper.Map<ProductDto>(product);
    }

    //POST: api/Products
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Product>> Post(ProductAddUpdateDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);
        _unitOfWork.Products.Add(product);
        await _unitOfWork.SaveAsync();
        if (product == null)
        {
            return BadRequest();
        }
        productDto.Id = product.Id;
        return CreatedAtAction(nameof(Post), new { id = productDto.Id }, productDto);
    }

    //PUT: api/Products/4
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ProductAddUpdateDto>> Put(int id, [FromBody] ProductAddUpdateDto productDto)
    {
        if (productDto == null)
            return NotFound();

        var product = _mapper.Map<Product>(productDto);
        _unitOfWork.Products.Update(product);
        await _unitOfWork.SaveAsync();

        return Ok(productDto);
    }

    //DELETE: api/Products/4
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        var product = await _unitOfWork.Products.GetByIdAsync(id);
        if (product == null)
            return NotFound();

        _unitOfWork.Products.Remove(product);
        await _unitOfWork.SaveAsync();

        return NoContent();
    }

}

