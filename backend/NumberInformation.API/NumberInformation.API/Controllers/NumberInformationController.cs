using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NumberInformation.Models;
using NumberInformation.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NumberInformation.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class NumberInformationController : ControllerBase
    {
        private readonly IProcessBackground<NIResponse> _processBackGround;

        public NumberInformationController(IProcessBackground<NIResponse> processBackGround)
        {
            _processBackGround = processBackGround;
        }
        /// <summary>
        /// Send number equal or greater than 1 to get information
        /// </summary>
        /// <param name="inputNumber">long number</param>
        /// <returns>A task id or Information of numbers</returns>
        /// <response code="200">A object with number informations</response>
        /// <response code="202">A taskId to consulting after</response>
        /// <response code="400">Number is less than 1 or greater than max of long size</response>
        /// <response code="500">Other errors</response>
        [HttpGet("{inputNumber}")]
        [ProducesResponseType(typeof(NIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(long inputNumber)
        {
            if( inputNumber < 1 )
            {
                return BadRequest("Number is less than 1");
            }

            var taskId = "";
            try
            {
                using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(1)))
                {
                    taskId = _processBackGround.ExecuteInBackground(() => inputNumber.GetDividingInformations());
                    var result = _processBackGround.GetResultBackgroundTask(taskId);
                    while(result?.Input == null)
                    {
                        cancellationTokenSource.Token.ThrowIfCancellationRequested();
                        result = _processBackGround.GetResultBackgroundTask(taskId);
                    }

                    return Ok(result);
                }
            }
            catch(Exception e)
            {
                return Accepted("",taskId);
            }
        }

        /// <summary>
        /// Get number information
        /// </summary>
        /// <param name="taskId">ID of task</param>
        /// <returns>None or Information of Numbers</returns>
        /// <response code="200">Information of numbers</response>
        /// <response code="204">The information of number is processing</response>
        [HttpGet("task/{taskId}")]
        [ProducesResponseType(typeof(NIResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Get(string taskId)
        {
            var result = _processBackGround.GetResultBackgroundTask(taskId);
            return Ok(result);
        }

    }
}
