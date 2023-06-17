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
            var VM = new IndexVM();
            return View(VM);
        }
        [HttpPost]
        public IActionResult Index(int n)
        {
            var VM = new IndexVM();
            if(n > 8 || n < 1)
            {
                VM.IsInRange = false;
                VM.ErrorMessage = "Error: n should be between 1 and 8 inclusive";
            }
            else
            {
                VM.Combinations = GenerateParenthesis(n);
            }
            return View(VM);
        }

        public IList<string> GenerateParenthesis(int n) {
            if(n<1 || n > 8)
                throw new ArgumentOutOfRangeException(nameof(n),"Valid values of n are 1<=n<=8");
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

}