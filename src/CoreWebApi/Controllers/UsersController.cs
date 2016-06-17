using CoreWebAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.IO.File;

namespace CoreWebAPI.Controllers
{
    /// <summary>
    /// Contains endpoints which work with user data
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UsersController : Controller
    {
        private readonly string pathToFile;

        /// <summary>
        /// Uses the hosting environment to determine the path to the data source file
        /// </summary>
        /// <param name="env"></param>
        public UsersController(IHostingEnvironment env)
        {
            pathToFile = env.ContentRootPath
               + Path.DirectorySeparatorChar.ToString()
               + "Data"
               + Path.DirectorySeparatorChar.ToString()
               + "users.json";
        }

        /// <summary>
        /// Returns all user data
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        public async Task<IActionResult> Get()
        {
            var users = await GetDataSet();

            return Ok(users);
        }

        /// <summary>
        /// Returns user with the provided id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(User), 200)]
        public async Task<IActionResult> Get(int id)
        {
            var users = await GetDataSet();

            return Ok(users.FirstOrDefault(u => u.Id == id));
        }

        /// <summary>
        /// Returns users with a matching name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name:alpha}")]
        [ProducesResponseType(typeof(IEnumerable<User>), 200)]
        public async Task<IActionResult> Get(string name)
        {
            var users = await GetDataSet();

            return Ok(users.Where(u => $"{u.FirstName} {u.LastName}".ToLowerInvariant().Contains(name.ToLowerInvariant())));
        }

        /// <summary>
        /// Gets data from the data source file and deserializes it into User models
        /// </summary>
        /// <returns></returns>
        private async Task<IEnumerable<User>> GetDataSet()
        {
            string source = "";

            using (StreamReader SourceReader = OpenText(pathToFile))
            {
                source = await SourceReader.ReadToEndAsync();
            }

            return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<IEnumerable<User>>(source));
        }
    }
}
