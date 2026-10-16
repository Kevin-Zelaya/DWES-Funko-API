using Microsoft.AspNetCore.Mvc;
using MinimalApi.models;

[ApiController]
[Route("api/[controller]")]
public class FunkoController(FunkoRepository _repository) : ControllerBase
{
    
    /// <summary>
    /// Get all funkos
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public ActionResult GetActionResult()
    {
        var funkos = _repository.GetAll();
        return Ok(funkos);
    }
    /// <summary>
    /// Get a funko by its ID
    /// </summary>
    /// <param name="id">The ID of the funko to retrieve</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public ActionResult GetActionResultById(int id)
    {
        var funko = _repository.GetById(id);
        return funko is not null ? Ok(funko) : NotFound();
    }
    /// <summary>
    /// Create a new funko
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<FunkoDto> CreateFunko([FromBody] FunkoDto dto)
    {
        var response = _repository.CreateFunko(dto);
        return CreatedAtAction(nameof(GetActionResultById), new { id = response.Id }, response.ToDto());
    }
    /// <summary>
    /// Actualizar un funko
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    public ActionResult<FunkoDto> UpdadeFunko([FromRoute]int id, [FromBody]FunkoDto dto
    )
    {
        var response = _repository.UpdateFunko(dto, id);
        return response is not null ? Ok(response.ToDto()) : NotFound();
    }

    [HttpDelete("{id:int}")]
    public ActionResult DeleteFunko([FromRoute]int id)
    {
        return _repository.DeleteFunko(id) is not null ? NoContent() : NotFound();
    }

}