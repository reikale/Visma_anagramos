using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        private IConfiguration _appSettings;
        public DownloadController(IConfiguration configuration)
        {
            _appSettings = configuration;
        }
        // GET: api/download
        [HttpGet]
        public async Task<FileContentResult> GetDictionaryFile()
        {
            var filePath = _appSettings["App:WordsRepoSource:PathToWordsRepo"];
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, "text/plain", Path.GetFileName(filePath));
        }
    }
}
