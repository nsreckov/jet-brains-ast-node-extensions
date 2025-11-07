using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstNodeExtensionsTask
{
    public interface IAstNode
    {
        IAstNode Parent { get; }
        IAstNode FirstChild { get; }
        IAstNode NextSibling { get; }
        IAstNode PrevSibling { get; }

        bool IsWhitespaceOrComment { get; }

        // returns this node’s text (for leaves)
        // or concatenated text of the subtree
        string GetText();
    }
}
