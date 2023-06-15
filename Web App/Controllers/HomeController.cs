using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;
using Web_App.Models;

namespace Web_App.Controllers
{
    public class HomeController : Controller
    {


        public IActionResult Index()
        {
            var list = new List<string>();
            return View(list);
        }
        [HttpPost]
        public IActionResult Index(int n)
        {
            var list = GenerateParenthesis(n);
            return View(list);
        }

        public IList<string> GenerateParenthesis(int n) {
            var result = new List<string>();
            var seq = new StringBuilder();
            
            void backtrack(int open, int close) {
                if(seq.Length == n * 2) {
                    result.Add(seq.ToString());
                    return;
                } 
                
                if(open < n) {
                    seq.Append("(");
                    backtrack(open + 1, close);
                    seq.Remove(seq.Length - 1, 1);
                }
                if(close < open) {
                    seq.Append(")");
                    backtrack(open, close + 1);
                    seq.Remove(seq.Length - 1, 1);
                }
                
            }

            backtrack(0, 0);

            return result;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class Solution {
}
}