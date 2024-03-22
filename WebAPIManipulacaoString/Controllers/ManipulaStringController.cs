using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManipulaStringController : ControllerBase
    {

        [HttpPost]
        public ActionResult<StringAnalysisResult> Post(StringInputData data)
        {
            if (string.IsNullOrWhiteSpace(data?.Texto))
            {
                return BadRequest("Favor informar a palavra.");
            }

            string texto = data.Texto.ToLower().Replace(" ", "");
            bool palindromo = IsPalindrome(texto);
            Dictionary<char, int> occurrences = CountOccurrences(texto);

            return Ok(new StringAnalysisResult
            {
                Palindromo = palindromo,
                QuantidadeCaracteres = occurrences
            });
        }

        private bool IsPalindrome(string text)
        {
            return text.SequenceEqual(text.Reverse());
        }

        private Dictionary<char, int> CountOccurrences(string text)
        {
            return text.GroupBy(c => c)
                       .ToDictionary(group => group.Key, group => group.Count());
        }
    }

    public class StringInputData
    {
        public string Texto { get; set; }
    }

    public class StringAnalysisResult
    {
        public bool Palindromo { get; set; }
        public Dictionary<char, int> QuantidadeCaracteres { get; set; }
    }

}


