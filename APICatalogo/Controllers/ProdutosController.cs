﻿using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositoy;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APICatalogo.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public ProdutosController(IUnitOfWork context, IMapper mapper)
    {
        _uof = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("menorpreco")]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosPrecos()
    {
        var produtos = await _uof.ProdutoRepository.GetProdutosPorPreco();
        var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);

        return produtosDto;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutosParameters produtosParameters)
    {
        var produtos = await _uof.ProdutoRepository.GetProdutos(produtosParameters);

        var metadata = new
        {
            produtos.TotalCount,
            produtos.PageSize,
            produtos.CurrentPage,
            produtos.TotalPages,
            produtos.HasNext,
            produtos.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
        
        var produtosDto = _mapper.Map<List<ProdutoDTO>>(produtos);
        return produtosDto;
    }
    
    [HttpGet("{id:int}", Name = "ObterProduto")]
    public async Task<ActionResult<ProdutoDTO>> Get(int id)
    {
        var produto = await _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);
        if (produto is null)
        {
            return NotFound("Produto não encontrado");
        }

        var produtoDto = _mapper.Map<ProdutoDTO>(produto);

        return produtoDto;
    }
    
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] ProdutoDTO produtoDto)
    {
        var produto = _mapper.Map<Produto>(produtoDto);
        _uof.ProdutoRepository.Add(produto);
        await _uof.Commit();

        var produtoDTO = _mapper.Map<ProdutoDTO>(produto);

        return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produtoDTO);
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id,[FromBody] ProdutoDTO produtoDto)
    {
        if (id != produtoDto.ProdutoId)
        {
            return BadRequest();
        }

        var produto = _mapper.Map<Produto>(produtoDto);

        _uof.ProdutoRepository.Update(produto);
        await _uof.Commit();

        return Ok(produto);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<ProdutoDTO>> Delete(int id)
    {
        var produto = await _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);
        if (produto is null)
        {
            return NotFound("Produto não encontrado...");
        }

        _uof.ProdutoRepository.Delete(produto);
        await _uof.Commit();

        var produtoDto = _mapper.Map<ProdutoDTO>(produto);

        return Ok(produtoDto);
    }

}