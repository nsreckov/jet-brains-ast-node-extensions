using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstNodeExtensionsTask
{
    public class TestNode : IAstNode
    {
        public IAstNode Parent { get; set; }
        public IAstNode FirstChild { get; set; }
        public IAstNode NextSibling { get; set; }
        public IAstNode PrevSibling { get; set; }
        public bool IsWhitespaceOrComment { get; set; }
        public string Text { get; set; }

        public string GetText() => Text;
    }
}
