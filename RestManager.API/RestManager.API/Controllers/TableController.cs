using Azure;
using Microsoft.AspNetCore.Mvc;
using RestManager.DataAccess.Models;
using RestManager.Services.Interfaces;
using RestManager.Services.ModelDTO;
using RestManager.Services.Records;
using Swashbuckle.AspNetCore.Annotations;

namespace RestManager.API.Controllers
{
    // on client side to do result model and catch errors
    [ApiController]
    [Route("[controller]")]
    public class TableController : ControllerBase
    {
        private readonly ILogger<TableController> _logger;
        private readonly IRestManager _restManager;

        public TableController(ILogger<TableController> logger, IRestManager restManager)
        {
            _logger = logger;
            _restManager = restManager;
        }

        [HttpGet("GetAvaibleTablesInRestorant")]
        [SwaggerOperation(
          Summary = "Returns avaible tables for restorant and clients count",
          Description = "parameters are restorantId and clientsCount, if restorant not found it returns 404 error, " +
            "if result ok it returns tables results (empty or not) priorities empty tables then not empty, if result is empty you can add clients to queue",
          OperationId = "getAvaibleTablesInRestorant")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SearchAvaibleTableResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(SearchAvaibleTableResult))]
        public async Task<IActionResult> GetAvaibleTablesForRestorant([FromQuery] long restorantId,
            [FromQuery] int clientsCount)
        {
            var searchResult = await _restManager.GetAvaibleTablesInRestorant(restorantId, clientsCount);
            if (searchResult.IsError)
            {
                return NotFound(searchResult);
            }
            return Ok(searchResult); 
        }

        
        [HttpPost("AddedQueueForClientGroup")]
        [SwaggerOperation(
            Summary = "Add client/clients to queue",
            Description = "If there are any free tables this method can add client/clients to queue " +
            "which will check free table every 30 secodns, returns queueId for notifications listener. " +
            "Group parameter is not added clients to system, if restorant not found it returns 404 error",
            OperationId = "addedQueueForClientGroup")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QueueForNextTableResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(QueueForNextTableResult))]
        public async Task<IActionResult> AddedQueueForClientGroup([FromQuery] long restorantId,
                [FromBody] ClientGroupDTO group)
        {

            var queueResult = await _restManager.QueueForNextAvaibleTable(group, restorantId);
            if (queueResult.IsError)
            {
                return NotFound(queueResult);
            }

            return Ok(queueResult);
        }

        [HttpPost("AddClientsToTable")]
        [SwaggerOperation(
           Summary = "Add client/clients to table",
           Description = "if restorant not found it returns 404 error, otherwise clients were set to table, returns clients",
           OperationId = "addClientsToTable")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SetClientsResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(SetClientsResult))]
        public async Task<IActionResult> AddClientsToTable([FromQuery] long tableId, [FromQuery] long restorantId,
            [FromBody] ClientGroupDTO group)
        {
            var queueResult = await _restManager.SetClientsToTable(group, restorantId, tableId);
            if (queueResult.IsError)
            {
                return NotFound(queueResult);
            }

            return Ok(queueResult);
        }


        [HttpPost("EndClientsVisiting")]
        [SwaggerOperation(
        Summary = "Ends visiting of clients",
        Description = "if groupd was not found it would return 404",
        OperationId = "addClientsToTable")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EndVisitResult))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(EndVisitResult))]
        public async Task<IActionResult> EndClientsVisiting([FromQuery] long clientsGroupId)
        {
            var result = await _restManager.EndClientsVisiting(clientsGroupId);
            if (result.IsError)
            {
                return NotFound(result);
            }
            return Ok(result);
        }





    }
}