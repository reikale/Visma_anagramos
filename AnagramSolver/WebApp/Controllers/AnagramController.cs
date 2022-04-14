using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnagramController : ControllerBase
    {
        
        private IAnagramSolver _anagramSolver;

        public AnagramController(IAnagramSolver anagramSolver)
        {
            
            _anagramSolver = anagramSolver;
        }
        
        // GET: api/anagram/
        [HttpGet("{word}", Name = "Get")]
        public ActionResult Get(string word)
        {
            var result = _anagramSolver.CheckForAnagram(word);
            return Ok(result);
        }
    }
}
