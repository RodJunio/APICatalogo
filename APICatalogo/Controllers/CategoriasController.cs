using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repositoy;
using APICatalogo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APICatalogo.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class CategoriasController : Controller
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;


    public CategoriasController(IUnitOfWork context, IMapper mapper)
    {
        _uof = context;
        _mapper = mapper;
    }
    
    [HttpGet("produtos")]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategoriasProdutos()
    {
        var categoriaProdutos = await _uof.CategoriaRepository.GetCategoriasProdutos();
        var categoriaProdutosDto = _mapper.Map<List<CategoriaDTO>>(categoriaProdutos);

        return categoriaProdutosDto;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get([FromQuery] CategoriaParameters categoriaParameters)
    {
        var categorias = await _uof.CategoriaRepository.GetCategorias(categoriaParameters);

        var metadata = new
        {
            categorias.TotalCount,
            categorias.PageSize,
            categorias.CurrentPage,
            categorias.TotalPages,
            categorias.HasNext,
            categorias.HasPrevious
        };

        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

        var categoriaDto = _mapper.Map<List<CategoriaDTO>>(categorias);
        return categoriaDto;
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public async Task<ActionResult<CategoriaDTO>> Get(int id)
    {
        var categoria = await _uof.CategoriaRepository.GetById(p => p.CategoriaId == id);

        if (categoria == null)
        {
            return NotFound("Categoria não encontrada...");
        }

        var categoriaDto = _mapper.Map<CategoriaDTO>(categoria);

        return Ok(categoriaDto);
    }
    
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CategoriaDTO categoriaDto)
    {
        var categoria = _mapper.Map<Categoria>(categoriaDto);

        if (categoriaDto is null)
            return new BadRequestResult();


        _uof.CategoriaRepository.Add(categoria);
        await _uof.Commit();

        var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

        return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoriaDTO);
    }
    
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Put(int id,[FromBody] CategoriaDTO categoriaDto)
    {
        if (id != categoriaDto.CategoriaId)
            return BadRequest();

        var categoria = _mapper.Map<Categoria>(categoriaDto);

        _uof.CategoriaRepository.Update(categoria);
        await _uof.Commit();

        return Ok(categoria);
    }

    [HttpDelete]
    public async Task<ActionResult<Categoria>> Delete(int id)
    {
        var categoria = await _uof.CategoriaRepository.GetById(p => p.CategoriaId == id);

        if (categoria == null)
        {
            return NotFound("Categoria não encontrada...");
        }

        _uof.CategoriaRepository.Delete(categoria);
        await _uof.Commit();

        var CategoriaDTO = _mapper.Map<CategoriaDTO>(categoria);

        return Ok(CategoriaDTO);
    }
}