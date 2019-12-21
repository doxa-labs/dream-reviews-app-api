using System.Threading.Tasks;
using RuyaTabircim.Data.Model;
using Microsoft.AspNetCore.Mvc;
using RuyaTabircim.Data.Service.Interface;

namespace RuyaTabircim.Api.Controllers
{
    [Produces("application/json")]
    public class SentencesController : BaseController
    {
        ISentenceService SentenceService { get; }
        public SentencesController(ISentenceService dreamService)
        {
            SentenceService = dreamService;
        }

        //[Authorize(Policy = "Oracle")]
        [HttpGet("api/v1/sentences/random")]
        public IActionResult GetRandom()
        {
            return Ok(Invoke(new Task<SentenceSummary>(() => SentenceService.GetRandom())));
        }

        [HttpGet("api/v1/sentences/next")]
        public IActionResult GetNext()
        {
            return Ok(Invoke(new Task<SentenceSummary>(() => SentenceService.GetForEdit())));
        }

        [HttpGet("api/v1/sentences/stats")]
        public IActionResult GetStats()
        {
            return Ok(Invoke(new Task<Stats>(() => SentenceService.GetStat())));
        }

        [HttpPut("api/v1/sentences/edit")]
        public IActionResult Edit([FromBody]SentenceSummary value)
        {
            return Ok(Invoke(new Task<bool>(() => SentenceService.Edit(value))));
        }

        /*
        [HttpGet("api/v1/sentences/insert")]
        public IActionResult InsertFiles()
        {
            Console.WriteLine("Begin");
            int countFiles = 2458;
            int counter = 0;

            for (int cf = 1; cf < countFiles + 1; cf++)
            {
                string fileName = @"C:\Users\song (" + cf + ")";
                
                string[] lines = System.IO.File.ReadAllLines(fileName);

                List<Sentence> sentences = new List<Sentence>();
                Sentence s = new Sentence();

                for (int i = 0; i < lines.Length; i++)
                {
                    if (i % 2 == 0)
                    {
                        s.En = lines[i];
                    }
                    else
                    {
                        s.Tr = lines[i];

                        sentences.Add(s);

                        s.No = counter;
                        // clear song
                        s = new Sentence();

                        counter++;
                    }
                }

                // Insert song ilfe
                Invoke(new Task<bool>(() => SentenceService.Insert(sentences)));

                Console.WriteLine("File: " + fileName + " Sentence Count: " + sentences.Count);

                // clear list
                sentences = new List<Sentence>();
            }
            
            return Ok(Invoke(new Task<Sentence>(() => SentenceService.GetRandom())));
        }
        */
    }
}